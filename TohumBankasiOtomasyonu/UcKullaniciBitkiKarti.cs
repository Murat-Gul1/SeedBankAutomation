using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class UcKullaniciBitkiKarti : DevExpress.XtraEditors.XtraUserControl
    {
        // Bu kart hangi bitkiyi tutuyor?
        // Which plant does this card hold?
        public int TakipBitkiId { get; private set; }

        // Ana listeyi yenilemek için bir olay (Event) tanımlıyoruz
        // We define an event to refresh the main list
        public event EventHandler Silindi;
        public event EventHandler DetayIstendi;

        public UcKullaniciBitkiKarti()
        {
            InitializeComponent();
        }

        private void UygulaDil()
        {
            btnDetay.ToolTip = Resources.btnBitkiDetay_ToolTip;
            btnSil.ToolTip = Resources.btnBitkiSil_ToolTip;
        }

        // Verileri Doldurma Metodu
        // Data Filling Method
        public void BilgileriDoldur(int id, string ad, string tarih, string resimYolu)
        {
            TakipBitkiId = id;
            lblBitkiAdi.Text = ad;
            lblTarih.Text = tarih;

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
                            picBitkiResmi.Image = Image.FromStream(stream);
                        }
                    }
                }
                catch { }
            }

            UygulaDil();
        }

        // --- SİLME İŞLEMİ ---
        // --- DELETION PROCESS ---
        private void btnSil_Click(object sender, EventArgs e)
        {
            // Onay al
            // Get confirmation
            if (XtraMessageBox.Show(Resources.MsgResimSilOnay, Resources.BaslikSil, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (var db = new TohumBankasiContext())
                    {
                        // Veritabanından sil (Kullanici_Bitkileri tablosu)
                        // (Not: Tablo adını Scaffold sonrası kontrol edin, 'KullaniciBitkileris' olabilir)
                        // Delete from database (Kullanici_Bitkileri table)
                        // (Note: Check the table name after Scaffold, it might be 'KullaniciBitkileris')
                        var bitki = db.KullaniciBitkileris.Find(TakipBitkiId);
                        if (bitki != null)
                        {
                            db.KullaniciBitkileris.Remove(bitki);
                            db.SaveChanges();

                            // Ana forma haber ver ki listeyi yenilesin
                            // Notify the main form to refresh the list
                            if (Silindi != null) Silindi(this, EventArgs.Empty);
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        // --- DETAY BUTONU ---
        // --- DETAIL BUTTON ---
        private void btnDetay_Click(object sender, EventArgs e)
        {
            // Ana forma "Bu karta tıklandı, detayını aç" haberi ver
            // Signal main form "This card clicked, open details"
            if (DetayIstendi != null) DetayIstendi(this, EventArgs.Empty);
        }
    }
}