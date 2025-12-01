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
        public int TakipBitkiId { get; private set; }

        // Ana listeyi yenilemek için bir olay (Event) tanımlıyoruz
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
        public void BilgileriDoldur(int id, string ad, string tarih, string resimYolu)
        {
            TakipBitkiId = id;
            lblBitkiAdi.Text = ad;
            lblTarih.Text = tarih;

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
                            picBitkiResmi.Image = Image.FromStream(stream);
                        }
                    }
                }
                catch { }
            }

            UygulaDil();
        }

        // --- SİLME İŞLEMİ ---
        private void btnSil_Click(object sender, EventArgs e)
        {
            // Onay al
            if (XtraMessageBox.Show(Resources.MsgResimSilOnay, Resources.BaslikSil, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (var db = new TohumBankasiContext())
                    {
                        // Veritabanından sil (Kullanici_Bitkileri tablosu)
                        // (Not: Tablo adını Scaffold sonrası kontrol edin, 'KullaniciBitkileris' olabilir)
                        var bitki = db.KullaniciBitkileris.Find(TakipBitkiId);
                        if (bitki != null)
                        {
                            db.KullaniciBitkileris.Remove(bitki);
                            db.SaveChanges();

                            // Ana forma haber ver ki listeyi yenilesin
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
        private void btnDetay_Click(object sender, EventArgs e)
        {
            // Ana forma "Bu karta tıklandı, detayını aç" haberi ver
            if (DetayIstendi != null) DetayIstendi(this, EventArgs.Empty);
        }
    }
}