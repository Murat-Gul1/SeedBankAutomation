using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using TohumBankasiOtomasyonu.Properties; 
using TohumBankasiOtomasyonu.Models; 

namespace TohumBankasiOtomasyonu
{
    public partial class FormBitkiIslemleri : DevExpress.XtraEditors.XtraForm
    {
        // Bu değişken 0 ise "Yeni Kayıt", 0'dan büyükse "Düzenleme" modundayız demektir.
        // If this variable is 0, it means "New Record"; if greater than 0, we are in "Edit" mode.
        private int _gelenBitkiId = 0;

        // 1. OLUŞTURUCU (Yeni Ekleme İçin)
        // 1. CONSTRUCTOR (For New Addition)
        public FormBitkiIslemleri()
        {
            InitializeComponent();
            _gelenBitkiId = 0;
        }

        public FormBitkiIslemleri(int bitkiId) : this()
        {
            _gelenBitkiId = bitkiId;
        }

        private void UygulaDil()
        {
            // ... (Başlık, butonlar ve sekme başlıkları aynı kalıyor) ...
            if (_gelenBitkiId == 0) this.Text = Resources.FormBitkiIslemleri_Title_Ekle;
            else this.Text = Resources.FormBitkiIslemleri_Title_Duzenle;

            if (btnKaydet != null) btnKaydet.Text = Resources.btnBitkiKaydet;
            if (btnResimSec != null) btnResimSec.Text = Resources.btnResimSec;
            if (pageTR != null) pageTR.Text = Resources.tabBitkiTR;
            if (pageEN != null) pageEN.Text = Resources.tabBitkiEN;

            // --- LAYOUT ETİKETLERİ (GÜNCELLENMİŞ HALİ) ---

            // 1. Ortak Alanlar (Bunlar ana layoutControl1'de olduğu için eski yöntem çalışır,
            // ama isterseniz bunlara da isim verip lci... diye çağırabilirsiniz)
            var lciBilimsel = layoutControl1.GetItemByControl(txtBilimselAd);
            if (lciBilimsel != null) lciBilimsel.Text = Resources.lblBitkiBilimselAd;

            var lciFiyat = layoutControl1.GetItemByControl(numFiyat);
            if (lciFiyat != null) lciFiyat.Text = Resources.lblBitkiFiyat;

            var lciStok = layoutControl1.GetItemByControl(numStok);
            if (lciStok != null) lciStok.Text = Resources.lblBitkiStok;

            var lciResim = layoutControl1.GetItemByControl(picBitkiResmi);
            if (lciResim != null) lciResim.Text = Resources.lblBitkiResim;

            // 2. Türkçe Sekmesi (İsimlerini verdiğimiz öğeler)
            // Artık 'if (lciAdTR != null)' kontrolüne bile gerek yok çünkü tasarımda oluşturduk.
            lciAdTR.Text = Resources.lblBitkiAdi;
            lciAciklamaTR.Text = Resources.lblBitkiAciklama;
            lciYetistirmeTR.Text = Resources.lblBitkiYetistirme;
            lciSaklamaTR.Text = Resources.lblBitkiSaklama;

            // 3. İngilizce Sekmesi
            lciAdEN.Text = Resources.lblBitkiAdi;
            lciAciklamaEN.Text = Resources.lblBitkiAciklama;
            lciYetistirmeEN.Text = Resources.lblBitkiYetistirme;
            lciSaklamaEN.Text = Resources.lblBitkiSaklama;
        }

        private void FormBitkiIslemleri_Load(object sender, EventArgs e)
        {
            UygulaDil();
        }

        private void FormBitkiIslemleri_Load_1(object sender, EventArgs e)
        {
            UygulaDil();
        }
    }
}