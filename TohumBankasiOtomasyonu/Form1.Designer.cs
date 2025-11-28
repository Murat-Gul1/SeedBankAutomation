namespace TohumBankasiOtomasyonu
{
    partial class Form1
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
            pnlToolbar = new DevExpress.XtraEditors.PanelControl();
            btnKullaniciAyarlari = new DevExpress.XtraEditors.SimpleButton();
            btnAdminPaneli = new DevExpress.XtraEditors.SimpleButton();
            btnGiris = new DevExpress.XtraEditors.SimpleButton();
            btnDil = new DevExpress.XtraEditors.SimpleButton();
            btnSepet = new DevExpress.XtraEditors.SimpleButton();
            btnCikisYap = new DevExpress.XtraEditors.SimpleButton();
            pnlDilSecenekleri = new DevExpress.XtraEditors.PanelControl();
            btnIngilizce = new DevExpress.XtraEditors.SimpleButton();
            btnTurkce = new DevExpress.XtraEditors.SimpleButton();
            pnlAnaIcerik = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)pnlToolbar).BeginInit();
            pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pnlDilSecenekleri).BeginInit();
            pnlDilSecenekleri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pnlAnaIcerik).BeginInit();
            SuspendLayout();
            // 
            // pnlToolbar
            // 
            pnlToolbar.Controls.Add(btnKullaniciAyarlari);
            pnlToolbar.Controls.Add(btnAdminPaneli);
            pnlToolbar.Controls.Add(btnGiris);
            pnlToolbar.Controls.Add(btnDil);
            pnlToolbar.Controls.Add(btnSepet);
            pnlToolbar.Controls.Add(btnCikisYap);
            pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            pnlToolbar.Location = new System.Drawing.Point(0, 0);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Size = new System.Drawing.Size(885, 56);
            pnlToolbar.TabIndex = 0;
            pnlToolbar.Click += pnlToolbar_Click;
            // 
            // btnKullaniciAyarlari
            // 
            btnKullaniciAyarlari.Dock = System.Windows.Forms.DockStyle.Left;
            btnKullaniciAyarlari.ImageOptions.Image = Properties.Resources.settings;
            btnKullaniciAyarlari.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnKullaniciAyarlari.Location = new System.Drawing.Point(63, 2);
            btnKullaniciAyarlari.LookAndFeel.UseDefaultLookAndFeel = false;
            btnKullaniciAyarlari.Name = "btnKullaniciAyarlari";
            btnKullaniciAyarlari.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnKullaniciAyarlari.Size = new System.Drawing.Size(75, 52);
            btnKullaniciAyarlari.TabIndex = 3;
            btnKullaniciAyarlari.Visible = false;
            btnKullaniciAyarlari.Click += btnKullaniciAyarlari_Click;
            // 
            // btnAdminPaneli
            // 
            btnAdminPaneli.Dock = System.Windows.Forms.DockStyle.Right;
            btnAdminPaneli.ImageOptions.Image = Properties.Resources.administrator;
            btnAdminPaneli.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnAdminPaneli.Location = new System.Drawing.Point(603, 2);
            btnAdminPaneli.LookAndFeel.UseDefaultLookAndFeel = false;
            btnAdminPaneli.Name = "btnAdminPaneli";
            btnAdminPaneli.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnAdminPaneli.Size = new System.Drawing.Size(75, 52);
            btnAdminPaneli.TabIndex = 2;
            btnAdminPaneli.Visible = false;
            btnAdminPaneli.Click += btnAdminPaneli_Click;
            // 
            // btnGiris
            // 
            btnGiris.Dock = System.Windows.Forms.DockStyle.Left;
            btnGiris.ImageOptions.Image = Properties.Resources.useraccount;
            btnGiris.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnGiris.Location = new System.Drawing.Point(2, 2);
            btnGiris.LookAndFeel.UseDefaultLookAndFeel = false;
            btnGiris.Name = "btnGiris";
            btnGiris.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnGiris.Size = new System.Drawing.Size(61, 52);
            btnGiris.TabIndex = 1;
            btnGiris.Click += btnGiris_Click;
            // 
            // btnDil
            // 
            btnDil.Dock = System.Windows.Forms.DockStyle.Right;
            btnDil.ImageOptions.Image = Properties.Resources.language;
            btnDil.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnDil.Location = new System.Drawing.Point(678, 2);
            btnDil.LookAndFeel.UseDefaultLookAndFeel = false;
            btnDil.Name = "btnDil";
            btnDil.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnDil.Size = new System.Drawing.Size(55, 52);
            btnDil.TabIndex = 5;
            btnDil.Click += btnDil_Click;
            // 
            // btnSepet
            // 
            btnSepet.Dock = System.Windows.Forms.DockStyle.Right;
            btnSepet.ImageOptions.Image = Properties.Resources.Basket;
            btnSepet.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnSepet.Location = new System.Drawing.Point(733, 2);
            btnSepet.LookAndFeel.UseDefaultLookAndFeel = false;
            btnSepet.Name = "btnSepet";
            btnSepet.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnSepet.Size = new System.Drawing.Size(75, 52);
            btnSepet.TabIndex = 6;
            btnSepet.Click += btnSepet_Click;
            // 
            // btnCikisYap
            // 
            btnCikisYap.Dock = System.Windows.Forms.DockStyle.Right;
            btnCikisYap.ImageOptions.Image = Properties.Resources.close;
            btnCikisYap.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnCikisYap.Location = new System.Drawing.Point(808, 2);
            btnCikisYap.LookAndFeel.UseDefaultLookAndFeel = false;
            btnCikisYap.Name = "btnCikisYap";
            btnCikisYap.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnCikisYap.Size = new System.Drawing.Size(75, 52);
            btnCikisYap.TabIndex = 0;
            btnCikisYap.Visible = false;
            btnCikisYap.Click += btnCikisYap_Click;
            // 
            // pnlDilSecenekleri
            // 
            pnlDilSecenekleri.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            pnlDilSecenekleri.Controls.Add(btnIngilizce);
            pnlDilSecenekleri.Controls.Add(btnTurkce);
            pnlDilSecenekleri.Location = new System.Drawing.Point(826, 62);
            pnlDilSecenekleri.Name = "pnlDilSecenekleri";
            pnlDilSecenekleri.Size = new System.Drawing.Size(131, 50);
            pnlDilSecenekleri.TabIndex = 1;
            pnlDilSecenekleri.Visible = false;
            pnlDilSecenekleri.Click += pnlDilSecenekleri_Click;
            // 
            // btnIngilizce
            // 
            btnIngilizce.Dock = System.Windows.Forms.DockStyle.Top;
            btnIngilizce.Location = new System.Drawing.Point(2, 25);
            btnIngilizce.Name = "btnIngilizce";
            btnIngilizce.Size = new System.Drawing.Size(127, 23);
            btnIngilizce.TabIndex = 1;
            btnIngilizce.Text = "English";
            btnIngilizce.Click += btnIngilizce_Click;
            // 
            // btnTurkce
            // 
            btnTurkce.Dock = System.Windows.Forms.DockStyle.Top;
            btnTurkce.Location = new System.Drawing.Point(2, 2);
            btnTurkce.Name = "btnTurkce";
            btnTurkce.Size = new System.Drawing.Size(127, 23);
            btnTurkce.TabIndex = 0;
            btnTurkce.Text = "Türkçe";
            btnTurkce.Click += btnTurkce_Click;
            // 
            // pnlAnaIcerik
            // 
            pnlAnaIcerik.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pnlAnaIcerik.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlAnaIcerik.Location = new System.Drawing.Point(0, 56);
            pnlAnaIcerik.Name = "pnlAnaIcerik";
            pnlAnaIcerik.Size = new System.Drawing.Size(885, 210);
            pnlAnaIcerik.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(885, 266);
            Controls.Add(pnlAnaIcerik);
            Controls.Add(pnlDilSecenekleri);
            Controls.Add(pnlToolbar);
            Name = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Click += Form1_Click;
            ((System.ComponentModel.ISupportInitialize)pnlToolbar).EndInit();
            pnlToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pnlDilSecenekleri).EndInit();
            pnlDilSecenekleri.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pnlAnaIcerik).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pnlToolbar;
        private DevExpress.XtraEditors.SimpleButton btnDil;
        private DevExpress.XtraEditors.PanelControl pnlDilSecenekleri;
        private DevExpress.XtraEditors.SimpleButton btnIngilizce;
        private DevExpress.XtraEditors.SimpleButton btnTurkce;
        private DevExpress.XtraEditors.SimpleButton btnGiris;
        private DevExpress.XtraEditors.SimpleButton btnAdminPaneli;
        private DevExpress.XtraEditors.SimpleButton btnKullaniciAyarlari;
        private DevExpress.XtraEditors.SimpleButton btnCikisYap;
        private DevExpress.XtraEditors.PanelControl pnlAnaIcerik;
        private DevExpress.XtraEditors.SimpleButton btnSepet;
    }
}