using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties; // Sözlük için

namespace TohumBankasiOtomasyonu
{
    public partial class UcUrunKarti : DevExpress.XtraEditors.XtraUserControl
    {
        // Kartın hangi bitkiyi tuttuğunu ve fiyatını hafızada tutalım
        public int BitkiId { get; private set; }
        private double _hamFiyat; // Fiyatı sayısal olarak saklamak için

        public UcUrunKarti()
        {
            InitializeComponent();
        }

        // Verileri Karta Doldurma Metodu
        public void BilgileriDoldur(int id, string ad, string bilimsel, double fiyat, int stok, string resimYolu)
        {
            BitkiId = id;
            _hamFiyat = fiyat; // Fiyatı sakla (ÖNEMLİ)

            lblUrunAdi.Text = ad;
            lblBilimselAd.Text = bilimsel;
            lblFiyat.Text = fiyat.ToString("C2"); // Ekranda ₺ ile göster
            lblStok.Text = $"Stok: {stok}";

            // Stok kontrolü
            if (stok <= 0)
            {
                lblStok.Text = "TÜKENDİ / OUT OF STOCK";
                lblStok.ForeColor = Color.Red;
                btnSepeteEkle.Enabled = false; // Stok yoksa buton pasif
            }

            // Resmi Yükle
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
                catch { /* Resim hatası programı kırmasın */ }
            }
        }

        // --- SEPETE EKLEME BUTONU ---
        private void btnSepeteEkle_Click(object sender, EventArgs e)
        {
            // Eğer Session'daki kullanıcı null ise, kimse giriş yapmamış demektir.
            if (Session.AktifKullanici == null)
            {
                XtraMessageBox.Show(Resources.HataGirisGerekli, Resources.HataBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // İşlemi durdur, sepete ekleme yapma.
            }

            // 1. Sepet Yöneticisine ürünü gönder ve sonucu al
            bool eklendi = SepetManager.Ekle(this.BitkiId, lblUrunAdi.Text, _hamFiyat, 1);

            if (eklendi)
            {
                // Başarılı
                XtraMessageBox.Show($"{lblUrunAdi.Text} - {Resources.MsgSepeteEklandi}",
                                    Resources.BasariBaslik,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            }
            else
            {
                // Başarısız (Stok Yetersiz)
                // (Sözlüğe "HataYetersizStok" eklemiştik)
                XtraMessageBox.Show($"{Resources.HataYetersizStok}",
                                    Resources.HataBaslik,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
            }

        }

        private void picUrunResmi_Click(object sender, EventArgs e)
        {
            // Eğer resim yoksa açma
            if (picUrunResmi.Image == null) return;

            // Büyük resim formunu oluştur ve resmi gönder
            FormResimGoruntule frm = new FormResimGoruntule(picUrunResmi.Image);

            // Formu aç
            frm.ShowDialog();
        }
    }
}