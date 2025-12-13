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
using TohumBankasiOtomasyonu.Models;
using System.Security.Cryptography;
using System.Text;
namespace TohumBankasiOtomasyonu
{
    public partial class FormKullaniciDuzenle : DevExpress.XtraEditors.XtraForm
    {      // Değişkenler
           // Variables
        private int _gelenKullaniciId = 0;
        private Kullanicilar _duzenlenecekKullanici;
        public FormKullaniciDuzenle()
        {
            InitializeComponent();
        }
        // Constructor (Oluşturucu)
        // Constructor (Builder)
        public FormKullaniciDuzenle(int kullaniciId)
        {
            InitializeComponent();
            _gelenKullaniciId = kullaniciId;
        }

        private void FormKullaniciDuzenle_Load(object sender, EventArgs e)
        {
            UygulaDil();     // Önce dili ve combobox seçeneklerini ayarla (First set language and combobox options)
            VerileriDoldur();// Sonra veritabanındaki veriyi getir ve doğru seçeneği işaretle (Then get data from database and mark correct option)
        }
        // Dil ve İçerik Ayarlama Metodu
        // Language and Content Setting Method
        private void UygulaDil()
        {
            // 1. Başlık ve Etiketler
            // 1. Title and Labels
            this.Text = Resources.FormKullaniciDuzenle_Title;

            lblAd.Text = Resources.labelHesapAd;
            lblSoyad.Text = Resources.labelHesapSoyad;
            lblEmail.Text = Resources.labelHesapEmail;
            lblKullaniciAdi.Text = Resources.labelHesapKullaniciAdi;
            lblKullaniciTipi.Text = Resources.lblKullaniciTipi; // "Yetki / Rol:"

            lblYeniSifre.Text = Resources.labelHesapYeniSifre;
            lblYeniSifreTekrar.Text = Resources.labelHesapYeniSifreTekrar;

            btnHesapGuncelle.Text = Resources.btnKullaniciGuncelle;
            btnHesapTemizle.Text = Resources.btnHesapTemizle_Text;

            // 2. COMBOBOX DOLDURMA (Sizin sorduğunuz kısım)
            // Önce mevcut listeyi temizle
            // 2. FILLING COMBOBOX (The part you asked)
            // First clear existing list
            cmbKullaniciTipi.Properties.Items.Clear();

            // Sözlükten gelen çevirileri ekle
            // Add translations from dictionary
            cmbKullaniciTipi.Properties.Items.Add(Resources.Role_Kullanici); // "Kullanıcı" veya "Standard User" ("User" or "Standard User")
            cmbKullaniciTipi.Properties.Items.Add(Resources.Role_Admin);     // "Admin" veya "Administrator" ("Admin" or "Administrator")
        }
        private void VerileriDoldur()
        {
            using (var db = new TohumBankasiContext())
            {
                _duzenlenecekKullanici = db.Kullanicilars.Find(_gelenKullaniciId);

                if (_duzenlenecekKullanici != null)
                {
                    txtAd.Text = _duzenlenecekKullanici.Ad;
                    txtSoyad.Text = _duzenlenecekKullanici.Soyad;
                    txtEmail.Text = _duzenlenecekKullanici.Email;
                    txtKullaniciAdi.Text = _duzenlenecekKullanici.KullaniciAdi;

                    // Kullanıcı adı değiştirilemez olsun
                    // Username should be unchangeable
                    txtKullaniciAdi.ReadOnly = true;

                    // KULLANICI TİPİNİ SEÇME (Önemli!)
                    // Veritabanında "Admin" veya "Kullanici" yazar (sabit).
                    // Biz bunu arayüzdeki dile çevirip seçtirmeliyiz.
                    // SELECTING USER TYPE (Important!)
                    // It writes "Admin" or "Kullanici" in database (fixed).
                    // We must translate this to interface language and make it selected.

                    if (_duzenlenecekKullanici.KullaniciTipi == "Admin")
                    {
                        cmbKullaniciTipi.SelectedItem = Resources.Role_Admin;
                    }
                    else
                    {
                        cmbKullaniciTipi.SelectedItem = Resources.Role_Kullanici;
                    }
                }
            }
        }
        // Şifreleme Yardımcı Metodu
        // Encryption Helper Method
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnHesapGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Temel Alanları Al
                // 1. Get Basic Fields
                string ad = txtAd.Text.Trim();
                string soyad = txtSoyad.Text.Trim();
                string email = txtEmail.Text.Trim();

                // Şifre alanlarını al
                // Get password fields
                string yeniSifre = txtYeniSifre.Text;
                string yeniSifreTekrar = txtYeniSifreTekrar.Text;

                // 2. Zorunlu Alan Kontrolü (Ad ve Soyad)
                // 2. Mandatory Field Check (Name and Surname)
                if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad))
                {
                    XtraMessageBox.Show(Resources.ZorunluAlanlarHata, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Şifre Değişikliği Kontrolü
                // 3. Password Change Check
                bool sifreDegistirilecek = false;

                // Eğer admin şifre kutularına bir şey yazdıysa, şifreyi değiştirmek istiyordur.
                // If admin wrote something in password boxes, they want to change password.
                if (!string.IsNullOrEmpty(yeniSifre) || !string.IsNullOrEmpty(yeniSifreTekrar))
                {
                    if (yeniSifre != yeniSifreTekrar)
                    {
                        XtraMessageBox.Show(Resources.SifreEslesmeHata, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    sifreDegistirilecek = true;
                }

                // 4. Rol (Kullanıcı Tipi) Belirleme
                // ComboBox'taki seçili metin "Administrator" veya "Yönetici" olabilir.
                // Bunu veritabanı diline ("Admin" / "Kullanici") çevirmeliyiz.
                // 4. Determining Role (User Type)
                // Selected text in ComboBox can be "Administrator" or "Manager".
                // We must translate this to database language ("Admin" / "Kullanici").

                string secilenRolMetni = cmbKullaniciTipi.SelectedItem.ToString();
                string veritabaniRolu = "Kullanici"; // Varsayılan (Default)

                // Eğer seçilen metin, sözlükteki "Admin" çevirisine eşitse:
                // If selected text equals "Admin" translation in dictionary:
                if (secilenRolMetni == Resources.Role_Admin)
                {
                    veritabaniRolu = "Admin";
                }
                // Değilse zaten "Kullanici" kalır.
                // Otherwise it stays "Kullanici".

                // 5. Veritabanı Güncelleme
                // 5. Database Update
                using (var db = new TohumBankasiContext())
                {
                    var kullanici = db.Kullanicilars.Find(_gelenKullaniciId);

                    if (kullanici != null)
                    {
                        // Bilgileri güncelle
                        // Update information
                        kullanici.Ad = ad;
                        kullanici.Soyad = soyad;
                        kullanici.Email = string.IsNullOrEmpty(email) ? null : email;
                        kullanici.KullaniciTipi = veritabaniRolu; // Rolü güncelle (Update role)

                        // Şifreyi güncelle (Sadece istenmişse)
                        // Update password (Only if requested)
                        if (sifreDegistirilecek)
                        {
                            kullanici.SifreHash = ComputeSha256Hash(yeniSifre);
                        }

                        db.SaveChanges();

                        XtraMessageBox.Show(Resources.GuncellemeBasarili, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Resources.GenelHata + " " + ex.Message, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHesapTemizle_Click(object sender, EventArgs e)
        {
            // Formu veritabanındaki orijinal verilerle tekrar doldur
            // Refill form with original data from database
            VerileriDoldur();
            
            // Şifre alanlarını temizle
            // Clear password fields
            txtYeniSifre.Text = "";
            txtYeniSifreTekrar.Text = "";
        }
    }
}