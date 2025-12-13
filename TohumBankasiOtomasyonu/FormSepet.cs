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
        // Defining an event to notify the main form that "Sale is Finished"
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
        // --- LANGUAGE SETTINGS ---
        private void UygulaDil()
        {
            // 1. Form Başlığı
            // 1. Form Title
            this.Text = Resources.FormSepet_Title;

            // 3. Label Metinleri
            // 3. Label Texts
            lblBaslikToplam.Text = Resources.lblSepetGenelToplam;

            // 4. Tablo Başlıkları (Sütunlar oluşmuşsa)
            // 4. Table Headers (If columns are created)
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
            // We DO NOT WANT text on buttons, only ToolTip:
            btnSatinAl.ToolTip = Resources.btnSepetSatinAl;
            btnSepetTemizle.ToolTip = Resources.btnSepetTemizle;
        }

        // --- VERİLERİ LİSTELEME ---
        // --- LISTING DATA ---
        public void SepetiListele()
        {
            // SepetManager'dan listeyi al
            // Get list from SepetManager
            var sepetListesi = SepetManager.ListeyiGetir();

            using (var db = new TohumBankasiContext())
            {
                // Sepetteki ürünlerin resimlerini veritabanından bulup eşleştiriyoruz
                // We find and match the images of the products in the cart from the database
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
                // We convert image paths (string) to real images (Image)
                var gridVerisi = goruntulenecekListe.Select(x => new
                {
                    x.Id,
                    Resim = ResmiYukle(x.ResimYolu), // Aşağıdaki metot resmi yükler (The method below loads the image)
                    x.UrunAdi,
                    x.Fiyat,
                    x.Adet,
                    x.Toplam
                }).ToList();

                gridSepet.DataSource = gridVerisi;

                // Genel Toplamı Yazdır
                // Print Grand Total
                lblGenelToplam.Text = SepetManager.GenelToplam().ToString("C2");
            }

            // Görsel ayarlar ve Dil
            // Visual settings and Language
            GridAyarlariniYap();
            UygulaDil();
        }

        // Resim Yükleme Yardımcısı (Dosya kilitlemeden)
        // Image Upload Helper (Without locking file)
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
                        // Downsize image for performance (Thumbnail)
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
                // Hide ID
                if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;

                // Resim sütunu genişliği ve satır yüksekliği
                // Image column width and row height
                if (view.Columns["Resim"] != null) view.Columns["Resim"].Width = 60;
                view.RowHeight = 60;

                // Sadece okunabilir yap
                // Make read-only
                view.OptionsBehavior.Editable = false;
                // Birim Fiyat Sütunu
                // Unit Price Column
                if (view.Columns["Fiyat"] != null)
                {
                    view.Columns["Fiyat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    view.Columns["Fiyat"].DisplayFormat.FormatString = "c2"; // c2 = Currency (Para Birimi) 2 basamak (c2 = Currency 2 digits)
                }

                // Toplam Sütunu
                // Total Column
                if (view.Columns["Toplam"] != null)
                {
                    view.Columns["Toplam"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    view.Columns["Toplam"].DisplayFormat.FormatString = "c2"; // c2 = ₺ simgesi ve kuruşları otomatik koyar (c2 = automatically puts ₺ symbol and cents)
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
            // 1. Get ID of selected row
            var view = gridSepet.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            var seciliIdObj = view.GetFocusedRowCellValue("Id");

            if (seciliIdObj == null)
            {
                // Eğer satır seçilmemişse uyar
                // Warn if row is not selected
                XtraMessageBox.Show(Resources.HataSatirSecilmedi, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bitkiId = Convert.ToInt32(seciliIdObj);

            // 2. YENİ MANTIK: Sepet Yöneticisinden "Adet Düşür"ü çağır
            // 2. NEW LOGIC: Call "Decrease Quantity" from Cart Manager
            SepetManager.AdetDusur(bitkiId);

            // 3. Listeyi Yenile (Adet azaldı mı yoksa silindi mi görmek için)
            // 3. Refresh List (To see if quantity decreased or was deleted)
            SepetiListele();
        }

        private void btnSepetTemizle_Click(object sender, EventArgs e)
        {
            // Sepet zaten boşsa işlem yapma
            // Do not take action if cart is already empty
            if (SepetManager.ListeyiGetir().Count == 0) return;

            // Onay iste (Dile göre onay mesajı)
            // Ask for confirmation (Confirmation message according to language)
            if (XtraMessageBox.Show(Resources.OnaySepetBosalt, Resources.OnayBaslik, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // 1. Sepeti tamamen temizle
                // 1. Completely clear cart
                SepetManager.Temizle();

                // 2. Listeyi Yenile
                // 2. Refresh List
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
            // 1. Is Cart Empty?
            var sepet = SepetManager.ListeyiGetir();
            if (sepet.Count == 0) return;

            try
            {
                using (var db = new TohumBankasiContext())
                {
                    // --- AŞAMA 1: STOK KONTROLÜ ---
                    // --- STAGE 1: STOCK CONTROL ---
                    foreach (var urun in sepet)
                    {
                        var dbUrun = db.Bitkilers.Find(urun.BitkiId);
                        if (dbUrun == null || dbUrun.StokAdedi < urun.Adet)
                        {
                            XtraMessageBox.Show($"{Resources.HataYetersizStok} {urun.UrunAdi}", Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // İşlemi iptal et (Cancel operation)
                        }
                    }

                    // --- AŞAMA 2: SATIŞI KAYDETME ---
                    // --- STAGE 2: SAVING SALE ---

                    // a. Satış Başlığı (Fiş)
                    // a. Sale Header (Receipt)
                    Satislar yeniSatis = new Satislar
                    {
                        KullaniciId = _satinAlanKullanici.KullaniciId,
                        SatisTarihi = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        ToplamTutar = SepetManager.GenelToplam(),
                        MakbuzNo = "TR-" + DateTime.Now.Ticks.ToString() // Benzersiz Fiş No (Unique Receipt No)
                    };
                    db.Satislars.Add(yeniSatis);
                    db.SaveChanges(); // ID oluşsun diye kaydediyoruz (Saving so ID is generated)

                    // b. Satış Detayları ve Stok Düşme
                    // b. Sale Details and Stock Deduction
                    foreach (var urun in sepet)
                    {
                        // Detay ekle
                        // Add detail
                        SatisDetaylari detay = new SatisDetaylari
                        {
                            SatisId = yeniSatis.SatisId,
                            BitkiId = urun.BitkiId,
                            Miktar = urun.Adet,
                            SatisAnindakiFiyat = urun.BirimFiyat
                        };
                        db.SatisDetaylaris.Add(detay);

                        // Stok düş
                        // Deduct stock
                        var dbUrun = db.Bitkilers.Find(urun.BitkiId);
                        dbUrun.StokAdedi -= urun.Adet;
                    }
                    db.SaveChanges();

                    // --- AŞAMA 3: BLOCKCHAIN KAYDI (Zincirleme) ---
                    // --- STAGE 3: BLOCKCHAIN RECORDING (Chaining) ---

                    // a. Önceki bloğun Hash'ini bul
                    // a. Find Hash of previous block
                    // (Veritabanındaki son bloğu al, yoksa "GENESIS_BLOCK" varsay)
                    // (Get last block from database, otherwise assume "GENESIS_BLOCK")
                    var sonBlok = db.SahteBlokzincirs.OrderByDescending(x => x.BlokId).FirstOrDefault();
                    string oncekiHash = (sonBlok != null) ? sonBlok.Hash : "0000000000000000";

                    // b. Yeni veriyi hazırla (Satış ID, Tutar, Tarih)
                    // b. Prepare new data (Sale ID, Amount, Date)
                    // (Bunu sözlükten formatlı çekiyoruz: "Sale ID: {0}...")
                    // (We fetch this formatted from dictionary: "Sale ID: {0}...")
                    string blokVerisi = string.Format(Resources.BlokzincirVerisi, yeniSatis.SatisId, yeniSatis.ToplamTutar, yeniSatis.SatisTarihi);

                    // c. Şifrele (Önceki Hash + Tarih + Veri = Yeni Hash)
                    // c. Encrypt (Previous Hash + Date + Data = New Hash)
                    string hamVeri = oncekiHash + yeniSatis.SatisTarihi + blokVerisi;
                    string yeniHash = ComputeSha256Hash(hamVeri);

                    // d. Zincire Ekle
                    // d. Add to Chain
                    SahteBlokzincir yeniBlok = new SahteBlokzincir
                    {
                        ZamanDamgasi = yeniSatis.SatisTarihi,
                        OncekiHash = oncekiHash,
                        Veri = blokVerisi,
                        Hash = yeniHash,
                        Nonce = 0 // Proof-of-work yapmadığımız için 0 (0 since we don't do Proof-of-work)
                    };
                    db.SahteBlokzincirs.Add(yeniBlok);
                    db.SaveChanges();

                    // --- AŞAMA 4: BİTİŞ ---
                    // --- STAGE 4: FINISH ---
                    XtraMessageBox.Show(Resources.SatinAlmaBasarili, Resources.BasariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    SepetManager.Temizle(); // Sepeti boşalt (Empty cart)
                    SepetiListele(); // Ekranı güncelle (Update screen)
                    // 2. Eğer bizi dinleyen varsa (Form1), ona "Satış Yapıldı" sinyali gönder
                    // 2. If someone is listening to us (Form1), send "Sale Made" signal to it
                    if (SatisYapildi != null)
                    {
                        SatisYapildi(this, EventArgs.Empty);
                    }
                    this.Close(); // Formu kapat (Close form)
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Resources.GenelHata + " " + ex.Message, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}