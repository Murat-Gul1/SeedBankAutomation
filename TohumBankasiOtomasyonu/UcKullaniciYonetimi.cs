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
using TohumBankasiOtomasyonu.Models;
namespace TohumBankasiOtomasyonu
{
    public partial class UcKullaniciYonetimi : DevExpress.XtraEditors.XtraUserControl
    {
        public UcKullaniciYonetimi()
        {
            InitializeComponent();
        }
        private void SutunBasliklariniAyarla()
        {
            var view = gridKullanicilar.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                // Anonim tipte verdiğimiz isimler: ID, KullaniciAdi, Ad, Soyad, Email, Yetki
                if (view.Columns["ID"] != null) view.Columns["ID"].Caption = Resources.colKullaniciID;
                if (view.Columns["KullaniciAdi"] != null) view.Columns["KullaniciAdi"].Caption = Resources.colKullaniciAdi;
                if (view.Columns["Ad"] != null) view.Columns["Ad"].Caption = Resources.colKullaniciAd;
                if (view.Columns["Soyad"] != null) view.Columns["Soyad"].Caption = Resources.colKullaniciSoyad;
                if (view.Columns["Email"] != null) view.Columns["Email"].Caption = Resources.colKullaniciEmail;
                if (view.Columns["Yetki"] != null) view.Columns["Yetki"].Caption = Resources.colKullaniciYetki;
            }
        }
        public void KullanicilariListele()
        {
            using (var db = new TohumBankasiContext())
            {
                // Veritabanından kullanıcıları çek
                // (Şifreyi seçmiyoruz, sadece gerekli bilgileri alıyoruz)
                var liste = db.Kullanicilars
                              .Select(k => new
                              {
                                  ID = k.KullaniciId,
                                  KullaniciAdi = k.KullaniciAdi,
                                  Ad = k.Ad,
                                  Soyad = k.Soyad,
                                  Email = k.Email,
                                  Yetki = k.KullaniciTipi // Admin mi Kullanıcı mı?
                              })
                              .ToList();

                gridKullanicilar.DataSource = liste;
                SutunBasliklariniAyarla();
            }
        }

        private void UcKullaniciYonetimi_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                KullanicilariListele();
            }
        }

        private void UcKullaniciYonetimi_Load_1(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                KullanicilariListele();
            }
        }

        private void btnKullaniciEkle_Click(object sender, EventArgs e)
        {
            // 1. Mevcut Kayıt Ol formunu bir "Ekleme Penceresi" gibi kullanıyoruz
            FormKayitOl frmEkle = new FormKayitOl();

            // 2. Formu aç ve işlemin bitmesini bekle
            frmEkle.ShowDialog();

            // 3. Form kapandıktan sonra listeyi yenile (Yeni kullanıcı görünsün diye)
            KullanicilariListele();
        }

        private void btnKullaniciSil_Click(object sender, EventArgs e)
        {
            // 1. Seçili satırın ID'sini al
            // (GridControl'deki kolon adımız "ID" idi)
            var seciliIdObj = gridView1.GetFocusedRowCellValue("ID");

            if (seciliIdObj == null)
            {
                // Satır seçilmemişse uyar
                XtraMessageBox.Show(Resources.HataSatirSecilmedi, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int silinecekId = Convert.ToInt32(seciliIdObj);

            // 2. Onay iste
            DialogResult onay = XtraMessageBox.Show(Resources.KullaniciSilOnayMesaj, Resources.KullaniciSilOnayBaslik, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay == DialogResult.Yes)
            {
                try
                {
                    using (var db = new TohumBankasiContext())
                    {
                        // 3. Kullanıcıyı bul
                        // (Dikkat: Tablo adı 'Kullanicilars' - s takısı var)
                        var silinecekKullanici = db.Kullanicilars.Find(silinecekId);

                        if (silinecekKullanici != null)
                        {
                            // 4. Sil ve Kaydet
                            db.Kullanicilars.Remove(silinecekKullanici);
                            db.SaveChanges();

                            // 5. Başarı mesajı ve Listeyi Yenileme
                            XtraMessageBox.Show(Resources.KullaniciSilBasarili, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            KullanicilariListele(); // Tabloyu güncelle
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(Resources.GenelHata + " " + ex.Message, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnKullaniciDuzenle_Click(object sender, EventArgs e)
        {
            // 1. Seçili ID'yi al
            var seciliIdObj = gridView1.GetFocusedRowCellValue("ID"); // GridView ismi sizde gridView1 olabilir, kontrol edin.

            if (seciliIdObj == null)
            {
                XtraMessageBox.Show(Resources.HataSatirSecilmedi, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(seciliIdObj);

            // 2. Formu aç
            FormKullaniciDuzenle frm = new FormKullaniciDuzenle(id);
            frm.ShowDialog();

            // 3. Kapanınca listeyi yenile
            KullanicilariListele();
        }
    }
}
