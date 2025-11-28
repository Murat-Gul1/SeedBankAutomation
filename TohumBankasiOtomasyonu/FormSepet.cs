using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;
namespace TohumBankasiOtomasyonu
{
    public partial class FormSepet : DevExpress.XtraEditors.XtraForm
    {
        // Ana forma "Satış Bitti" haberini vermek için bir olay tanımlıyoruz
        public event EventHandler SatisYapildi;

        private Kullanicilar _satinAlanKullanici;

        public FormSepet(Kullanicilar kullanici) : this()
        {
            _satinAlanKullanici = kullanici;
        }
        public FormSepet()
        {
            InitializeComponent();
        }

        // --- DİL AYARLARI ---
        private void UygulaDil()
        {
            // 1. Form Başlığı
            this.Text = Resources.FormSepet_Title;

            // 3. Label Metinleri
            lblBaslikToplam.Text = Resources.lblSepetGenelToplam;

            // 4. Tablo Başlıkları (Sütunlar oluşmuşsa)
            var view = gridSepet.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                if (view.Columns["Resim"] != null) view.Columns["Resim"].Caption = Resources.colSepetResim;
                if (view.Columns["UrunAdi"] != null) view.Columns["UrunAdi"].Caption = Resources.colSepetUrunAdi;
                if (view.Columns["Fiyat"] != null) view.Columns["Fiyat"].Caption = Resources.colSepetFiyat;
                if (view.Columns["Adet"] != null) view.Columns["Adet"].Caption = Resources.colSepetAdet;
                if (view.Columns["Toplam"] != null) view.Columns["Toplam"].Caption = Resources.colSepetToplam;
            }
            // Butonlarda yazı İSTEMİYORUZ, sadece İpucu (ToolTip) olsun:
            btnSatinAl.ToolTip = Resources.btnSepetSatinAl;
            btnSepetTemizle.ToolTip = Resources.btnSepetTemizle;
        }

        // --- VERİLERİ LİSTELEME ---
        public void SepetiListele()
        {
            // SepetManager'dan listeyi al
            var sepetListesi = SepetManager.ListeyiGetir();

            using (var db = new TohumBankasiContext())
            {
                // Sepetteki ürünlerin resimlerini veritabanından bulup eşleştiriyoruz
                var goruntulenecekListe = (from s in sepetListesi
                                           join g in db.BitkiGorselleris on s.BitkiId equals g.BitkiId
                                           where g.AnaGorsel == 1
                                           select new
                                           {
                                               Id = s.BitkiId,
                                               ResimYolu = g.DosyaYolu,
                                               UrunAdi = s.UrunAdi,
                                               Fiyat = s.BirimFiyat,
                                               Adet = s.Adet,
                                               Toplam = s.ToplamTutar
                                           }).ToList();

                // Resim yollarını (string) gerçek resimlere (Image) çeviriyoruz
                var gridVerisi = goruntulenecekListe.Select(x => new
                {
                    x.Id,
                    Resim = ResmiYukle(x.ResimYolu), // Aşağıdaki metot resmi yükler
                    x.UrunAdi,
                    x.Fiyat,
                    x.Adet,
                    x.Toplam
                }).ToList();

                gridSepet.DataSource = gridVerisi;

                // Genel Toplamı Yazdır
                lblGenelToplam.Text = SepetManager.GenelToplam().ToString("C2");
            }

            // Görsel ayarlar ve Dil
            GridAyarlariniYap();
            UygulaDil();
        }

        // Resim Yükleme Yardımcısı (Dosya kilitlemeden)
        private Image ResmiYukle(string yol)
        {
            try
            {
                string tamYol = Path.Combine(Application.StartupPath, yol);
                if (File.Exists(tamYol))
                {
                    using (var stream = new FileStream(tamYol, FileMode.Open, FileAccess.Read))
                    {
                        Image img = Image.FromStream(stream);
                        // Performans için resmi küçült (Thumbnail)
                        return new Bitmap(img, new Size(50, 50));
                    }
                }
            }
            catch { }
            return null;
        }

        private void GridAyarlariniYap()
        {
            var view = gridSepet.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                // ID'yi gizle
                if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;

                // Resim sütunu genişliği ve satır yüksekliği
                if (view.Columns["Resim"] != null) view.Columns["Resim"].Width = 60;
                view.RowHeight = 60;

                // Sadece okunabilir yap
                view.OptionsBehavior.Editable = false;
                // Birim Fiyat Sütunu
                if (view.Columns["Fiyat"] != null)
                {
                    view.Columns["Fiyat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    view.Columns["Fiyat"].DisplayFormat.FormatString = "c2"; // c2 = Currency (Para Birimi) 2 basamak
                }

                // Toplam Sütunu
                if (view.Columns["Toplam"] != null)
                {
                    view.Columns["Toplam"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    view.Columns["Toplam"].DisplayFormat.FormatString = "c2"; // c2 = ₺ simgesi ve kuruşları otomatik koyar
                }
            }
        }

        private void FormSepet_Load(object sender, EventArgs e)
        {
            SepetiListele();
        }

        private void btnUrunSil_Click(object sender, EventArgs e)
        {
            // 1. Seçili satırın ID'sini al
            var view = gridSepet.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            var seciliIdObj = view.GetFocusedRowCellValue("Id");

            if (seciliIdObj == null)
            {
                // Eğer satır seçilmemişse uyar
                XtraMessageBox.Show(Resources.HataSatirSecilmedi, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bitkiId = Convert.ToInt32(seciliIdObj);

            // 2. YENİ MANTIK: Sepet Yöneticisinden "Adet Düşür"ü çağır
            SepetManager.AdetDusur(bitkiId);

            // 3. Listeyi Yenile (Adet azaldı mı yoksa silindi mi görmek için)
            SepetiListele();
        }

        private void btnSepetTemizle_Click(object sender, EventArgs e)
        {
            // Sepet zaten boşsa işlem yapma
            if (SepetManager.ListeyiGetir().Count == 0) return;

            // Onay iste (Dile göre onay mesajı)
            if (XtraMessageBox.Show(Resources.OnaySepetBosalt, Resources.OnayBaslik, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // 1. Sepeti tamamen temizle
                SepetManager.Temizle();

                // 2. Listeyi Yenile
                SepetiListele();
            }
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnSatinAl_Click(object sender, EventArgs e)
        {
            // 1. Sepet Boş mu?
            var sepet = SepetManager.ListeyiGetir();
            if (sepet.Count == 0) return;

            try
            {
                using (var db = new TohumBankasiContext())
                {
                    // --- AŞAMA 1: STOK KONTROLÜ ---
                    foreach (var urun in sepet)
                    {
                        var dbUrun = db.Bitkilers.Find(urun.BitkiId);
                        if (dbUrun == null || dbUrun.StokAdedi < urun.Adet)
                        {
                            XtraMessageBox.Show($"{Resources.HataYetersizStok} {urun.UrunAdi}", Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // İşlemi iptal et
                        }
                    }

                    // --- AŞAMA 2: SATIŞI KAYDETME ---

                    // a. Satış Başlığı (Fiş)
                    Satislar yeniSatis = new Satislar
                    {
                        KullaniciId = _satinAlanKullanici.KullaniciId,
                        SatisTarihi = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        ToplamTutar = SepetManager.GenelToplam(),
                        MakbuzNo = "TR-" + DateTime.Now.Ticks.ToString() // Benzersiz Fiş No
                    };
                    db.Satislars.Add(yeniSatis);
                    db.SaveChanges(); // ID oluşsun diye kaydediyoruz

                    // b. Satış Detayları ve Stok Düşme
                    foreach (var urun in sepet)
                    {
                        // Detay ekle
                        SatisDetaylari detay = new SatisDetaylari
                        {
                            SatisId = yeniSatis.SatisId,
                            BitkiId = urun.BitkiId,
                            Miktar = urun.Adet,
                            SatisAnindakiFiyat = urun.BirimFiyat
                        };
                        db.SatisDetaylaris.Add(detay);

                        // Stok düş
                        var dbUrun = db.Bitkilers.Find(urun.BitkiId);
                        dbUrun.StokAdedi -= urun.Adet;
                    }
                    db.SaveChanges();

                    // --- AŞAMA 3: BLOCKCHAIN KAYDI (Zincirleme) ---

                    // a. Önceki bloğun Hash'ini bul
                    // (Veritabanındaki son bloğu al, yoksa "GENESIS_BLOCK" varsay)
                    var sonBlok = db.SahteBlokzincirs.OrderByDescending(x => x.BlokId).FirstOrDefault();
                    string oncekiHash = (sonBlok != null) ? sonBlok.Hash : "0000000000000000";

                    // b. Yeni veriyi hazırla (Satış ID, Tutar, Tarih)
                    // (Bunu sözlükten formatlı çekiyoruz: "Sale ID: {0}...")
                    string blokVerisi = string.Format(Resources.BlokzincirVerisi, yeniSatis.SatisId, yeniSatis.ToplamTutar, yeniSatis.SatisTarihi);

                    // c. Şifrele (Önceki Hash + Tarih + Veri = Yeni Hash)
                    string hamVeri = oncekiHash + yeniSatis.SatisTarihi + blokVerisi;
                    string yeniHash = ComputeSha256Hash(hamVeri);

                    // d. Zincire Ekle
                    SahteBlokzincir yeniBlok = new SahteBlokzincir
                    {
                        ZamanDamgasi = yeniSatis.SatisTarihi,
                        OncekiHash = oncekiHash,
                        Veri = blokVerisi,
                        Hash = yeniHash,
                        Nonce = 0 // Proof-of-work yapmadığımız için 0
                    };
                    db.SahteBlokzincirs.Add(yeniBlok);
                    db.SaveChanges();

                    // --- AŞAMA 4: BİTİŞ ---
                    XtraMessageBox.Show(Resources.SatinAlmaBasarili, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SepetManager.Temizle(); // Sepeti boşalt
                    SepetiListele(); // Ekranı güncelle
                    // 2. Eğer bizi dinleyen varsa (Form1), ona "Satış Yapıldı" sinyali gönder
                    if (SatisYapildi != null)
                    {
                        SatisYapildi(this, EventArgs.Empty);
                    }
                    this.Close(); // Formu kapat
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Resources.GenelHata + " " + ex.Message, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}