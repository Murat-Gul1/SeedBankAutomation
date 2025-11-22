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

namespace TohumBankasiOtomasyonu
{
    public partial class UcBitkiYonetimi : DevExpress.XtraEditors.XtraUserControl
    {
        public UcBitkiYonetimi()
        {
            InitializeComponent();
        }

        private void btnBitkiEkle_Click(object sender, EventArgs e)
        {
            // 1. İşlem formunu oluştur (Parametre vermiyoruz = Yeni Ekleme Modu)
            // 1. Create the operation form (We do not pass parameters = New Addition Mode)
            FormBitkiIslemleri frmIslem = new FormBitkiIslemleri();

            // 2. Formu Dialog olarak aç
            // (Dialog olarak açıyoruz ki, işlem bitene kadar arkadaki panele dokunulamasın)
            // 2. Open the Form as a Dialog  
            // (We open it as a Dialog so that the background panel cannot be touched until the process is finished)
            frmIslem.ShowDialog();

        }
    }
}
