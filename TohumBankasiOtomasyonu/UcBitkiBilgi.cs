using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class UcBitkiBilgi : DevExpress.XtraEditors.XtraUserControl
    {
        public void DiliYenile()
        {
            UygulaDil(); // Sabit metinleri (Başlık, sekmeler) güncelle

            // Eğer bir arama yapılmışsa ve detaylar açıksa, onları da yeni dille tekrar çek
            // (Bunu yapmak için en son seçilen bitki ID'sini bir değişkende tutmalıyız)
            // Şimdilik sadece arama kutusunu temizleyelim veya olduğu gibi bırakalım.
            if (_aktifBitkiId > 0)
            {
                BitkiDetayiniGetir(_aktifBitkiId);
            }
        }
        // Arama sonuçlarını tutmak için geçici bir sınıf
        private class AramaSonucu
        {
            public int Id { get; set; }
            public string GorunenMetin { get; set; } // "Gül (Rosa damascena)"
        }

        List<AramaSonucu> _sonuclar = new List<AramaSonucu>();
        //  Şu an ekranda hangi bitki var? (Hafızada tutuyoruz)
        private int _aktifBitkiId = 0;
        public UcBitkiBilgi()
        {
            InitializeComponent();
        }

        private void UygulaDil()
        {
            
            pageGenel.Text = Resources.tabGenelBilgi;
            pageTeknik.Text = Resources.tabTeknikBilgi;
            txtArama.Properties.NullValuePrompt = Resources.lblAramaBaslik;
            // --- NEW LINES FOR TAB PAGES ---
            pageGenel.Text = Resources.tabGenelBilgi;    // "Genel Bilgiler" / "General Info"
            pageTeknik.Text = Resources.tabTeknikBilgi;
        }

        private void UcBitkiBilgi_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                UygulaDil();
                listAramaSonuclari.Visible = false;

                // Varsayılan olarak ilk bitkiyi getir
                using (var db = new TohumBankasiContext())
                {
                    var ilkBitki = db.Bitkilers.FirstOrDefault(b => b.Aktif == 1);
                    if (ilkBitki != null)
                    {
                        BitkiDetayiniGetir(ilkBitki.BitkiId);
                    }
                    else
                    {
                        Temizle(); // Hiç bitki yoksa temiz kalsın
                    }
                }
            }
        }

        // --- 1. ARAMA YAPILDIKÇA ÇALIŞAN KOD ---
        private void txtArama_EditValueChanged(object sender, EventArgs e)
        {
            string aranan = txtArama.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(aranan))
            {
                listAramaSonuclari.Visible = false;
                return;
            }

            using (var db = new TohumBankasiContext())
            {
                string aktifDil = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                var bulunanlar = (from c in db.BitkiCevirileris
                                  where c.DilKodu == aktifDil &&
                                        (c.BitkiAdi.ToLower().Contains(aranan) || c.BilimselAd.ToLower().Contains(aranan))
                                  select new
                                  {
                                      c.BitkiId,
                                      c.BitkiAdi,
                                      c.BilimselAd
                                  }).Take(10).ToList();

                if (bulunanlar.Count > 0)
                {
                    _sonuclar.Clear();
                    listAramaSonuclari.Items.Clear();

                    foreach (var item in bulunanlar)
                    {
                        string metin = $"{item.BitkiAdi} ({item.BilimselAd})";
                        _sonuclar.Add(new AramaSonucu { Id = item.BitkiId, GorunenMetin = metin });
                        listAramaSonuclari.Items.Add(metin);
                    }

                    // --- KONUM VE BOYUT AYARLAMA (DÜZELTİLEN KISIM) ---

                    // 1. Arama kutusunun EKRAN üzerindeki gerçek konumunu bul
                    Point screenPoint = txtArama.PointToScreen(Point.Empty);

                    // 2. Bu konumu UserControl'ün (this) koordinatına çevir
                    Point formPoint = this.PointToClient(screenPoint);

                    // 3. Listeyi tam altına yerleştir
                    listAramaSonuclari.Location = new Point(formPoint.X, formPoint.Y + txtArama.Height);

                    // 4. Genişliği eşitle
                    listAramaSonuclari.Width = txtArama.Width;

                    // 5. Yükseklik Ayarı (Çok küçülmesini istemiyorsanız burayı değiştirebiliriz)
                    // Mevcut: İçerik kadar büyür/küçülür.
                    // İsterseniz sabit yapabilirsiniz: listAramaSonuclari.Height = 200;
                    int hesaplananYukseklik = (listAramaSonuclari.ItemHeight * bulunanlar.Count) + 15; // Biraz pay ekledim
                    listAramaSonuclari.Height = Math.Max(hesaplananYukseklik, 50); // En az 50px olsun

                    listAramaSonuclari.Visible = true;
                    listAramaSonuclari.BringToFront(); // En öne getir (Resmin üzerinde dursun)
                }
                else
                {
                    listAramaSonuclari.Visible = false;
                }
            }
        }

        // --- 2. LİSTEDEN SEÇİM YAPILINCA ---
        private void listAramaSonuclari_Click(object sender, EventArgs e)
        {
            if (listAramaSonuclari.SelectedIndex == -1) return;

            // Seçilen bitkinin ID'sini bul
            int secilenIndex = listAramaSonuclari.SelectedIndex;
            int bitkiId = _sonuclar[secilenIndex].Id;

            // Detayları getir ve ekrana bas
            BitkiDetayiniGetir(bitkiId);

            // Listeyi gizle ve arama kutusunu güncelle
            listAramaSonuclari.Visible = false;
            txtArama.Text = _sonuclar[secilenIndex].GorunenMetin;
        }

        // --- 3. DETAYLARI GETİRME ---
        private void BitkiDetayiniGetir(int id)
        {
            _aktifBitkiId = id;
            using (var db = new TohumBankasiContext())
            {
                string aktifDil = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                // Çeviri bilgilerini çek
                var ceviri = db.BitkiCevirileris.FirstOrDefault(c => c.BitkiId == id && c.DilKodu == aktifDil);

                // Ana görseli çek
                var gorsel = db.BitkiGorselleris.FirstOrDefault(g => g.BitkiId == id && g.AnaGorsel == 1);

                if (ceviri != null)
                {
                    lblBilgiAd.Text = ceviri.BitkiAdi;
                    lblBilgiBilimsel.Text = ceviri.BilimselAd;
                    memoGenelBilgi.Text = ceviri.Aciklama;
                    string teknikMetin = $"{Resources.BaslikYetistirme}\r\n" +
                         $"{ceviri.YetistirmeKosullari}\r\n\r\n" +
                         $"{Resources.BaslikSaklama}\r\n" +
                         $"{ceviri.SaklamaKosullari}";

                    memoTeknikBilgi.Text = teknikMetin;
                }

                if (gorsel != null)
                {
                    string tamYol = Path.Combine(Application.StartupPath, gorsel.DosyaYolu);
                    if (File.Exists(tamYol))
                    {
                        using (var stream = new FileStream(tamYol, FileMode.Open, FileAccess.Read))
                        {
                            picBilgiResim.Image = Image.FromStream(stream);
                        }
                    }
                }
                else
                {
                    picBilgiResim.Image = null;
                }
            }
        }

        private void Temizle()
        {
            lblBilgiAd.Text = "";
            lblBilgiBilimsel.Text = "";
            memoGenelBilgi.Text = "";
            memoTeknikBilgi.Text = "";
            picBilgiResim.Image = null;
        }
    }
}