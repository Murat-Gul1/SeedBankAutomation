namespace TohumBankasiOtomasyonu
{
    partial class FormAdminPaneli
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            btnAdminBlockchain = new DevExpress.XtraEditors.SimpleButton();
            btnAdminSatislar = new DevExpress.XtraEditors.SimpleButton();
            btnAdminKullanicilar = new DevExpress.XtraEditors.SimpleButton();
            btnAdminBitkiler = new DevExpress.XtraEditors.SimpleButton();
            navFrameAdmin = new DevExpress.XtraBars.Navigation.NavigationFrame();
            pageBitkiler = new DevExpress.XtraBars.Navigation.NavigationPage();
            ucBitkiYonetimi1 = new UcBitkiYonetimi();
            pageKullanicilar = new DevExpress.XtraBars.Navigation.NavigationPage();
            ucKullaniciYonetimi1 = new UcKullaniciYonetimi();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)navFrameAdmin).BeginInit();
            navFrameAdmin.SuspendLayout();
            pageBitkiler.SuspendLayout();
            pageKullanicilar.SuspendLayout();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(btnAdminBlockchain);
            panelControl1.Controls.Add(btnAdminSatislar);
            panelControl1.Controls.Add(btnAdminKullanicilar);
            panelControl1.Controls.Add(btnAdminBitkiler);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(86, 532);
            panelControl1.TabIndex = 0;
            // 
            // btnAdminBlockchain
            // 
            btnAdminBlockchain.Dock = System.Windows.Forms.DockStyle.Top;
            btnAdminBlockchain.ImageOptions.Image = Properties.Resources.blockchain;
            btnAdminBlockchain.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnAdminBlockchain.Location = new System.Drawing.Point(2, 170);
            btnAdminBlockchain.LookAndFeel.UseDefaultLookAndFeel = false;
            btnAdminBlockchain.Name = "btnAdminBlockchain";
            btnAdminBlockchain.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnAdminBlockchain.Size = new System.Drawing.Size(82, 56);
            btnAdminBlockchain.TabIndex = 3;
            // 
            // btnAdminSatislar
            // 
            btnAdminSatislar.Dock = System.Windows.Forms.DockStyle.Top;
            btnAdminSatislar.ImageOptions.Image = Properties.Resources.sell;
            btnAdminSatislar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnAdminSatislar.Location = new System.Drawing.Point(2, 114);
            btnAdminSatislar.LookAndFeel.UseDefaultLookAndFeel = false;
            btnAdminSatislar.Name = "btnAdminSatislar";
            btnAdminSatislar.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnAdminSatislar.Size = new System.Drawing.Size(82, 56);
            btnAdminSatislar.TabIndex = 2;
            // 
            // btnAdminKullanicilar
            // 
            btnAdminKullanicilar.Dock = System.Windows.Forms.DockStyle.Top;
            btnAdminKullanicilar.ImageOptions.Image = Properties.Resources.useraccount;
            btnAdminKullanicilar.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnAdminKullanicilar.Location = new System.Drawing.Point(2, 58);
            btnAdminKullanicilar.LookAndFeel.UseDefaultLookAndFeel = false;
            btnAdminKullanicilar.Name = "btnAdminKullanicilar";
            btnAdminKullanicilar.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnAdminKullanicilar.Size = new System.Drawing.Size(82, 56);
            btnAdminKullanicilar.TabIndex = 1;
            btnAdminKullanicilar.Click += btnAdminKullanicilar_Click;
            // 
            // btnAdminBitkiler
            // 
            btnAdminBitkiler.Dock = System.Windows.Forms.DockStyle.Top;
            btnAdminBitkiler.ImageOptions.Image = Properties.Resources.plant;
            btnAdminBitkiler.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnAdminBitkiler.Location = new System.Drawing.Point(2, 2);
            btnAdminBitkiler.LookAndFeel.UseDefaultLookAndFeel = false;
            btnAdminBitkiler.Name = "btnAdminBitkiler";
            btnAdminBitkiler.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnAdminBitkiler.Size = new System.Drawing.Size(82, 56);
            btnAdminBitkiler.TabIndex = 0;
            btnAdminBitkiler.Click += btnAdminBitkiler_Click;
            // 
            // navFrameAdmin
            // 
            navFrameAdmin.Controls.Add(pageBitkiler);
            navFrameAdmin.Controls.Add(pageKullanicilar);
            navFrameAdmin.Dock = System.Windows.Forms.DockStyle.Fill;
            navFrameAdmin.Location = new System.Drawing.Point(86, 0);
            navFrameAdmin.Name = "navFrameAdmin";
            navFrameAdmin.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { pageBitkiler, pageKullanicilar });
            navFrameAdmin.SelectedPage = pageBitkiler;
            navFrameAdmin.Size = new System.Drawing.Size(1051, 532);
            navFrameAdmin.TabIndex = 1;
            navFrameAdmin.Text = "navigationFrame1";
            // 
            // pageBitkiler
            // 
            pageBitkiler.Caption = "pageBitkiler";
            pageBitkiler.Controls.Add(ucBitkiYonetimi1);
            pageBitkiler.Name = "pageBitkiler";
            pageBitkiler.Size = new System.Drawing.Size(1051, 532);
            // 
            // ucBitkiYonetimi1
            // 
            ucBitkiYonetimi1.Dock = System.Windows.Forms.DockStyle.Fill;
            ucBitkiYonetimi1.Location = new System.Drawing.Point(0, 0);
            ucBitkiYonetimi1.Name = "ucBitkiYonetimi1";
            ucBitkiYonetimi1.Size = new System.Drawing.Size(1051, 532);
            ucBitkiYonetimi1.TabIndex = 0;
            // 
            // pageKullanicilar
            // 
            pageKullanicilar.Caption = "navigationPage2";
            pageKullanicilar.Controls.Add(ucKullaniciYonetimi1);
            pageKullanicilar.Name = "pageKullanicilar";
            pageKullanicilar.Size = new System.Drawing.Size(1051, 532);
            // 
            // ucKullaniciYonetimi1
            // 
            ucKullaniciYonetimi1.Dock = System.Windows.Forms.DockStyle.Fill;
            ucKullaniciYonetimi1.Location = new System.Drawing.Point(0, 0);
            ucKullaniciYonetimi1.Name = "ucKullaniciYonetimi1";
            ucKullaniciYonetimi1.Size = new System.Drawing.Size(1051, 532);
            ucKullaniciYonetimi1.TabIndex = 0;
            // 
            // FormAdminPaneli
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1137, 532);
            Controls.Add(navFrameAdmin);
            Controls.Add(panelControl1);
            Name = "FormAdminPaneli";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FormAdminPaneli";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += FormAdminPaneli_Load;
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)navFrameAdmin).EndInit();
            navFrameAdmin.ResumeLayout(false);
            pageBitkiler.ResumeLayout(false);
            pageKullanicilar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnAdminBitkiler;
        private DevExpress.XtraEditors.SimpleButton btnAdminKullanicilar;
        private DevExpress.XtraEditors.SimpleButton btnAdminSatislar;
        private DevExpress.XtraEditors.SimpleButton btnAdminBlockchain;
        private DevExpress.XtraBars.Navigation.NavigationFrame navFrameAdmin;
        private DevExpress.XtraBars.Navigation.NavigationPage pageBitkiler;
        private DevExpress.XtraBars.Navigation.NavigationPage pageKullanicilar;
        private UcBitkiYonetimi ucBitkiYonetimi1;
        private UcKullaniciYonetimi ucKullaniciYonetimi1;
    }
}