using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using System.Security.Cryptography;
namespace TohumBankasiOtomasyonu
{
    public partial class FormGiris : DevExpress.XtraEditors.XtraForm
    {
        // Form1'e "Kayıt Ol"a basıldığını söylemek için bir bayrak
        // A flag to notify Form1 that the "Register" button has been clicked
        public bool KayitOlmakIstiyor { get; private set; } = false;

        // Başarıyla giriş yapan kullanıcıyı tutar (yoksa null kalır)
        // Stores the successfully logged-in user (otherwise remains null)

        public Kullanicilar GirisYapanKullanici { get; private set; } = null;
        public FormGiris()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(420, 200);
        }
        // Bu metot, formdaki tüm metinleri o an seçili olan dile göre
        // .resx sözlük dosyasından yeniden yükler.

        // This method reloads all texts on the form
        // from the .resx dictionary file
        // according to the currently selected language.
        private void UygulaDil()
        {
            // 1. Formun başlığını sözlükten çek
            // 1. Retrieve the form's title from the dictionary
            this.Text = Resources.FormGiris_Title;

            // 2. Labellar
            // 2. Labels
            lblKullaniciAdi.Text = Resources.labelKullaniciAdi;
            lblSifre.Text = Resources.labelSifre;
            // 3. Butonlar ve Linkler
            // 3. Buttons and Links
            linkKayitOl.Text = Resources.btnKayitOl_Text;
            btnGirisYap.Text = Resources.btnGirisYap_Text;
            btnTemizle.Text = Resources.btnTemizle_Text;

        }
        private void FormGiris_Load(object sender, EventArgs e)
        {
            // Form açılırken, o anki program dilini uygula
            // Apply the current program language
            // when the form is opening
            UygulaDil();
        }
        private string ComputeSha256Hash(string rawData)
        {
            // SHA256'nın bir örneğini oluştur
            // Create an instance of SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Girdiyi byte dizisine çevir ve hash'i hesapla
                // Convert the input to a byte array
                // and compute its hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Byte dizisini string'e çevir
                // Convert the byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve şifre kutularının içini boşalt
            // Clear the contents of the username and password fields
            txtKullaniciAdi.Text = "";
            txtSifre.Text = "";

            // Odağı tekrar kullanıcı adı kutusuna getir
            // Set the focus back to the username field
            txtKullaniciAdi.Focus();
        }

        private void linkKayitOl_Click(object sender, EventArgs e)
        {
            this.KayitOlmakIstiyor = true;
            this.Close();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kullanıcı adı ve şifreyi al
                // 1. Get username and password
                string kullaniciAdi = txtKullaniciAdi.Text.Trim();
                string sifre = txtSifre.Text;

                // Alanların boş olup olmadığını kontrol et
                // Check if fields are empty
                if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
                {
                    
                    XtraMessageBox.Show(Resources.GirisAlanlarBosHata, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Girilen şifreyi hash'le
                // 2. Hash the entered password
                string sifreHash = ComputeSha256Hash(sifre);

                // --- Veritabanı Kontrolü (EF Core) ---
                // --- Database Control (EF Core) ---
                using (var db = new TohumBankasiContext())
                {
                    // 3. Kullanıcıyı bulmaya çalış (Kullanıcı adı VE Şifre Hash'i eşleşmeli)
                    // 'FirstOrDefault' eşleşen ilk kaydı getirir, yoksa null döner.
                    // 3. Try to find user (Username AND Password Hash must match)
                    // 'FirstOrDefault' returns first matching record, or null if none.
                    Kullanicilar bulunanKullanici = db.Kullanicilars
                                                     .FirstOrDefault(k => k.KullaniciAdi == kullaniciAdi && k.SifreHash == sifreHash);

                    // 4. Sonucu kontrol et
                    // 4. Check the result
                    if (bulunanKullanici != null)
                    {
                        // --- BAŞARILI GİRİŞ ---
                        // --- SUCCESSFUL LOGIN ---
                        // a. Giriş yapan kullanıcıyı public özelliğe ata
                        // a. Assign logged-in user to public property
                        this.GirisYapanKullanici = bulunanKullanici;

                        // b. Formu kapat (Form1'deki döngü bu özelliği kontrol edecek)
                        // b. Close form (Loop in Form1 will check this property)
                        this.Close();
                    }
                    else
                    {
                        // --- BAŞARISIZ GİRİŞ ---
                        // --- FAILED LOGIN ---
                        XtraMessageBox.Show(Resources.GirisHataliMesaj, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Şifre kutusunu temizle ve odaklan
                        // Clear password box and focus
                        txtSifre.Text = "";
                        txtSifre.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                // Beklenmedik bir veritabanı hatası olursa
                // If an unexpected database error occurs
                XtraMessageBox.Show(Resources.GenelHata + " " + ex.Message, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}