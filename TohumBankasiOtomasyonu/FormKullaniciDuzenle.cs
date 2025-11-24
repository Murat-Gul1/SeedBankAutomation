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
        private int _gelenKullaniciId = 0;
        private Kullanicilar _duzenlenecekKullanici;
        public FormKullaniciDuzenle()
        {
            InitializeComponent();
        }
        // Constructor (Oluşturucu)
        public FormKullaniciDuzenle(int kullaniciId)
        {
            InitializeComponent();
            _gelenKullaniciId = kullaniciId;
        }

        private void FormKullaniciDuzenle_Load(object sender, EventArgs e)
        {
            UygulaDil();     // Önce dili ve combobox seçeneklerini ayarla
            VerileriDoldur();// Sonra veritabanındaki veriyi getir ve doğru seçeneği işaretle
        }
        // Dil ve İçerik Ayarlama Metodu
        private void UygulaDil()
        {
            // 1. Başlık ve Etiketler
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
            cmbKullaniciTipi.Properties.Items.Clear();

            // Sözlükten gelen çevirileri ekle
            cmbKullaniciTipi.Properties.Items.Add(Resources.Role_Kullanici); // "Kullanıcı" veya "Standard User"
            cmbKullaniciTipi.Properties.Items.Add(Resources.Role_Admin);     // "Admin" veya "Administrator"
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
                    txtKullaniciAdi.ReadOnly = true;

                    // KULLANICI TİPİNİ SEÇME (Önemli!)
                    // Veritabanında "Admin" veya "Kullanici" yazar (sabit).
                    // Biz bunu arayüzdeki dile çevirip seçtirmeliyiz.

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
                string ad = txtAd.Text.Trim();
                string soyad = txtSoyad.Text.Trim();
                string email = txtEmail.Text.Trim();

                // Şifre alanlarını al
                string yeniSifre = txtYeniSifre.Text;
                string yeniSifreTekrar = txtYeniSifreTekrar.Text;

                // 2. Zorunlu Alan Kontrolü (Ad ve Soyad)
                if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad))
                {
                    XtraMessageBox.Show(Resources.ZorunluAlanlarHata, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Şifre Değişikliği Kontrolü
                bool sifreDegistirilecek = false;

                // Eğer admin şifre kutularına bir şey yazdıysa, şifreyi değiştirmek istiyordur.
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

                string secilenRolMetni = cmbKullaniciTipi.SelectedItem.ToString();
                string veritabaniRolu = "Kullanici"; // Varsayılan

                // Eğer seçilen metin, sözlükteki "Admin" çevirisine eşitse:
                if (secilenRolMetni == Resources.Role_Admin)
                {
                    veritabaniRolu = "Admin";
                }
                // Değilse zaten "Kullanici" kalır.

                // 5. Veritabanı Güncelleme
                using (var db = new TohumBankasiContext())
                {
                    var kullanici = db.Kullanicilars.Find(_gelenKullaniciId);

                    if (kullanici != null)
                    {
                        // Bilgileri güncelle
                        kullanici.Ad = ad;
                        kullanici.Soyad = soyad;
                        kullanici.Email = string.IsNullOrEmpty(email) ? null : email;
                        kullanici.KullaniciTipi = veritabaniRolu; // Rolü güncelle

                        // Şifreyi güncelle (Sadece istenmişse)
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
            VerileriDoldur();

            // Şifre alanlarını temizle
            txtYeniSifre.Text = "";
            txtYeniSifreTekrar.Text = "";
        }
    }
}