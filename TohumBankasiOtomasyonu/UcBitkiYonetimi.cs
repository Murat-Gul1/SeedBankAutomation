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
using System.Linq; 
using System.Threading; 
using System.Globalization;
using TohumBankasiOtomasyonu.Models; 
namespace TohumBankasiOtomasyonu
{
    public partial class UcBitkiYonetimi : DevExpress.XtraEditors.XtraUserControl
    {
        public UcBitkiYonetimi()
        {
            InitializeComponent();
        }
        public void BitkileriListele()
        {
            using (var db = new TohumBankasiContext())
            {
                // 1. Şu anki program dili ne? ("tr" veya "en")
                // (Veritabanında 'tr' ve 'en' olarak tuttuğumuz için ilk iki harfi alıyoruz)
                string aktifDil = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                // 2. LINQ Sorgusu: İki tabloyu birleştir
                var bitkiListesi = from b in db.Bitkilers
                                   join c in db.BitkiCevirileris on b.BitkiId equals c.BitkiId
                                   where c.DilKodu == aktifDil // Sadece aktif dildeki çeviriyi getir
                                   select new
                                   {
                                       ID = b.BitkiId,
                                       BitkiAdi = c.BitkiAdi, // Çeviri tablosundan
                                       BilimselAd = c.BilimselAd, // Çeviri tablosundan
                                       Fiyat = b.Fiyat,       // Ana tablodan
                                       Stok = b.StokAdedi,    // Ana tablodan
                                       Durum = b.Aktif == 1 ? "Satışta" : "Pasif" // 1/0 yerine yazı gösterelim
                                   };

                // 3. Sonucu GridControl'e bağla
                // (gridBitkiler, tasarımda GridControl'e verdiğimiz isim olmalı)
                gridBitkiler.DataSource = bitkiListesi.ToList();
            }
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

            // Form kapandıktan sonra listeyi tazele
            // Refresh the list after the Form is closed
            BitkileriListele();

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridBitkiler_Click(object sender, EventArgs e)
        {

        }

        private void UcBitkiYonetimi_Load(object sender, EventArgs e)
        {
            // Tasarım modunda çalışmasını engelle (Visual Studio hata vermesin diye)
            // Prevent it from running in Design Mode (to avoid Visual Studio errors)
            if (!this.DesignMode)
            {
                BitkileriListele();
            }
        }

        private void btnBitkiSil_Click(object sender, EventArgs e)
        {
            // 1. Grid üzerinden seçili satırın "ID" değerini al
            // (Not: GridView isminiz varsayılan olarak 'gridView1'dir. 
            // Eğer değiştirdiyseniz kendi verdiğiniz ismi kullanın)

            var seciliIdObj = gridView1.GetFocusedRowCellValue("ID");

            // Eğer hiçbir satır seçilmemişse veya liste boşsa
            if (seciliIdObj == null)
            {
                XtraMessageBox.Show(Resources.HataSatirSecilmedi, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int silinecekId = Convert.ToInt32(seciliIdObj);

            // 2. Kullanıcıdan ONAY iste (Güvenlik için şart)
            DialogResult onay = XtraMessageBox.Show(Resources.BitkiSilOnayMesaj, Resources.BitkiSilOnayBaslik, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay == DialogResult.Yes)
            {
                try
                {
                    using (var db = new TohumBankasiContext())
                    {
                        // 3. Veritabanında bu ID'ye sahip bitkiyi bul
                        var silinecekBitki = db.Bitkilers.Find(silinecekId);

                        if (silinecekBitki != null)
                        {
                            // 4. Sil ve Kaydet
                            // (EF Core, ilişkili tabloları -Çeviriler ve Görseller- otomatik silebilir, 
                            // eğer silmezse veritabanı ayarlarından Cascade Delete açılmalıdır)
                            db.Bitkilers.Remove(silinecekBitki);
                            db.SaveChanges();

                            // 5. Başarı mesajı ver
                            XtraMessageBox.Show(Resources.BitkiSilBasarili, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // 6. LİSTEYİ YENİLE (Çok Önemli: Silinen satırın ekrandan gitmesi için)
                            BitkileriListele();
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(Resources.GenelHata + " " + ex.Message, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBitkiDuzenle_Click(object sender, EventArgs e)
        {
            // 1. Seçili ID'yi al (Silme işlemindeki gibi)
            var seciliIdObj = gridView1.GetFocusedRowCellValue("ID");
            if (seciliIdObj == null)
            {
                XtraMessageBox.Show(Resources.HataSatirSecilmedi, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int id = Convert.ToInt32(seciliIdObj);

            // 2. Formu bu ID ile oluştur (Düzenleme Modu)
            FormBitkiIslemleri frmIslem = new FormBitkiIslemleri(id);

            // 3. Aç
            frmIslem.ShowDialog();

            // 4. Kapanınca listeyi yenile
            BitkileriListele();
        }
    }
}
