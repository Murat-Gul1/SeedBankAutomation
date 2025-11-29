using DevExpress.XtraEditors;
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
    public partial class FormSatisDetay : DevExpress.XtraEditors.XtraForm
    {
        private int _gelenSatisId = 0;

        public FormSatisDetay(int satisId) : this()
        {
            _gelenSatisId = satisId;
        }

        public FormSatisDetay() { InitializeComponent(); }

        // --- RESİM YÜKLEME YARDIMCISI ---
        private Image ResmiYukle(string yol)
        {
            try
            {
                if (string.IsNullOrEmpty(yol)) return null;

                string tamYol = Path.Combine(Application.StartupPath, yol);
                if (File.Exists(tamYol))
                {
                    using (var stream = new FileStream(tamYol, FileMode.Open, FileAccess.Read))
                    {
                        // Resmi yükle ve küçük bir kopyasını (Thumbnail) döndür
                        // (Performans için resmi küçültmek iyidir)
                        Image img = Image.FromStream(stream);
                        return new Bitmap(img, new Size(50, 50));
                    }
                }
            }
            catch { }
            return null;
        }

        private void VerileriYukle()
        {
            if (_gelenSatisId == 0) return;

            using (var db = new TohumBankasiContext())
            {
                // 1. Başlık Bilgilerini Çek
                var satis = db.Satislars.Find(_gelenSatisId);
                if (satis != null)
                {
                    txtMakbuzNo.Text = satis.MakbuzNo;
                    txtTarih.Text = satis.SatisTarihi;
                    txtToplamTutar.Text = satis.ToplamTutar.ToString("C2"); // Para formatı
                }

                // 2. Detayları Çek (GÜNCELLENMİŞ KISIM)
                string aktifDil = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                // a. Önce ham veriyi çek (Resim yolu dahil)
                var hamListe = (from d in db.SatisDetaylaris
                                where d.SatisId == _gelenSatisId
                                select new
                                {
                                    d.BitkiId,
                                    d.Miktar,
                                    d.SatisAnindakiFiyat,
                                    // Çevirileri ve Resimleri al
                                    Ceviriler = db.BitkiCevirileris.Where(c => c.BitkiId == d.BitkiId).ToList(),
                                    Gorsel = db.BitkiGorselleris.FirstOrDefault(g => g.BitkiId == d.BitkiId && g.AnaGorsel == 1)
                                }).ToList();

                // b. Bellekte işle (Dil seçimi ve Resim Yükleme)
                var sonucListesi = hamListe.Select(item => {
                    var uygunCeviri = item.Ceviriler.FirstOrDefault(c => c.DilKodu == aktifDil) ??
                                      item.Ceviriler.FirstOrDefault(c => c.DilKodu == "tr");

                    string urunAdi = (uygunCeviri != null) ? uygunCeviri.BitkiAdi : "Unknown";
                    string bilimselAd = (uygunCeviri != null) ? uygunCeviri.BilimselAd : "";
                    string resimYolu = (item.Gorsel != null) ? item.Gorsel.DosyaYolu : "";

                    return new
                    {
                        Resim = ResmiYukle(resimYolu), // Resmi Image objesi olarak döndür
                        UrunAdi = urunAdi,
                        BilimselAd = bilimselAd, // Yeni Sütun
                        BirimFiyat = item.SatisAnindakiFiyat,
                        Miktar = item.Miktar,
                        AraToplam = item.Miktar * item.SatisAnindakiFiyat
                    };
                }).ToList();

                gridDetaylar.DataSource = sonucListesi;

                // 3. Ayarları Yap (Başlıklar ve Resim Sütunu)
                GridAyarlariniYap();
            }
        }

        private void GridAyarlariniYap()
        {
            var view = gridDetaylar.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                // Başlıkları Ayarla (Sözlükten)
                if (view.Columns["Resim"] != null) view.Columns["Resim"].Caption = Resources.colDetayResim;
                if (view.Columns["UrunAdi"] != null) view.Columns["UrunAdi"].Caption = Resources.colUrunAdi;
                if (view.Columns["BilimselAd"] != null) view.Columns["BilimselAd"].Caption = Resources.colDetayBilimselAd;
                if (view.Columns["BirimFiyat"] != null) view.Columns["BirimFiyat"].Caption = Resources.colBirimFiyat;
                if (view.Columns["Miktar"] != null) view.Columns["Miktar"].Caption = Resources.colMiktar;
                if (view.Columns["AraToplam"] != null) view.Columns["AraToplam"].Caption = Resources.colSatirToplam;

                // Resim Sütunu Ayarı (Görünmesi için önemli)
                if (view.Columns["Resim"] != null)
                {
                    view.Columns["Resim"].Width = 60; // Biraz genişlik ver
                }
                view.RowHeight = 60; // Satır yüksekliğini artır ki resim sığsın

                // Para Formatı
                if (view.Columns["BirimFiyat"] != null)
                {
                    view.Columns["BirimFiyat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    view.Columns["BirimFiyat"].DisplayFormat.FormatString = "c2";
                }
                if (view.Columns["AraToplam"] != null)
                {
                    view.Columns["AraToplam"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    view.Columns["AraToplam"].DisplayFormat.FormatString = "c2";
                }

                view.OptionsBehavior.Editable = false; // Düzenlenemesin
            }
        }

        private void FormSatisDetay_Load(object sender, EventArgs e)
        {
            // UygulaDil(); // Başlıkları zaten GridAyarlariniYap içinde hallettik
            this.Text = Resources.FormSatisDetay_Title;
            layoutControl1.GetItemByControl(txtMakbuzNo).Text = Resources.lblDetayMakbuzNo;
            layoutControl1.GetItemByControl(txtTarih).Text = Resources.lblDetayTarih;
            layoutControl1.GetItemByControl(txtToplamTutar).Text = Resources.lblDetayToplam;
            btnKapat.Text = Resources.btnKapat;

            VerileriYukle();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}