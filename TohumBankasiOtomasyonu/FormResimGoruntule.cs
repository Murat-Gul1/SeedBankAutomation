using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Properties; // Sözlük için

namespace TohumBankasiOtomasyonu
{
    public partial class FormResimGoruntule : DevExpress.XtraEditors.XtraForm
    {
        // Parametresiz ctor (Designer için gerekli)
        public FormResimGoruntule()
        {
            InitializeComponent();
        }

        // ASIL KULLANACAĞIMIZ (Resim Alan) OLUŞTURUCU
        public FormResimGoruntule(Image resim) : this()
        {
            // Resmi kutuya yükle
            if (resim != null)
            {
                picBuyukResim.Image = resim;
            }

            // Başlığı ayarla
            this.Text = Resources.FormResimGoruntule_Title;
        }

        // İsteğe bağlı: Resme tıklayınca veya ESC basınca kapansın
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