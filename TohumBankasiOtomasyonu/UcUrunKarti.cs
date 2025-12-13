using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties; // Sözlük için (For Dictionary)

namespace TohumBankasiOtomasyonu
{
    public partial class UcUrunKarti : DevExpress.XtraEditors.XtraUserControl
    {
        // Kartın hangi bitkiyi tuttuğunu ve fiyatını hafızada tutalım
        // Let's keep in memory which plant the card holds and its price
        public int BitkiId { get; private set; }
        private double _hamFiyat; // Fiyatı sayısal olarak saklamak için (To store the price numerically)

        public UcUrunKarti()
        {
            InitializeComponent();
        }

        // Verileri Karta Doldurma Metodu
        // Method to Fill Data into Card
        public void BilgileriDoldur(int id, string ad, string bilimsel, double fiyat, int stok, string resimYolu)
        {
            BitkiId = id;
            _hamFiyat = fiyat; // Fiyatı sakla (ÖNEMLİ) (Store the price (IMPORTANT))

            lblUrunAdi.Text = ad;
            lblBilimselAd.Text = bilimsel;
            lblFiyat.Text = fiyat.ToString("C2"); // Ekranda ₺ ile göster (Show with ₺ on screen)
            lblStok.Text = $"Stok: {stok}";

            // Stok kontrolü
            // Stock control
            if (stok <= 0)
            {
                lblStok.Text = "TÜKENDİ / OUT OF STOCK";
                lblStok.ForeColor = Color.Red;
                btnSepeteEkle.Enabled = false; // Stok yoksa buton pasif (Button passive if no stock)
            }

            // Resmi Yükle
            // Load Image
            if (!string.IsNullOrEmpty(resimYolu))
            {
                try
                {
                    string tamYol = Path.Combine(Application.StartupPath, resimYolu);
                    if (File.Exists(tamYol))
                    {
                        using (var stream = new FileStream(tamYol, FileMode.Open, FileAccess.Read))
                        {
                            picUrunResmi.Image = Image.FromStream(stream);
                        }
                    }
                }
                catch { /* Resim hatası programı kırmasın */ /* Image error should not break the program */ }
            }
        }

        // --- SEPETE EKLEME BUTONU ---
        // --- ADD TO CART BUTTON ---
        private void btnSepeteEkle_Click(object sender, EventArgs e)
        {
            // Eğer Session'daki kullanıcı null ise, kimse giriş yapmamış demektir.
            // If user in Session is null, it means no one has logged in.
            if (Session.AktifKullanici == null)
            {
                XtraMessageBox.Show(Resources.HataGirisGerekli, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // İşlemi durdur, sepete ekleme yapma. (Stop the operation, do not add to cart.)
            }

            // 1. Sepet Yöneticisine ürünü gönder ve sonucu al
            // 1. Send product to Cart Manager and get the result
            bool eklendi = SepetManager.Ekle(this.BitkiId, lblUrunAdi.Text, _hamFiyat, 1);

            if (eklendi)
            {
                // Başarılı
                // Successful
                XtraMessageBox.Show($"{lblUrunAdi.Text} - {Resources.MsgSepeteEklandi}",
                                    Resources.BasariBaslik,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            }
            else
            {
                // Başarısız (Stok Yetersiz)
                // (Sözlüğe "HataYetersizStok" eklemiştik)
                // Failed (Insufficient Stock)
                // (We added "HataYetersizStok" to the dictionary)
                XtraMessageBox.Show($"{Resources.HataYetersizStok}",
                                    Resources.HataBaslik,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
            }

        }

        private void picUrunResmi_Click(object sender, EventArgs e)
        {
            // Eğer resim yoksa açma
            // If no image, do not open
            if (picUrunResmi.Image == null) return;

            // Büyük resim formunu oluştur ve resmi gönder
            // Create large image form and send the image
            FormResimGoruntule frm = new FormResimGoruntule(picUrunResmi.Image);

            // Formu aç
            // Open form
            frm.ShowDialog();
        }
    }
}