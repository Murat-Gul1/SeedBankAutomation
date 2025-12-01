namespace TohumBankasiOtomasyonu
{
    partial class UcKullaniciBitkiKarti
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
            btnSil = new DevExpress.XtraEditors.SimpleButton();
            btnDetay = new DevExpress.XtraEditors.SimpleButton();
            lblTarih = new DevExpress.XtraEditors.LabelControl();
            lblBitkiAdi = new DevExpress.XtraEditors.LabelControl();
            picBitkiResmi = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBitkiResmi.Properties).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            panelControl1.Appearance.Options.UseBackColor = true;
            panelControl1.Controls.Add(btnSil);
            panelControl1.Controls.Add(btnDetay);
            panelControl1.Controls.Add(lblTarih);
            panelControl1.Controls.Add(lblBitkiAdi);
            panelControl1.Controls.Add(picBitkiResmi);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(220, 320);
            panelControl1.TabIndex = 0;
            // 
            // btnSil
            // 
            btnSil.Dock = System.Windows.Forms.DockStyle.Right;
            btnSil.ImageOptions.Image = Properties.Resources.delete;
            btnSil.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnSil.Location = new System.Drawing.Point(109, 214);
            btnSil.Name = "btnSil";
            btnSil.Size = new System.Drawing.Size(109, 104);
            btnSil.TabIndex = 4;
            btnSil.Click += btnSil_Click;
            // 
            // btnDetay
            // 
            btnDetay.Dock = System.Windows.Forms.DockStyle.Left;
            btnDetay.ImageOptions.Image = Properties.Resources.chat;
            btnDetay.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnDetay.Location = new System.Drawing.Point(2, 214);
            btnDetay.LookAndFeel.UseDefaultLookAndFeel = false;
            btnDetay.Name = "btnDetay";
            btnDetay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnDetay.Size = new System.Drawing.Size(101, 104);
            btnDetay.TabIndex = 3;
            btnDetay.Click += btnDetay_Click;
            // 
            // lblTarih
            // 
            lblTarih.Appearance.ForeColor = System.Drawing.Color.Gray;
            lblTarih.Appearance.Options.UseForeColor = true;
            lblTarih.Dock = System.Windows.Forms.DockStyle.Top;
            lblTarih.Location = new System.Drawing.Point(2, 201);
            lblTarih.Name = "lblTarih";
            lblTarih.Size = new System.Drawing.Size(63, 13);
            lblTarih.TabIndex = 2;
            lblTarih.Text = "labelControl1";
            // 
            // lblBitkiAdi
            // 
            lblBitkiAdi.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblBitkiAdi.Appearance.Options.UseFont = true;
            lblBitkiAdi.Appearance.Options.UseTextOptions = true;
            lblBitkiAdi.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            lblBitkiAdi.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            lblBitkiAdi.Dock = System.Windows.Forms.DockStyle.Top;
            lblBitkiAdi.Location = new System.Drawing.Point(2, 182);
            lblBitkiAdi.Name = "lblBitkiAdi";
            lblBitkiAdi.Size = new System.Drawing.Size(216, 19);
            lblBitkiAdi.TabIndex = 1;
            lblBitkiAdi.Text = "labelControl1";
            // 
            // picBitkiResmi
            // 
            picBitkiResmi.Dock = System.Windows.Forms.DockStyle.Top;
            picBitkiResmi.Location = new System.Drawing.Point(2, 2);
            picBitkiResmi.Name = "picBitkiResmi";
            picBitkiResmi.Properties.ReadOnly = true;
            picBitkiResmi.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            picBitkiResmi.Properties.ShowMenu = false;
            picBitkiResmi.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            picBitkiResmi.Size = new System.Drawing.Size(216, 180);
            picBitkiResmi.TabIndex = 0;
            // 
            // UcKullaniciBitkiKarti
            // 
            Appearance.BackColor = System.Drawing.Color.White;
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "UcKullaniciBitkiKarti";
            Size = new System.Drawing.Size(220, 320);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picBitkiResmi.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PictureEdit picBitkiResmi;
        private DevExpress.XtraEditors.SimpleButton btnDetay;
        private DevExpress.XtraEditors.LabelControl lblTarih;
        private DevExpress.XtraEditors.LabelControl lblBitkiAdi;
        private DevExpress.XtraEditors.SimpleButton btnSil;
    }
}
