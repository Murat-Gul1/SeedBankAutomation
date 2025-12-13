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
        // --- MAIN LISTING METHOD ---
        public void ListeyiGetir()
        {
            // 1. Paneli temizle
            // 1. Clear the panel
            flowPanelBitkilerim.Controls.Clear();

            // Güvenlik: Kullanıcı yoksa işlem yapma
            // Security: If no user, do not process
            if (Session.AktifKullanici == null) return;

            try
            {
                using (var db = new TohumBankasiContext())
                {
                    // 2. Sadece BU KULLANICIYA ait bitkileri çek
                    // 2. Fetch plants belonging ONLY to THIS USER
                    var bitkilerim = db.KullaniciBitkileris
                                       .Where(x => x.KullaniciId == Session.AktifKullanici.KullaniciId)
                                       .OrderByDescending(x => x.Id) // En son eklenen en başa (Newest added first)
                                       .ToList();

                    // 3. Hiç bitki yoksa?
                    // 3. If no plants?
                    if (bitkilerim.Count == 0)
                    {
                        // Ekrana "Henüz bitkiniz yok" yazan bir Label ekleyebiliriz
                        // We can add a Label saying "You have no plants yet" to the screen
                        LabelControl lblBos = new LabelControl();
                        lblBos.Text = Resources.HataBitkiYok; // "Henüz takip ettiğiniz bir bitki yok." (You don't have any plants you follow yet.)
                        lblBos.Appearance.Font = new Font("Tahoma", 12, FontStyle.Italic);
                        lblBos.Appearance.ForeColor = Color.Gray;
                        lblBos.Padding = new Padding(20);
                        flowPanelBitkilerim.Controls.Add(lblBos);
                        return;
                    }

                    // 4. Kartları Oluştur ve Ekle
                    // 4. Create and Add Cards
                    foreach (var bitki in bitkilerim)
                    {
                        UcKullaniciBitkiKarti kart = new UcKullaniciBitkiKarti();

                        // Kartı doldur
                        // Fill the card
                        kart.BilgileriDoldur(
                            bitki.Id,
                            bitki.BitkiAdi,
                            bitki.OlusturmaTarihi, // String olarak kaydetmiştik (We saved it as String)
                            bitki.GorselYolu
                        );

                        kart.Margin = new Padding(15); // Kartlar arası boşluk (Space between cards)

                        // --- OLAYLARI BAĞLA (EVENTS) ---
                        // --- POOL EVENTS ---

                        // Kart silinirse listeyi yenile
                        // Refresh list if card is deleted
                        kart.Silindi += Kart_Silindi;

                        // Detaya tıklanırsa detay formunu aç
                        // Open details form if details clicked
                        kart.DetayIstendi += Kart_DetayIstendi;

                        // Panele ekle
                        // Add to panel
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
        // --- EVENT MANAGEMENT ---

        private void Kart_Silindi(object sender, EventArgs e)
        {
            // Bir kart silindiğinde, listeyi veritabanından tekrar çekip yeniliyoruz
            // When a card is deleted, we fetch the list from the database again and refresh it
            ListeyiGetir();
        }

        private void Kart_DetayIstendi(object sender, EventArgs e)
        {
            UcKullaniciBitkiKarti tiklananKart = (UcKullaniciBitkiKarti)sender;
            int bitkiId = tiklananKart.TakipBitkiId;

            // Formu ID ile aç
            // Open form with ID
            FormBitkiDetay detayFormu = new FormBitkiDetay(bitkiId);
            detayFormu.ShowDialog();
        }

        private void btnYeniEkle_Click(object sender, EventArgs e)
        {
            FormYeniBitkiEkle frm = new FormYeniBitkiEkle();
            frm.ShowDialog();

            // Ekleme bittikten sonra listeyi yenile
            // Refresh list after adding is finished
            ListeyiGetir();
        }
    }
}