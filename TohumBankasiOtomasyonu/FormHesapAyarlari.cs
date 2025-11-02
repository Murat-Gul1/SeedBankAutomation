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
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class FormHesapAyarlari : DevExpress.XtraEditors.XtraForm
    {
        // Form1'den gelen kullanıcıyı bu değişkende saklayacağız
        // We'll store the user coming from Form1 in this variable
        private Kullanicilar guncellenecekKullanici;
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
            linkHesapSil.Text = Resources.linkHesapSil;
        }
        private void VerileriDoldur()
        {
            // Sakladığımız kullanıcı bilgisi (guncellenecekKullanici) boş değilse
            if (guncellenecekKullanici != null)
            {
                // Metin kutularını doldur
                txtAd.Text = guncellenecekKullanici.Ad;
                txtSoyad.Text = guncellenecekKullanici.Soyad;
                txtEmail.Text = guncellenecekKullanici.Email;
                txtKullaniciAdi.Text = guncellenecekKullanici.KullaniciAdi;

                // GÜVENLİK ÖNLEMİ:
                // Kullanıcı adının değiştirilmesini engelle (çünkü bu birincil anahtar gibidir)
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
    }
}