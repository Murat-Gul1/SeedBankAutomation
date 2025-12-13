using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid; // RowStyle için
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TohumBankasiOtomasyonu.Models;
using TohumBankasiOtomasyonu.Properties;

namespace TohumBankasiOtomasyonu
{
    public partial class UcStokYonetimi : DevExpress.XtraEditors.XtraUserControl
    {
        private int _seciliBitkiId = 0; // Hangi bitkiye stok ekleyeceğiz?

        public UcStokYonetimi()
        {
            InitializeComponent();
        }

        // 1. Listeyi Getir
        // 1. Get the List
        public void StoklariListele()
        {
            using (var db = new TohumBankasiContext())
            {
                string aktifDil = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

                // Tüm bitkileri çek (Left Join mantığıyla, resmi olmasa da gelsin)
                // Fetch all plants (Using Left Join logic, so even without image it returns)
                var list = (from b in db.Bitkilers
                            join c in db.BitkiCevirileris on b.BitkiId equals c.BitkiId
                            where c.DilKodu == aktifDil
                            // Resim için Left Join
                            // Left Join for Image
                            join g in db.BitkiGorselleris.Where(x => x.AnaGorsel == 1)
                                 on b.BitkiId equals g.BitkiId into gorselGrubu
                            from img in gorselGrubu.DefaultIfEmpty()
                            select new
                            {
                                ID = b.BitkiId,
                                Urun = c.BitkiAdi,
                                Stok = b.StokAdedi,
                                ResimYolu = (img == null ? "" : img.DosyaYolu)
                            }).ToList();

                gridStokListesi.DataSource = list;
            }
        }

        private void UcStokYonetimi_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                StoklariListele();
            }
        }

        // 2. Renklendirme (RowStyle Olayı)
        // 2. Coloring (RowStyle Event)
        // Tasarımcıda GridView -> Events -> RowStyle olayına bu kodu bağlayın!
        // Connect this code to GridView -> Events -> RowStyle event in Designer!
        private void gridViewStok_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // "Stok" sütunundaki değeri al
                // Get the value in "Stok" (Stock) column
                var view = sender as GridView;
                int stok = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "Stok"));

                if (stok <= 0)
                {
                    e.Appearance.BackColor = Color.MistyRose; // Kırmızımsı
                    e.Appearance.BackColor2 = Color.Red;
                }
                else if (stok < 10)
                {
                    e.Appearance.BackColor = Color.LemonChiffon; // Sarımsı
                }
            }
        }

        // 3. Satır Seçilince Sağ Tarafı Doldur (FocusedRowChanged Olayı)
        // 3. Fill Right Side When Row Selected (FocusedRowChanged Event)
        // Tasarımcıda GridView -> Events -> FocusedRowChanged olayına bağlayın!
        // Connect to GridView -> Events -> FocusedRowChanged event in Designer!
        private void gridViewStok_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle >= 0) // Geçerli bir satır mı?
            {
                // Seçili verileri al
                // Get selected data
                _seciliBitkiId = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "ID"));
                string urunAdi = view.GetRowCellValue(view.FocusedRowHandle, "Urun").ToString();
                int stok = Convert.ToInt32(view.GetRowCellValue(view.FocusedRowHandle, "Stok"));
                string resimYolu = view.GetRowCellValue(view.FocusedRowHandle, "ResimYolu")?.ToString();

                // Sağ panele yaz
                // Write to right panel
                lblUrunAdi.Text = urunAdi;
                lblMevcutStok.Text = $"Mevcut Stok: {stok}";

                if (stok <= 0) lblMevcutStok.ForeColor = Color.Red;
                else lblMevcutStok.ForeColor = Color.Green;

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
                    catch { picUrunResmi.Image = null; }
                }
                else
                {
                    picUrunResmi.Image = null;
                }
            }
        }

        // 4. Stok Güncelleme Butonu
        // 4. Stock Update Button
        private void btnStokGuncelle_Click(object sender, EventArgs e)
        {
            // 1. Bir bitki seçili mi kontrol et (_seciliBitkiId, FocusedRowChanged olayında doluyor)
            // 1. Check if a plant is selected (_seciliBitkiId is filled in FocusedRowChanged event)
            if (_seciliBitkiId == 0)
            {
                XtraMessageBox.Show("Lütfen listeden stok eklemek istediğiniz bitkiyi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Eklenecek miktarı al
            // 2. Get amount to add
            int eklenecekMiktar = (int)numStokEkle.Value; // SpinEdit kullanıyorsanız .Value

            if (eklenecekMiktar <= 0)
            {
                XtraMessageBox.Show("Lütfen 0'dan büyük bir miktar girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var db = new TohumBankasiContext())
                {
                    // 3. Veritabanından bitkiyi bul
                    // 3. Find plant from database
                    var bitki = db.Bitkilers.Find(_seciliBitkiId);

                    if (bitki != null)
                    {
                        // 4. Stoğu Artır
                        // 4. Increase Stock
                        bitki.StokAdedi += eklenecekMiktar;

                        // 5. Kaydet
                        // 5. Save
                        db.SaveChanges();

                        XtraMessageBox.Show($"{eklenecekMiktar} adet stok eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 6. Temizlik ve Yenileme
                        // 6. Cleanup and Refresh
                        numStokEkle.Value = 0;
                        StoklariListele(); // Listeyi yenile ki tablodaki renkler ve sayılar güncellensin

                        // Güncel stoğu sağ panele de yansıt (Listeden otomatik çekmesi lazım ama manuel de güncelleyebiliriz)
                        lblMevcutStok.Text = $"Mevcut Stok: {bitki.StokAdedi}";
                        if (bitki.StokAdedi > 0) lblMevcutStok.ForeColor = Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnStokAzalt_Click(object sender, EventArgs e)
        {
            if (_seciliBitkiId == 0)
            {
                XtraMessageBox.Show("Lütfen listeden işlem yapılacak bitkiyi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int dusulecekMiktar = (int)numStokEkle.Value;
            if (dusulecekMiktar <= 0)
            {
                XtraMessageBox.Show("Lütfen 0'dan büyük bir miktar girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var db = new TohumBankasiContext())
                {
                    var bitki = db.Bitkilers.Find(_seciliBitkiId);
                    if (bitki != null)
                    {
                        // KRİTİK KONTROL: Stok eksiye düşmemeli
                        // CRITICAL CHECK: Stock should not go negative
                        if (bitki.StokAdedi < dusulecekMiktar)
                        {
                            XtraMessageBox.Show($"Yetersiz stok! Mevcut stok ({bitki.StokAdedi}), düşmek istediğiniz miktardan ({dusulecekMiktar}) az.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Stoğu düş
                        // Decrease stock
                        bitki.StokAdedi -= dusulecekMiktar;
                        db.SaveChanges();

                        XtraMessageBox.Show("Stok başarıyla düşüldü.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Ekranı Yenile
                        // Refresh Screen
                        numStokEkle.Value = 0;
                        StoklariListele(); // Tabloyu güncelle

                        // Sağ paneldeki etiketi de güncelle
                        // Update the label on the right panel too
                        lblMevcutStok.Text = $"Mevcut Stok: {bitki.StokAdedi}";
                        if (bitki.StokAdedi <= 0) lblMevcutStok.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hata: " + ex.Message);
            }
        }

    }
}