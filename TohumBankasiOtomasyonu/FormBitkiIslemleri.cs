using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using TohumBankasiOtomasyonu.Properties; 
using TohumBankasiOtomasyonu.Models;
using System.IO; 
using System.Drawing.Imaging; 
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

        private void FormBitkiIslemleri_Load(object sender, EventArgs e)
        {
            UygulaDil();
        }

        private void FormBitkiIslemleri_Load_1(object sender, EventArgs e)
        {
            UygulaDil();
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
                // --- 1. VALİDASYON ---
                // --- 1. VALIDATION ---


                // ZORUNLU OLAN TEK ALAN: BİLİMSEL AD
                // The only required field: Scientific Name
                if (string.IsNullOrWhiteSpace(txtBilimselAd.Text))
                {
                    XtraMessageBox.Show(Resources.HataBilimselAd, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBilimselAd.Focus();
                    return;
                }

                // Fiyat kontrolü 
                // Price check
                if (numFiyat.Value <= 0)
                {
                    XtraMessageBox.Show(Resources.HataBitkiFiyat, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numFiyat.Focus();
                    return;
                }

                // Resim kontrolü 
                // Image check
                if (picBitkiResmi.Image == null)
                {
                    XtraMessageBox.Show(Resources.HataBitkiResim, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // --- 2. VERİ HAZIRLIĞI (OTOMATİK DOLDURMA) ---
                // --- 2. DATA PREPARATION (AUTOFILL) ---

                string bilimselAd = txtBilimselAd.Text.Trim();

                // Eğer Türkçe ad girilmediyse, Bilimsel adı kullan
                // If the Turkish name is not entered, use the Scientific name

                string kayitAdTR = string.IsNullOrWhiteSpace(txtAdTR.Text) ? bilimselAd : txtAdTR.Text.Trim();

                // Eğer İngilizce ad girilmediyse, Bilimsel adı kullan
                // If the English name is not entered, use the Scientific name

                string kayitAdEN = string.IsNullOrWhiteSpace(txtAdEN.Text) ? bilimselAd : txtAdEN.Text.Trim();


                // --- 3. RESMİ KLASÖRE KAYDETME ---
                // --- 3. SAVE TO THE OFFICIAL FOLDER ---
                string klasorYolu = Path.Combine(Application.StartupPath, "Gorseller");
                if (!Directory.Exists(klasorYolu)) Directory.CreateDirectory(klasorYolu);
                string dosyaAdi = Guid.NewGuid().ToString() + ".jpg";
                string tamYol = Path.Combine(klasorYolu, dosyaAdi);
                picBitkiResmi.Image.Save(tamYol, ImageFormat.Jpeg);

                // --- 4. VERİTABANI İŞLEMLERİ  ---
                // --- 4. DATABASE OPERATIONS ---
                using (var db = new TohumBankasiContext())
                {
                    // A. ANA TABLO
                    // A. MAIN TABLE
                    Bitkiler yeniBitki = new Bitkiler
                    {
                        Fiyat = (double)numFiyat.Value,
                        StokAdedi = (int)numStok.Value,
                        Aktif = 1
                    };
                    db.Bitkilers.Add(yeniBitki);
                    db.SaveChanges();

                    // B. ÇEVİRİ TABLOSU
                    // B. TRANSLATION TABLE

                    // B1. Türkçe Kaydı
                    // B1. Turkish Record

                    BitkiCevirileri cevirTr = new BitkiCevirileri
                    {
                        BitkiId = yeniBitki.BitkiId,
                        DilKodu = "tr",
                        // Otomatik doldurulmuş veya girilmiş veri
                        // Automatically filled or entered data
                        BitkiAdi = kayitAdTR, 
                        BilimselAd = bilimselAd,
                        Aciklama = memoAciklamaTR.Text,
                        YetistirmeKosullari = memoYetistirmeTR.Text,
                        SaklamaKosullari = memoSaklamaTR.Text
                    };
                    db.BitkiCevirileris.Add(cevirTr);

                    // B2. İngilizce Kaydı
                    // B2. English Record
                    BitkiCevirileri cevirEn = new BitkiCevirileri
                    {
                        BitkiId = yeniBitki.BitkiId,
                        DilKodu = "en",
                        // Otomatik doldurulmuş veya girilmiş veri
                        // Automatically filled or entered data
                        BitkiAdi = kayitAdEN, 
                        BilimselAd = bilimselAd,
                        Aciklama = memoAciklamaEN.Text,
                        YetistirmeKosullari = memoYetistirmeEN.Text,
                        SaklamaKosullari = memoSaklamaEN.Text
                    };
                    db.BitkiCevirileris.Add(cevirEn);

                    // C. GÖRSEL TABLOSU 
                    // C. IMAGE TABLE
                    BitkiGorselleri yeniGorsel = new BitkiGorselleri
                    {
                        BitkiId = yeniBitki.BitkiId,
                        DosyaYolu = "Gorseller/" + dosyaAdi,
                        AnaGorsel = 1
                    };
                    db.BitkiGorselleris.Add(yeniGorsel);

                    db.SaveChanges();

                    XtraMessageBox.Show(Resources.BitkiKayitBasarili, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Formu kapatma, temizle ve yeni kayda hazırla
                    // Close the form, clear it, and prepare for a new record
                    Temizle();
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