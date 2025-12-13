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
                // Names we gave in anonymous type: ID, KullaniciAdi, Ad, Soyad, Email, Yetki
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
                // Fetch users from database
                // (We don't select the password, we only get necessary information)
                var liste = db.Kullanicilars
                              .Select(k => new
                              {
                                  ID = k.KullaniciId,
                                  KullaniciAdi = k.KullaniciAdi,
                                  Ad = k.Ad,
                                  Soyad = k.Soyad,
                                  Email = k.Email,
                                  Yetki = k.KullaniciTipi // Admin mi Kullanıcı mı? (Is it Admin or User?)
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
            // 1. We use the existing Register form like an "Add Window"
            FormKayitOl frmEkle = new FormKayitOl();

            // 2. Formu aç ve işlemin bitmesini bekle
            // 2. Open form and wait for process to finish
            frmEkle.ShowDialog();

            // 3. Form kapandıktan sonra listeyi yenile (Yeni kullanıcı görünsün diye)
            // 3. Refresh list after form closes (So new user appears)
            KullanicilariListele();
        }

        private void btnKullaniciSil_Click(object sender, EventArgs e)
        {
            // 1. Seçili satırın ID'sini al
            // (GridControl'deki kolon adımız "ID" idi)
            // 1. Get ID of selected row
            // (Our column name in GridControl was "ID")
            var seciliIdObj = gridView1.GetFocusedRowCellValue("ID");

            if (seciliIdObj == null)
            {
                // Satır seçilmemişse uyar
                // Warn if row is not selected
                XtraMessageBox.Show(Resources.HataSatirSecilmedi, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int silinecekId = Convert.ToInt32(seciliIdObj);

            // 2. Onay iste
            // 2. Request confirmation
            DialogResult onay = XtraMessageBox.Show(Resources.KullaniciSilOnayMesaj, Resources.KullaniciSilOnayBaslik, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay == DialogResult.Yes)
            {
                try
                {
                    using (var db = new TohumBankasiContext())
                    {
                        // 3. Kullanıcıyı bul
                        // (Dikkat: Tablo adı 'Kullanicilars' - s takısı var)
                        // 3. Find user
                        // (Attention: Table name is 'Kullanicilars' - it has 's' suffix)
                        var silinecekKullanici = db.Kullanicilars.Find(silinecekId);

                        if (silinecekKullanici != null)
                        {
                            // 4. Sil ve Kaydet
                            // 4. Delete and Save
                            db.Kullanicilars.Remove(silinecekKullanici);
                            db.SaveChanges();

                            // 5. Başarı mesajı ve Listeyi Yenileme
                            // 5. Success message and List Refreshing
                            XtraMessageBox.Show(Resources.KullaniciSilBasarili, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            KullanicilariListele(); // Tabloyu güncelle (Update table)
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
            // 1. Get selected ID
            var seciliIdObj = gridView1.GetFocusedRowCellValue("ID"); // GridView ismi sizde gridView1 olabilir, kontrol edin. (GridView name might be gridView1 for you, check it.)

            if (seciliIdObj == null)
            {
                XtraMessageBox.Show(Resources.HataSatirSecilmedi, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(seciliIdObj);

            // 2. Formu aç
            // 2. Open form
            FormKullaniciDuzenle frm = new FormKullaniciDuzenle(id);
            frm.ShowDialog();

            // 3. Kapanınca listeyi yenile
            // 3. Refresh list when closed
            KullanicilariListele();
        }
    }
}
