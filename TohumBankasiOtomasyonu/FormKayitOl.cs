using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using TohumBankasiOtomasyonu.Properties;
using System.Security.Cryptography;
using TohumBankasiOtomasyonu.Models;
namespace TohumBankasiOtomasyonu
{
    public partial class FormKayitOl : DevExpress.XtraEditors.XtraForm
    {
        public bool KayitBasarili { get; private set; } = false;
        public FormKayitOl()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(420, 350);
        }
        private void UygulaDil()
        {
            // 1. Formun başlığını sözlükten çek
            // 1. Retrieve the form's title from the dictionary
            this.Text = Resources.FormKayitOl_Title;

            lblKayitKullaniciAdi.Text = Resources.labelKayitKullaniciAdi;
            lblKayitSifre.Text = Resources.labelKayitSifre;
            lblKayitSifreTekrar.Text = Resources.labelKayitSifreTekrar;
            lblAd.Text = Resources.labelKayitAd;
            lblSoyad.Text = Resources.labelKayitSoyad;
            lblEmail.Text = Resources.labelKayitEmail;

            btnKaydol.Text = Resources.btnKaydol_Text;
            btnKayitTemizle.Text = Resources.btnKayitTemizle_Text;
        }

        private void FormKayitOl_Load(object sender, EventArgs e)
        {
            // Form açılırken, o anki program dilini uygula
            // Apply the current program language
            // when the form is being opened
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

        private void btnKayitTemizle_Click(object sender, EventArgs e)
        {
            // Tüm metin kutularının .Text özelliğini boş bir string ("") olarak ayarla
            // Set the .Text property of all text boxes
            // to an empty string ("")
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtEmail.Text = "";
            txtKayitKullaniciAdi.Text = "";
            txtKayitSifre.Text = "";
            txtKayitSifreTekrar.Text = "";

            // Temizledikten sonra imleci (fokusu) ilk kutuya (Ad) geri getir.
            // After clearing, set the cursor (focus)
            // back to the first field (Name)
            txtKayitKullaniciAdi.Focus();
        }

        private void btnKaydol_Click(object sender, EventArgs e)
        {
            try
            {
                // --- 1. Validasyon: Zorunlu Alan Kontrolü ---
                // --- 1. Validation: Mandatory Field Check ---

                // .Trim() komutu, kullanıcının girdiği metnin başındaki/sonundaki boşlukları siler.
                // .Trim() command removes leading/trailing spaces from user input.
                string ad = txtAd.Text.Trim();
                string soyad = txtSoyad.Text.Trim();
                string kullaniciAdi = txtKayitKullaniciAdi.Text.Trim();
                string sifre = txtKayitSifre.Text; // Şifrelerde .Trim() kullanmayız (We don't use .Trim() for passwords)
                string sifreTekrar = txtKayitSifreTekrar.Text;
                string email = txtEmail.Text.Trim();

                // Zorunlu alanların (DB'de NOT NULL olanlar) boş olup olmadığını kontrol et
                // Check if mandatory fields (NOT NULL in DB) are empty
                if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad) ||
                    string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre) ||
                    string.IsNullOrEmpty(sifreTekrar))
                {
                    // Hata mesajını sözlükten çek
                    // Get error message from dictionary
                    XtraMessageBox.Show(Resources.ZorunluAlanlarHata, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Metottan çık, kayıt yapma
                    // Exit method, do not register
                }

                // --- 2. Validasyon: Şifre Eşleşme Kontrolü ---
                // --- 2. Validation: Password Match Check ---
                if (sifre != sifreTekrar)
                {
                    XtraMessageBox.Show(Resources.SifreEslesmeHata, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // --- Veritabanı İşlemleri (EF Core) ---
                // 'using' bloğu, işlem bitince veritabanı bağlantısını otomatik olarak kapatır.
                // --- Database Operations (EF Core) ---
                // 'using' block automatically closes database connection when process ends.
                using (var db = new TohumBankasiContext())
                {
                    // --- 3. Validasyon: Kullanıcı Adı Benzersiz mi? ---
                    // --- 3. Validation: Is Username Unique? ---
                    bool kullaniciMevcut = db.Kullanicilars.Any(k => k.KullaniciAdi == kullaniciAdi);
                    if (kullaniciMevcut)
                    {
                        XtraMessageBox.Show(Resources.KullaniciAdiMevcutHata, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // --- 4. Güvenlik: Şifreyi Hash'leme ---
                    // --- 4. Security: Hashing Password ---
                    string sifreHash = ComputeSha256Hash(sifre);

                    // --- 5. Kayıt: Yeni Kullanıcıyı Oluşturma ---
                    // --- 5. Registration: Creating New User ---
                    Kullanicilar yeniKullanici = new Kullanicilar
                    {
                        Ad = ad,
                        Soyad = soyad,
                        KullaniciAdi = kullaniciAdi,
                        SifreHash = sifreHash,
                        Email = string.IsNullOrEmpty(email) ? null : email, // E-posta boşsa DB'ye null yaz (Write null to DB if email is empty)
                        KullaniciTipi = "Kullanici" // Varsayılan kullanıcı tipi (Default user type)
                    };

                    // --- 6. Kayıt: Veritabanına Ekleme ---
                    // --- 6. Registration: Adding to Database ---
                    db.Kullanicilars.Add(yeniKullanici);
                    db.SaveChanges(); // Değişiklikleri kaydet (Save changes)

                    // --- 7. Geri Bildirim ve Formu Kapatma ---
                    // --- 7. Feedback and Closing Form ---

                    // Başarı mesajı
                    // Success message
                    XtraMessageBox.Show(Resources.KayitBasariliMesaj, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Form1'deki 'while' döngüsünün bilmesi için bayrağı kaldır
                    // Raise flag so 'while' loop in Form1 knows
                    this.KayitBasarili = true;

                    // Bu formu (FormKayitOl) kapat
                    // Close this form (FormKayitOl)
                    this.Close();
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