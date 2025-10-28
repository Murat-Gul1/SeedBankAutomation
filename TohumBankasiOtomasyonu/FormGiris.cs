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
namespace TohumBankasiOtomasyonu
{
    public partial class FormGiris : DevExpress.XtraEditors.XtraForm
    {
        // Form1'e "Kayıt Ol"a basıldığını söylemek için bir bayrak
        // A flag to notify Form1 that the "Register" button has been clicked
        public bool KayitOlmakIstiyor { get; private set; } = false;
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
    }
}