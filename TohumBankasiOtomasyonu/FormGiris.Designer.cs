namespace TohumBankasiOtomasyonu
{
    partial class FormGiris
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
            lblKullaniciAdi = new DevExpress.XtraEditors.LabelControl();
            linkKayitOl = new DevExpress.XtraEditors.HyperlinkLabelControl();
            btnTemizle = new DevExpress.XtraEditors.SimpleButton();
            btnGirisYap = new DevExpress.XtraEditors.SimpleButton();
            txtSifre = new DevExpress.XtraEditors.TextEdit();
            lblSifre = new DevExpress.XtraEditors.LabelControl();
            txtKullaniciAdi = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)txtSifre.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtKullaniciAdi.Properties).BeginInit();
            SuspendLayout();
            // 
            // lblKullaniciAdi
            // 
            lblKullaniciAdi.Location = new System.Drawing.Point(93, 28);
            lblKullaniciAdi.Name = "lblKullaniciAdi";
            lblKullaniciAdi.Size = new System.Drawing.Size(63, 13);
            lblKullaniciAdi.TabIndex = 1;
            lblKullaniciAdi.Text = "labelControl1";
            // 
            // linkKayitOl
            // 
            linkKayitOl.Location = new System.Drawing.Point(136, 135);
            linkKayitOl.Name = "linkKayitOl";
            linkKayitOl.Size = new System.Drawing.Size(109, 13);
            linkKayitOl.TabIndex = 1;
            linkKayitOl.Text = "hyperlinkLabelControl1";
            linkKayitOl.Click += linkKayitOl_Click;
            // 
            // btnTemizle
            // 
            btnTemizle.Location = new System.Drawing.Point(198, 90);
            btnTemizle.Name = "btnTemizle";
            btnTemizle.Size = new System.Drawing.Size(109, 22);
            btnTemizle.TabIndex = 4;
            btnTemizle.Text = "simpleButton1";
            btnTemizle.Click += btnTemizle_Click;
            // 
            // btnGirisYap
            // 
            btnGirisYap.Location = new System.Drawing.Point(93, 90);
            btnGirisYap.Name = "btnGirisYap";
            btnGirisYap.Size = new System.Drawing.Size(99, 22);
            btnGirisYap.TabIndex = 3;
            btnGirisYap.Text = "simpleButton1";
            btnGirisYap.Click += btnGirisYap_Click;
            // 
            // txtSifre
            // 
            txtSifre.Location = new System.Drawing.Point(175, 51);
            txtSifre.Name = "txtSifre";
            txtSifre.Properties.UseSystemPasswordChar = true;
            txtSifre.Size = new System.Drawing.Size(193, 20);
            txtSifre.TabIndex = 1;
            // 
            // lblSifre
            // 
            lblSifre.Location = new System.Drawing.Point(93, 58);
            lblSifre.Name = "lblSifre";
            lblSifre.Size = new System.Drawing.Size(63, 13);
            lblSifre.TabIndex = 1;
            lblSifre.Text = "labelControl1";
            // 
            // txtKullaniciAdi
            // 
            txtKullaniciAdi.Location = new System.Drawing.Point(175, 25);
            txtKullaniciAdi.Name = "txtKullaniciAdi";
            txtKullaniciAdi.Size = new System.Drawing.Size(193, 20);
            txtKullaniciAdi.TabIndex = 0;
            // 
            // FormGiris
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(390, 176);
            Controls.Add(txtSifre);
            Controls.Add(lblKullaniciAdi);
            Controls.Add(txtKullaniciAdi);
            Controls.Add(linkKayitOl);
            Controls.Add(lblSifre);
            Controls.Add(btnTemizle);
            Controls.Add(btnGirisYap);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormGiris";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FormGiris";
            Load += FormGiris_Load;
            ((System.ComponentModel.ISupportInitialize)txtSifre.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtKullaniciAdi.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblKullaniciAdi;
        private DevExpress.XtraEditors.LabelControl lblSifre;
        private DevExpress.XtraEditors.TextEdit txtSifre;
        private DevExpress.XtraEditors.SimpleButton btnGirisYap;
        private DevExpress.XtraEditors.SimpleButton btnTemizle;
        private DevExpress.XtraEditors.HyperlinkLabelControl linkKayitOl;
        private DevExpress.XtraEditors.TextEdit txtKullaniciAdi;
    }
}