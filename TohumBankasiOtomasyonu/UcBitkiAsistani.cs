using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using TohumBankasiOtomasyonu.Properties;
using DevExpress.XtraRichEdit.API.Native; // RichEdit kontrolü için gerekli
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
            lnkGoogleAI.Text = Resources.linkApiKeyAl;
            lblApiKey.Text = Resources.lblApiKeyBaslik;
            btnKeyKaydet.Text = Resources.btnKeyKaydet;

            // İpuçları
            btnAsistanResimSec.ToolTip = "Bir bitki fotoğrafı seçin / Select a plant photo";
        }

        private void UcBitkiAsistani_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                UygulaDil();

                //  Duruma göre "İpucu Yazısı" (Placeholder) gösterelim
                string kayitliKey = Properties.Settings.Default.KullaniciApiKey;
                if (!string.IsNullOrEmpty(kayitliKey))
                {
                    // Anahtar varsa, kutu boş kalsın ama arkada silik yazı yazsın
                    txtApiKey.Text = "";
                    txtApiKey.Properties.NullValuePrompt = Resources.MsgKeyMevcut;
                    // İsterseniz kullanıcının kafası karışmasın diye butonu "Güncelle" yapabilirsiniz
                    btnKeyKaydet.Text = Resources.btnKeyGuncelle;
                }
                else
                {
                    // Anahtar yoksa
                    txtApiKey.Properties.NullValuePrompt = Resources.MsgKeyGirin;
                }
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
            // 1. Key Kontrolü
            string kayitliKey = Properties.Settings.Default.KullaniciApiKey;
            if (string.IsNullOrEmpty(kayitliKey))
            {
                XtraMessageBox.Show(Resources.HataKeyEksik, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApiKey.Focus();
                return;
            }

            // 2. Soru Hazırlığı
            string soru = txtAsistanSoru.Text.Trim();

            // Eğer hem resim yok hem soru yoksa uyarı ver
            if (string.IsNullOrEmpty(soru) && picAsistanResim.Image == null)
            {
                XtraMessageBox.Show("Lütfen bir resim yükleyin veya bir soru yazın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Varsayılan soru (Sadece resim atıldıysa)
            if (string.IsNullOrEmpty(soru) && picAsistanResim.Image != null)
            {
                soru = "What is this?";
            }

            // 3. Gönderilecek Resmi Al (Varsa)
            Image gonderilecekResim = null;
            if (picAsistanResim.Image != null)
            {
                // Resmi kopyala (Çünkü aşağıda picAsistanResim'i temizleyeceğiz)
                gonderilecekResim = new Bitmap(picAsistanResim.Image);
            }

            // --- CHAT EKRANINA YAZ (Resim varsa o da eklenecek) ---
            MesajEkle(Resources.ChatSender_User + " " + soru, gonderilecekResim, true);

            // --- TEMİZLİK ---
            txtAsistanSoru.Text = "";
            picAsistanResim.Image = null; // Sol taraftaki resmi kaldır (İsteğiniz)

            // Arayüz Kilitleme
            btnAsistanAnaliz.Enabled = false;
            btnAsistanResimSec.Enabled = false;

            // Bekleme Mesajı (Resimsiz)
            DocumentRange beklemeMesaji = MesajEkle(Resources.ChatDurum_Bekleyin, null, false, true);

            try
            {
                string formatTalimati = " (Cevabı verirken başlıkları BÜYÜK HARFLE yaz. Markdown kullanma.)";
                string tamSoru = soru + formatTalimati;

                // API'ye Gönder (Resim null ise sadece metin gider)
                string cevap = await GeminiManager.BitkiAnalizEt(tamSoru, gonderilecekResim, kayitliKey);
                string guzelCevap = MetniGuzellestir(cevap);

                // Bekleme mesajını sil
                chatEkrani.Document.Delete(beklemeMesaji);

                // Cevabı Yaz (Resimsiz)
                MesajEkle(Resources.ChatSender_AI + "\n" + guzelCevap, null, false);
            }
            catch (Exception ex)
            {
                chatEkrani.Document.Delete(beklemeMesaji);
                MesajEkle("HATA: " + ex.Message, null, false);
            }
            finally
            {
                btnAsistanAnaliz.Enabled = true;
                btnAsistanResimSec.Enabled = true;
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

        private void lnkGoogleAI_Click(object sender, EventArgs e)
        {
            // Varsayılan tarayıcıda siteyi aç
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://aistudio.google.com/",
                UseShellExecute = true
            });
        }

        private void btnKeyKaydet_Click(object sender, EventArgs e)
        {
            string girilenKey = txtApiKey.Text.Trim();

            if (!string.IsNullOrEmpty(girilenKey))
            {
                // 1. Ayarlara kaydet
                Properties.Settings.Default.KullaniciApiKey = girilenKey;
                Properties.Settings.Default.Save();

                // 2. Bilgi ver
                XtraMessageBox.Show(Resources.MsgKeyKaydedildi, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 3. Kutuyu Temizle (YENİ EKLENEN SATIR)
                txtApiKey.Text = "";
            }
        }
        public void DiliYenile()
        {
            // 1. Sabit yazıları (Başlık, Butonlar, Linkler) güncelle
            // (Bu metot zaten yukarıda tanımlıydı, onu tekrar çağırıyoruz)
            UygulaDil();

            // 2. API Key kutusunun içindeki "İpucu" yazısını (Placeholder) güncelle
            // (Çünkü dil değişince "Kayıtlı" -> "Saved" olmalı)
            string kayitliKey = Properties.Settings.Default.KullaniciApiKey;

            if (!string.IsNullOrEmpty(kayitliKey))
            {
                // Anahtar varsa:
                txtApiKey.Properties.NullValuePrompt = Resources.MsgKeyMevcut; // ******* (Saved) / (Kayıtlı)
                btnKeyKaydet.Text = Resources.btnKeyGuncelle; // Update / Güncelle
            }
            else
            {
                // Anahtar yoksa:
                txtApiKey.Properties.NullValuePrompt = Resources.MsgKeyGirin; // Enter key...
            }
        }
        // Geriye DocumentRange döndürüyoruz ki sonradan silebilelim
        // Parametreye 'resim' eklendi (Opsiyonel, null olabilir)
        // Change 'void' back to 'DocumentRange'
        private DocumentRange MesajEkle(string mesaj, Image resim, bool kullaniciMi, bool sistemMesajiMi = false)
        {
            Document doc = chatEkrani.Document;
            doc.AppendText("\n");

            // --- 1. ADD IMAGE (IF EXISTS) ---
            if (resim != null)
            {
                Image kucukResim = new Bitmap(resim, new Size(150, 150));
                DocumentPosition pos = doc.Range.End;
                doc.Images.Insert(pos, kucukResim);
                doc.AppendText("\n");

                ParagraphProperties ppImg = doc.BeginUpdateParagraphs(doc.CreateRange(pos.ToInt(), 1));
                ppImg.Alignment = kullaniciMi ? ParagraphAlignment.Right : ParagraphAlignment.Left;
                doc.EndUpdateParagraphs(ppImg);
            }

            // --- 2. ADD TEXT ---
            // Capture the range of the added text so we can return it
            DocumentRange range = doc.AppendText(mesaj + "\n");

            ParagraphProperties pp = doc.BeginUpdateParagraphs(range);
            CharacterProperties cp = doc.BeginUpdateCharacters(range);

            if (sistemMesajiMi)
            {
                pp.Alignment = ParagraphAlignment.Center;
                cp.ForeColor = Color.Gray;
                cp.Italic = true;
                cp.Bold = false;
            }
            else if (kullaniciMi)
            {
                pp.Alignment = ParagraphAlignment.Right;
                cp.ForeColor = Color.DarkBlue;
                cp.Bold = true;
            }
            else
            {
                pp.Alignment = ParagraphAlignment.Left;
                cp.ForeColor = Color.Black;
                cp.Bold = false;
            }

            doc.EndUpdateCharacters(cp);
            doc.EndUpdateParagraphs(pp);
            chatEkrani.ScrollToCaret();

            // RETURN THE RANGE (Crucial for deleting the 'Please Wait' message later)
            return range;
        }
    }
}