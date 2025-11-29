namespace TohumBankasiOtomasyonu
{
    partial class FormHakkinda
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
            pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            lblProjeAdi = new DevExpress.XtraEditors.LabelControl();
            lblVersiyon = new DevExpress.XtraEditors.LabelControl();
            lblGelistirici = new DevExpress.XtraEditors.LabelControl();
            lblAciklama = new DevExpress.XtraEditors.LabelControl();
            lblTelif = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).BeginInit();
            SuspendLayout();
            // 
            // pictureEdit1
            // 
            pictureEdit1.BackgroundImage = Properties.Resources.seed;
            pictureEdit1.EditValue = Properties.Resources.seed;
            pictureEdit1.Location = new System.Drawing.Point(154, 12);
            pictureEdit1.Name = "pictureEdit1";
            pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Gainsboro;
            pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pictureEdit1.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            pictureEdit1.Size = new System.Drawing.Size(64, 64);
            pictureEdit1.TabIndex = 0;
            // 
            // lblProjeAdi
            // 
            lblProjeAdi.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblProjeAdi.Appearance.Options.UseFont = true;
            lblProjeAdi.Location = new System.Drawing.Point(12, 91);
            lblProjeAdi.Name = "lblProjeAdi";
            lblProjeAdi.Size = new System.Drawing.Size(129, 23);
            lblProjeAdi.TabIndex = 1;
            lblProjeAdi.Text = "labelControl1";
            // 
            // lblVersiyon
            // 
            lblVersiyon.Location = new System.Drawing.Point(12, 120);
            lblVersiyon.Name = "lblVersiyon";
            lblVersiyon.Size = new System.Drawing.Size(63, 13);
            lblVersiyon.TabIndex = 2;
            lblVersiyon.Text = "labelControl1";
            // 
            // lblGelistirici
            // 
            lblGelistirici.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblGelistirici.Appearance.Options.UseFont = true;
            lblGelistirici.Location = new System.Drawing.Point(12, 139);
            lblGelistirici.Name = "lblGelistirici";
            lblGelistirici.Size = new System.Drawing.Size(85, 16);
            lblGelistirici.TabIndex = 3;
            lblGelistirici.Text = "labelControl1";
            // 
            // lblAciklama
            // 
            lblAciklama.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            lblAciklama.Location = new System.Drawing.Point(12, 161);
            lblAciklama.Name = "lblAciklama";
            lblAciklama.Size = new System.Drawing.Size(346, 13);
            lblAciklama.TabIndex = 4;
            lblAciklama.Text = "labelControl1";
            // 
            // lblTelif
            // 
            lblTelif.Appearance.ForeColor = System.Drawing.Color.Gray;
            lblTelif.Appearance.Options.UseForeColor = true;
            lblTelif.Location = new System.Drawing.Point(119, 227);
            lblTelif.Name = "lblTelif";
            lblTelif.Size = new System.Drawing.Size(63, 13);
            lblTelif.TabIndex = 5;
            lblTelif.Text = "labelControl1";
            // 
            // FormHakkinda
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(320, 236);
            Controls.Add(lblTelif);
            Controls.Add(lblAciklama);
            Controls.Add(lblGelistirici);
            Controls.Add(lblVersiyon);
            Controls.Add(lblProjeAdi);
            Controls.Add(pictureEdit1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormHakkinda";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FormHakkinda";
            Load += FormHakkinda_Load;
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl lblProjeAdi;
        private DevExpress.XtraEditors.LabelControl lblVersiyon;
        private DevExpress.XtraEditors.LabelControl lblGelistirici;
        private DevExpress.XtraEditors.LabelControl lblAciklama;
        private DevExpress.XtraEditors.LabelControl lblTelif;
    }
}