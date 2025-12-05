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
        private static SerialPort serialPort1;
        private static readonly object portKilidi = new object();

        // --- EŞİK DEĞERLERİ ---
        float LIMIT_SICAKLIK = 27.0f;   // °C
        float LIMIT_NEM = 55.0f;        // %RH
        int LIMIT_GAZ = 350;
        int LIMIT_ISIK_HAM = 20;

        // --- HER SENSÖR İÇİN AYRI 30 SN KÖRLEME ---
        DateTime hareketYasakBitis = DateTime.MinValue;
        DateTime nemYasakBitis = DateTime.MinValue;
        DateTime gazYasakBitis = DateTime.MinValue;
        DateTime sicaklikYasakBitis = DateTime.MinValue;
        DateTime isikYasakBitis = DateTime.MinValue;

        Image orjinalFanResmi;
        int fanAci = 0;

        // Arduino'ya düzenli "PING" göndermek için timer
        System.Windows.Forms.Timer tmrPing = new System.Windows.Forms.Timer();

        public FormKasaDepo()
        {
            InitializeComponent();

            // --- TİMERLER ---
            if (tmrAlarmSusturucu != null)
            {
                // Alarm 2 saniye boyunca açık kalacak
                tmrAlarmSusturucu.Interval = 2000;
                tmrAlarmSusturucu.Tick += TmrAlarmSusturucu_Tick;
            }

            if (tmrFanAnimasyon != null)
            {
                tmrFanAnimasyon.Interval = 50;
                tmrFanAnimasyon.Tick += tmrFanAnimasyon_Tick;
            }

            // --- PING TIMER AYARI ---
            tmrPing.Interval = 1000; // 1 saniyede bir PING
            tmrPing.Tick += TmrPing_Tick;
        }

        private void FormKasaDepo_Load(object sender, EventArgs e)
        {
            acilisZamani = DateTime.Now;
            UygulaDil();
            baslangicGecikmeSayaci = 0; // Sayacı sıfırla

            // --- FAN RESMİ YÜKLE ---
            try { orjinalFanResmi = Properties.Resources.fan; }
            catch { if (picFan.Image != null) orjinalFanResmi = picFan.Image; }
            if (orjinalFanResmi != null) picFan.Image = orjinalFanResmi;

            // --- ARDUINO BAĞLANTISI ---
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
                    serialPort1.DataReceived -= SerialPort1_DataReceived;
                    serialPort1.DataReceived += SerialPort1_DataReceived;

                    if (!serialPort1.IsOpen)
                    {
                        serialPort1.Open();

                        // Açılır açılmaz buffer'da bekleyen eski veriyi SİL!
                        Thread.Sleep(250);            // Çeyrek saniye bekle ki veri akışı otursun
                        serialPort1.DiscardInBuffer();
                        serialPort1.DiscardOutBuffer();
                    }
                }

                // Timer'ı başlat
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
                    return;
                }

                if (string.IsNullOrWhiteSpace(gelenVeri))
                    return;

                gelenVeri = gelenVeri.Trim();

                // >>> THROTTLE: En fazla 10 Hz (100 ms'de bir)
                var simdi = DateTime.Now;
                if ((simdi - sonUIGuncelleme).TotalMilliseconds < 100)
                {
                    // 100ms dolmadan yeni veri geldiyse, UI'ya yollamadan çöpe at
                    return;
                }
                sonUIGuncelleme = simdi;
                // <<<

                if (this.IsDisposed || !this.IsHandleCreated || formKapaniyor)
                    return;

                // Artık gerçekten UI'da işlenecek veri bu
                this.BeginInvoke(new Action<string>(VeriIsle), gelenVeri);
            }
            catch
            {
                // Kapanış sırasında oluşabilecek saçma hataları yutuyoruz
            }
        }



        // --- VERİ İŞLEME (UI THREAD) ---
        private void VeriIsle(string gelenVeri)
        {
            // 1. GÜVENLİK KİLİDİ: Form açıldıktan sonraki ilk 4 saniye işlem yapma!
            // Sensörlerin (özellikle Gaz ve PIR) kendine gelmesi için zaman tanı.
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
                float.TryParse(parcalar[0], NumberStyles.Any, CultureInfo.InvariantCulture, out sicaklik);
                float.TryParse(parcalar[1], NumberStyles.Any, CultureInfo.InvariantCulture, out nem);
                int.TryParse(parcalar[2], out gaz);
                int.TryParse(parcalar[3], out isikHam);
                int.TryParse(parcalar[4], out hareket);

                float isikYuzde = (1023 - isikHam) * 100f / 1023f;
                if (isikYuzde < 0) isikYuzde = 0;
                if (isikYuzde > 100) isikYuzde = 100;

                // --- GÖSTERGELERİ GÜNCELLE ---
                GostergeAyarla(gaugeSicaklik, sicaklik);
                GostergeAyarla(gaugeNem, nem);
                GostergeAyarla(gaugeGaz, gaz);
                GostergeAyarla(gaugeIsik, isikYuzde);

                if (stateHareket != null)
                    stateHareket.StateIndex = (hareket == 1) ? 3 : 1;

                // --- TEHLİKE DURUMLARI ---
                bool hareketTehlike = (hareket == 1);
                bool nemTehlike = (nem >= LIMIT_NEM);
                bool gazTehlike = (gaz > LIMIT_GAZ);
                bool sicaklikTehlike = (sicaklik > LIMIT_SICAKLIK);
                bool isikTehlike = false;   // Örn. %75 üstü ise alarm

                // --- ALARM / KÖRLEME MANTIĞI ---
                DateTime simdi = DateTime.Now;
                bool yeniAlarmVar = false;

                // BU TURDA YENİ TETİKLENEN SEBEPLER
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

                //if (isikTehlike && simdi > isikYasakBitis)
                //{
                //    yeniAlarmVar = true;
                //    isikYasakBitis = simdi.AddSeconds(30);
                //}

                if (yeniAlarmVar)
                {
                    // 1) Önce buzzer'ı / LED'i çalıştır
                    AlarmCalistir();

                    // 2) Kullanıcıya sebebi göster
                    string mesaj = "Alarm sebebi:\n";

                    if (alarmHareket)
                        mesaj += "- Hareket algılandı.\n";

                    if (alarmNem)
                        mesaj += $"- Nem yüksek. (Nem: {nem:F1} %, Limit: {LIMIT_NEM:F1} %)\n";

                    if (alarmGaz)
                        mesaj += $"- Gaz seviyesi yüksek. (Gaz: {gaz}, Limit: {LIMIT_GAZ})\n";

                    if (alarmSicaklik)
                        mesaj += $"- Sıcaklık yüksek. (Sıcaklık: {sicaklik:F1} °C, Limit: {LIMIT_SICAKLIK:F1} °C)\n";

                    XtraMessageBox.Show(
                        mesaj,
                        "Kasa / Depo Alarm",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }

                // --- FAN MANTIĞI ---
                // Fan butonu (toggle) null kontrolü ve sıcaklık kontrolü
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
        void AlarmCalistir()
        {
            if (formKapaniyor) return;
            try
            {
                // Zaten alarm çalıyorsa yeniden başlatma
                if (tmrAlarmSusturucu != null && tmrAlarmSusturucu.Enabled)
                    return;

                lock (portKilidi)
                {
                    if (serialPort1 != null && serialPort1.IsOpen)
                        serialPort1.Write("ALARM_AC\n");  // Arduino: BUZZER HIGH + kırmızı LED
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
            AlarmDurdur();
        }

        // --- FAN KONTROLÜ ---
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
        private void GostergeAyarla(GaugeControl kutu, float deger)
        {
            try
            {
                if (kutu == null || kutu.Gauges == null || kutu.Gauges.Count == 0)
                    return;

                var gauge = kutu.Gauges[0];

                // DAİRESEL GÖSTERGE
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
                else if (gauge is LinearGauge cubuk)
                {
                    if (cubuk.Scales.Count > 0)
                    {
                        var sc = cubuk.Scales[0];

                        float v = deger;
                        if (v < sc.MinValue) v = sc.MinValue;
                        if (v > sc.MaxValue) v = sc.MaxValue;

                        // 1) Scale değeri
                        sc.Value = v;

                        // 2) LEVEL (mavi çubuk) değeri
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
            formKapaniyor = true;

            // 2. Timerları durdur
            if (tmrPing != null) tmrPing.Stop();
            if (tmrFanAnimasyon != null) tmrFanAnimasyon.Stop();
            if (tmrAlarmSusturucu != null) tmrAlarmSusturucu.Stop();

            // 3. Portun Eventini İptal Et (Önce veri akışını kes)
            try
            {
                if (serialPort1 != null)
                    serialPort1.DataReceived -= SerialPort1_DataReceived;
            }
            catch { }

            // 4. KRİTİK NOKTA: UI Thread ile SerialPort Thread'in vedalaşması için
            // çok kısa bekle. Bu, "Deadlock" (Donma) riskini %99 azaltır.
            System.Threading.Thread.Sleep(100);

            // 5. Portu tamamen kapat ve yok et
            SeriPortuKapat();
        }

        // Uygulama tamamen kapanırken çağırmak için yardımcı metot (Form1'den çağırabilirsin)
        // Bu metot FormKasaDepo class'ının en altında
        public static void SeriPortuKapat()
        {
            SerialPort port = null;

            // 1) Sadece referansı çek ve static alanı boşalt
            lock (portKilidi)
            {
                if (serialPort1 == null)
                    return;

                port = serialPort1;
                serialPort1 = null;   // Artık başka kimse bu portu kullanmasın
            }

            // 2) Asıl işi kilidin DIŞINDA yap
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
