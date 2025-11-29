namespace TohumBankasiOtomasyonu
{
    partial class UcBitkiAsistani
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            lblBaslik = new DevExpress.XtraEditors.LabelControl();
            panelControl2 = new DevExpress.XtraEditors.PanelControl();
            lnkGoogleAI = new DevExpress.XtraEditors.HyperlinkLabelControl();
            btnKeyKaydet = new DevExpress.XtraEditors.SimpleButton();
            txtApiKey = new DevExpress.XtraEditors.TextEdit();
            lblApiKey = new DevExpress.XtraEditors.LabelControl();
            btnAsistanResimSec = new DevExpress.XtraEditors.SimpleButton();
            picAsistanResim = new DevExpress.XtraEditors.PictureEdit();
            panelControl3 = new DevExpress.XtraEditors.PanelControl();
            txtAsistanCevap = new DevExpress.XtraEditors.MemoEdit();
            lblCevapBaslik = new DevExpress.XtraEditors.LabelControl();
            lblSoruBaslik = new DevExpress.XtraEditors.LabelControl();
            txtAsistanSoru = new DevExpress.XtraEditors.MemoEdit();
            btnAsistanAnaliz = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
            panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtApiKey.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picAsistanResim.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl3).BeginInit();
            panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtAsistanCevap.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtAsistanSoru.Properties).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(lblBaslik);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(945, 60);
            panelControl1.TabIndex = 0;
            // 
            // lblBaslik
            // 
            lblBaslik.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblBaslik.Appearance.Options.UseFont = true;
            lblBaslik.Location = new System.Drawing.Point(5, 17);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Size = new System.Drawing.Size(160, 29);
            lblBaslik.TabIndex = 0;
            lblBaslik.Text = "labelControl1";
            // 
            // panelControl2
            // 
            panelControl2.Controls.Add(lnkGoogleAI);
            panelControl2.Controls.Add(btnKeyKaydet);
            panelControl2.Controls.Add(txtApiKey);
            panelControl2.Controls.Add(lblApiKey);
            panelControl2.Controls.Add(btnAsistanResimSec);
            panelControl2.Controls.Add(picAsistanResim);
            panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            panelControl2.Location = new System.Drawing.Point(0, 60);
            panelControl2.Name = "panelControl2";
            panelControl2.Size = new System.Drawing.Size(400, 511);
            panelControl2.TabIndex = 1;
            // 
            // lnkGoogleAI
            // 
            lnkGoogleAI.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lnkGoogleAI.Location = new System.Drawing.Point(5, 482);
            lnkGoogleAI.Name = "lnkGoogleAI";
            lnkGoogleAI.Size = new System.Drawing.Size(109, 13);
            lnkGoogleAI.TabIndex = 5;
            lnkGoogleAI.Text = "hyperlinkLabelControl1";
            lnkGoogleAI.Click += lnkGoogleAI_Click;
            // 
            // btnKeyKaydet
            // 
            btnKeyKaydet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            btnKeyKaydet.ImageOptions.Image = Properties.Resources.save;
            btnKeyKaydet.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnKeyKaydet.Location = new System.Drawing.Point(5, 444);
            btnKeyKaydet.Name = "btnKeyKaydet";
            btnKeyKaydet.Size = new System.Drawing.Size(133, 32);
            btnKeyKaydet.TabIndex = 4;
            btnKeyKaydet.Click += btnKeyKaydet_Click;
            // 
            // txtApiKey
            // 
            txtApiKey.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtApiKey.Location = new System.Drawing.Point(5, 418);
            txtApiKey.Name = "txtApiKey";
            txtApiKey.Size = new System.Drawing.Size(389, 20);
            txtApiKey.TabIndex = 3;
            // 
            // lblApiKey
            // 
            lblApiKey.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblApiKey.Location = new System.Drawing.Point(5, 399);
            lblApiKey.Name = "lblApiKey";
            lblApiKey.Size = new System.Drawing.Size(63, 13);
            lblApiKey.TabIndex = 2;
            lblApiKey.Text = "labelControl1";
            // 
            // btnAsistanResimSec
            // 
            btnAsistanResimSec.Dock = System.Windows.Forms.DockStyle.Top;
            btnAsistanResimSec.ImageOptions.Image = Properties.Resources.camera;
            btnAsistanResimSec.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnAsistanResimSec.Location = new System.Drawing.Point(2, 355);
            btnAsistanResimSec.LookAndFeel.UseDefaultLookAndFeel = false;
            btnAsistanResimSec.Name = "btnAsistanResimSec";
            btnAsistanResimSec.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnAsistanResimSec.Size = new System.Drawing.Size(396, 38);
            btnAsistanResimSec.TabIndex = 1;
            btnAsistanResimSec.Click += btnAsistanResimSec_Click;
            // 
            // picAsistanResim
            // 
            picAsistanResim.Dock = System.Windows.Forms.DockStyle.Top;
            picAsistanResim.Location = new System.Drawing.Point(2, 2);
            picAsistanResim.Name = "picAsistanResim";
            picAsistanResim.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            picAsistanResim.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            picAsistanResim.Size = new System.Drawing.Size(396, 353);
            picAsistanResim.TabIndex = 0;
            // 
            // panelControl3
            // 
            panelControl3.Controls.Add(txtAsistanCevap);
            panelControl3.Controls.Add(lblCevapBaslik);
            panelControl3.Controls.Add(lblSoruBaslik);
            panelControl3.Controls.Add(txtAsistanSoru);
            panelControl3.Controls.Add(btnAsistanAnaliz);
            panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl3.Location = new System.Drawing.Point(400, 60);
            panelControl3.Name = "panelControl3";
            panelControl3.Size = new System.Drawing.Size(545, 511);
            panelControl3.TabIndex = 2;
            // 
            // txtAsistanCevap
            // 
            txtAsistanCevap.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtAsistanCevap.Location = new System.Drawing.Point(4, 25);
            txtAsistanCevap.Name = "txtAsistanCevap";
            txtAsistanCevap.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            txtAsistanCevap.Properties.Appearance.Options.UseFont = true;
            txtAsistanCevap.Properties.ReadOnly = true;
            txtAsistanCevap.Size = new System.Drawing.Size(536, 344);
            txtAsistanCevap.TabIndex = 4;
            // 
            // lblCevapBaslik
            // 
            lblCevapBaslik.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            lblCevapBaslik.Appearance.Options.UseFont = true;
            lblCevapBaslik.Location = new System.Drawing.Point(4, 6);
            lblCevapBaslik.Name = "lblCevapBaslik";
            lblCevapBaslik.Size = new System.Drawing.Size(75, 16);
            lblCevapBaslik.TabIndex = 3;
            lblCevapBaslik.Text = "labelControl1";
            // 
            // lblSoruBaslik
            // 
            lblSoruBaslik.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            lblSoruBaslik.Appearance.Options.UseFont = true;
            lblSoruBaslik.Dock = System.Windows.Forms.DockStyle.Bottom;
            lblSoruBaslik.Location = new System.Drawing.Point(2, 372);
            lblSoruBaslik.Name = "lblSoruBaslik";
            lblSoruBaslik.Size = new System.Drawing.Size(75, 16);
            lblSoruBaslik.TabIndex = 0;
            lblSoruBaslik.Text = "labelControl1";
            // 
            // txtAsistanSoru
            // 
            txtAsistanSoru.Dock = System.Windows.Forms.DockStyle.Bottom;
            txtAsistanSoru.Location = new System.Drawing.Point(2, 388);
            txtAsistanSoru.Name = "txtAsistanSoru";
            txtAsistanSoru.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            txtAsistanSoru.Properties.Appearance.Options.UseFont = true;
            txtAsistanSoru.Size = new System.Drawing.Size(541, 80);
            txtAsistanSoru.TabIndex = 1;
            // 
            // btnAsistanAnaliz
            // 
            btnAsistanAnaliz.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnAsistanAnaliz.ImageOptions.Image = Properties.Resources.paper_plane;
            btnAsistanAnaliz.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnAsistanAnaliz.Location = new System.Drawing.Point(2, 468);
            btnAsistanAnaliz.LookAndFeel.UseDefaultLookAndFeel = false;
            btnAsistanAnaliz.Name = "btnAsistanAnaliz";
            btnAsistanAnaliz.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnAsistanAnaliz.Size = new System.Drawing.Size(541, 41);
            btnAsistanAnaliz.TabIndex = 2;
            btnAsistanAnaliz.Click += btnAsistanAnaliz_Click;
            // 
            // UcBitkiAsistani
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl3);
            Controls.Add(panelControl2);
            Controls.Add(panelControl1);
            Name = "UcBitkiAsistani";
            Size = new System.Drawing.Size(945, 571);
            Load += UcBitkiAsistani_Load;
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
            panelControl2.ResumeLayout(false);
            panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtApiKey.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)picAsistanResim.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl3).EndInit();
            panelControl3.ResumeLayout(false);
            panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtAsistanCevap.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtAsistanSoru.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblBaslik;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnAsistanResimSec;
        private DevExpress.XtraEditors.PictureEdit picAsistanResim;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnAsistanAnaliz;
        private DevExpress.XtraEditors.MemoEdit txtAsistanSoru;
        private DevExpress.XtraEditors.LabelControl lblSoruBaslik;
        private DevExpress.XtraEditors.MemoEdit txtAsistanCevap;
        private DevExpress.XtraEditors.LabelControl lblCevapBaslik;
        private DevExpress.XtraEditors.TextEdit txtApiKey;
        private DevExpress.XtraEditors.LabelControl lblApiKey;
        private DevExpress.XtraEditors.HyperlinkLabelControl lnkGoogleAI;
        private DevExpress.XtraEditors.SimpleButton btnKeyKaydet;
    }
}
