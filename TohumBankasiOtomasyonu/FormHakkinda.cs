using DevExpress.XtraEditors;
using System;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class FormHakkinda : DevExpress.XtraEditors.XtraForm
    {
        public FormHakkinda()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Size = new System.Drawing.Size(400, 350);
        }

        private void FormHakkinda_Load(object sender, EventArgs e)
        {
            // Metinleri Sözlükten Çek
            this.Text = Resources.FormHakkinda_Title;
            lblProjeAdi.Text = Resources.lblProjeAdi;
            lblVersiyon.Text = Resources.lblVersiyon;
            lblGelistirici.Text = Resources.lblGelistirici;
            lblAciklama.Text = Resources.lblAciklama;
            lblTelif.Text = Resources.lblTelif;

            // Logoyu proje kaynaklarından (Resources) yükleyebilirsiniz
            // veya tasarımcıda sabit bir resim koyduysanız kodla ellemenize gerek yok.
        }
    }
}