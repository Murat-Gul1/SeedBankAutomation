using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class UcBitkiTakip : DevExpress.XtraEditors.XtraUserControl
    {
        public UcBitkiTakip()
        {
            InitializeComponent();
        }

        private void UygulaDil()
        {
            lblBaslik.Text = Resources.lblBitkilerimBaslik;
            btnYeniEkle.Text = Resources.btnYeniTakipEkle;
        }

        private void UcBitkiTakip_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                UygulaDil();
                ListeyiGetir();
            }
        }

        public void DiliYenile()
        {
            UygulaDil();
            ListeyiGetir();
        }

        // --- ANA LİSTELEME METODU ---
        public void ListeyiGetir()
        {
            // 1. Paneli temizle
            flowPanelBitkilerim.Controls.Clear();

            // Güvenlik: Kullanıcı yoksa işlem yapma
            if (Session.AktifKullanici == null) return;

            try
            {
                using (var db = new TohumBankasiContext())
                {
                    // 2. Sadece BU KULLANICIYA ait bitkileri çek
                    var bitkilerim = db.KullaniciBitkileris
                                       .Where(x => x.KullaniciId == Session.AktifKullanici.KullaniciId)
                                       .OrderByDescending(x => x.Id) // En son eklenen en başa
                                       .ToList();

                    // 3. Hiç bitki yoksa?
                    if (bitkilerim.Count == 0)
                    {
                        // Ekrana "Henüz bitkiniz yok" yazan bir Label ekleyebiliriz
                        LabelControl lblBos = new LabelControl();
                        lblBos.Text = Resources.HataBitkiYok; // "Henüz takip ettiğiniz bir bitki yok."
                        lblBos.Appearance.Font = new Font("Tahoma", 12, FontStyle.Italic);
                        lblBos.Appearance.ForeColor = Color.Gray;
                        lblBos.Padding = new Padding(20);
                        flowPanelBitkilerim.Controls.Add(lblBos);
                        return;
                    }

                    // 4. Kartları Oluştur ve Ekle
                    foreach (var bitki in bitkilerim)
                    {
                        UcKullaniciBitkiKarti kart = new UcKullaniciBitkiKarti();

                        // Kartı doldur
                        kart.BilgileriDoldur(
                            bitki.Id,
                            bitki.BitkiAdi,
                            bitki.OlusturmaTarihi, // String olarak kaydetmiştik
                            bitki.GorselYolu
                        );

                        kart.Margin = new Padding(15); // Kartlar arası boşluk

                        // --- OLAYLARI BAĞLA (EVENTS) ---

                        // Kart silinirse listeyi yenile
                        kart.Silindi += Kart_Silindi;

                        // Detaya tıklanırsa detay formunu aç
                        kart.DetayIstendi += Kart_DetayIstendi;

                        // Panele ekle
                        flowPanelBitkilerim.Controls.Add(kart);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Liste yüklenirken hata: " + ex.Message);
            }
        }

        // --- OLAY YÖNETİMİ ---

        private void Kart_Silindi(object sender, EventArgs e)
        {
            // Bir kart silindiğinde, listeyi veritabanından tekrar çekip yeniliyoruz
            ListeyiGetir();
        }

        private void Kart_DetayIstendi(object sender, EventArgs e)
        {
            UcKullaniciBitkiKarti tiklananKart = (UcKullaniciBitkiKarti)sender;
            int bitkiId = tiklananKart.TakipBitkiId;

            // Formu ID ile aç
            FormBitkiDetay detayFormu = new FormBitkiDetay(bitkiId);
            detayFormu.ShowDialog();
        }

        private void btnYeniEkle_Click(object sender, EventArgs e)
        {
            FormYeniBitkiEkle frm = new FormYeniBitkiEkle();
            frm.ShowDialog();

            // Ekleme bittikten sonra listeyi yenile
            ListeyiGetir();
        }
    }
}