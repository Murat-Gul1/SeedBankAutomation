using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
