using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;

namespace TohumBankasiOtomasyonu
{


    public partial class UcAnaSayfa : DevExpress.XtraEditors.XtraUserControl
    {
        // Veritabanından çektiğimiz listeyi burada tutacağız
        // We will keep the list we fetch from the database here
        private List<GaleriBitkisi> _galeriListesi = new List<GaleriBitkisi>();

        public UcAnaSayfa()
        {
            InitializeComponent();

            // DÜZELTME 1: Olayı bağlama
            // FIX 1: Binding the event
            sliderBitkiler.CurrentImageIndexChanged += SliderBitkiler_CurrentImageIndexChanged;
        }


        // Ana Metot: Verileri Çek ve Yükle
        // Main Method: Fetch and Load Data
        public void GaleriYukle()
        {
            try
            {
                using (var db = new TohumBankasiContext())
                {
                    string aktifDil = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                    // --- 1. Veritabanı Sorgusu ---
                    // Burada OrderBy(Guid...) KULLANMIYORUZ. Veriyi düz çekiyoruz.
                    // --- 1. Database Query ---
                    // We DO NOT use OrderBy(Guid...) here. We fetch data directly.
                    var sorgu = (from b in db.Bitkilers
                                 where b.Aktif == 1 // Sadece aktif bitkiler (Only active plants)
                                 join g in db.BitkiGorselleris on b.BitkiId equals g.BitkiId
                                 where g.AnaGorsel == 1 // Sadece ana görsel (Only main image)
                                 join c in db.BitkiCevirileris on b.BitkiId equals c.BitkiId
                                 where c.DilKodu == aktifDil // Aktif dildeki çeviri (Translation in active language)
                                 select new
                                 {
                                     b.BitkiId,
                                     c.BitkiAdi,
                                     c.Aciklama,
                                     g.DosyaYolu
                                 });

                    // --- 2. Veriyi Hafızaya Al ve Karıştır ---
                    // Önce ToList() diyerek veriyi SQL'den C#'a çekiyoruz.
                    // --- 2. Load Data into Memory and Shuffle ---
                    // We first fetch data from SQL to C# by saying ToList().
                    var tumListe = sorgu.ToList();

                    // Şimdi C# içinde karıştırıp 5 tane alıyoruz (Burada hata vermez)
                    // Now we shuffle within C# and take 5 (This won't cause error here)
                    var hamVeri = tumListe.OrderBy(x => Guid.NewGuid()).Take(5).ToList();

                    // --- 3. Slider'a Yükle ---
                    // --- 3. Load to Slider ---
                    _galeriListesi.Clear();
                    sliderBitkiler.Images.Clear();

                    if (hamVeri.Count == 0)
                    {
                        lblAnaBaslik.Text = "Henüz Bitki Yok";
                        return;
                    }

                    foreach (var item in hamVeri)
                    {
                        string tamYol = Path.Combine(Application.StartupPath, item.DosyaYolu);
                        Image img = null;
                        if (File.Exists(tamYol))
                        {
                            using (FileStream stream = new FileStream(tamYol, FileMode.Open, FileAccess.Read))
                            {
                                img = Image.FromStream(stream);
                            }
                        }

                        if (img != null)
                        {
                            _galeriListesi.Add(new GaleriBitkisi
                            {
                                BitkiId = item.BitkiId,
                                BitkiAdi = item.BitkiAdi,
                                Aciklama = item.Aciklama,
                                AnaGorselYolu = item.DosyaYolu,
                                GorselResim = img
                            });

                            sliderBitkiler.Images.Add(img);
                        }
                    }

                    if (_galeriListesi.Count > 0)
                    {
                        BilgileriGuncelle(0);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Galeri yüklenirken hata: " + ex.Message);
            }
        }

        // DÜZELTME 2: Metot ismini ve parametreyi düzelttik
        // (ImageChangedEventArgs -> EventArgs oldu)
        // (Metot ismi yukarıdaki += ile aynı oldu)
        // FIX 2: We corrected the method name and parameter
        // (ImageChangedEventArgs -> EventArgs)
        // (Method name became the same as += above)
        private void SliderBitkiler_CurrentImageIndexChanged(object sender, EventArgs e)
        {
            BilgileriGuncelle(sliderBitkiler.CurrentImageIndex);
        }

        // Sağdaki yazıları güncelleyen yardımcı metot
        // Helper method that updates text on the right
        private void BilgileriGuncelle(int index)
        {
            if (index >= 0 && index < _galeriListesi.Count)
            {
                var bitki = _galeriListesi[index];

                // Başlık (Label)
                // Title (Label)
                lblAnaBaslik.Text = bitki.BitkiAdi;

                // Açıklama (MemoEdit) - ARTIK METNİ KISALTMIYORUZ!
                // MemoEdit kaydırma çubuğu olduğu için tüm metni basabiliriz.
                // Description (MemoEdit) - WE ARE NOT SHORTENING THE TEXT ANYMORE!
                // Since MemoEdit has a scroll bar, we can print the entire text.
                memoAnaAciklama.Text = bitki.Aciklama;

                // (İsteğe bağlı) İmleci başa al, bazen en altta kalabiliyor
                // (Optional) Move cursor to start, sometimes it stays at the bottom
                memoAnaAciklama.SelectionStart = 0;
                memoAnaAciklama.ScrollToCaret();
            }
        }

        private void UcAnaSayfa_Load(object sender, EventArgs e)
        {
            IcerigiOrtala();
            if (!this.DesignMode)
            {
                GaleriYukle();
                VitriniDoldur();
            }
        }

        public void DiliYenile()
        {
            GaleriYukle();
            VitriniDoldur();
        }
        public void VitriniDoldur()
        {
            // Paneli temizle (Eski kartları sil)
            // Clear panel (Delete old cards)
            flowPanelUrunler.Controls.Clear();

            try
            {
                using (var db = new TohumBankasiContext())
                {
                    string aktifDil = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                    // Aktif olan tüm bitkileri çek
                    // Fetch all active plants
                    var bitkiler = (from b in db.Bitkilers
                                    where b.Aktif == 1
                                    join g in db.BitkiGorselleris on b.BitkiId equals g.BitkiId
                                    where g.AnaGorsel == 1
                                    join c in db.BitkiCevirileris on b.BitkiId equals c.BitkiId
                                    where c.DilKodu == aktifDil
                                    select new
                                    {
                                        b.BitkiId,
                                        c.BitkiAdi,
                                        c.BilimselAd,
                                        b.Fiyat,
                                        b.StokAdedi,
                                        g.DosyaYolu
                                    }).ToList();

                    // Her bitki için bir kart oluştur
                    // Create a card for each plant
                    foreach (var bitki in bitkiler)
                    {
                        UcUrunKarti kart = new UcUrunKarti();
                        // Kartın verilerini doldur
                        // Fill card data
                        kart.BilgileriDoldur(
                            bitki.BitkiId,
                            bitki.BitkiAdi,
                            bitki.BilimselAd,
                            bitki.Fiyat,
                            bitki.StokAdedi,
                            bitki.DosyaYolu
                        );

                        // Kartlar birbirine yapışmasın diye boşluk verelim
                        // Let's add spacing so cards don't stick to each other
                        kart.Margin = new Padding(10);

                        // KARTI PANELE EKLE
                        // ADD CARD TO PANEL
                        flowPanelUrunler.Controls.Add(kart);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Vitrin yüklenirken hata: " + ex.Message);
            }
            KartlariOrtala();
        }
        private void KartlariOrtala()
        {
            // Eğer vitrinde hiç kart yoksa işlem yapma
            // Do not process if there are no cards in the showcase
            if (flowPanelUrunler.Controls.Count == 0) return;

            // 1. Kartın Genişliğini Hesapla
            // Kart genişliği (200) + Margin (Sol 10 + Sağ 10 = 20) = 220
            // (Eğer kart boyutunu değiştirdiyseniz burayı ona göre güncelleyin)
            // 1. Calculate Card Width
            // Card width (200) + Margin (Left 10 + Right 10 = 20) = 220
            // (If you changed card size, update here accordingly)
            int kartTamGenisligi = 220;

            // 2. Panelin Genişliği
            // 2. Panel Width
            int panelGenisligi = flowPanelUrunler.ClientSize.Width;

            // 3. Bir satıra en fazla kaç kart sığar?
            // 3. How many cards can fit in a row at most?
            int satirdakiKartSayisi = Math.Max(1, panelGenisligi / kartTamGenisligi);

            // 4. Bu kartlar toplam ne kadar yer kaplıyor?
            // 4. How much space do these cards take up in total?
            int doluAlan = satirdakiKartSayisi * kartTamGenisligi;

            // 5. Kalan boşluğu bul ve ikiye böl (Sol boşluk = Sağ boşluk)
            // 5. Find remaining space and divide by two (Left space = Right space)
            int solBosluk = (panelGenisligi - doluAlan) / 2;

            // 6. Panelin sol dolgusunu (Padding) ayarla
            // (Sol boşluk kadar it, üstten 10px boşluk bırak)
            // 6. Set Panel's left padding
            // (Push as much as left space, leave 10px space from top)
            flowPanelUrunler.Padding = new Padding(solBosluk, 10, 0, 0);
        }

        private void UcAnaSayfa_Resize(object sender, EventArgs e)
        {
            IcerigiOrtala();
        }
        private void IcerigiOrtala()
        {
            if (pnlMerkezKutu == null) return;

            // 1. Yatay Ortalama (X)
            // Zemin genişliğinden kutu genişliğini çıkarıp ikiye bölüyoruz.
            // 1. Horizontal Centering (X)
            // We subtract box width from background width and divide by two.
            int x = (this.ClientSize.Width - pnlMerkezKutu.Width) / 2;

            // Sol kenara yapışmaması için güvenlik (Mobil gibi dar ekranlar için)
            // Safety to prevent sticking to left edge (For narrow screens like mobile)
            if (x < 10) x = 10;

            // 2. Dikey Konum (Y)
            // Hep en üstten başlasın (Kaydırma çubuğu (Scrollbar) işi halledecek)
            // 2. Vertical Position (Y)
            // Always start from top (Scrollbar will handle the rest)
            int y = 10;

            pnlMerkezKutu.Location = new Point(x, y);
        }


    }




    public class GaleriBitkisi
    {
        public int BitkiId { get; set; }
        public string BitkiAdi { get; set; }
        public string Aciklama { get; set; }
        public string AnaGorselYolu { get; set; }
        public Image GorselResim { get; set; }
    }


}