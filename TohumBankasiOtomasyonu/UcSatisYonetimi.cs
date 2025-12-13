using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class UcSatisYonetimi : DevExpress.XtraEditors.XtraUserControl
    {
        public UcSatisYonetimi()
        {
            InitializeComponent();
        }
        private void SutunBasliklariniAyarla()
        {
            var view = gridSatislar.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                // Anonim tipte verdiğimiz isimler: ID, MakbuzNo, Musteri, Tarih, Tutar
                // Names we gave in anonymous type: ID, MakbuzNo, Musteri, Tarih, Tutar
                if (view.Columns["ID"] != null) view.Columns["ID"].Caption = Resources.colSatisId;
                if (view.Columns["MakbuzNo"] != null) view.Columns["MakbuzNo"].Caption = Resources.colSatisMakbuz;
                if (view.Columns["Musteri"] != null) view.Columns["Musteri"].Caption = Resources.colSatisMusteri;
                if (view.Columns["Tarih"] != null) view.Columns["Tarih"].Caption = Resources.colSatisTarih;
                if (view.Columns["Tutar"] != null) view.Columns["Tutar"].Caption = Resources.colSatisTutar;
            }
        }
        // Dil ve Buton Metinleri
        // Language and Button Texts
        private void UygulaDil()
        {
            btnSatisDetay.ToolTip = Resources.btnSatisDetay_Text;

        }

        public void SatislariListele()
        {
            using (var db = new TohumBankasiContext())
            {
                var satisListesi = from s in db.Satislars
                                   join k in db.Kullanicilars on s.KullaniciId equals k.KullaniciId
                                   select new
                                   {
                                       ID = s.SatisId,
                                       MakbuzNo = s.MakbuzNo,
                                       Musteri = k.Ad + " " + k.Soyad,
                                       Tarih = s.SatisTarihi,
                                       Tutar = s.ToplamTutar + " ₺"
                                   };

                var sonucListesi = satisListesi.ToList(); // Listeyi önce çekelim (Let's fetch the list first)

                gridSatislar.DataSource = sonucListesi;
                SutunBasliklariniAyarla();
            }
        }

        private void UcSatisYonetimi_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                UygulaDil();
                SatislariListele();
            }
        }

        // Buton tıklama olayını (Detayları Gör) bir sonraki adımda kodlayacağız.
        // Çünkü önce detayları gösterecek bir forma ihtiyacımız var.
        // We will code the button click event (View Details) in the next step.
        // Because we first need a form to show the details.
        private void btnSatisDetay_Click(object sender, EventArgs e)
        {
            // 1. Seçili satırın ID'sini al (Grid'deki kolon adımız "ID" idi)
            // (Sizin GridView isminiz gridView1 ise bu kod çalışır)
            // 1. Get ID of selected row (Our column name in Grid was "ID")
            // (This code works if your GridView name is gridView1)
            var seciliIdObj = gridView1.GetFocusedRowCellValue("ID");

            if (seciliIdObj == null)
            {
                // Eğer satır seçilmediyse uyar (HataSatirSecilmedi sözlüğümüzde vardı)
                // Warn if row is not selected (HataSatirSecilmedi was in our dictionary)
                XtraMessageBox.Show(Resources.HataSatirSecilmedi, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int satisId = Convert.ToInt32(seciliIdObj);

            // 2. Formu ID ile oluştur ve aç
            // 2. Create and open form with ID
            FormSatisDetay frmDetay = new FormSatisDetay(satisId);
            frmDetay.ShowDialog();
        }

        private void btnSatisDetay_Click_1(object sender, EventArgs e)
        {

        }
    }
}