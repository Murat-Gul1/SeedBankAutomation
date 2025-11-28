using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class UcBlockchainYonetimi : DevExpress.XtraEditors.XtraUserControl
    {
        public UcBlockchainYonetimi()
        {
            InitializeComponent();
        }

        // Sütun başlıklarını dile göre ayarla
        private void SutunBasliklariniAyarla()
        {
            var view = gridBlockchain.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                // Veritabanındaki sütun isimleri: BlokID, ZamanDamgasi, OncekiHash, Hash, Veri
                if (view.Columns["BlokID"] != null) view.Columns["BlokID"].Caption = Resources.colBlokID;
                if (view.Columns["ZamanDamgasi"] != null) view.Columns["ZamanDamgasi"].Caption = Resources.colZamanDamgasi;
                if (view.Columns["OncekiHash"] != null) view.Columns["OncekiHash"].Caption = Resources.colOncekiHash;
                if (view.Columns["Hash"] != null) view.Columns["Hash"].Caption = Resources.colMevcutHash;
                if (view.Columns["Veri"] != null) view.Columns["Veri"].Caption = Resources.colVeri;
            }
        }

        public void BloklariListele()
        {
            using (var db = new TohumBankasiContext())
            {
                // Sahte_Blokzincir tablosunu çek
                // (Scaffold yaparken 'SahteBlokzincirs' olarak gelmiş olabilir, kontrol edin)
                var blokListesi = db.SahteBlokzincirs.ToList();

                gridBlockchain.DataSource = blokListesi;

                SutunBasliklariniAyarla();
            }
        }

        private void UcBlockchainYonetimi_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                BloklariListele();
            }
        }
    }
}