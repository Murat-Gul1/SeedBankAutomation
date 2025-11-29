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
        private List<GaleriBitkisi> _galeriListesi = new List<GaleriBitkisi>();

        public UcAnaSayfa()
        {
            InitializeComponent();

            // DÜZELTME 1: Olayı bağlama
            sliderBitkiler.CurrentImageIndexChanged += SliderBitkiler_CurrentImageIndexChanged;
        }


        // Ana Metot: Verileri Çek ve Yükle
        public void GaleriYukle()
        {
            try
            {
                using (var db = new TohumBankasiContext())
                {
                    string aktifDil = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                    // --- 1. Veritabanı Sorgusu ---
                    // Burada OrderBy(Guid...) KULLANMIYORUZ. Veriyi düz çekiyoruz.
                    var sorgu = (from b in db.Bitkilers
                                 where b.Aktif == 1 // Sadece aktif bitkiler
                                 join g in db.BitkiGorselleris on b.BitkiId equals g.BitkiId
                                 where g.AnaGorsel == 1 // Sadece ana görsel
                                 join c in db.BitkiCevirileris on b.BitkiId equals c.BitkiId
                                 where c.DilKodu == aktifDil // Aktif dildeki çeviri
                                 select new
                                 {
                                     b.BitkiId,
                                     c.BitkiAdi,
                                     c.Aciklama,
                                     g.DosyaYolu
                                 });

                    // --- 2. Veriyi Hafızaya Al ve Karıştır ---
                    // Önce ToList() diyerek veriyi SQL'den C#'a çekiyoruz.
                    var tumListe = sorgu.ToList();

                    // Şimdi C# içinde karıştırıp 5 tane alıyoruz (Burada hata vermez)
                    var hamVeri = tumListe.OrderBy(x => Guid.NewGuid()).Take(5).ToList();

                    // --- 3. Slider'a Yükle ---
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
        private void SliderBitkiler_CurrentImageIndexChanged(object sender, EventArgs e)
        {
            BilgileriGuncelle(sliderBitkiler.CurrentImageIndex);
        }

        // Sağdaki yazıları güncelleyen yardımcı metot
        private void BilgileriGuncelle(int index)
        {
            if (index >= 0 && index < _galeriListesi.Count)
            {
                var bitki = _galeriListesi[index];

                // Başlık (Label)
                lblAnaBaslik.Text = bitki.BitkiAdi;

                // Açıklama (MemoEdit) - ARTIK METNİ KISALTMIYORUZ!
                // MemoEdit kaydırma çubuğu olduğu için tüm metni basabiliriz.
                memoAnaAciklama.Text = bitki.Aciklama;

                // (İsteğe bağlı) İmleci başa al, bazen en altta kalabiliyor
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
            flowPanelUrunler.Controls.Clear();

            try
            {
                using (var db = new TohumBankasiContext())
                {
                    string aktifDil = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                    // Aktif olan tüm bitkileri çek
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
                    foreach (var bitki in bitkiler)
                    {
                        UcUrunKarti kart = new UcUrunKarti();
                        // Kartın verilerini doldur
                        kart.BilgileriDoldur(
                            bitki.BitkiId,
                            bitki.BitkiAdi,
                            bitki.BilimselAd,
                            bitki.Fiyat,
                            bitki.StokAdedi,
                            bitki.DosyaYolu
                        );

                        // Kartlar birbirine yapışmasın diye boşluk verelim
                        kart.Margin = new Padding(10);

                        // KARTI PANELE EKLE
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
            if (flowPanelUrunler.Controls.Count == 0) return;

            // 1. Kartın Genişliğini Hesapla
            // Kart genişliği (200) + Margin (Sol 10 + Sağ 10 = 20) = 220
            // (Eğer kart boyutunu değiştirdiyseniz burayı ona göre güncelleyin)
            int kartTamGenisligi = 220;

            // 2. Panelin Genişliği
            int panelGenisligi = flowPanelUrunler.ClientSize.Width;

            // 3. Bir satıra en fazla kaç kart sığar?
            int satirdakiKartSayisi = Math.Max(1, panelGenisligi / kartTamGenisligi);

            // 4. Bu kartlar toplam ne kadar yer kaplıyor?
            int doluAlan = satirdakiKartSayisi * kartTamGenisligi;

            // 5. Kalan boşluğu bul ve ikiye böl (Sol boşluk = Sağ boşluk)
            int solBosluk = (panelGenisligi - doluAlan) / 2;

            // 6. Panelin sol dolgusunu (Padding) ayarla
            // (Sol boşluk kadar it, üstten 10px boşluk bırak)
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
            int x = (this.ClientSize.Width - pnlMerkezKutu.Width) / 2;

            // Sol kenara yapışmaması için güvenlik (Mobil gibi dar ekranlar için)
            if (x < 10) x = 10;

            // 2. Dikey Konum (Y)
            // Hep en üstten başlasın (Kaydırma çubuğu (Scrollbar) işi halledecek)
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