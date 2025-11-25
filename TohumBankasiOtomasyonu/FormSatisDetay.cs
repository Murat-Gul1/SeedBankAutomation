using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Globalization;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class FormSatisDetay : DevExpress.XtraEditors.XtraForm
    {
        private int _gelenSatisId = 0;

        // Parametresiz oluşturucu (Hata vermemesi için)
        public FormSatisDetay()
        {
            InitializeComponent();
        }
        private void SutunBasliklariniAyarla()
        {
            var view = gridDetaylar.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                // Anonim tipte verdiğimiz isimler: UrunAdi, BirimFiyat, Miktar, AraToplam

                if (view.Columns["UrunAdi"] != null)
                    view.Columns["UrunAdi"].Caption = Resources.colUrunAdi;

                if (view.Columns["BirimFiyat"] != null)
                    view.Columns["BirimFiyat"].Caption = Resources.colBirimFiyat;

                if (view.Columns["Miktar"] != null)
                    view.Columns["Miktar"].Caption = Resources.colMiktar;

                if (view.Columns["AraToplam"] != null)
                    view.Columns["AraToplam"].Caption = Resources.colSatirToplam;
            }
        }

        // ASIL KULLANACAĞIMIZ OLUŞTURUCU
        public FormSatisDetay(int satisId) : this()
        {
            _gelenSatisId = satisId;
        }

        private void UygulaDil()
        {
            this.Text = Resources.FormSatisDetay_Title;

            // LayoutControl içindeki etiketler
            layoutControl1.GetItemByControl(txtMakbuzNo).Text = Resources.lblDetayMakbuzNo;
            layoutControl1.GetItemByControl(txtTarih).Text = Resources.lblDetayTarih;
            layoutControl1.GetItemByControl(txtToplamTutar).Text = Resources.lblDetayToplam;

            btnKapat.Text = Resources.btnKapat;
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
                    txtToplamTutar.Text = satis.ToplamTutar.ToString() + " ₺";
                }

                // 2. Detayları Çek (GÜNCELLENMİŞ KISIM)
                string aktifDil = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                // Tüm detayları ve ilişkili tüm çevirileri çek (Bellekte işlemek için)
                var hamListe = (from d in db.SatisDetaylaris
                                where d.SatisId == _gelenSatisId
                                select new
                                {
                                    Detay = d,
                                    // Bu ürünün tüm çevirilerini getir
                                    Ceviriler = db.BitkiCevirileris.Where(c => c.BitkiId == d.BitkiId).ToList()
                                }).ToList();

                // Bellekte dil seçimi yap
                var sonucListesi = hamListe.Select(item => {
                    // Aktif dildeki ismi bul
                    var uygunCeviri = item.Ceviriler.FirstOrDefault(c => c.DilKodu == aktifDil);

                    // Yoksa Türkçe ismi bul (Fallback)
                    if (uygunCeviri == null)
                        uygunCeviri = item.Ceviriler.FirstOrDefault(c => c.DilKodu == "tr");

                    // Hiçbiri yoksa (olmamalı ama güvenlik için)
                    string urunAdi = (uygunCeviri != null) ? uygunCeviri.BitkiAdi : "Unknown Plant";

                    return new
                    {
                        UrunAdi = urunAdi,
                        BirimFiyat = item.Detay.SatisAnindakiFiyat,
                        Miktar = item.Detay.Miktar,
                        AraToplam = item.Detay.Miktar * item.Detay.SatisAnindakiFiyat
                    };
                }).ToList();

                gridDetaylar.DataSource = sonucListesi;

                // 3. Sütun başlıklarını güncelle
                SutunBasliklariniAyarla();
            }
        }
        private void FormSatisDetay_Load(object sender, EventArgs e)
        {
            UygulaDil();
            VerileriYukle();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}