namespace TohumBankasiOtomasyonu
{
    partial class FormYeniBitkiEkle
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
            picTakipResim = new DevExpress.XtraEditors.PictureEdit();
            btnResimSec = new DevExpress.XtraEditors.SimpleButton();
            lblTakipAdi = new DevExpress.XtraEditors.LabelControl();
            txtTakipAdi = new DevExpress.XtraEditors.TextEdit();
            btnTakipBaslat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)picTakipResim.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtTakipAdi.Properties).BeginInit();
            SuspendLayout();
            // 
            // picTakipResim
            // 
            picTakipResim.Dock = System.Windows.Forms.DockStyle.Top;
            picTakipResim.Location = new System.Drawing.Point(0, 0);
            picTakipResim.Name = "picTakipResim";
            picTakipResim.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            picTakipResim.Properties.ShowMenu = false;
            picTakipResim.Size = new System.Drawing.Size(484, 200);
            picTakipResim.TabIndex = 0;
            // 
            // btnResimSec
            // 
            btnResimSec.Dock = System.Windows.Forms.DockStyle.Top;
            btnResimSec.ImageOptions.Image = Properties.Resources.camera;
            btnResimSec.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnResimSec.Location = new System.Drawing.Point(0, 200);
            btnResimSec.LookAndFeel.UseDefaultLookAndFeel = false;
            btnResimSec.Name = "btnResimSec";
            btnResimSec.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnResimSec.Size = new System.Drawing.Size(484, 41);
            btnResimSec.TabIndex = 1;
            btnResimSec.Click += btnResimSec_Click;
            // 
            // lblTakipAdi
            // 
            lblTakipAdi.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblTakipAdi.Appearance.Options.UseFont = true;
            lblTakipAdi.Dock = System.Windows.Forms.DockStyle.Top;
            lblTakipAdi.Location = new System.Drawing.Point(0, 241);
            lblTakipAdi.Name = "lblTakipAdi";
            lblTakipAdi.Padding = new System.Windows.Forms.Padding(0, 10, 0, 5);
            lblTakipAdi.Size = new System.Drawing.Size(85, 31);
            lblTakipAdi.TabIndex = 2;
            lblTakipAdi.Text = "labelControl1";
            // 
            // txtTakipAdi
            // 
            txtTakipAdi.Dock = System.Windows.Forms.DockStyle.Top;
            txtTakipAdi.Location = new System.Drawing.Point(0, 272);
            txtTakipAdi.Name = "txtTakipAdi";
            txtTakipAdi.Size = new System.Drawing.Size(484, 20);
            txtTakipAdi.TabIndex = 3;
            // 
            // btnTakipBaslat
            // 
            btnTakipBaslat.Dock = System.Windows.Forms.DockStyle.Top;
            btnTakipBaslat.ImageOptions.Image = Properties.Resources._checked;
            btnTakipBaslat.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnTakipBaslat.Location = new System.Drawing.Point(0, 292);
            btnTakipBaslat.Name = "btnTakipBaslat";
            btnTakipBaslat.Size = new System.Drawing.Size(484, 41);
            btnTakipBaslat.TabIndex = 4;
            btnTakipBaslat.Click += btnTakipBaslat_Click;
            // 
            // FormYeniBitkiEkle
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(484, 352);
            Controls.Add(btnTakipBaslat);
            Controls.Add(txtTakipAdi);
            Controls.Add(lblTakipAdi);
            Controls.Add(btnResimSec);
            Controls.Add(picTakipResim);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormYeniBitkiEkle";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FormYeniBitkiEkle";
            Load += FormYeniBitkiEkle_Load;
            ((System.ComponentModel.ISupportInitialize)picTakipResim.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtTakipAdi.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit picTakipResim;
        private DevExpress.XtraEditors.SimpleButton btnResimSec;
        private DevExpress.XtraEditors.LabelControl lblTakipAdi;
        private DevExpress.XtraEditors.TextEdit txtTakipAdi;
        private DevExpress.XtraEditors.SimpleButton btnTakipBaslat;
    }
}