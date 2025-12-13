using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Drawing.Imaging; 
using System.Globalization;
using System.IO; 
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;
namespace TohumBankasiOtomasyonu
{
    public partial class FormBitkiIslemleri : DevExpress.XtraEditors.XtraForm
    {
        // Bu değişken 0 ise "Yeni Kayıt", 0'dan büyükse "Düzenleme" modundayız demektir.
        // If this variable is 0, it means "New Record"; if greater than 0, we are in "Edit" mode.
        private int _gelenBitkiId = 0;
        // YENİ: Resim değişti mi kontrolü için bayrak
        // NEW: Flag to check if image changed
        private bool _resimDegisti = false;
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
            // If in new record mode (ID = 0), do nothing, keep empty.
            if (_gelenBitkiId == 0) return;

            using (var db = new TohumBankasiContext())
            {
                // 1. Ana Tablo Verisini Çek
                // 1. Fetch Main Table Data
                var bitki = db.Bitkilers.Find(_gelenBitkiId);
                if (bitki == null) return;

                // Ortak alanları doldur
                // Fill common fields
                txtBilimselAd.Text = ""; // (Bunu aşağıda çevirilerden veya ana tablodan alacağız ama önce temizleyelim)
                // (We will get this from translations or main table below but clear first)
                numFiyat.Value = (decimal)bitki.Fiyat;
                numStok.Value = (decimal)bitki.StokAdedi;

                // 2. Resmi Getir
                // Görsel tablosundan bu bitkiye ait "Ana Görseli" bul
                // 2. Get Image
                // Find "Main Image" for this plant from image table
                var gorselKaydi = db.BitkiGorselleris.FirstOrDefault(g => g.BitkiId == _gelenBitkiId && g.AnaGorsel == 1);
                if (gorselKaydi != null)
                {
                    // Resim yolunu tam yola çevir (bin/debug/Gorseller/...)
                    // Convert image path to full path (bin/debug/Images/...)
                    string tamYol = Path.Combine(Application.StartupPath, gorselKaydi.DosyaYolu);
                    if (File.Exists(tamYol))
                    {
                        // Resmi yükle (Stream kullanarak yüklüyoruz ki dosya kilitlenmesin)
                        // Load image (Using Stream so file doesn't get locked)
                        using (var stream = new FileStream(tamYol, FileMode.Open, FileAccess.Read))
                        {
                            picBitkiResmi.Image = System.Drawing.Image.FromStream(stream);
                        }
                    }
                }

                // 3. Çevirileri Getir (Türkçe)
                // 3. Get Translations (Turkish)
                var tr = db.BitkiCevirileris.FirstOrDefault(c => c.BitkiId == _gelenBitkiId && c.DilKodu == "tr");
                if (tr != null)
                {
                    txtAdTR.Text = tr.BitkiAdi;
                    txtBilimselAd.Text = tr.BilimselAd; // Bilimsel ad her dilde aynıdır, buradan alabiliriz
                    // Scientific name is same in every language, we can get it from here
                    memoAciklamaTR.Text = tr.Aciklama;
                    memoYetistirmeTR.Text = tr.YetistirmeKosullari;
                    memoSaklamaTR.Text = tr.SaklamaKosullari;
                }

                // 4. Çevirileri Getir (İngilizce)
                // 4. Get Translations (English)
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
                // Sözlükten filtre ve başlığı al
                // Get filter and title from dictionary
                openFileDialog.Filter = Resources.ResimSecDialogFilter;
                openFileDialog.Title = Resources.ResimSecDialogTitle;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // --- GÜVENLİ RESİM YÜKLEME (File Lock Önleme) ---
                        // --- SAFE IMAGE LOADING (Prevent File Lock) ---
                        // Resmi dosyadan 'akış' (stream) olarak okuyoruz
                        // We read image from file as 'stream'
                        using (var stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            // Geçici bir resim oluşturuyoruz
                            // Creating a temporary image
                            using (var tempImage = Image.FromStream(stream))
                            {
                                // Bu resmin hafızada temiz bir kopyasını (Bitmap) oluşturup kutuya atıyoruz.
                                // Böylece 'stream' kapansa bile resim hafızada kalıyor ve dosya kilitlenmiyor.
                                // We create a clean copy (Bitmap) of this image in memory and put it in the box.
                                // So even if 'stream' closes, image stays in memory and file is not locked.
                                picBitkiResmi.Image = new Bitmap(tempImage);
                            }
                        }

                        // Görüntüleme ayarı
                        // Display setting
                        picBitkiResmi.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;

                        // --- KRİTİK EKLEME ---
                        // Kaydet butonuna "Bak, kullanıcı resmi değiştirdi" diye haber veriyoruz.
                        // --- CRITICAL ADDITION ---
                        // We notify Save button "Look, user changed the image".
                        _resimDegisti = true;
                    }
                    catch (Exception ex)
                    {
                        // Hata mesajı (Dil destekli)
                        // Error message (Language supported)
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
                // --- 1. VALIDATION (Remains Same) ---
                if (string.IsNullOrWhiteSpace(txtBilimselAd.Text))
                {
                    XtraMessageBox.Show(Resources.HataBilimselAd, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBilimselAd.Focus();
                    return;
                }
                // ... (Fiyat ve Resim validasyonlarını buraya ekleyin veya olduğu gibi bırakın) ...
                // ... (Add Price and Image validations here or leave as is) ...

                // --- 2. VERİ HAZIRLIĞI ---
                // --- 2. DATA PREPARATION ---
                string bilimselAd = txtBilimselAd.Text.Trim();
                string kayitAdTR = string.IsNullOrWhiteSpace(txtAdTR.Text) ? bilimselAd : txtAdTR.Text.Trim();
                string kayitAdEN = string.IsNullOrWhiteSpace(txtAdEN.Text) ? bilimselAd : txtAdEN.Text.Trim();

                using (var db = new TohumBankasiContext())
                {
                    // ==========================================
                    // DURUM A: YENİ KAYIT (INSERT)
                    // CASE A: NEW RECORD (INSERT)
                    // ==========================================
                    if (_gelenBitkiId == 0)
                    {
                        // 1. Resim Kaydetme (Sadece yeni kayıtta veya resim değiştiyse yapılır)
                        // (Basitlik için her zaman kaydediyoruz, gelişmiş versiyonda kontrol edilebilir)
                        // 1. Save Image (Done only in new record or if image changed)
                        // (We always save for simplicity, can be checked in advanced version)
                        string klasorYolu = Path.Combine(Application.StartupPath, "Gorseller");
                        if (!Directory.Exists(klasorYolu)) Directory.CreateDirectory(klasorYolu);
                        string dosyaAdi = Guid.NewGuid().ToString() + ".jpg";
                        string tamYol = Path.Combine(klasorYolu, dosyaAdi);
                        picBitkiResmi.Image.Save(tamYol, ImageFormat.Jpeg);

                        // 2. Bitki Ekle
                        // 2. Add Plant
                        Bitkiler yeniBitki = new Bitkiler
                        {
                            Fiyat = (double)numFiyat.Value,
                            StokAdedi = (int)numStok.Value,
                            Aktif = 1
                        };
                        db.Bitkilers.Add(yeniBitki);
                        db.SaveChanges(); // ID oluşsun diye
                        // So ID generates

                        // 3. Çevirileri Ekle
                        // 3. Add Translations
                        db.BitkiCevirileris.Add(new BitkiCevirileri { BitkiId = yeniBitki.BitkiId, DilKodu = "tr", BitkiAdi = kayitAdTR, BilimselAd = bilimselAd, Aciklama = memoAciklamaTR.Text, YetistirmeKosullari = memoYetistirmeTR.Text, SaklamaKosullari = memoSaklamaTR.Text });
                        db.BitkiCevirileris.Add(new BitkiCevirileri { BitkiId = yeniBitki.BitkiId, DilKodu = "en", BitkiAdi = kayitAdEN, BilimselAd = bilimselAd, Aciklama = memoAciklamaEN.Text, YetistirmeKosullari = memoYetistirmeEN.Text, SaklamaKosullari = memoSaklamaEN.Text });

                        // 4. Görsel Ekle
                        // 4. Add Image
                        db.BitkiGorselleris.Add(new BitkiGorselleri { BitkiId = yeniBitki.BitkiId, DosyaYolu = "Gorseller/" + dosyaAdi, AnaGorsel = 1 });

                        db.SaveChanges();
                        XtraMessageBox.Show(Resources.BitkiKayitBasarili, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Temizle(); // Yeni kayıt için temizle
                        // Clear for new record
                    }
                    // ==========================================
                    // DURUM B: GÜNCELLEME (UPDATE)
                    // CASE B: UPDATE
                    // ==========================================
                    else
                    {
                        // 1. Mevcut Bitkiyi Bul
                        // 1. Find Existing Plant
                        var bitki = db.Bitkilers.Find(_gelenBitkiId);
                        if (bitki != null)
                        {
                            // Ana tabloyu güncelle
                            // Update main table
                            bitki.Fiyat = (double)numFiyat.Value;
                            bitki.StokAdedi = (int)numStok.Value;

                            // 2. Çevirileri Güncelle (TR)
                            // 2. Update Translations (TR)
                            var tr = db.BitkiCevirileris.FirstOrDefault(x => x.BitkiId == _gelenBitkiId && x.DilKodu == "tr");
                            if (tr != null)
                            {
                                tr.BitkiAdi = kayitAdTR; tr.BilimselAd = bilimselAd; tr.Aciklama = memoAciklamaTR.Text; tr.YetistirmeKosullari = memoYetistirmeTR.Text; tr.SaklamaKosullari = memoSaklamaTR.Text;
                            }

                            // 3. Çevirileri Güncelle (EN)
                            // 3. Update Translations (EN)
                            var en = db.BitkiCevirileris.FirstOrDefault(x => x.BitkiId == _gelenBitkiId && x.DilKodu == "en");
                            if (en != null)
                            {
                                en.BitkiAdi = kayitAdEN; en.BilimselAd = bilimselAd; en.Aciklama = memoAciklamaEN.Text; en.YetistirmeKosullari = memoYetistirmeEN.Text; en.SaklamaKosullari = memoSaklamaEN.Text;
                            }

                            // --- 4. RESİM GÜNCELLEME (YENİ EKLENEN KISIM) ---
                            // --- 4. IMAGE UPDATE (NEWLY ADDED PART) ---
                            if (_resimDegisti && picBitkiResmi.Image != null)
                            {
                                // a. Yeni resmi diske kaydet (Yeni bir isimle)
                                // a. Save new image to disk (With a new name)
                                string klasorYolu = Path.Combine(Application.StartupPath, "Gorseller");
                                string dosyaAdi = Guid.NewGuid().ToString() + ".jpg"; // Yeni benzersiz isim
                                // New unique name
                                string tamYol = Path.Combine(klasorYolu, dosyaAdi);

                                // GDI+ hatası olmaması için kopyasını kaydet
                                // Save copy to prevent GDI+ error
                                using (Bitmap bmp = new Bitmap(picBitkiResmi.Image))
                                {
                                    bmp.Save(tamYol, ImageFormat.Jpeg);
                                }

                                // b. Veritabanındaki resim yolunu güncelle
                                // b. Update image path in database
                                var gorselKaydi = db.BitkiGorselleris.FirstOrDefault(g => g.BitkiId == _gelenBitkiId && g.AnaGorsel == 1);

                                if (gorselKaydi != null)
                                {
                                    // Eski kaydı güncelle
                                    // Update old record
                                    gorselKaydi.DosyaYolu = "Gorseller/" + dosyaAdi;

                                    // (İsteğe bağlı: Eski resim dosyasını diskten silebilirsiniz ama riskli olabilir, şimdilik kalsın)
                                    // (Optional: You can delete the old image file from disk but might be risky, keep it for now)
                                }
                                else
                                {
                                    // Eğer eskiden resmi yoksa yeni kayıt oluştur
                                    // If it had no image before, create new record
                                    db.BitkiGorselleris.Add(new BitkiGorselleri
                                    {
                                        BitkiId = _gelenBitkiId,
                                        DosyaYolu = "Gorseller/" + dosyaAdi,
                                        AnaGorsel = 1
                                    });
                                }
                            }
                            // ------------------------------------------------

                            db.SaveChanges();
                            XtraMessageBox.Show("Bitki başarıyla güncellendi.", Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
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