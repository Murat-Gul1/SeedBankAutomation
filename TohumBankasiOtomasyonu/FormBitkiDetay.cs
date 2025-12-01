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
        private List<Image> _secilenResimler = new List<Image>();

        // Constructor (Parametreli)
        public FormBitkiDetay(int id) : this()
        {
            _takipBitkiId = id;
        }

        // Constructor (Varsayılan)
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
            btnRaporAl.Enabled = false;
        }

        private void UygulaDil()
        {
            // Buton metinlerini sözlükten çek
            btnRaporAl.Text = Resources.btnRaporAl_Text;
            btnResimEkle.Text = Resources.btnResimEkle_Text;
            lblHafizaUyarisi.Text = Resources.LblUyariHafiza;
            btnRaporArsivi.Text = Resources.btnGecmisRaporlar;
        }

        private void ChatAyarlariniYap()
        {
            // Chat ekranını 'WhatsApp' gibi göster
            chatEkrani.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            chatEkrani.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            chatEkrani.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            chatEkrani.ReadOnly = true; // Kullanıcı değiştiremesin
            chatEkrani.Options.Behavior.ShowPopupMenu = DevExpress.XtraRichEdit.DocumentCapability.Disabled;
        }

        private void BilgileriYukle()
        {
            using (var db = new TohumBankasiContext())
            {
                // Bitki bilgilerini veritabanından çek
                var bitki = db.KullaniciBitkileris.Find(_takipBitkiId);
                if (bitki != null)
                {
                    lblBitkiAdi.Text = bitki.BitkiAdi;
                    this.Text = bitki.BitkiAdi + " - Detaylar";

                    // Profil Resmini Yükle
                    string tamYol = Path.Combine(Application.StartupPath, bitki.GorselYolu);
                    if (File.Exists(tamYol))
                    {
                        using (var stream = new FileStream(tamYol, FileMode.Open, FileAccess.Read))
                        {
                            // Resmi hem kutuya koyuyoruz hem de analiz listesine ilk resim olarak ekliyoruz
                            Image anaResim = Image.FromStream(stream);
                        }
                    }
                }
            }
        }

        // --- 1. RESİM EKLEME İŞLEMİ (Çoklu) ---
        private void btnResimEkle_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = Resources.ResimSecDialogFilter;
                ofd.Multiselect = true; // Birden fazla resim seçilebilsin

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
                            }
                        }
                        catch { }
                    }
                    //En az bir resim seçildiyse rapor butonunu aç
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
            pic.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pic.Properties.ShowMenu = false;
            pic.ToolTip = "Sağ tık ile sil";

            // Sağ tık ile resmi listeden silme
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
        private async void btnGonder_Click(object sender, EventArgs e)
        {
            string soru = txtMesaj.Text.Trim();

            // Hem soru boşsa hem de resim yoksa işlem yapma
            if (string.IsNullOrEmpty(soru) && _secilenResimler.Count == 0) return;

            // Kullanıcı mesajını ekrana bas (Sağa Hizalı)
            MesajEkle(Resources.ChatSender_User + " " + soru, _secilenResimler, true);

            txtMesaj.Text = ""; // Kutuyu temizle

            // Bekleme mesajı göster
            DocumentRange bekleme = MesajEkle(Resources.ChatDurum_Bekleyin, null, false, true);
            btnGonder.Enabled = false;

            try
            {
                string apiKey = Properties.Settings.Default.KullaniciApiKey;

                // Listeyi kopyalayıp gönder (Güvenlik için)
                List<Image> gonderilecekler = new List<Image>(_secilenResimler);

                // Format Talimatı Ekle (Düzgün çıktı için)
                string formatTalimati = " (When answering, write the title in CAPITAL LETTERS. Do not use Markdown symbols (*, #). Write as plain text.)";
                string tamSoru = soru + formatTalimati;

                // API'ye Gönder
                string cevap = await GeminiManager.BitkiAnalizEt(tamSoru, gonderilecekler, apiKey);
                string guzelCevap = MetniGuzellestir(cevap);

                // Beklemeyi sil ve cevabı yaz (Sola Hizalı)
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
                btnRaporAl.Enabled = true;
            }
        }

        // --- 3. RAPOR OLUŞTURMA VE KAYDETME ---
        private async void btnRaporAl_Click(object sender, EventArgs e)
        {
            // 1. Verileri Hazırla
            string kullaniciNotu = txtMesaj.Text.Trim();
            List<Image> gonderilecekResimler = new List<Image>(_secilenResimler);

            // 2. Ekranda Gösterilecek Mesajı Belirle
            // Eğer kullanıcı bir not yazdıysa onu göster, yazmadıysa "(Rapor Oluştur)" yazsın.
            string ekranMesaji = string.IsNullOrEmpty(kullaniciNotu)
                ? $"({Resources.btnRaporAl_Text})"
                : kullaniciNotu;

            // --- DÜZELTME 1: RESİMLERİ CHAT EKRANINA BAS ---
            // Artık sadece metni değil, gönderilen resimleri de chat'e ekliyoruz.
            MesajEkle(Resources.ChatSender_User + " " + ekranMesaji, gonderilecekResimler, true);

            // 3. Prompt (Talimat) Oluştur
            string raporEmri = Resources.Promt_RaporEki;
            string tamPrompt = string.IsNullOrEmpty(kullaniciNotu)
                ? raporEmri
                : $"{kullaniciNotu} {raporEmri}";

            // 4. Temizlik (Veriler kullanıldı, kutuları boşalt)
            txtMesaj.Text = "";
            _secilenResimler.Clear();
            flowResimPaneli.Controls.Clear();

            // 5. Arayüzü Kilitle ve Bekleme Mesajı
            DocumentRange bekleme = MesajEkle("📄 " + Resources.ChatDurum_Bekleyin, null, false, true);

            // --- DÜZELTME 2: BUTONU PASİF YAP ---
            // Veriler temizlendiği için buton pasif olmalı.
            btnRaporAl.Enabled = false;
            btnGonder.Enabled = false;

            try
            {
                string apiKey = Properties.Settings.Default.KullaniciApiKey;

                // API'ye Gönder
                string raporMetni = await GeminiManager.BitkiAnalizEt(tamPrompt, gonderilecekResimler, apiKey);
                string guzelRapor = MetniGuzellestir(raporMetni);

                chatEkrani.Document.Delete(bekleme);

                // Raporu Ekrana Bas
                MesajEkle("📄 RAPOR:\n" + guzelRapor, null, false);

                // Veritabanına Kaydet
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

                // --- DÜZELTME 3: RAPOR BUTONU KAPALI KALSIN ---
                // İşlem bitti ama veri (resim/metin) olmadığı için buton PASİF kalmalı.
                // Kullanıcı yeni bir resim ekleyene veya yazı yazana kadar bu buton açılmayacak.
                btnRaporAl.Enabled = false;
            }
        }

        // --- YARDIMCI METOTLAR ---

        // Chat ekranına mesaj ve resim ekleyen metot
        private DocumentRange MesajEkle(string mesaj, List<Image> resimler, bool kullaniciMi, bool sistemMesajiMi = false)
        {
            Document doc = chatEkrani.Document;
            doc.AppendText("\n");

            // A. RESİMLERİ EKLE (Eğer varsa)
            if (resimler != null && resimler.Count > 0)
            {
                DocumentPosition startPos = doc.Range.End;
                foreach (var img in resimler)
                {
                    Image kucukResim = new Bitmap(img, new Size(100, 100)); // Küçük resim
                    doc.Images.Insert(doc.Range.End, kucukResim);
                    doc.AppendText("  "); // Aralarında boşluk
                }
                doc.AppendText("\n");

                // Resimleri hizala
                DocumentRange imgRange = doc.CreateRange(startPos.ToInt(), doc.Range.End.ToInt() - startPos.ToInt());
                ParagraphProperties ppImg = doc.BeginUpdateParagraphs(imgRange);
                ppImg.Alignment = kullaniciMi ? ParagraphAlignment.Right : ParagraphAlignment.Left;
                doc.EndUpdateParagraphs(ppImg);
            }

            // B. METNİ EKLE
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
        private DocumentRange MesajEkle(string mesaj, bool kullaniciMi, bool sistemMesajiMi = false)
        {
            return MesajEkle(mesaj, null, kullaniciMi, sistemMesajiMi);
        }

        // Metni temizleyen metot
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
            FormRaporListesi frm = new FormRaporListesi(_takipBitkiId);
            frm.ShowDialog();
        }
    }
}