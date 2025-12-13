using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Properties; // Sözlük için (for dictionary)

namespace TohumBankasiOtomasyonu
{
    public partial class FormResimGoruntule : DevExpress.XtraEditors.XtraForm
    {
        // Parametresiz ctor (Designer için gerekli)
        // Parameterless constructor (for Designer)
        public FormResimGoruntule()
        {
            InitializeComponent();
        }

        // ASIL KULLANACAĞIMIZ (Resim Alan) OLUŞTURUCU
        // PRIMARY CONSTRUCTOR (for Image)
        public FormResimGoruntule(Image resim) : this()
        {
            // Resmi kutuya yükle
            // Load Image into PictureBox
            if (resim != null)
            {
                picBuyukResim.Image = resim;
            }

            // Başlığı ayarla
            // Set header
            this.Text = Resources.FormResimGoruntule_Title;
        }

        // İsteğe bağlı: Resme tıklayınca veya ESC basınca kapansın
        // Optional: Close when ESC is pressed or when the image is clicked
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}