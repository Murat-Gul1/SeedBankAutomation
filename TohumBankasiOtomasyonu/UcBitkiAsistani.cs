using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class UcBitkiAsistani : DevExpress.XtraEditors.XtraUserControl
    {
        public UcBitkiAsistani()
        {
            InitializeComponent();
        }

        // Dil Ayarlarını Uygula (Load olayında çağıracağız)
        private void UygulaDil()
        {
            lblBaslik.Text = Resources.lblAsistanBaslik;
            lblSoruBaslik.Text = Resources.lblSoruBaslik;
            lblCevapBaslik.Text = Resources.lblCevapBaslik;
            btnAsistanResimSec.Text = Resources.btnAsistanResimSec;
            btnAsistanAnaliz.Text = Resources.btnAsistanAnaliz;

            // İpuçları
            btnAsistanResimSec.ToolTip = "Bir bitki fotoğrafı seçin / Select a plant photo";
        }

        private void UcBitkiAsistani_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                UygulaDil();
            }
        }

        // --- 1. RESİM SEÇME İŞLEMİ ---
        private void btnAsistanResimSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                // Sözlükteki filtreyi kullanıyoruz (Daha önce düzeltmiştik: | işareti ile)
                ofd.Filter = Resources.ResimSecDialogFilter;
                ofd.Title = Resources.ResimSecDialogTitle;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Resmi dosyayı kilitlemeden yükle
                        using (var stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            picAsistanResim.Image = Image.FromStream(stream);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Resim yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // --- 2. YAPAY ZEKA ANALİZİ (ASYNC) ---
        // Not: 'async' kelimesi çok önemli, donmayı engeller.
        private async void btnAsistanAnaliz_Click(object sender, EventArgs e)
        {
            // A. Kontroller
            if (picAsistanResim.Image == null)
            {
                XtraMessageBox.Show(Resources.HataResimSecilmedi, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Eğer soru boşsa varsayılan bir soru sor
            string soru = txtAsistanSoru.Text.Trim();
            if (string.IsNullOrEmpty(soru))
            {
                soru = "What is the name of this plant, what does it look like, and what do you recommend for its care?";
            }

            // B. Arayüzü "Bekleyin" Moduna Al
            btnAsistanAnaliz.Enabled = false; // Butonu kilitle (tekrar basılmasın)
            btnAsistanResimSec.Enabled = false;
            txtAsistanCevap.Text = Resources.DurumAnalizEdiliyor; // "Lütfen bekleyin..." yazısı

            // (İsteğe bağlı: Mouse imlecini bekleme moduna al)
            Cursor = Cursors.WaitCursor;

            try
            {
                // C. API'ye GÖNDER VE BEKLE (await)
                // Arka planda Gemini'ye gidip gelene kadar burası bekler ama program donmaz.
                string formatTalimati = " (When answering, write the title in CAPITAL LETTERS. Leave a blank line after each section. Do not use Markdown symbols.)";
                string tamSoru = soru + formatTalimati;
                string cevap = await GeminiManager.BitkiAnalizEt(soru, picAsistanResim.Image);

                // D. Cevabı Yazdır
                string guzelCevap = MetniGuzellestir(cevap);
                txtAsistanCevap.Text = guzelCevap;
            }
            catch (Exception ex)
            {
                txtAsistanCevap.Text = "Hata oluştu: " + ex.Message;
            }
            finally
            {
                // E. İşlem Bitince Arayüzü Eski Haline Getir
                btnAsistanAnaliz.Enabled = true;
                btnAsistanResimSec.Enabled = true;
                Cursor = Cursors.Default;
            }
        }
        // Gelen metindeki yıldızları ve işaretleri temizleyip düzenleyen metot
        private string MetniGuzellestir(string hamMetin)
        {
            if (string.IsNullOrEmpty(hamMetin)) return "";

            // 1. Kalınlık işaretlerini (**) kaldır
            string temiz = hamMetin.Replace("**", "");

            // 2. Başlık işaretlerini (##) kaldır
            temiz = temiz.Replace("##", "");

            // 3. Madde işaretlerini (* ) tireye (- ) çevir ki liste gibi dursun
            temiz = temiz.Replace("* ", "- ");

            // 4. Satır sonlarını Windows formatına uyarla (Düzgün paragraflar için)
            temiz = temiz.Replace("\n", Environment.NewLine);

            return temiz.Trim();
        }
    }
}