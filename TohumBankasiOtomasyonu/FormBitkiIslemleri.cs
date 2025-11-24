using DevExpress.XtraEditors;
using System;
using System.Drawing.Imaging; 
using System.Globalization;
using System.IO; 
using System.Threading;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;
using System.Linq;
namespace TohumBankasiOtomasyonu
{
    public partial class FormBitkiIslemleri : DevExpress.XtraEditors.XtraForm
    {
        // Bu değişken 0 ise "Yeni Kayıt", 0'dan büyükse "Düzenleme" modundayız demektir.
        // If this variable is 0, it means "New Record"; if greater than 0, we are in "Edit" mode.
        private int _gelenBitkiId = 0;

        // 1. OLUŞTURUCU (Yeni Ekleme İçin)
        // 1. CONSTRUCTOR (For New Addition)
        public FormBitkiIslemleri()
        {
            InitializeComponent();
            _gelenBitkiId = 0;
        }

        public FormBitkiIslemleri(int bitkiId) : this()
        {
            _gelenBitkiId = bitkiId;
        }

        private void UygulaDil()
        {
            if (_gelenBitkiId == 0) this.Text = Resources.FormBitkiIslemleri_Title_Ekle;
            else this.Text = Resources.FormBitkiIslemleri_Title_Duzenle;

            if (btnKaydet != null) btnKaydet.Text = Resources.btnBitkiKaydet;
            if (btnResimSec != null) btnResimSec.Text = Resources.btnResimSec;
            if (pageTR != null) pageTR.Text = Resources.tabBitkiTR;
            if (pageEN != null) pageEN.Text = Resources.tabBitkiEN;


            var lciBilimsel = layoutControl1.GetItemByControl(txtBilimselAd);
            if (lciBilimsel != null) lciBilimsel.Text = Resources.lblBitkiBilimselAd;

            var lciFiyat = layoutControl1.GetItemByControl(numFiyat);
            if (lciFiyat != null) lciFiyat.Text = Resources.lblBitkiFiyat;

            var lciStok = layoutControl1.GetItemByControl(numStok);
            if (lciStok != null) lciStok.Text = Resources.lblBitkiStok;

            var lciResim = layoutControl1.GetItemByControl(picBitkiResmi);
            if (lciResim != null) lciResim.Text = Resources.lblBitkiResim;

            lciAdTR.Text = Resources.lblBitkiAdi;
            lciAciklamaTR.Text = Resources.lblBitkiAciklama;
            lciYetistirmeTR.Text = Resources.lblBitkiYetistirme;
            lciSaklamaTR.Text = Resources.lblBitkiSaklama;

            
            lciAdEN.Text = Resources.lblBitkiAdi;
            lciAciklamaEN.Text = Resources.lblBitkiAciklama;
            lciYetistirmeEN.Text = Resources.lblBitkiYetistirme;
            lciSaklamaEN.Text = Resources.lblBitkiSaklama;
        }
        private void VerileriDoldur()
        {
            // Eğer yeni kayıt modundaysak (ID = 0), hiçbir şey yapma, boş kalsın.
            if (_gelenBitkiId == 0) return;

            using (var db = new TohumBankasiContext())
            {
                // 1. Ana Tablo Verisini Çek
                var bitki = db.Bitkilers.Find(_gelenBitkiId);
                if (bitki == null) return;

                // Ortak alanları doldur
                txtBilimselAd.Text = ""; // (Bunu aşağıda çevirilerden veya ana tablodan alacağız ama önce temizleyelim)
                numFiyat.Value = (decimal)bitki.Fiyat;
                numStok.Value = (decimal)bitki.StokAdedi;

                // 2. Resmi Getir
                // Görsel tablosundan bu bitkiye ait "Ana Görseli" bul
                var gorselKaydi = db.BitkiGorselleris.FirstOrDefault(g => g.BitkiId == _gelenBitkiId && g.AnaGorsel == 1);
                if (gorselKaydi != null)
                {
                    // Resim yolunu tam yola çevir (bin/debug/Gorseller/...)
                    string tamYol = Path.Combine(Application.StartupPath, gorselKaydi.DosyaYolu);
                    if (File.Exists(tamYol))
                    {
                        // Resmi yükle (Stream kullanarak yüklüyoruz ki dosya kilitlenmesin)
                        using (var stream = new FileStream(tamYol, FileMode.Open, FileAccess.Read))
                        {
                            picBitkiResmi.Image = System.Drawing.Image.FromStream(stream);
                        }
                    }
                }

                // 3. Çevirileri Getir (Türkçe)
                var tr = db.BitkiCevirileris.FirstOrDefault(c => c.BitkiId == _gelenBitkiId && c.DilKodu == "tr");
                if (tr != null)
                {
                    txtAdTR.Text = tr.BitkiAdi;
                    txtBilimselAd.Text = tr.BilimselAd; // Bilimsel ad her dilde aynıdır, buradan alabiliriz
                    memoAciklamaTR.Text = tr.Aciklama;
                    memoYetistirmeTR.Text = tr.YetistirmeKosullari;
                    memoSaklamaTR.Text = tr.SaklamaKosullari;
                }

                // 4. Çevirileri Getir (İngilizce)
                var en = db.BitkiCevirileris.FirstOrDefault(c => c.BitkiId == _gelenBitkiId && c.DilKodu == "en");
                if (en != null)
                {
                    txtAdEN.Text = en.BitkiAdi;
                    memoAciklamaEN.Text = en.Aciklama;
                    memoYetistirmeEN.Text = en.YetistirmeKosullari;
                    memoSaklamaEN.Text = en.SaklamaKosullari;
                }
            }
        }

        private void FormBitkiIslemleri_Load(object sender, EventArgs e)
        {
            UygulaDil();
            VerileriDoldur();
        }

        private void FormBitkiIslemleri_Load_1(object sender, EventArgs e)
        {
            UygulaDil();
            VerileriDoldur();
        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                
                openFileDialog.Filter = Resources.ResimSecDialogFilter;
                openFileDialog.Title = Resources.ResimSecDialogTitle;

                
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        
                        picBitkiResmi.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
                        picBitkiResmi.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
                    }
                    catch (Exception ex)
                    {
                        
                        XtraMessageBox.Show(Resources.ResimYuklemeHata + " " + ex.Message, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // --- 1. VALİDASYON (Aynı Kalıyor) ---
                if (string.IsNullOrWhiteSpace(txtBilimselAd.Text))
                {
                    XtraMessageBox.Show(Resources.HataBilimselAd, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBilimselAd.Focus();
                    return;
                }
                // ... (Fiyat ve Resim validasyonlarını buraya ekleyin veya olduğu gibi bırakın) ...

                // --- 2. VERİ HAZIRLIĞI ---
                string bilimselAd = txtBilimselAd.Text.Trim();
                string kayitAdTR = string.IsNullOrWhiteSpace(txtAdTR.Text) ? bilimselAd : txtAdTR.Text.Trim();
                string kayitAdEN = string.IsNullOrWhiteSpace(txtAdEN.Text) ? bilimselAd : txtAdEN.Text.Trim();

                using (var db = new TohumBankasiContext())
                {
                    // ==========================================
                    // DURUM A: YENİ KAYIT (INSERT)
                    // ==========================================
                    if (_gelenBitkiId == 0)
                    {
                        // 1. Resim Kaydetme (Sadece yeni kayıtta veya resim değiştiyse yapılır)
                        // (Basitlik için her zaman kaydediyoruz, gelişmiş versiyonda kontrol edilebilir)
                        string klasorYolu = Path.Combine(Application.StartupPath, "Gorseller");
                        if (!Directory.Exists(klasorYolu)) Directory.CreateDirectory(klasorYolu);
                        string dosyaAdi = Guid.NewGuid().ToString() + ".jpg";
                        string tamYol = Path.Combine(klasorYolu, dosyaAdi);
                        picBitkiResmi.Image.Save(tamYol, ImageFormat.Jpeg);

                        // 2. Bitki Ekle
                        Bitkiler yeniBitki = new Bitkiler
                        {
                            Fiyat = (double)numFiyat.Value,
                            StokAdedi = (int)numStok.Value,
                            Aktif = 1
                        };
                        db.Bitkilers.Add(yeniBitki);
                        db.SaveChanges(); // ID oluşsun diye

                        // 3. Çevirileri Ekle
                        db.BitkiCevirileris.Add(new BitkiCevirileri { BitkiId = yeniBitki.BitkiId, DilKodu = "tr", BitkiAdi = kayitAdTR, BilimselAd = bilimselAd, Aciklama = memoAciklamaTR.Text, YetistirmeKosullari = memoYetistirmeTR.Text, SaklamaKosullari = memoSaklamaTR.Text });
                        db.BitkiCevirileris.Add(new BitkiCevirileri { BitkiId = yeniBitki.BitkiId, DilKodu = "en", BitkiAdi = kayitAdEN, BilimselAd = bilimselAd, Aciklama = memoAciklamaEN.Text, YetistirmeKosullari = memoYetistirmeEN.Text, SaklamaKosullari = memoSaklamaEN.Text });

                        // 4. Görsel Ekle
                        db.BitkiGorselleris.Add(new BitkiGorselleri { BitkiId = yeniBitki.BitkiId, DosyaYolu = "Gorseller/" + dosyaAdi, AnaGorsel = 1 });

                        db.SaveChanges();
                        XtraMessageBox.Show(Resources.BitkiKayitBasarili, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Temizle(); // Yeni kayıt için temizle
                    }
                    // ==========================================
                    // DURUM B: GÜNCELLEME (UPDATE)
                    // ==========================================
                    else
                    {
                        // 1. Mevcut Bitkiyi Bul
                        var bitki = db.Bitkilers.Find(_gelenBitkiId);
                        if (bitki != null)
                        {
                            // Ana tabloyu güncelle
                            bitki.Fiyat = (double)numFiyat.Value;
                            bitki.StokAdedi = (int)numStok.Value;

                            // 2. Çevirileri Güncelle (TR)
                            var tr = db.BitkiCevirileris.FirstOrDefault(x => x.BitkiId == _gelenBitkiId && x.DilKodu == "tr");
                            if (tr != null)
                            {
                                tr.BitkiAdi = kayitAdTR; tr.BilimselAd = bilimselAd; tr.Aciklama = memoAciklamaTR.Text; tr.YetistirmeKosullari = memoYetistirmeTR.Text; tr.SaklamaKosullari = memoSaklamaTR.Text;
                            }

                            // 3. Çevirileri Güncelle (EN)
                            var en = db.BitkiCevirileris.FirstOrDefault(x => x.BitkiId == _gelenBitkiId && x.DilKodu == "en");
                            if (en != null)
                            {
                                en.BitkiAdi = kayitAdEN; en.BilimselAd = bilimselAd; en.Aciklama = memoAciklamaEN.Text; en.YetistirmeKosullari = memoYetistirmeEN.Text; en.SaklamaKosullari = memoSaklamaEN.Text;
                            }

                            // (Resim güncelleme işlemi biraz daha karmaşıktır, şimdilik resmi değiştirmeyi es geçiyoruz veya
                            // eskiyi silip yenisini eklemek gerekir. Basitlik adına şu an sadece metinleri güncelliyoruz.)

                            db.SaveChanges();
                            XtraMessageBox.Show("Bitki başarıyla güncellendi.", Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); // Güncelleme bitince formu kapat
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Resources.GenelHata + " " + ex.Message, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Formdaki tüm alanları sıfırlayan metot
        // Method that resets all fields in the form

        private void Temizle()
        {
            
            txtBilimselAd.Text = "";
            numFiyat.Value = 0;
            numStok.Value = 0;
            picBitkiResmi.Image = null; 

            
            txtAdTR.Text = "";
            memoAciklamaTR.Text = "";
            memoYetistirmeTR.Text = "";
            memoSaklamaTR.Text = "";

            
            txtAdEN.Text = "";
            memoAciklamaEN.Text = "";
            memoYetistirmeEN.Text = "";
            memoSaklamaEN.Text = "";

            
            txtBilimselAd.Focus();
        }
    }
}