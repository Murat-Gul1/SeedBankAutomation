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
namespace TohumBankasiOtomasyonu
{
    public partial class FormAdminPaneli : DevExpress.XtraEditors.XtraForm
    {
        public FormAdminPaneli()
        {
            InitializeComponent();
        }

        private void UygulaDil()
        {
            // 1. Pencere Başlığını ayarla
            // 1. Set the Window Title

            this.Text = Resources.FormAdminPaneli_Title;

            // 2. Butonların İpuçlarını (ToolTip) ayarla
            // (Butonların Text'i boş olacak, sadece üzerine gelince bu yazılar çıkacak)
            // 2. Set the Buttons' ToolTips  
            // (The Buttons' Text will be empty, only these texts will appear when hovered)
            btnAdminBitkiler.ToolTip = Resources.btnAdminBitkiler_ToolTip;
            btnAdminKullanicilar.ToolTip = Resources.btnAdminKullanicilar_ToolTip;
            btnAdminSatislar.ToolTip = Resources.btnAdminSatislar_ToolTip;
            btnAdminBlockchain.ToolTip = Resources.btnAdminBlockchain_ToolTip;
        }

        private void FormAdminPaneli_Load(object sender, EventArgs e)
        {
            // Form açılırken dil ayarlarını uygula
            // Apply language settings when the Form opens
            UygulaDil();
        }

        private void btnAdminBitkiler_Click(object sender, EventArgs e)
        {
            // Tell the NavigationFrame to display the "pageBitkiler" page
            // NavigationFrame'e "pageBitkiler" sayfasını göstermesini söyle
            navFrameAdmin.SelectedPage = pageBitkiler;
        }

        private void btnAdminKullanicilar_Click(object sender, EventArgs e)
        {
            // Kullanıcılar sayfasını göster
            navFrameAdmin.SelectedPage = pageKullanicilar;
        }
    }
}