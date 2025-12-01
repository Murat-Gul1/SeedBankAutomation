using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.API.Native; // RichEdit kontrolü için gerekli
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
        private List<Image> _secilenResimler = new List<Image>();
        public UcBitkiAsistani()
        {
            InitializeComponent();
        }

        // Dil Ayarlarını Uygula (Load olayında çağıracağız)
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
            btnAsistanResimSec.ToolTip = "Bir bitki fotoğrafı seçin / Select a plant photo";
            lblHafizaUyarisi.Text = Resources.LblUyariHafiza;
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
            // 1. Sınır Kontrolü (En fazla 4 resim)
            if (_secilenResimler.Count >= 4)
            {
                XtraMessageBox.Show("En fazla 4 adet fotoğraf ekleyebilirsiniz.", "Sınır", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = Resources.ResimSecDialogFilter;
                ofd.Title = Resources.ResimSecDialogTitle;
                ofd.Multiselect = true; // Birden fazla seçime izin ver

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string dosyaYolu in ofd.FileNames)
                    {
                        // Tekrar sınır kontrolü (Çoklu seçimde 4'ü geçerse dur)
                        if (_secilenResimler.Count >= 4) break;

                        try
                        {
                            // Resmi yükle
                            Image img;
                            using (var stream = new FileStream(dosyaYolu, FileMode.Open, FileAccess.Read))
                            {
                                img = Image.FromStream(stream);
                            }

                            // Listeye ekle
                            _secilenResimler.Add(img);

                            // Ekrana (FlowPanel) Küçük Resim Olarak Ekle
                            ResimKutusunuOlustur(img);
                        }
                        catch { }
                    }
                }
            }
        }

        // --- 2. YAPAY ZEKA ANALİZİ (ASYNC) ---
        // Not: 'async' kelimesi çok önemli, donmayı engeller.
        private async void btnAsistanAnaliz_Click(object sender, EventArgs e)
        {
            // ... (Key Kontrolü aynı) ...
            string kayitliKey = Properties.Settings.Default.KullaniciApiKey;
            if (string.IsNullOrEmpty(kayitliKey))
            {
                XtraMessageBox.Show(Resources.HataKeyEksik, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApiKey.Focus();
                return;
            }

            string soru = txtAsistanSoru.Text.Trim();

            // Resim listesi veya soru kontrolü
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
            List<Image> chatResimleri = new List<Image>(_secilenResimler);

            MesajEkle(Resources.ChatSender_User + " " + soru, chatResimleri, true);

            // --- TEMİZLİK ---
            txtAsistanSoru.Text = "";
            _secilenResimler.Clear(); // Listeyi temizle
            flowResimPaneli.Controls.Clear(); // Ekranı temizle

            // Arayüz Kilitleme
            btnAsistanAnaliz.Enabled = false;
            btnAsistanResimSec.Enabled = false;

            DocumentRange beklemeMesaji = MesajEkle(Resources.ChatDurum_Bekleyin, null, false, true);

            try
            {
                string formatTalimati = " (Cevabı verirken başlıkları BÜYÜK HARFLE yaz. Markdown kullanma.)";
                string tamSoru = soru + formatTalimati;

                // API'ye Gönder (chatResimleri listesini kullanıyoruz)
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
        // Parametre değişti: 'Image resim' -> 'List<Image> resimler'
        private DocumentRange MesajEkle(string mesaj, List<Image> resimler, bool kullaniciMi, bool sistemMesajiMi = false)
        {
            Document doc = chatEkrani.Document;
            doc.AppendText("\n");

            // --- 1. RESİMLERİ EKLE (YAN YANA) ---
            if (resimler != null && resimler.Count > 0)
            {
                // Resimlerin başlangıç pozisyonunu al
                DocumentPosition startPos = doc.Range.End;

                foreach (var img in resimler)
                {
                    // Resmi küçült (Thumbnail)
                    Image kucukResim = new Bitmap(img, new Size(150, 150));

                    // Resmi ekle
                    doc.Images.Insert(doc.Range.End, kucukResim);

                    // Resimler arasına biraz boşluk bırak (Space karakteri ile)
                    doc.AppendText("  ");
                }

                // Resimler bitti, alt satıra geç
                doc.AppendText("\n");

                // Tüm resim grubunu sağa veya sola hizala
                DocumentRange imgRange = doc.CreateRange(startPos.ToInt(), doc.Range.End.ToInt() - startPos.ToInt());
                ParagraphProperties ppImg = doc.BeginUpdateParagraphs(imgRange);
                ppImg.Alignment = kullaniciMi ? ParagraphAlignment.Right : ParagraphAlignment.Left;
                doc.EndUpdateParagraphs(ppImg);
            }

            // --- 2. METNİ EKLE ---
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