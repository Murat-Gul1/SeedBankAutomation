using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.API.Native; // RichEdit kontrolü için gerekli (Required for RichEdit control)
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Properties;
using System.Collections.Generic;
namespace TohumBankasiOtomasyonu
{
    public partial class UcBitkiAsistani : DevExpress.XtraEditors.XtraUserControl
    {
        // Seçilen resimleri hafızada tutacağımız liste
        // List where we will keep selected images in memory
        private List<Image> _secilenResimler = new List<Image>();
        public UcBitkiAsistani()
        {
            InitializeComponent();
        }

        // Dil Ayarlarını Uygula (Load olayında çağıracağız)
        // Apply Language Settings (We will call in Load event)
        private void UygulaDil()
        {
            lblBaslik.Text = Resources.lblAsistanBaslik;
            lblCevapBaslik.Text = Resources.lblCevapBaslik;
            btnAsistanResimSec.Text = Resources.btnAsistanResimSec;
            btnAsistanAnaliz.Text = Resources.btnAsistanAnaliz;
            lnkGoogleAI.Text = Resources.linkApiKeyAl;
            lblApiKey.Text = Resources.lblApiKeyBaslik;
            btnKeyKaydet.Text = Resources.btnKeyKaydet;

            // İpuçları
            // Hints
            btnAsistanResimSec.ToolTip = "Bir bitki fotoğrafı seçin / Select a plant photo";
            lblHafizaUyarisi.Text = Resources.LblUyariHafiza;
        }

        private void UcBitkiAsistani_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                UygulaDil();

                //  Duruma göre "İpucu Yazısı" (Placeholder) gösterelim
                //  Let's show "Hint Text" (Placeholder) according to situation
                string kayitliKey = Properties.Settings.Default.KullaniciApiKey;
                if (!string.IsNullOrEmpty(kayitliKey))
                {
                    // Anahtar varsa, kutu boş kalsın ama arkada silik yazı yazsın
                    // If key exists, let box remain empty but show faint text in background
                    txtApiKey.Text = "";
                    txtApiKey.Properties.NullValuePrompt = Resources.MsgKeyMevcut;
                    // İsterseniz kullanıcının kafası karışmasın diye butonu "Güncelle" yapabilirsiniz
                    // If you want, you can make the button "Update" so user doesn't get confused
                    btnKeyKaydet.Text = Resources.btnKeyGuncelle;
                }
                else
                {
                    // Anahtar yoksa
                    // If no key
                    txtApiKey.Properties.NullValuePrompt = Resources.MsgKeyGirin;
                }
            }
        }

        // --- 1. RESİM SEÇME İŞLEMİ ---
        // --- 1. IMAGE SELECTION PROCESS ---
        private void btnAsistanResimSec_Click(object sender, EventArgs e)
        {
            // 1. Sınır Kontrolü (En fazla 4 resim)
            // 1. Limit Check (Max 4 images)
            if (_secilenResimler.Count >= 4)
            {
                XtraMessageBox.Show("En fazla 4 adet fotoğraf ekleyebilirsiniz.", "Sınır", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = Resources.ResimSecDialogFilter;
                ofd.Title = Resources.ResimSecDialogTitle;
                ofd.Multiselect = true; // Birden fazla seçime izin ver (Allow multiple selection)

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string dosyaYolu in ofd.FileNames)
                    {
                        // Tekrar sınır kontrolü (Çoklu seçimde 4'ü geçerse dur)
                        // Limit check again (Stop if exceeding 4 in multiple selection)
                        if (_secilenResimler.Count >= 4) break;

                        try
                        {
                            // Resmi yükle
                            // Load image
                            Image img;
                            using (var stream = new FileStream(dosyaYolu, FileMode.Open, FileAccess.Read))
                            {
                                img = Image.FromStream(stream);
                            }

                            // Listeye ekle
                            // Add to list
                            _secilenResimler.Add(img);

                            // Ekrana (FlowPanel) Küçük Resim Olarak Ekle
                            // Add to Screen (FlowPanel) as Thumbnail
                            ResimKutusunuOlustur(img);
                        }
                        catch { }
                    }
                }
            }
        }

        // --- 2. YAPAY ZEKA ANALİZİ (ASYNC) ---
        // Not: 'async' kelimesi çok önemli, donmayı engeller.
        // --- 2. ARTIFICIAL INTELLIGENCE ANALYSIS (ASYNC) ---
        // Note: 'async' keyword is very important, prevents freezing.
        private async void btnAsistanAnaliz_Click(object sender, EventArgs e)
        {
            // ... (Key Kontrolü aynı) ...
            // ... (Key Check same) ...
            string kayitliKey = Properties.Settings.Default.KullaniciApiKey;
            if (string.IsNullOrEmpty(kayitliKey))
            {
                XtraMessageBox.Show(Resources.HataKeyEksik, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApiKey.Focus();
                return;
            }

            string soru = txtAsistanSoru.Text.Trim();

            // Resim listesi veya soru kontrolü
            // Image list or question check
            if (string.IsNullOrEmpty(soru) && _secilenResimler.Count == 0)
            {
                XtraMessageBox.Show(Resources.HataSoruVeyaResimEksik, Resources.UyariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(soru) && _secilenResimler.Count > 0)
            {
                soru = "What can you tell me about these plants?";
            }

            // --- CHAT EKRANINA YAZ ---
            // Artık tüm listeyi (_secilenResimler) gönderiyoruz, böylece hepsi yan yana çıkacak.
            // Listeyi kopyalıyoruz çünkü birazdan temizleyeceğiz.
            // --- WRITE TO CHAT SCREEN ---
            // We now send the entire list (_secilenResimler), so they will all appear side by side.
            // We copy the list because we will clear it soon.
            List<Image> chatResimleri = new List<Image>(_secilenResimler);

            MesajEkle(Resources.ChatSender_User + " " + soru, chatResimleri, true);

            // --- TEMİZLİK ---
            // --- CLEANUP ---
            txtAsistanSoru.Text = "";
            _secilenResimler.Clear(); // Listeyi temizle (Clear list)
            flowResimPaneli.Controls.Clear(); // Ekranı temizle (Clear screen)

            // Arayüz Kilitleme
            // Interface Locking
            btnAsistanAnaliz.Enabled = false;
            btnAsistanResimSec.Enabled = false;

            DocumentRange beklemeMesaji = MesajEkle(Resources.ChatDurum_Bekleyin, null, false, true);

            try
            {
                string formatTalimati = " (Cevabı verirken başlıkları BÜYÜK HARFLE yaz. Markdown kullanma.)";
                string tamSoru = soru + formatTalimati;

                // API'ye Gönder (chatResimleri listesini kullanıyoruz)
                // Send to API (Using chatResimleri list)
                string cevap = await GeminiManager.BitkiAnalizEt(tamSoru, chatResimleri, kayitliKey);

                string guzelCevap = MetniGuzellestir(cevap);

                chatEkrani.Document.Delete(beklemeMesaji);
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
        // Method that cleans and organizes stars and signs in incoming text
        private string MetniGuzellestir(string hamMetin)
        {
            if (string.IsNullOrEmpty(hamMetin)) return "";

            // 1. Kalınlık işaretlerini (**) kaldır
            // 1. Remove bold marks (**)
            string temiz = hamMetin.Replace("**", "");

            // 2. Başlık işaretlerini (##) kaldır
            // 2. Remove header marks (##)
            temiz = temiz.Replace("##", "");

            // 3. Madde işaretlerini (* ) tireye (- ) çevir ki liste gibi dursun
            // 3. Convert bullet points (* ) to dashes (- ) so it looks like a list
            temiz = temiz.Replace("* ", "- ");

            // 4. Satır sonlarını Windows formatına uyarla (Düzgün paragraflar için)
            // 4. Adapt line endings to Windows format (For proper paragraphs)
            temiz = temiz.Replace("\n", Environment.NewLine);

            return temiz.Trim();
        }

        private void lnkGoogleAI_Click(object sender, EventArgs e)
        {
            // Varsayılan tarayıcıda siteyi aç
            // Open site in default browser
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
                // 1. Save to settings
                Properties.Settings.Default.KullaniciApiKey = girilenKey;
                Properties.Settings.Default.Save();

                // 2. Bilgi ver
                // 2. Inform
                XtraMessageBox.Show(Resources.MsgKeyKaydedildi, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 3. Kutuyu Temizle (YENİ EKLENEN SATIR)
                // 3. Clear Box (NEW ADDED LINE)
                txtApiKey.Text = "";
            }
        }
        public void DiliYenile()
        {
            // 1. Sabit yazıları (Başlık, Butonlar, Linkler) güncelle
            // (Bu metot zaten yukarıda tanımlıydı, onu tekrar çağırıyoruz)
            // 1. Update static texts (Title, Buttons, Links)
            // (This method was already defined above, we call it again)
            UygulaDil();

            // 2. API Key kutusunun içindeki "İpucu" yazısını (Placeholder) güncelle
            // (Çünkü dil değişince "Kayıtlı" -> "Saved" olmalı)
            // 2. Update "Hint" text (Placeholder) inside API Key box
            // (Because when language changes it should be "Kayıtlı" -> "Saved")
            string kayitliKey = Properties.Settings.Default.KullaniciApiKey;

            if (!string.IsNullOrEmpty(kayitliKey))
            {
                // Anahtar varsa:
                // If key exists:
                txtApiKey.Properties.NullValuePrompt = Resources.MsgKeyMevcut; // ******* (Saved) / (Kayıtlı)
                btnKeyKaydet.Text = Resources.btnKeyGuncelle; // Update / Güncelle
            }
            else
            {
                // Anahtar yoksa:
                // If no key:
                txtApiKey.Properties.NullValuePrompt = Resources.MsgKeyGirin; // Enter key...
            }
        }
        // Geriye DocumentRange döndürüyoruz ki sonradan silebilelim
        // Parametreye 'resim' eklendi (Opsiyonel, null olabilir)
        // Change 'void' back to 'DocumentRange'
        // Parametre değişti: 'Image resim' -> 'List<Image> resimler'
        // We return DocumentRange so we can delete it later
        // Added 'resim' to parameter (Optional, can be null)
        // Parameter changed: 'Image resim' -> 'List<Image> resimler'
        private DocumentRange MesajEkle(string mesaj, List<Image> resimler, bool kullaniciMi, bool sistemMesajiMi = false)
        {
            Document doc = chatEkrani.Document;
            doc.AppendText("\n");

            // --- 1. RESİMLERİ EKLE (YAN YANA) ---
            // --- 1. ADD IMAGES (SIDE BY SIDE) ---
            if (resimler != null && resimler.Count > 0)
            {
                // Resimlerin başlangıç pozisyonunu al
                // Get start position of images
                DocumentPosition startPos = doc.Range.End;

                foreach (var img in resimler)
                {
                    // Resmi küçült (Thumbnail)
                    // Downsize image (Thumbnail)
                    Image kucukResim = new Bitmap(img, new Size(150, 150));

                    // Resmi ekle
                    // Add image
                    doc.Images.Insert(doc.Range.End, kucukResim);

                    // Resimler arasına biraz boşluk bırak (Space karakteri ile)
                    // Leave some space between images (With Space character)
                    doc.AppendText("  ");
                }

                // Resimler bitti, alt satıra geç
                // Images finished, go to next line
                doc.AppendText("\n");

                // Tüm resim grubunu sağa veya sola hizala
                // Align entire image group to right or left
                DocumentRange imgRange = doc.CreateRange(startPos.ToInt(), doc.Range.End.ToInt() - startPos.ToInt());
                ParagraphProperties ppImg = doc.BeginUpdateParagraphs(imgRange);
                ppImg.Alignment = kullaniciMi ? ParagraphAlignment.Right : ParagraphAlignment.Left;
                doc.EndUpdateParagraphs(ppImg);
            }

            // --- 2. METNİ EKLE ---
            // --- 2. ADD TEXT ---
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

            return range;
        }
        private void ResimKutusunuOlustur(Image img)
        {
            PictureEdit pic = new PictureEdit();
            pic.Image = img;
            pic.Size = new Size(100, 100);
            pic.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pic.Properties.ShowMenu = false;

            pic.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    // DÜZELTME BURADA: Sözlükten çekiyoruz
                    // FIX HERE: We fetch from dictionary
                    if (XtraMessageBox.Show(Resources.MsgResimSilOnay, Resources.BaslikSil, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _secilenResimler.Remove(img);
                        flowResimPaneli.Controls.Remove(pic);
                    }
                }
            };

            flowResimPaneli.Controls.Add(pic);
        }
    }
}