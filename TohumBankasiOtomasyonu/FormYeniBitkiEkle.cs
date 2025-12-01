using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Drawing.Imaging; // Resim formatı için
using System.IO; // Dosya işlemleri için
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
            btnResimSec.Text = Resources.btnResimSec; // Admin panelinde eklemiştik
        }

        private void FormYeniBitkiEkle_Load(object sender, EventArgs e)
        {
            UygulaDil();
        }

        // 1. RESİM SEÇME (Admin panelindekiyle aynı mantık)
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
                        using (var stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            // Önce stream'den geçici bir resim oluştur
                            using (var tempImage = Image.FromStream(stream))
                            {
                                // Sonra bunun hafızada yaşayan temiz bir kopyasını (Bitmap) al
                                // Böylece stream kapansa bile resim hafızada kalır.
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
        private void btnTakipBaslat_Click(object sender, EventArgs e)
        {
            // Güvenlik: Oturum kontrolü
            if (Session.AktifKullanici == null) return;

            // A. Validasyon
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
                string klasorYolu = Path.Combine(Application.StartupPath, "Gorseller");
                if (!Directory.Exists(klasorYolu)) Directory.CreateDirectory(klasorYolu);

                string dosyaAdi = Guid.NewGuid().ToString() + ".jpg";
                string tamYol = Path.Combine(klasorYolu, dosyaAdi);

                picTakipResim.Image.Save(tamYol, ImageFormat.Jpeg);

                // C. Veritabanına Kaydet
                using (var db = new TohumBankasiContext())
                {
                    // Not: Tablo ismi 'KullaniciBitkileris' veya 'Kullanici_Bitkileris' olabilir.
                    // Intellisense ile doğru ismi kontrol edin.
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