using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks; // Task için
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class FormBitkiDetay : DevExpress.XtraEditors.XtraForm
    {
        private int _takipBitkiId;

        public FormBitkiDetay(int id) : this()
        {
            _takipBitkiId = id;
        }

        public FormBitkiDetay() { InitializeComponent(); }

        private void FormBitkiDetay_Load(object sender, EventArgs e)
        {
            BilgileriYukle();
            SohbetGecmisiniYukle();
        }

        private void BilgileriYukle()
        {
            using (var db = new TohumBankasiContext())
            {
                var bitki = db.KullaniciBitkileris.Find(_takipBitkiId);
                if (bitki != null)
                {
                    lblBitkiAdi.Text = bitki.BitkiAdi;
                    this.Text = bitki.BitkiAdi + " - Detaylar";

                    // Resmi Yükle
                    string tamYol = Path.Combine(Application.StartupPath, bitki.GorselYolu);
                    if (File.Exists(tamYol))
                    {
                        using (var stream = new FileStream(tamYol, FileMode.Open, FileAccess.Read))
                        {
                            picBitkiResmi.Image = Image.FromStream(stream);
                        }
                    }
                }
            }
        }

        private void SohbetGecmisiniYukle()
        {
            using (var db = new TohumBankasiContext())
            {
                // Eski mesajları tarihe göre çek
                var mesajlar = db.BitkiSohbetGecmisis
                                 .Where(x => x.KullaniciBitkiId == _takipBitkiId)
                                 .OrderBy(x => x.MesajId) // Eskiden yeniye
                                 .ToList();

                foreach (var msj in mesajlar)
                {
                    bool kullaniciMi = (msj.Gonderen == "User");
                    MesajEkle(msj.Gonderen + ": " + msj.Mesaj, kullaniciMi);
                }
            }
        }

        private async void btnGonder_Click(object sender, EventArgs e)
        {
            string soru = txtMesaj.Text.Trim();
            if (string.IsNullOrEmpty(soru)) return;

            // 1. Ekrana ve Veritabanına KULLANICI mesajını ekle
            MesajEkle("SİZ: " + soru, true);
            MesajKaydet("User", soru);
            txtMesaj.Text = "";

            try
            {

                // 2. API'ye Gönder (Bitkinin resmini de bağlam gönderiyoruz ki tanısın)
                string apiKey = Properties.Settings.Default.KullaniciApiKey;
                // 1. Resmi bir listeye koy (Çünkü metot liste bekliyor)
                List<Image> gonderilecekResimler = new List<Image>();
                if (picBitkiResmi.Image != null)
                {
                    // Kopyasını oluştur ki GDI+ hatası olmasın
                    gonderilecekResimler.Add(new Bitmap(picBitkiResmi.Image));
                }

                // 2. API'ye Listeyi Gönder
                string cevap = await GeminiManager.BitkiAnalizEt(soru, gonderilecekResimler, apiKey);

                // 3. Ekrana ve Veritabanına AI mesajını ekle
                MesajEkle("GEMINI: " + cevap, false);
                MesajKaydet("AI", cevap);
            }
            catch (Exception ex)
            {
                MesajEkle("HATA: " + ex.Message, false);
            }
        }

        private void MesajKaydet(string gonderen, string mesaj)
        {
            using (var db = new TohumBankasiContext())
            {
                var yeniMesaj = new BitkiSohbetGecmisi
                {
                    KullaniciBitkiId = _takipBitkiId,
                    Gonderen = gonderen,
                    Mesaj = mesaj,
                    Tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                db.BitkiSohbetGecmisis.Add(yeniMesaj);
                db.SaveChanges();
            }
        }

        // (UcBitkiAsistani'ndaki metodun basitleştirilmiş hali)
        private void MesajEkle(string mesaj, bool kullaniciMi)
        {
            Document doc = chatEkrani.Document;
            doc.AppendText("\n");
            DocumentRange range = doc.AppendText(mesaj + "\n");

            ParagraphProperties pp = doc.BeginUpdateParagraphs(range);
            pp.Alignment = kullaniciMi ? ParagraphAlignment.Right : ParagraphAlignment.Left;
            doc.EndUpdateParagraphs(pp);

            CharacterProperties cp = doc.BeginUpdateCharacters(range);
            cp.ForeColor = kullaniciMi ? Color.DarkBlue : Color.Black;
            cp.Bold = kullaniciMi;
            doc.EndUpdateCharacters(cp);

            chatEkrani.ScrollToCaret();
        }

        // Rapor Oluştur Butonu
        private async void btnRaporAl_Click(object sender, EventArgs e)
        {
            // Tüm geçmişi çekip özetlemesini isteyeceğiz
            string talimat = "Şu ana kadar konuştuğumuz tüm bilgileri derleyerek bu bitkinin gelişimi hakkında bir ÖZET RAPOR yaz.";
            // (Bunu btnGonder_Click mantığıyla Gemini'ye gönderip cevabı ekrana basabilirsiniz)
            txtMesaj.Text = talimat;
            btnGonder.PerformClick(); // Otomatik gönder
        }
    }
}