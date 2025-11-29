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
    public partial class FormSiparisGecmisi : DevExpress.XtraEditors.XtraForm
    {
        private Kullanicilar _aktifKullanici;

        public FormSiparisGecmisi()
        {
            InitializeComponent();
        }

        public FormSiparisGecmisi(Kullanicilar kullanici) : this()
        {
            _aktifKullanici = kullanici;
        }

        // Resim Yükleme Yardımcısı
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
                        Image img = Image.FromStream(stream);
                        return new Bitmap(img, new Size(50, 50)); // Küçük resim
                    }
                }
            }
            catch { }
            return null;
        }

        private void UygulaDil()
        {
            this.Text = Resources.FormSiparisGecmisi_Title;

            var view = gridGecmis.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                // Başlıkları Ayarla
                if (view.Columns["Resim"] != null) view.Columns["Resim"].Caption = Resources.colDetayResim;
                if (view.Columns["UrunAdi"] != null) view.Columns["UrunAdi"].Caption = Resources.colSepetUrunAdi;
                if (view.Columns["BilimselAd"] != null) view.Columns["BilimselAd"].Caption = Resources.colDetayBilimselAd;
                if (view.Columns["Tarih"] != null) view.Columns["Tarih"].Caption = Resources.colGecmisTarih;
                if (view.Columns["Miktar"] != null) view.Columns["Miktar"].Caption = Resources.colSepetAdet;
                if (view.Columns["Tutar"] != null) view.Columns["Tutar"].Caption = Resources.colGecmisTutar;
                if (view.Columns["MakbuzNo"] != null) view.Columns["MakbuzNo"].Caption = Resources.colGecmisMakbuz;
            }
        }

        private void VerileriYukle()
        {
            if (_aktifKullanici == null) return;

            using (var db = new TohumBankasiContext())
            {
                string aktifDil = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                // Kullanıcının aldığı TÜM ürünleri (Detayları) çekiyoruz
                var gecmisListesi = (from d in db.SatisDetaylaris
                                     join s in db.Satislars on d.SatisId equals s.SatisId
                                     where s.KullaniciId == _aktifKullanici.KullaniciId
                                     orderby s.SatisTarihi descending
                                     select new
                                     {
                                         d.BitkiId,
                                         d.Miktar,
                                         BirimFiyat = d.SatisAnindakiFiyat, // O anki fiyat
                                         s.SatisTarihi,
                                         s.MakbuzNo,
                                         // Çevirileri ve Resimleri alt sorgu ile alıyoruz
                                         Ceviriler = db.BitkiCevirileris.Where(c => c.BitkiId == d.BitkiId).ToList(),
                                         Gorsel = db.BitkiGorselleris.FirstOrDefault(g => g.BitkiId == d.BitkiId && g.AnaGorsel == 1)
                                     }).ToList();

                // Bellekte resim ve dil işlemleri
                var sonucListesi = gecmisListesi.Select(item => {
                    var uygunCeviri = item.Ceviriler.FirstOrDefault(c => c.DilKodu == aktifDil) ??
                                      item.Ceviriler.FirstOrDefault(c => c.DilKodu == "tr");

                    string urunAdi = (uygunCeviri != null) ? uygunCeviri.BitkiAdi : "Bilinmiyor";
                    string bilimselAd = (uygunCeviri != null) ? uygunCeviri.BilimselAd : "";
                    string resimYolu = (item.Gorsel != null) ? item.Gorsel.DosyaYolu : "";

                    return new
                    {
                        Resim = ResmiYukle(resimYolu), // RESİM SÜTUNU
                        UrunAdi = urunAdi,             // BİTKİ ADI
                        BilimselAd = bilimselAd,       // BİLİMSEL AD
                        Miktar = item.Miktar,
                        Tutar = item.Miktar * item.BirimFiyat,
                        Tarih = item.SatisTarihi,
                        MakbuzNo = item.MakbuzNo
                    };
                }).ToList();

                gridGecmis.DataSource = sonucListesi;
            }

            GridAyarlariniYap();
            UygulaDil();
        }

        private void GridAyarlariniYap()
        {
            var view = gridGecmis.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                // Resim Sütunu Ayarı
                if (view.Columns["Resim"] != null) view.Columns["Resim"].Width = 60;
                view.RowHeight = 60; // Satırları genişlet

                // Para formatı
                if (view.Columns["Tutar"] != null)
                {
                    view.Columns["Tutar"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    view.Columns["Tutar"].DisplayFormat.FormatString = "c2";
                }

                view.OptionsBehavior.Editable = false;
            }
        }

        private void FormSiparisGecmisi_Load(object sender, EventArgs e)
        {
            VerileriYukle();
        }
    }
}