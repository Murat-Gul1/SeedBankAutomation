using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit; // PDF Çevirici için ŞART (REQUIRED for PDF Converter)
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class FormRaporListesi : DevExpress.XtraEditors.XtraForm
    {
        private int _filtreBitkiId = 0;

        public FormRaporListesi(int bitkiId) : this()
        {
            _filtreBitkiId = bitkiId;
        }

        public FormRaporListesi()
        {
            InitializeComponent();
        }

        private void FormRaporListesi_Load(object sender, EventArgs e)
        {
            this.Text = Resources.FormRaporListesi_Title;
            RaporlariListele();
            GridAyarlariniYap();
        }

        private void RaporlariListele()
        {
            using (var db = new TohumBankasiContext())
            {
                // Sadece bu bitkiye ait raporları çek (Tarihe göre yeniden eskiye)
                // Fetch only reports originating from this plant (Sorted by date, newest to oldest)
                var raporlar = (from r in db.BitkiRaporlaris
                                join kb in db.KullaniciBitkileris on r.KullaniciBitkiId equals kb.Id
                                where kb.Id == _filtreBitkiId
                                orderby r.RaporTarihi descending
                                select new
                                {
                                    r.RaporId,
                                    kb.BitkiAdi,
                                    kb.GorselYolu, // Resim için (For image)
                                    r.RaporTarihi,
                                    r.RaporMetni // PDF yapmak için buna ihtiyacımız var (Gizli kalacak) (We need this to make PDF (Will correspond to hidden))
                                }).ToList();

                // Resim yollarını Image nesnesine çevir
                // Convert image paths to Image object
                var gridVerisi = raporlar.Select(x => new
                {
                    x.RaporId,
                    Resim = ResmiYukle(x.GorselYolu),
                    x.BitkiAdi,
                    x.RaporTarihi,
                    x.RaporMetni,
                    Indir = "PDF" // Buton gibi görünecek sütun (Column that will look like a button)
                }).ToList();

                gridRaporlar.DataSource = gridVerisi;
            }
        }

        private Image ResmiYukle(string yol)
        {
            try
            {
                string tamYol = Path.Combine(Application.StartupPath, yol);
                if (File.Exists(tamYol))
                {
                    using (var stream = new FileStream(tamYol, FileMode.Open, FileAccess.Read))
                    {
                        return new Bitmap(Image.FromStream(stream), new Size(50, 50));
                    }
                }
            }
            catch { }
            return null;
        }

        private void GridAyarlariniYap()
        {
            var view = gridRaporlar.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                // Gereksiz sütunları gizle
                // Hide unnecessary columns
                if (view.Columns["RaporId"] != null) view.Columns["RaporId"].Visible = false;
                if (view.Columns["RaporMetni"] != null) view.Columns["RaporMetni"].Visible = false;

                // Başlıkları ayarla
                // Set headers
                if (view.Columns["Resim"] != null) { view.Columns["Resim"].Caption = Resources.colSepetResim; view.Columns["Resim"].Width = 60; }
                if (view.Columns["BitkiAdi"] != null) view.Columns["BitkiAdi"].Caption = Resources.colSepetUrunAdi;
                if (view.Columns["RaporTarihi"] != null) view.Columns["RaporTarihi"].Caption = Resources.colRaporTarih;
                if (view.Columns["Indir"] != null) view.Columns["Indir"].Caption = Resources.colRaporIndir;

                view.RowHeight = 60;
                view.OptionsBehavior.Editable = false;

                // Tıklama olayını bağla (PDF İndirme İçin)
                // Bind click event (For PDF Download)
                view.RowCellClick += View_RowCellClick;
            }
        }

        // --- PDF İNDİRME VE OLUŞTURMA ---
        // --- PDF DOWNLOADING AND CREATION ---
        private void View_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            // Sadece "Indir" sütununa tıklanırsa çalışsın
            // Only run if "Indir" (Download) column is clicked
            if (e.Column.FieldName == "Indir")
            {
                var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

                // Gizli olan RaporMetni'ni al
                // Get the hidden RaporMetni (Report Text)
                string raporIcerigi = view.GetRowCellValue(e.RowHandle, "RaporMetni").ToString();
                string bitkiAdi = view.GetRowCellValue(e.RowHandle, "BitkiAdi").ToString();
                string tarih = view.GetRowCellValue(e.RowHandle, "RaporTarihi").ToString();

                // Kaydetme Penceresi Aç
                // Open Save Dialog
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    // Dosya adı önerisi: BitkiAdi_Tarih.pdf
                    // Filename suggestion: PlantName_Date.pdf
                    sfd.FileName = $"{bitkiAdi}_Rapor_{DateTime.Now.Ticks}.pdf";
                    sfd.Filter = "PDF Dosyası|*.pdf";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        PdfOlusturVeKaydet(raporIcerigi, sfd.FileName, bitkiAdi, tarih);
                        XtraMessageBox.Show(Resources.MsgRaporIndirildi, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void PdfOlusturVeKaydet(string icerik, string yol, string baslik, string tarih)
        {
            // DevExpress'in görünmez Word işlemcisini kullanıyoruz
            // We are using DevExpress's invisible Word processor
            using (RichEditDocumentServer server = new RichEditDocumentServer())
            {
                server.Document.AppendText($"BITKI GELISIM RAPORU\n");
                server.Document.AppendText($"Bitki: {baslik}\n");
                server.Document.AppendText($"Tarih: {tarih}\n\n");
                server.Document.AppendText("--------------------------------------------------\n\n");
                server.Document.AppendText(icerik);

                // PDF olarak dışa aktar
                // Export as PDF
                using (FileStream fs = new FileStream(yol, FileMode.Create))
                {
                    server.ExportToPdf(fs);
                }
            }
        }
    }
}