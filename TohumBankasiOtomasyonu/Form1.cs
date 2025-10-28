using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace TohumBankasiOtomasyonu
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. This checks whether the program is running for the first time.
            if (Properties.Settings.Default.FirstLaunch)
            {
                // Your request: Start in fullscreen mode on first launch.
                this.WindowState = FormWindowState.Maximized;

                // "İlk açılış" bayrağını 'false' yapıyoruz.
                // We set the "first launch" flag to 'false'.
                Properties.Settings.Default.FirstLaunch = false;
                // Ayarı hemen kaydediyoruz
                // We save the setting immediately.
                Properties.Settings.Default.Save();
            }
            else
            {
                // Bu ilk açılış DEĞİLSE, kaydettiğimiz ayarları yüklüyoruz.
                // If this is NOT the first launch, we load the saved settings.

                // Kaydedilen 'Maximized' ayarını kontrol et.
                // Check the saved 'Maximized' setting.
                if (Properties.Settings.Default.IsMaximized)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    // Eğer 'Maximized' değilse, 'Normal' moda al
                    // ve kaydettiğimiz konumu ve boyutu geri yükle.

                    // If not 'Maximized', switch to 'Normal' mode  
                    // and restore the saved position and size.
                    this.WindowState = FormWindowState.Normal;
                    this.Location = Properties.Settings.Default.WindowLocation;
                    this.Size = Properties.Settings.Default.WindowSize;
                }
            }
            UygulaDil();
        }

        // Bu metot, formdaki tüm metinleri o an seçili olan dile göre
        // .resx sözlük dosyasından yeniden yükler.
        // This method reloads all texts on the form from the
        // .resx dictionary file according to the currently selected language.

        private void UygulaDil()
        {
            // Sözlükten verileri çek
            // 'Resources' sınıfı, bizim .resx dosyalarımız için otomatik olarak oluşturuldu.
            // O anki dile (Culture) göre doğru sözlüğü kendisi seçer.
            // Fetch data from the dictionary  
            // The 'Resources' class was automatically generated for our .resx dictionary files.  
            // It automatically selects the correct dictionary based on the current language (Culture).

            this.Text = Resources.Form1_Title;

            //Giriş butonunun açıklamasını (ToolTip) ayarla
            // Set the description (ToolTip) of the login button.
            btnGiris.ToolTip = Resources.btnGiris_ToolTip;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Program kapanırken, o anki durumu kaydediyoruz.
            // When the program closes, we save the current state.

            // Pencere durumu 'Maximized' mi?
            // Is the window state 'Maximized'?
            if (this.WindowState == FormWindowState.Maximized)
            {
                Properties.Settings.Default.IsMaximized = true;

                // Not: 'Maximized' durumdayken konumu veya boyutu kaydetmiyoruz,
                // çünkü 'Normal' moda döndüğünde eski boyutunu hatırlamasını istiyoruz.

                // Note: We don't save position or size while in 'Maximized' state,  
                // because we want it to remember its previous size when returning to 'Normal' mode.
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                // Pencere 'Normal' moddaysa, o anki konumunu ve boyutunu kaydet.
                // If the window is in 'Normal' mode, save its current position and size.
                Properties.Settings.Default.IsMaximized = false;
                Properties.Settings.Default.WindowLocation = this.Location;
                Properties.Settings.Default.WindowSize = this.Size;
            }
            // (Eğer 'Minimized' (Simge Durumu) ise hiçbir ayarı kaydetmiyoruz,
            // ki bir sonraki açılışta görev çubuğunda başlamasın.)

            // (If 'Minimized', we don't save any settings  
            // so that it doesn't start in the taskbar on the next launch.)

            // Tüm değişiklikleri ayar dosyasına kaydet.
            // Save all changes to the settings file.
            Properties.Settings.Default.Save();
        }

        private void btnDil_Click(object sender, EventArgs e)
        {
            // 1. Paneli, butonun tam altına ve sağına hizalı şekilde taşıyalım.
            //    Butonun form üzerindeki ekran koordinatlarını al.
            // 1. Move the panel aligned directly below and to the right of the button.  
            //    Get the button's screen coordinates on the form.

            Point btnKonum = btnDil.PointToScreen(Point.Empty);
            //    Formun ekran koordinatlarını al.
            //    Get the form's screen coordinates.

            Point formKonum = this.PointToScreen(Point.Empty);

            //    Panelin 'x' pozisyonu: (Butonun x'i - Formun x'i) + Butonun genişliği - Panelin genişliği
            //    Bu hesap, panelin sağ kenarını butonun sağ kenarına hizalar.
            //    Panel's 'x' position: (Button's x - Form's x) + Button's width - Panel's width  
            //    This calculation aligns the panel's right edge with the button's right edge.

            int x = (btnKonum.X - formKonum.X) + btnDil.Width - pnlDilSecenekleri.Width;

            //    Panelin 'y' pozisyonu: (Butonun y'si - Formun y'si) + Butonun yüksekliği
            //    Bu hesap, paneli butonun tam altına yerleştirir.
            //    Panel's 'y' position: (Button's y - Form's y) + Button's height  
            //    This calculation places the panel directly below the button.

            int y = (btnKonum.Y - formKonum.Y) + btnDil.Height;

            pnlDilSecenekleri.Location = new Point(x, y);

            // 2. Panelin görünürlüğünü tersine çevir (Açıksa kapat, kapalıysa aç).
            // 2. Toggle the panel's visibility (Close if open, open if closed).

            pnlDilSecenekleri.Visible = !pnlDilSecenekleri.Visible;

            // 3. Paneli açtıysak, en üste getirelim ve odaklanalım.
            // 3. If we opened the panel, bring it to the front and focus on it.

            if (pnlDilSecenekleri.Visible)
            {
                pnlDilSecenekleri.BringToFront(); // Diğer kontrollerin üstünde dursun(Let it stay above the other controls.)
                pnlDilSecenekleri.Focus();
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            // Formun boş bir yerine tıklanırsa paneli gizle
            // Hide the panel when an empty area of the form is clicked.

            pnlDilSecenekleri.Visible = false;
        }

        private void pnlToolbar_Click(object sender, EventArgs e)
        {
            // Üst menü paneline tıklanırsa da paneli gizle
            // Also hide the panel when the top menu panel is clicked.

            pnlDilSecenekleri.Visible = false;
        }

        private void pnlDilSecenekleri_Click(object sender, EventArgs e)
        {

        }

        private void btnTurkce_Click(object sender, EventArgs e)
        {
            // 1. Programın aktif dilini "Türkçe" (tr-TR) olarak ayarla
            // 1. Set the program's active language to "Turkish" (tr-TR)

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr-TR");

            // 2. Formdaki tüm metinleri sözlükten yeniden yükle
            // 2. Reload all texts on the form from the dictionary.

            UygulaDil();
            pnlDilSecenekleri.Visible = false;
        }

        private void btnIngilizce_Click(object sender, EventArgs e)
        {
            // 1. Programın aktif dilini "İngilizce" (en-US) olarak ayarla
            // 1. Set the program's active language to "English" (en-US)

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            // 2. Formdaki tüm metinleri sözlükten yeniden yükle
            // 2. Reload all texts on the form from the dictionary.

            UygulaDil();
            pnlDilSecenekleri.Visible = false;
        }
    }
}
