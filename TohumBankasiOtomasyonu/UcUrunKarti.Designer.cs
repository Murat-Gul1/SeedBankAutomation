namespace TohumBankasiOtomasyonu
{
    partial class UcUrunKarti
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
            btnSepeteEkle = new DevExpress.XtraEditors.SimpleButton();
            lblStok = new DevExpress.XtraEditors.LabelControl();
            lblFiyat = new DevExpress.XtraEditors.LabelControl();
            lblBilimselAd = new DevExpress.XtraEditors.LabelControl();
            lblUrunAdi = new DevExpress.XtraEditors.LabelControl();
            picUrunResmi = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picUrunResmi.Properties).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            panelControl1.Appearance.Options.UseBackColor = true;
            panelControl1.Controls.Add(btnSepeteEkle);
            panelControl1.Controls.Add(lblStok);
            panelControl1.Controls.Add(lblFiyat);
            panelControl1.Controls.Add(lblBilimselAd);
            panelControl1.Controls.Add(lblUrunAdi);
            panelControl1.Controls.Add(picUrunResmi);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(250, 300);
            panelControl1.TabIndex = 0;
            // 
            // btnSepeteEkle
            // 
            btnSepeteEkle.ImageOptions.Image = Properties.Resources.add_to_basket;
            btnSepeteEkle.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnSepeteEkle.Location = new System.Drawing.Point(87, 258);
            btnSepeteEkle.LookAndFeel.UseDefaultLookAndFeel = false;
            btnSepeteEkle.Name = "btnSepeteEkle";
            btnSepeteEkle.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnSepeteEkle.Size = new System.Drawing.Size(75, 32);
            btnSepeteEkle.TabIndex = 5;
            btnSepeteEkle.Click += btnSepeteEkle_Click;
            // 
            // lblStok
            // 
            lblStok.Location = new System.Drawing.Point(6, 224);
            lblStok.Name = "lblStok";
            lblStok.Size = new System.Drawing.Size(63, 13);
            lblStok.TabIndex = 4;
            lblStok.Text = "labelControl1";
            // 
            // lblFiyat
            // 
            lblFiyat.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblFiyat.Appearance.ForeColor = System.Drawing.Color.Green;
            lblFiyat.Appearance.Options.UseFont = true;
            lblFiyat.Appearance.Options.UseForeColor = true;
            lblFiyat.Location = new System.Drawing.Point(6, 199);
            lblFiyat.Name = "lblFiyat";
            lblFiyat.Size = new System.Drawing.Size(110, 19);
            lblFiyat.TabIndex = 3;
            lblFiyat.Text = "labelControl1";
            // 
            // lblBilimselAd
            // 
            lblBilimselAd.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 162);
            lblBilimselAd.Appearance.ForeColor = System.Drawing.Color.Gray;
            lblBilimselAd.Appearance.Options.UseFont = true;
            lblBilimselAd.Appearance.Options.UseForeColor = true;
            lblBilimselAd.Appearance.Options.UseTextOptions = true;
            lblBilimselAd.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            lblBilimselAd.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            lblBilimselAd.Location = new System.Drawing.Point(5, 180);
            lblBilimselAd.Name = "lblBilimselAd";
            lblBilimselAd.Size = new System.Drawing.Size(240, 13);
            lblBilimselAd.TabIndex = 2;
            lblBilimselAd.Text = "labelControl1";
            // 
            // lblUrunAdi
            // 
            lblUrunAdi.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblUrunAdi.Appearance.Options.UseFont = true;
            lblUrunAdi.Appearance.Options.UseTextOptions = true;
            lblUrunAdi.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            lblUrunAdi.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            lblUrunAdi.Location = new System.Drawing.Point(5, 158);
            lblUrunAdi.Name = "lblUrunAdi";
            lblUrunAdi.Size = new System.Drawing.Size(240, 16);
            lblUrunAdi.TabIndex = 1;
            lblUrunAdi.Text = "labelControl1";
            // 
            // picUrunResmi
            // 
            picUrunResmi.Dock = System.Windows.Forms.DockStyle.Top;
            picUrunResmi.Location = new System.Drawing.Point(2, 2);
            picUrunResmi.Name = "picUrunResmi";
            picUrunResmi.Properties.ReadOnly = true;
            picUrunResmi.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            picUrunResmi.Properties.ShowMenu = false;
            picUrunResmi.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            picUrunResmi.Size = new System.Drawing.Size(246, 150);
            picUrunResmi.TabIndex = 0;
            picUrunResmi.Click += picUrunResmi_Click;
            // 
            // UcUrunKarti
            // 
            Appearance.BackColor = System.Drawing.Color.White;
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "UcUrunKarti";
            Size = new System.Drawing.Size(250, 300);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picUrunResmi.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblUrunAdi;
        private DevExpress.XtraEditors.PictureEdit picUrunResmi;
        private DevExpress.XtraEditors.SimpleButton btnSepeteEkle;
        private DevExpress.XtraEditors.LabelControl lblStok;
        private DevExpress.XtraEditors.LabelControl lblFiyat;
        private DevExpress.XtraEditors.LabelControl lblBilimselAd;
    }
}
