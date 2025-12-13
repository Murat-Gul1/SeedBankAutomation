using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class FormBitkiDetay : DevExpress.XtraEditors.XtraForm
    {
        private int _takipBitkiId;
        // Sohbet için anlık yüklenen resimlerin listesi
        // List of instantly uploaded images for chat
        private List<Image> _secilenResimler = new List<Image>();

        // Constructor (Parametreli)
        // Constructor (Parameterized)
        public FormBitkiDetay(int id) : this()
        {
            _takipBitkiId = id;
        }

        // Constructor (Varsayılan)
        // Constructor (Default)
        public FormBitkiDetay()
        {
            InitializeComponent();
        }

        private void FormBitkiDetay_Load(object sender, EventArgs e)
        {
            UygulaDil();
            BilgileriYukle();
            ChatAyarlariniYap();
            //  Başlangıçta rapor butonunu kapat (Çünkü henüz veri yok)
            // Close report button initially (Because there is no data yet)
            btnRaporAl.Enabled = false;
        }

        private void UygulaDil()
        {
            // Buton metinlerini sözlükten çek
            // Fetch button texts from dictionary
            btnRaporAl.Text = Resources.btnRaporAl_Text;
            btnResimEkle.Text = Resources.btnResimEkle_Text;
            lblHafizaUyarisi.Text = Resources.LblUyariHafiza;
            btnRaporArsivi.Text = Resources.btnGecmisRaporlar;
        }

        private void ChatAyarlariniYap()
        {
            // Chat ekranını 'WhatsApp' gibi göster
            // Show chat screen like 'WhatsApp'
            chatEkrani.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            chatEkrani.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            chatEkrani.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            chatEkrani.ReadOnly = true; // Kullanıcı değiştiremesin
            // User cannot change
            chatEkrani.Options.Behavior.ShowPopupMenu = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
        }

        private void BilgileriYukle()
        {
            using (var db = new TohumBankasiContext())
            {
                // Bitki bilgilerini veritabanından çek
                // Fetch plant information from database
                var bitki = db.KullaniciBitkileris.Find(_takipBitkiId);
                if (bitki != null)
                {
                    lblBitkiAdi.Text = bitki.BitkiAdi;
                    this.Text = bitki.BitkiAdi + " - Detaylar";

                    // Profil Resmini Yükle
                    // Load Profile Image
                    string tamYol = Path.Combine(Application.StartupPath, bitki.GorselYolu);
                    if (File.Exists(tamYol))
                    {
                        using (var stream = new FileStream(tamYol, FileMode.Open, FileAccess.Read))
                        {
                            // Resmi hem kutuya koyuyoruz hem de analiz listesine ilk resim olarak ekliyoruz
                            // We put the image in the box and add it as the first image to the analysis list
                            Image anaResim = Image.FromStream(stream);
                        }
                    }
                }
            }
        }

        // --- 1. RESİM EKLEME İŞLEMİ (Çoklu) ---
        // --- 1. IMAGE ADDING PROCESS (Multiple) ---
        private void btnResimEkle_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = Resources.ResimSecDialogFilter;
                ofd.Multiselect = true; // Birden fazla resim seçilebilsin
                // Allow multiple image selection

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string dosya in ofd.FileNames)
                    {
                        try
                        {
                            using (var stream = new FileStream(dosya, FileMode.Open, FileAccess.Read))
                            {
                                Image img = Image.FromStream(stream);
                                _secilenResimler.Add(img);
                                ResimKutusunuOlustur(img); // Küçük resim oluştur
                                // Create thumbnail
                            }
                        }
                        catch { }
                    }
                    //En az bir resim seçildiyse rapor butonunu aç
                    // Enable report button if at least one image is selected
                    if (_secilenResimler.Count > 0)
                    {
                        btnRaporAl.Enabled = true;
                    }
                }
            }
        }

        private void ResimKutusunuOlustur(Image img)
        {
            PictureEdit pic = new PictureEdit();
            pic.Image = img;
            pic.Size = new Size(70, 70); // Küçük kareler
            // Small squares
            pic.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pic.Properties.ShowMenu = false;
            pic.ToolTip = "Sağ tık ile sil";

            // Sağ tık ile resmi listeden silme
            // Delete image from list with right click
            pic.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (XtraMessageBox.Show(Resources.MsgResimSilOnay, "Sil", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _secilenResimler.Remove(img);
                        flowResimPaneli.Controls.Remove(pic);
                    }
                }
            };

            flowResimPaneli.Controls.Add(pic);
        }

        // --- 2. MESAJ GÖNDERME (Anlık Analiz) ---
        // --- 2. SENDING MESSAGE (Instant Analysis) ---
        private async void btnGonder_Click(object sender, EventArgs e)
        {
            string soru = txtMesaj.Text.Trim();

            // Hem soru boşsa hem de resim yoksa işlem yapma
            // Do not process if both question is empty and no image
            if (string.IsNullOrEmpty(soru) && _secilenResimler.Count == 0) return;

            // Kullanıcı mesajını ekrana bas (Sağa Hizalı)
            // Print user message to screen (Right Aligned)
            MesajEkle(Resources.ChatSender_User + " " + soru, _secilenResimler, true);

            txtMesaj.Text = ""; // Kutuyu temizle
            // Clear box

            // Bekleme mesajı göster
            // Show waiting message
            DocumentRange bekleme = MesajEkle(Resources.ChatDurum_Bekleyin, null, false, true);
            btnGonder.Enabled = false;

            try
            {
                string apiKey = Properties.Settings.Default.KullaniciApiKey;

                // Listeyi kopyalayıp gönder (Güvenlik için)
                // Copy and send list (For security)
                List<Image> gonderilecekler = new List<Image>(_secilenResimler);

                // Format Talimatı Ekle (Düzgün çıktı için)
                // Add Format Instruction (For proper output)
                string formatTalimati = " (When answering, write the title in CAPITAL LETTERS. Do not use Markdown symbols (*, #). Write as plain text.)";
                string tamSoru = soru + formatTalimati;

                // API'ye Gönder
                // Send to API
                string cevap = await GeminiManager.BitkiAnalizEt(tamSoru, gonderilecekler, apiKey);
                string guzelCevap = MetniGuzellestir(cevap);

                // Beklemeyi sil ve cevabı yaz (Sola Hizalı)
                // Delete waiting and write answer (Left Aligned)
                chatEkrani.Document.Delete(bekleme);
                MesajEkle(Resources.ChatSender_AI + "\n" + guzelCevap, null, false);
            }
            catch (Exception ex)
            {
                chatEkrani.Document.Delete(bekleme);
                MesajEkle("HATA: " + ex.Message, null, false);
            }
            finally
            {
                btnGonder.Enabled = true;
                // İşlem bittiğine göre artık konuşulmuş bir şeyler var.
                // Rapor butonunu aktif et.
                // Since the process is finished, there is something talked about now.
                // Activate report button.
                btnRaporAl.Enabled = true;
            }
        }

        // --- 3. RAPOR OLUŞTURMA VE KAYDETME ---
        // --- 3. REPORT CREATION AND SAVING ---
        private async void btnRaporAl_Click(object sender, EventArgs e)
        {
            // 1. Verileri Hazırla
            // 1. Prepare Data
            string kullaniciNotu = txtMesaj.Text.Trim();
            List<Image> gonderilecekResimler = new List<Image>(_secilenResimler);

            // 2. Ekranda Gösterilecek Mesajı Belirle
            // Eğer kullanıcı bir not yazdıysa onu göster, yazmadıysa "(Rapor Oluştur)" yazsın.
            // 2. Determine Message to Show on Screen
            // If user wrote a note show it, otherwise write "(Create Report)".
            string ekranMesaji = string.IsNullOrEmpty(kullaniciNotu)
                ? $"({Resources.btnRaporAl_Text})"
                : kullaniciNotu;

            // --- DÜZELTME 1: RESİMLERİ CHAT EKRANINA BAS ---
            // Artık sadece metni değil, gönderilen resimleri de chat'e ekliyoruz.
            // --- FIX 1: PRINT IMAGES TO CHAT SCREEN ---
            // Now we add not only text but also sent images to chat.
            MesajEkle(Resources.ChatSender_User + " " + ekranMesaji, gonderilecekResimler, true);

            // 3. Prompt (Talimat) Oluştur
            // 3. Create Prompt (Instruction)
            string raporEmri = Resources.Promt_RaporEki;
            string tamPrompt = string.IsNullOrEmpty(kullaniciNotu)
                ? raporEmri
                : $"{kullaniciNotu} {raporEmri}";

            // 4. Temizlik (Veriler kullanıldı, kutuları boşalt)
            // 4. Cleanup (Data used, empty boxes)
            txtMesaj.Text = "";
            _secilenResimler.Clear();
            flowResimPaneli.Controls.Clear();

            // 5. Arayüzü Kilitle ve Bekleme Mesajı
            // 5. Lock Interface and Waiting Message
            DocumentRange bekleme = MesajEkle("📄 " + Resources.ChatDurum_Bekleyin, null, false, true);

            // --- DÜZELTME 2: BUTONU PASİF YAP ---
            // Veriler temizlendiği için buton pasif olmalı.
            // --- FIX 2: DISABLE BUTTON ---
            // Button should be disabled because data is cleared.
            btnRaporAl.Enabled = false;
            btnGonder.Enabled = false;

            try
            {
                string apiKey = Properties.Settings.Default.KullaniciApiKey;

                // API'ye Gönder
                // Send to API
                string raporMetni = await GeminiManager.BitkiAnalizEt(tamPrompt, gonderilecekResimler, apiKey);
                string guzelRapor = MetniGuzellestir(raporMetni);

                chatEkrani.Document.Delete(bekleme);

                // Raporu Ekrana Bas
                // Print Report to Screen
                MesajEkle("📄 RAPOR:\n" + guzelRapor, null, false);

                // Veritabanına Kaydet
                // Save to Database
                using (var db = new TohumBankasiContext())
                {
                    var yeniRapor = new BitkiRaporlari
                    {
                        KullaniciBitkiId = _takipBitkiId,
                        RaporTarihi = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        RaporMetni = guzelRapor
                    };
                    db.BitkiRaporlaris.Add(yeniRapor);
                    db.SaveChanges();
                }

                XtraMessageBox.Show(Resources.MsgRaporBasarili, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                chatEkrani.Document.Delete(bekleme);
                MesajEkle("Rapor Hatası: " + ex.Message, null, false);
            }
            finally
            {
                btnGonder.Enabled = true; // Sohbet butonu her zaman açılabilir
                // Chat button can always be opened

                // --- DÜZELTME 3: RAPOR BUTONU KAPALI KALSIN ---
                // İşlem bitti ama veri (resim/metin) olmadığı için buton PASİF kalmalı.
                // Kullanıcı yeni bir resim ekleyene veya yazı yazana kadar bu buton açılmayacak.
                // --- FIX 3: KEEP REPORT BUTTON CLOSED ---
                // Button must remain PASSIVE as process ended but no data (image/text).
                // This button will not open until user adds a new image or writes text.
                btnRaporAl.Enabled = false;
            }
        }

        // --- YARDIMCI METOTLAR ---
        // --- HELPER METHODS ---

        // Chat ekranına mesaj ve resim ekleyen metot
        // Method that adds message and image to chat screen
        private DocumentRange MesajEkle(string mesaj, List<Image> resimler, bool kullaniciMi, bool sistemMesajiMi = false)
        {
            Document doc = chatEkrani.Document;
            doc.AppendText("\n");

            // A. RESİMLERİ EKLE (Eğer varsa)
            // A. ADD IMAGES (If any)
            if (resimler != null && resimler.Count > 0)
            {
                DocumentPosition startPos = doc.Range.End;
                foreach (var img in resimler)
                {
                    Image kucukResim = new Bitmap(img, new Size(100, 100)); // Küçük resim
                    // Small image
                    doc.Images.Insert(doc.Range.End, kucukResim);
                    doc.AppendText("  "); // Aralarında boşluk
                    // Space between them
                }
                doc.AppendText("\n");

                // Resimleri hizala
                // Align images
                DocumentRange imgRange = doc.CreateRange(startPos.ToInt(), doc.Range.End.ToInt() - startPos.ToInt());
                ParagraphProperties ppImg = doc.BeginUpdateParagraphs(imgRange);
                ppImg.Alignment = kullaniciMi ? ParagraphAlignment.Right : ParagraphAlignment.Left;
                doc.EndUpdateParagraphs(ppImg);
            }

            // B. METNİ EKLE
            // B. ADD TEXT
            DocumentRange range = doc.AppendText(mesaj + "\n");

            ParagraphProperties pp = doc.BeginUpdateParagraphs(range);
            pp.Alignment = (sistemMesajiMi) ? ParagraphAlignment.Center : (kullaniciMi ? ParagraphAlignment.Right : ParagraphAlignment.Left);
            doc.EndUpdateParagraphs(pp);

            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.ForeColor = (sistemMesajiMi) ? Color.Gray : (kullaniciMi ? Color.DarkBlue : Color.Black);
            cp.Bold = kullaniciMi;
            cp.Italic = sistemMesajiMi;
            doc.EndUpdateCharacters(cp);

            chatEkrani.ScrollToCaret();
            return range;
        }

        // Tekli mesaj ekleme (Aşırı yükleme - Overload)
        // Single message adding (Overload)
        private DocumentRange MesajEkle(string mesaj, bool kullaniciMi, bool sistemMesajiMi = false)
        {
            return MesajEkle(mesaj, null, kullaniciMi, sistemMesajiMi);
        }

        // Metni temizleyen metot
        // Method that cleans text
        private string MetniGuzellestir(string hamMetin)
        {
            if (string.IsNullOrEmpty(hamMetin)) return "";
            string temiz = hamMetin.Replace("**", "");
            temiz = temiz.Replace("##", "");
            temiz = temiz.Replace("* ", "- ");
            temiz = temiz.Replace("\n", Environment.NewLine);
            return temiz.Trim();
        }

        private void btnRaporArsivi_Click(object sender, EventArgs e)
        {
            // Rapor listesi formunu aç (Bitki ID'sini gönderiyoruz ki sadece bu bitkinin raporları gelsin)
            // Eğer tüm raporları görmek isterseniz parametre göndermeyebilirsiniz.
            // Open report list form (We follow Plant ID so only reports of this plant come)
            // If you want to see all reports you don't have to send parameter.
            FormRaporListesi frm = new FormRaporListesi(_takipBitkiId);
            frm.ShowDialog();
        }
    }
}