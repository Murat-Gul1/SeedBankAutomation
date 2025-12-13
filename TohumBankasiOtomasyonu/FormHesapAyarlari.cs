using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class FormHesapAyarlari : DevExpress.XtraEditors.XtraForm
    {
        // Form1'den gelen kullanıcıyı bu değişkende saklayacağız
        // We'll store the user coming from Form1 in this variable
        private Kullanicilar guncellenecekKullanici;

        // Form1'e "Güncelleme başarılı oldu" haberini vermek için.
        // To notify Form1 that the update was successful.
        public bool GuncellemeBasarili { get; private set; } = false;
        public FormHesapAyarlari()
        {
            InitializeComponent();
        }

        // YENİ OLUŞTURUCU (CONSTRUCTOR)
        // NEW CONSTRUCTOR
        public FormHesapAyarlari(Kullanicilar kullanici) : this()
        {
            // 'this()' komutu, yukarıdaki varsayılan 'public FormHesapAyarlari()' 
            // metodunu (ve içindeki 'InitializeComponent()' komutunu) otomatik olarak çağırır.
            // The 'this()' command automatically calls the default 'public FormHesapAyarlari()' method above  
            // along with its 'InitializeComponent()' command inside.


            // Form1'den gelen 'kullanici' nesnesini, bu formdaki değişkene ata
            // Assign the 'kullanici' object received from Form1 to the corresponding variable in this form
            this.guncellenecekKullanici = kullanici;
        }
        private void UygulaDil()
        {
            // 1. Form başlığı
            // 1. Form title
            this.Text = Resources.FormHesapAyarlari_Title;

            // 2. Labellar
            // 2. Labels
            lblAd.Text = Resources.labelHesapAd;
            lblSoyad.Text = Resources.labelHesapSoyad;
            lblEmail.Text = Resources.labelHesapEmail;
            lblKullaniciAdi.Text = Resources.labelHesapKullaniciAdi;
            lblYeniSifre.Text = Resources.labelHesapYeniSifre;
            lblYeniSifreTekrar.Text = Resources.labelHesapYeniSifreTekrar;
            lblMevcutSifre.Text = Resources.labelHesapMevcutSifre;

            // 3. Butonlar ve Linkler
            // 3. Buttons and Links
            btnHesapGuncelle.Text = Resources.btnHesapGuncelle;
            btnHesapTemizle.Text = Resources.btnHesapTemizle_Text;
            linkHesapSil.Text = Resources.linkHesapSil;
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
        /// Formdaki tüm girişleri temizler veya orijinal hallerine döndürür.
        private void AlanlariTemizle()
        {
            // 1. Ad, Soyad, Email ve Kullanıcı Adı alanlarını 
            //    veritabanındaki orijinal değerlerine geri yükle
            // 1. Restore the Name, Surname, Email, and Username fields  
            //    to their original values from the database
            VerileriDoldur();

            // 2. Şifre alanlarını boşalt
            // 2. Clear the password fields
            txtYeniSifre.Text = "";
            txtYeniSifreTekrar.Text = "";
            txtMevcutSifre.Text = "";

            // 3. Odağı, değiştirilebilir ilk alan olan 'Ad' kutusuna getir
            // 3. Set the focus to the first editable field, the 'Name' textbox
            txtAd.Focus();
        }
        private void VerileriDoldur()
        {
            // Sakladığımız kullanıcı bilgisi (guncellenecekKullanici) boş değilse
            // If the user information we stored (guncellenecekKullanici) is not empty
            if (guncellenecekKullanici != null)
            {
                // Metin kutularını doldur
                // Fill text boxes
                txtAd.Text = guncellenecekKullanici.Ad;
                txtSoyad.Text = guncellenecekKullanici.Soyad;
                txtEmail.Text = guncellenecekKullanici.Email;
                txtKullaniciAdi.Text = guncellenecekKullanici.KullaniciAdi;

                // GÜVENLİK ÖNLEMİ:
                // Kullanıcı adının değiştirilmesini engelle (çünkü bu birincil anahtar gibidir)
                // SECURITY MEASURE:
                // Prevent username change (because it's like a primary key)
                txtKullaniciAdi.ReadOnly = true;
            }
        }

        private void FormHesapAyarlari_Load(object sender, EventArgs e)
        {
            // Form açılırken, o anki program dilini uygula
            // Apply the current program language when the form is opened
            UygulaDil();
            // Kullanıcının mevcut verilerini forma yükle
            // Load the user's current data into the form
            VerileriDoldur();
        }

        private void btnHesapTemizle_Click(object sender, EventArgs e)
        {
            // Formu temizleyen/sıfırlayan metodu çağır
            // Call the method that clears/resets the form
            AlanlariTemizle();
        }

        private void btnHesapGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                // --- 1. Güvenlik Validasyonu: MEVCUT ŞİFRE ---
                // Kullanıcı, herhangi bir değişiklik yapmak için mevcut şifresini girmek ZORUNDADIR.
                // --- 1. Security Validation: CURRENT PASSWORD ---  
                // The user MUST enter their current password to make any changes.
                string mevcutSifre = txtMevcutSifre.Text;
                if (string.IsNullOrEmpty(mevcutSifre))
                {
                    XtraMessageBox.Show(Resources.MevcutSifreHatali, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMevcutSifre.Focus();
                    return;
                }

                // Girilen mevcut şifreyi hash'le ve sakladığımızla karşılaştır
                // Hash the entered current password and compare it with the one we have stored

                string mevcutSifreHash = ComputeSha256Hash(mevcutSifre);
                if (mevcutSifreHash != guncellenecekKullanici.SifreHash)
                {
                    // Şifre yanlış
                    // Incorrect password
                    XtraMessageBox.Show(Resources.MevcutSifreHatali, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMevcutSifre.Text = ""; // Yanlış şifreyi temizle  (Clear the incorrect password)
                    txtMevcutSifre.Focus();
                    return;
                }

                // --- Mevcut Şifre DOĞRU, şimdi diğer alanları kontrol et ---
                // --- Current Password is CORRECT, now check the other fields ---

                // 2. Alanları al
                // 2. Retrieve the fields
                string ad = txtAd.Text.Trim();
                string soyad = txtSoyad.Text.Trim();
                string email = txtEmail.Text.Trim();
                string yeniSifre = txtYeniSifre.Text;
                string yeniSifreTekrar = txtYeniSifreTekrar.Text;

                // Ad ve Soyad (DB'de NOT NULL) boş olamaz
                // Name and Surname (NOT NULL in DB) cannot be empty
                if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad))
                {
                    // Sözlükteki "ZorunluAlanlarHata"yı kullanabiliriz
                    // We can use the "ZorunluAlanlarHata" entry from the dictionary
                    XtraMessageBox.Show(Resources.ZorunluAlanlarHata, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // --- 3. Yeni Şifre Validasyonu ---
                // --- 3. New Password Validation ---
                bool sifreDegisecek = false;
                // Eğer "Yeni Şifre" alanlarından herhangi biri doldurulmuşsa, 
                // kullanıcı şifresini değiştirmek istiyor demektir.
                // If any of the "New Password" fields are filled,  
                // it means the user intends to change their password.
                if (!string.IsNullOrEmpty(yeniSifre) || !string.IsNullOrEmpty(yeniSifreTekrar))
                {
                    if (yeniSifre != yeniSifreTekrar)
                    {
                        // Yeni şifreler eşleşmiyor
                        // New passwords do not match
                        XtraMessageBox.Show(Resources.SifreEslesmeHata, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    sifreDegisecek = true;
                }

                // --- 4. Veritabanı İşlemleri ---
                // --- 4. Database Operations ---
                using (var db = new TohumBankasiContext())
                {
                    // 'Find' komutu, Primary Key (KullaniciId) üzerinden kullanıcıyı bulur.
                    // The 'Find' command locates the user by their Primary Key (KullaniciId)
                    var userToUpdate = db.Kullanicilars.Find(guncellenecekKullanici.KullaniciId);

                    if (userToUpdate == null)
                    {
                        XtraMessageBox.Show("Kullanıcı bulunamadı.", Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    // 5. Bilgileri Güncelle
                    // 5. Update the information
                    userToUpdate.Ad = ad;
                    userToUpdate.Soyad = soyad;
                    userToUpdate.Email = string.IsNullOrEmpty(email) ? null : email;

                    // 6. Şifreyi (gerekliyse) Güncelle
                    // 6. Update the password (if necessary)
                    if (sifreDegisecek)
                    {
                        userToUpdate.SifreHash = ComputeSha256Hash(yeniSifre);
                    }

                    // 7. Kaydet
                    // 7. Save changes
                    db.SaveChanges();

                    // 8. Geri Bildirim ve Kapat
                    // Sözlükte "GuncellemeBasarili" anahtarımız vardı
                    // 8. Feedback and Close  
                    // We had the "GuncellemeBasarili" key in the dictionary for this purpose

                    XtraMessageBox.Show(Resources.GuncellemeBasarili, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.GuncellemeBasarili = true; // Form1'e haber ver
                    // Notify Form1
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Resources.GenelHata + " " + ex.Message, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}