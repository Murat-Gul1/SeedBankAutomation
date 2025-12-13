using DevExpress.XtraEditors;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Properties;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGauges.Win.Gauges.Circular;
using DevExpress.XtraGauges.Win.Gauges.Linear;
using System.Globalization;

namespace TohumBankasiOtomasyonu
{
    public partial class FormKasaDepo : DevExpress.XtraEditors.XtraForm
    {
        DateTime acilisZamani;
        int baslangicGecikmeSayaci = 0;
        private bool formKapaniyor = false;
        DateTime sonUIGuncelleme = DateTime.MinValue;
        // --- SERİ PORT NESNESİ (UYGULAMA GENELİNDE TEK) ---
        // --- SERIAL PORT OBJECT (SINGLETON IN APPLICATION) ---
        // --- SERIAL PORT OBJECT (SINGLETON IN APPLICATION) ---
        private static SerialPort serialPort1;
        private static readonly object portKilidi = new object();

        // --- EŞİK DEĞERLERİ ---
        // --- THRESHOLD VALUES ---
        float LIMIT_SICAKLIK = 25.0f;   // °C
        float LIMIT_NEM = 75.0f;        // %RH
        int LIMIT_GAZ = 350;
        int LIMIT_ISIK_HAM = 20;

        // --- HER SENSÖR İÇİN AYRI 30 SN KÖRLEME ---
        // --- SEPARATE 30 SEC BLINDNESS FOR EACH SENSOR ---
        // --- SEPARATE 30 SEC BLINDNESS FOR EACH SENSOR ---
        DateTime hareketYasakBitis = DateTime.MinValue;
        DateTime nemYasakBitis = DateTime.MinValue;
        DateTime gazYasakBitis = DateTime.MinValue;
        DateTime sicaklikYasakBitis = DateTime.MinValue;
        DateTime isikYasakBitis = DateTime.MinValue;

        Image orjinalFanResmi;
        int fanAci = 0;

        // Arduino'ya düzenli "PING" göndermek için timer
        // Timer to send regular "PING" to Arduino
        // Timer to send regular "PING" to Arduino
        System.Windows.Forms.Timer tmrPing = new System.Windows.Forms.Timer();

        public FormKasaDepo()
        {
            InitializeComponent();

            // --- TİMERLER ---
            // --- TIMERS ---
            if (tmrAlarmSusturucu != null)
            {
                // Alarm 2 saniye boyunca açık kalacak
                // Alarm will stay on for 2 seconds
                tmrAlarmSusturucu.Interval = 2000;
                tmrAlarmSusturucu.Tick += TmrAlarmSusturucu_Tick;
            }

            if (tmrFanAnimasyon != null)
            {
                tmrFanAnimasyon.Interval = 50;
                tmrFanAnimasyon.Tick += tmrFanAnimasyon_Tick;
            }

            // --- PING TIMER AYARI ---
            // --- PING TIMER SETTING ---
            tmrPing.Interval = 1000; // 1 saniyede bir PING
            tmrPing.Tick += TmrPing_Tick;
        }

        private void FormKasaDepo_Load(object sender, EventArgs e)
        {
            acilisZamani = DateTime.Now;
            UygulaDil();
            baslangicGecikmeSayaci = 0; // Sayacı sıfırla
            // Reset counter

            // --- FAN RESMİ YÜKLE ---
            // --- LOAD FAN IMAGE ---
            try { orjinalFanResmi = Properties.Resources.fan; }
            catch { if (picFan.Image != null) orjinalFanResmi = picFan.Image; }
            if (orjinalFanResmi != null) picFan.Image = orjinalFanResmi;

            // --- ARDUINO BAĞLANTISI ---
            // --- ARDUINO CONNECTION ---
            try
            {
                lock (portKilidi)
                {
                    if (serialPort1 == null)
                    {
                        serialPort1 = new SerialPort();
                        serialPort1.PortName = "COM4";
                        serialPort1.BaudRate = 9600;
                        serialPort1.ReadTimeout = 1000;
                        serialPort1.WriteTimeout = 1000;
                    }

                    // Event çakışmasını önlemek için önce çıkar, sonra ekle
                    // Remove then add to prevent event conflict
                    // Remove then add to prevent event conflict
                    serialPort1.DataReceived -= SerialPort1_DataReceived;
                    serialPort1.DataReceived += SerialPort1_DataReceived;

                    if (!serialPort1.IsOpen)
                    {
                        serialPort1.Open();

                        // Açılır açılmaz buffer'da bekleyen eski veriyi SİL!
                        // CLEAR old data waiting in buffer immediately after opening!
                        // CLEAR old data waiting in buffer immediately after opening!
                        Thread.Sleep(250);            // Çeyrek saniye bekle ki veri akışı otursun
                        // Wait quarter second for data flow to settle
                        // Wait quarter second for data flow to settle
                        serialPort1.DiscardInBuffer();
                        serialPort1.DiscardOutBuffer();
                    }
                }

                // Timer'ı başlat
                // Start Timer
                tmrPing.Start();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Bağlantı Hatası: " + ex.Message + "\nLütfen kabloyu kontrol edip tekrar deneyin.",
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            GostergeAraliginiAyarla(gaugeSicaklik, 0, 40);
            GostergeAraliginiAyarla(gaugeNem, 0, 100);
            GostergeAraliginiAyarla(gaugeGaz, 0, 400);  // GAZ
            GostergeAraliginiAyarla(gaugeIsik, 0, 100);  // IŞIK
        }

        // --- ARDUINO'DAN VERİ GELDİĞİNDE (AYRI THREAD) ---
        // --- WHEN DATA RECEIVED FROM ARDUINO (SEPARATE THREAD) ---
        // --- WHEN DATA RECEIVED FROM ARDUINO (SEPARATE THREAD) ---
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (formKapaniyor) return;

            try
            {
                SerialPort port = serialPort1;
                if (port == null || !port.IsOpen) return;

                string gelenVeri;

                try
                {
                    gelenVeri = port.ReadLine();
                }
                catch
                {
                    // Timeout / kesinti vs.
                    // Timeout / interruption etc.
                    return;
                }

                if (string.IsNullOrWhiteSpace(gelenVeri))
                    return;

                gelenVeri = gelenVeri.Trim();

                // >>> THROTTLE: En fazla 10 Hz (100 ms'de bir)
                // >>> THROTTLE: Max 10 Hz (Every 100 ms)
                // >>> THROTTLE: Max 10 Hz (Every 100 ms)
                var simdi = DateTime.Now;
                if ((simdi - sonUIGuncelleme).TotalMilliseconds < 100)
                {
                    // 100ms dolmadan yeni veri geldiyse, UI'ya yollamadan çöpe at
                    // If new data came before 100ms, trash it without sending to UI
                    // If new data came before 100ms, trash it without sending to UI
                    return;
                }
                sonUIGuncelleme = simdi;
                // <<<

                if (this.IsDisposed || !this.IsHandleCreated || formKapaniyor)
                    return;

                // Artık gerçekten UI'da işlenecek veri bu
                // This is the data that will actually be processed in UI
                this.BeginInvoke(new Action<string>(VeriIsle), gelenVeri);
            }
            catch
            {
                // Kapanış sırasında oluşabilecek saçma hataları yutuyoruz
                // We swallow nonsense errors that may occur during closing
            }
        }



        // --- VERİ İŞLEME (UI THREAD) ---
        // --- DATA PROCESSING (UI THREAD) ---
        // --- DATA PROCESSING (UI THREAD) ---
        private void VeriIsle(string gelenVeri)
        {
            // 1. GÜVENLİK KİLİDİ: Form açıldıktan sonraki ilk 4 saniye işlem yapma!
            // 1. SECURITY LOCK: Do not process for the first 4 seconds after form opens!
            // 1. SECURITY LOCK: Do not process for the first 4 seconds after form opens!
            // Sensörlerin (özellikle Gaz ve PIR) kendine gelmesi için zaman tanı.
            // Give time for sensors (especially Gas and PIR) to stabilize.
            // Give time for sensors (especially Gas and PIR) to stabilize.
            if ((DateTime.Now - acilisZamani).TotalSeconds < 4)
            {
                return;
            }

            try
            {
                string[] parcalar = gelenVeri.Split(';');
                if (parcalar.Length != 5)
                    return;

                float sicaklik = 0, nem = 0;

                int gaz = 0, isikHam = 0, hareket = 0;

                // --- GÜVENLİ PARSE İŞLEMİ (Nokta/Virgül sorununu çözer) ---
                // CultureInfo.InvariantCulture sayesinde replace yapmaya gerek kalmaz.
                // --- SAFE PARSE OPERATION (Solves Dot/Comma problem) ---
                // Thanks to CultureInfo.InvariantCulture, no need to replace.
                float.TryParse(parcalar[0], NumberStyles.Any, CultureInfo.InvariantCulture, out sicaklik);
                float.TryParse(parcalar[1], NumberStyles.Any, CultureInfo.InvariantCulture, out nem);
                int.TryParse(parcalar[2], out gaz);
                int.TryParse(parcalar[3], out isikHam);
                int.TryParse(parcalar[4], out hareket);

                float isikYuzde = (1023 - isikHam) * 100f / 1023f;
                if (isikYuzde < 0) isikYuzde = 0;
                if (isikYuzde > 100) isikYuzde = 100;

                // --- GÖSTERGELERİ GÜNCELLE ---
                // --- UPDATE GAUGES ---
                GostergeAyarla(gaugeSicaklik, sicaklik);
                GostergeAyarla(gaugeNem, nem);
                GostergeAyarla(gaugeGaz, gaz);
                GostergeAyarla(gaugeIsik, isikYuzde);

                if (stateHareket != null)
                    stateHareket.StateIndex = (hareket == 1) ? 3 : 1;

                // --- TEHLİKE DURUMLARI ---
                // --- DANGER STATES ---
                bool hareketTehlike = (hareket == 1);
                bool nemTehlike = (nem >= LIMIT_NEM);
                bool gazTehlike = (gaz > LIMIT_GAZ);
                bool sicaklikTehlike = (sicaklik > LIMIT_SICAKLIK);
                bool isikTehlike = false;   // Örn. %75 üstü ise alarm
                // Ex. Alarm if above 75%

                // --- ALARM / KÖRLEME MANTIĞI ---
                // --- ALARM / BLINDING LOGIC ---
                DateTime simdi = DateTime.Now;
                bool yeniAlarmVar = false;

                // BU TURDA YENİ TETİKLENEN SEBEPLER
                // REASONS NEWLY TRIGGERED IN THIS TURN
                bool alarmHareket = false;
                bool alarmNem = false;
                bool alarmGaz = false;
                bool alarmSicaklik = false;

                if (hareketTehlike && simdi > hareketYasakBitis)
                {
                    yeniAlarmVar = true;
                    alarmHareket = true;
                    hareketYasakBitis = simdi.AddSeconds(30);
                }

                if (nemTehlike && simdi > nemYasakBitis)
                {
                    yeniAlarmVar = true;
                    alarmNem = true;
                    nemYasakBitis = simdi.AddSeconds(30);
                }

                if (gazTehlike && simdi > gazYasakBitis)
                {
                    yeniAlarmVar = true;
                    alarmGaz = true;
                    gazYasakBitis = simdi.AddSeconds(30);
                }

                if (sicaklikTehlike && simdi > sicaklikYasakBitis)
                {
                   yeniAlarmVar = true;
                    alarmSicaklik = true;
                    sicaklikYasakBitis = simdi.AddSeconds(30);
                }

                if (yeniAlarmVar)
                {
                    // 1) Önce buzzer'ı / LED'i çalıştır
                    // 1) First start buzzer / LED
                    AlarmCalistir();
                    // 2) Mesajı seçili dile göre Resources'tan oluştur
                    // 2) Create message from Resources according to selected language
                    var culture = CultureInfo.CurrentUICulture;
                    // 2) Kullanıcıya sebebi göster
                    // 2) Show reason to user
                    string mesaj = Resources.KasaAlarm_SebepBaslik + Environment.NewLine;

                    if (alarmHareket)
                        mesaj += Resources.KasaAlarm_Hareket + Environment.NewLine;

                    if (alarmNem)
                        mesaj += string.Format(
            culture,
            Resources.KasaAlarm_Nem,
            nem.ToString("F1", culture),
            LIMIT_NEM.ToString("F1", culture)
        ) + Environment.NewLine;

                    if (alarmGaz)
                        mesaj += string.Format(
            culture,
            Resources.KasaAlarm_Gaz,
            gaz.ToString(culture),
            LIMIT_GAZ.ToString(culture)
        ) + Environment.NewLine;

                    if (alarmSicaklik)
                        mesaj += string.Format(
            culture,
            Resources.KasaAlarm_Sicaklik,
            sicaklik.ToString("F1", culture),
            LIMIT_SICAKLIK.ToString("F1", culture)
        ) + Environment.NewLine;

                    XtraMessageBox.Show(
     mesaj,
     Resources.KasaAlarm_Baslik,
     MessageBoxButtons.OK,
     MessageBoxIcon.Warning
 );
                }

                // --- FAN MANTIĞI ---
                // --- FAN LOGIC ---
                // --- FAN LOGIC ---
                // Fan butonu (toggle) null kontrolü ve sıcaklık kontrolü
                // Fan button (toggle) null check and temperature check
                bool fanManuelAcik = (tglFan != null && tglFan.IsOn);

                if (sicaklik > LIMIT_SICAKLIK || fanManuelAcik)
                    FanKontrol(true);
                else
                    FanKontrol(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("VeriIsle hata: " + ex);
            }
        }

        // --- ARDUINO'YA DÜZENLİ PING GÖNDEREN TIMER ---
        // --- TIMER SENDING REGULAR PING TO ARDUINO ---
        private void TmrPing_Tick(object sender, EventArgs e)
        {
            if (formKapaniyor) return;
            try
            {
                lock (portKilidi)
                {
                    if (serialPort1 != null && serialPort1.IsOpen)
                    {
                        serialPort1.Write("PING\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("TmrPing_Tick hata: " + ex);
            }
        }

        // --- ALARM / BUZZER KONTROLÜ ---
        // --- ALARM / BUZZER CONTROL ---
        void AlarmCalistir()
        {
            if (formKapaniyor) return;
            try
            {
                // Zaten alarm çalıyorsa yeniden başlatma
                // If alarm is already ringing do not restart
                if (tmrAlarmSusturucu != null && tmrAlarmSusturucu.Enabled)
                    return;

                lock (portKilidi)
                {
                    if (serialPort1 != null && serialPort1.IsOpen)
                        serialPort1.Write("ALARM_AC\n");  // Arduino: BUZZER HIGH + kırmızı LED
                        // Arduino: BUZZER HIGH + red LED
                        // Arduino: BUZZER HIGH + red LED
                }

                if (tmrAlarmSusturucu != null)
                    tmrAlarmSusturucu.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AlarmCalistir hata: " + ex);
            }
        }

        void AlarmDurdur()
        {
            if (formKapaniyor) return;
            try
            {
                lock (portKilidi)
                {
                    if (serialPort1 != null && serialPort1.IsOpen)
                    {
                        serialPort1.Write("ALARM_KAPAT\n"); // Arduino: BUZZER LOW + yeşil LED
                        // Arduino: BUZZER LOW + green LED
                        Thread.Sleep(10);
                        serialPort1.Write("ALARM_KAPAT\n");
                    }
                }

                if (tmrAlarmSusturucu != null)
                    tmrAlarmSusturucu.Stop();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AlarmDurdur hata: " + ex);
            }
        }

        private void TmrAlarmSusturucu_Tick(object sender, EventArgs e)
        {
            // 2 saniye doldu → buzzer + kırmızı LED kapat, yeşili yak
            // 2 seconds up -> turn off buzzer + red LED, turn on green
            AlarmDurdur();
        }

        // --- FAN KONTROLÜ ---
        // --- FAN CONTROL ---
        void FanKontrol(bool ac)
        {
            if (formKapaniyor) return;
            if (tmrFanAnimasyon == null)
                return;

            try
            {
                if (ac)
                {
                    if (!tmrFanAnimasyon.Enabled)
                    {
                        lock (portKilidi)
                        {
                            if (serialPort1 != null && serialPort1.IsOpen)
                                serialPort1.Write("FAN_AC\n");
                        }
                        tmrFanAnimasyon.Start();
                    }
                }
                else
                {
                    if (tmrFanAnimasyon.Enabled)
                    {
                        lock (portKilidi)
                        {
                            if (serialPort1 != null && serialPort1.IsOpen)
                                serialPort1.Write("FAN_KAPAT\n");
                        }
                        tmrFanAnimasyon.Stop();

                        if (orjinalFanResmi != null)
                            picFan.Image = orjinalFanResmi;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("FanKontrol hata: " + ex);
            }
        }

        // --- GÖSTERGE AYARI ---
        // --- GAUGE SETTING ---
        private void GostergeAyarla(GaugeControl kutu, float deger)
        {
            try
            {
                if (kutu == null || kutu.Gauges == null || kutu.Gauges.Count == 0)
                    return;

                var gauge = kutu.Gauges[0];

                // DAİRESEL GÖSTERGE
                // CIRCULAR GAUGE
                if (gauge is CircularGauge yuvarlak)
                {
                    if (yuvarlak.Scales.Count > 0)
                    {
                        var sc = yuvarlak.Scales[0];
                        float v = deger;
                        if (v < sc.MinValue) v = sc.MinValue;
                        if (v > sc.MaxValue) v = sc.MaxValue;
                        sc.Value = v;
                    }
                }
                // ÇUBUK (LINEAR) GÖSTERGE
                // BAR (LINEAR) GAUGE
                else if (gauge is LinearGauge cubuk)
                {
                    if (cubuk.Scales.Count > 0)
                    {
                        var sc = cubuk.Scales[0];

                        float v = deger;
                        if (v < sc.MinValue) v = sc.MinValue;
                        if (v > sc.MaxValue) v = sc.MaxValue;

                        // 1) Scale değeri
                        // 1) Scale value
                        sc.Value = v;

                        // 2) LEVEL (mavi çubuk) değeri
                        // 2) LEVEL (blue bar) value
                        if (cubuk.Levels != null && cubuk.Levels.Count > 0)
                        {
                            foreach (var lvl in cubuk.Levels)
                            {
                                if (lvl != null)
                                    lvl.Value = v;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GostergeAyarla hata: " + ex);
            }
        }


        private void GostergeAraliginiAyarla(GaugeControl kutu, float min, float max)
        {
            try
            {
                if (kutu == null || kutu.Gauges == null || kutu.Gauges.Count == 0)
                    return;

                if (kutu.Gauges[0] is CircularGauge yuvarlak)
                {
                    foreach (var sc in yuvarlak.Scales)
                    {
                        if (sc == null) continue;
                        sc.MinValue = min;
                        sc.MaxValue = max;
                    }
                }
                else if (kutu.Gauges[0] is LinearGauge cubuk)
                {
                    foreach (var sc in cubuk.Scales)
                    {
                        if (sc == null) continue;
                        sc.MinValue = min;
                        sc.MaxValue = max;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GostergeAraliginiAyarla hata: " + ex);
            }
        }



        // --- DİL UYGULAMA ---
        // --- LANGUAGE APPLICATION ---
        public void UygulaDil()
        {
            try
            {
                this.Text = Resources.Kasa_Baslik;
                if (lblBaslikSicaklik != null) lblBaslikSicaklik.Text = Resources.Kasa_Lbl_Sicaklik;
                if (lblBaslikNem != null) lblBaslikNem.Text = Resources.Kasa_Lbl_Nem;
                if (lblBaslikGaz != null) lblBaslikGaz.Text = Resources.Kasa_Lbl_Gaz;
                if (lblBaslikIsik != null) lblBaslikIsik.Text = Resources.Kasa_Lbl_Isik;
                if (lblBaslikHareket != null) lblBaslikHareket.Text = Resources.Kasa_Lbl_Hareket;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UygulaDil hata: " + ex);
            }
        }

        // --- FAN RESMİ DÖNDÜRME ---
        // --- ROTATING FAN IMAGE ---
        private Image ResimDondur(Image img, float angle)
        {
            if (img == null) return null;

            int size = 256;
            Bitmap bmp = new Bitmap(size, size);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.TranslateTransform(size / 2f, size / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-size / 2f, -size / 2f);
                g.DrawImage(img, 0, 0, size, size);
            }

            return bmp;
        }

        private void tmrFanAnimasyon_Tick(object sender, EventArgs e)
        {
            if (orjinalFanResmi == null) return;

            fanAci += 20;
            if (fanAci >= 360) fanAci = 0;

            Image eski = picFan.Image;
            picFan.Image = ResimDondur(orjinalFanResmi, fanAci);

            if (eski != null && eski != orjinalFanResmi)
                eski.Dispose();
        }

        private void tglFan_Toggled(object sender, EventArgs e)
        {
            try
            {
                if (tglFan != null)
                    FanKontrol(tglFan.IsOn);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("tglFan_Toggled hata: " + ex);
            }
        }

        private void FormKasaDepo_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 1. Bayrağı kaldır: Artık veri işleme!
            // 1. Remove flag: Do not process data anymore!
            formKapaniyor = true;

            // 2. Timerları durdur
            // 2. Stop Timers
            if (tmrPing != null) tmrPing.Stop();
            if (tmrFanAnimasyon != null) tmrFanAnimasyon.Stop();
            if (tmrAlarmSusturucu != null) tmrAlarmSusturucu.Stop();

            // 3. Portun Eventini İptal Et (Önce veri akışını kes)
            // 3. Cancel Port Event (First cut off data flow)
            try
            {
                if (serialPort1 != null)
                    serialPort1.DataReceived -= SerialPort1_DataReceived;
            }
            catch { }

            // 4. KRİTİK NOKTA: UI Thread ile SerialPort Thread'in vedalaşması için
            // 4. CRITICAL POINT: To say goodbye between UI Thread and SerialPort Thread
            // 4. CRITICAL POINT: To say goodbye between UI Thread and SerialPort Thread
            // çok kısa bekle. Bu, "Deadlock" (Donma) riskini %99 azaltır.
            // wait briefly. This reduces "Deadlock" risk by 99%.
            // wait briefly. This reduces "Deadlock" risk by 99%.
            System.Threading.Thread.Sleep(100);

            // 5. Portu tamamen kapat ve yok et
            // 5. Completely close and destroy port
            SeriPortuKapat();
        }

        // Uygulama tamamen kapanırken çağırmak için yardımcı metot (Form1'den çağırabilirsin)
        // Bu metot FormKasaDepo class'ının en altında
        // Helper method to call when application is closing completely (You can call from Form1)
        // This method is at the bottom of FormKasaDepo class
        public static void SeriPortuKapat()
        {
            SerialPort port = null;

            // 1) Sadece referansı çek ve static alanı boşalt
            // 1) Just take reference and clear static field
            lock (portKilidi)
            {
                if (serialPort1 == null)
                    return;

                port = serialPort1;
                serialPort1 = null;   // Artık başka kimse bu portu kullanmasın
                // No one else should use this port anymore
            }

            // 2) Asıl işi kilidin DIŞINDA yap
            // 2) Do real work OUTSIDE the lock
            try
            {
                if (port.IsOpen)
                {
                    port.DiscardInBuffer();
                    port.DiscardOutBuffer();
                    port.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Port kapatma hatası: " + ex.Message);
            }
            finally
            {
                try
                {
                    port.Dispose();
                }
                catch { }
            }
        }

    }
}
