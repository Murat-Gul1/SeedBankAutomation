using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Drawing.Imaging; // Resim formatı için (For image format)
using System.IO; // Dosya işlemleri için (For file operations)
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class FormYeniBitkiEkle : DevExpress.XtraEditors.XtraForm
    {
        public FormYeniBitkiEkle()
        {
            InitializeComponent();
        }

        private void UygulaDil()
        {
            this.Text = Resources.FormYeniBitkiEkle_Title;
            lblTakipAdi.Text = Resources.lblTakipAdi;
            btnTakipBaslat.Text = Resources.btnTakipBaslat;
            btnResimSec.Text = Resources.btnResimSec; // Admin panelinde eklemiştik (We added it in Admin panel)
        }

        private void FormYeniBitkiEkle_Load(object sender, EventArgs e)
        {
            UygulaDil();
        }

        // 1. RESİM SEÇME (Admin panelindekiyle aynı mantık)
        // 1. SELECT IMAGE (Same logic as in Admin panel)
        private void btnResimSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = Resources.ResimSecDialogFilter;
                ofd.Title = Resources.ResimSecDialogTitle;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Resmi dosyayı kilitlemeden GÜVENLİ şekilde yükle
                        // SAFELY load the image without locking the file
                        using (var stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            // Önce stream'den geçici bir resim oluştur
                            // First create a temporary image from stream
                            using (var tempImage = Image.FromStream(stream))
                            {
                                // Sonra bunun hafızada yaşayan temiz bir kopyasını (Bitmap) al
                                // Böylece stream kapansa bile resim hafızada kalır.
                                // Then take a clean copy (Bitmap) of it living in memory
                                // So the image stays in memory even if stream is closed.
                                picTakipResim.Image = new Bitmap(tempImage);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Resim yüklenirken hata: " + ex.Message);
                    }
                }
            }
        }

        // 2. KAYDETME (Takibi Başlat)
        // 2. SAVE (Start Tracking)
        private void btnTakipBaslat_Click(object sender, EventArgs e)
        {
            // Güvenlik: Oturum kontrolü
            // Security: Session control
            if (Session.AktifKullanici == null) return;

            // A. Validasyon
            // A. Validation
            if (picTakipResim.Image == null)
            {
                XtraMessageBox.Show(Resources.HataTakipResim, Resources.UyariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTakipAdi.Text))
            {
                XtraMessageBox.Show(Resources.HataTakipAd, Resources.UyariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTakipAdi.Focus();
                return;
            }

            try
            {
                // B. Resmi Klasöre Kaydet
                // B. Save Image to Folder
                string klasorYolu = Path.Combine(Application.StartupPath, "Gorseller");
                if (!Directory.Exists(klasorYolu)) Directory.CreateDirectory(klasorYolu);

                string dosyaAdi = Guid.NewGuid().ToString() + ".jpg";
                string tamYol = Path.Combine(klasorYolu, dosyaAdi);

                picTakipResim.Image.Save(tamYol, ImageFormat.Jpeg);

                // C. Veritabanına Kaydet
                // C. Save to Database
                using (var db = new TohumBankasiContext())
                {
                    // Not: Tablo ismi 'KullaniciBitkileris' veya 'Kullanici_Bitkileris' olabilir.
                    // Intellisense ile doğru ismi kontrol edin.
                    // Note: Table name can be 'KullaniciBitkileris' or 'Kullanici_Bitkileris'.
                    // Check correct name with Intellisense.
                    KullaniciBitkileri yeniTakip = new KullaniciBitkileri
                    {
                        KullaniciId = Session.AktifKullanici.KullaniciId,
                        BitkiAdi = txtTakipAdi.Text.Trim(),
                        GorselYolu = "Gorseller/" + dosyaAdi,
                        OlusturmaTarihi = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                    db.KullaniciBitkileris.Add(yeniTakip);
                    db.SaveChanges();
                }

                // D. Başarı ve Kapat
                // D. Success and Close
                XtraMessageBox.Show(Resources.BitkiKayitBasarili, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Resources.GenelHata + " " + ex.Message, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}