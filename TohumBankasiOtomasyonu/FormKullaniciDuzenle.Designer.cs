namespace TohumBankasiOtomasyonu
{
    partial class FormKullaniciDuzenle
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
            btnHesapTemizle = new DevExpress.XtraEditors.SimpleButton();
            txtYeniSifreTekrar = new DevExpress.XtraEditors.TextEdit();
            txtYeniSifre = new DevExpress.XtraEditors.TextEdit();
            txtKullaniciAdi = new DevExpress.XtraEditors.TextEdit();
            txtEmail = new DevExpress.XtraEditors.TextEdit();
            txtSoyad = new DevExpress.XtraEditors.TextEdit();
            txtAd = new DevExpress.XtraEditors.TextEdit();
            btnHesapGuncelle = new DevExpress.XtraEditors.SimpleButton();
            lblYeniSifreTekrar = new DevExpress.XtraEditors.LabelControl();
            lblYeniSifre = new DevExpress.XtraEditors.LabelControl();
            lblKullaniciAdi = new DevExpress.XtraEditors.LabelControl();
            lblEmail = new DevExpress.XtraEditors.LabelControl();
            lblSoyad = new DevExpress.XtraEditors.LabelControl();
            lblAd = new DevExpress.XtraEditors.LabelControl();
            lblKullaniciTipi = new DevExpress.XtraEditors.LabelControl();
            cmbKullaniciTipi = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)txtYeniSifreTekrar.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtYeniSifre.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtKullaniciAdi.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtEmail.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSoyad.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtAd.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbKullaniciTipi.Properties).BeginInit();
            SuspendLayout();
            // 
            // btnHesapTemizle
            // 
            btnHesapTemizle.Location = new System.Drawing.Point(226, 314);
            btnHesapTemizle.Name = "btnHesapTemizle";
            btnHesapTemizle.Size = new System.Drawing.Size(102, 36);
            btnHesapTemizle.TabIndex = 33;
            btnHesapTemizle.Text = "simpleButton1";
            btnHesapTemizle.Click += btnHesapTemizle_Click;
            // 
            // txtYeniSifreTekrar
            // 
            txtYeniSifreTekrar.Location = new System.Drawing.Point(196, 222);
            txtYeniSifreTekrar.Name = "txtYeniSifreTekrar";
            txtYeniSifreTekrar.Properties.UseSystemPasswordChar = true;
            txtYeniSifreTekrar.Size = new System.Drawing.Size(139, 20);
            txtYeniSifreTekrar.TabIndex = 31;
            // 
            // txtYeniSifre
            // 
            txtYeniSifre.Location = new System.Drawing.Point(196, 181);
            txtYeniSifre.Name = "txtYeniSifre";
            txtYeniSifre.Properties.UseSystemPasswordChar = true;
            txtYeniSifre.Size = new System.Drawing.Size(139, 20);
            txtYeniSifre.TabIndex = 30;
            // 
            // txtKullaniciAdi
            // 
            txtKullaniciAdi.Location = new System.Drawing.Point(196, 140);
            txtKullaniciAdi.Name = "txtKullaniciAdi";
            txtKullaniciAdi.Size = new System.Drawing.Size(139, 20);
            txtKullaniciAdi.TabIndex = 29;
            // 
            // txtEmail
            // 
            txtEmail.Location = new System.Drawing.Point(196, 99);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new System.Drawing.Size(139, 20);
            txtEmail.TabIndex = 28;
            // 
            // txtSoyad
            // 
            txtSoyad.Location = new System.Drawing.Point(196, 58);
            txtSoyad.Name = "txtSoyad";
            txtSoyad.Size = new System.Drawing.Size(139, 20);
            txtSoyad.TabIndex = 27;
            // 
            // txtAd
            // 
            txtAd.Location = new System.Drawing.Point(196, 17);
            txtAd.Name = "txtAd";
            txtAd.Size = new System.Drawing.Size(139, 20);
            txtAd.TabIndex = 26;
            // 
            // btnHesapGuncelle
            // 
            btnHesapGuncelle.Location = new System.Drawing.Point(118, 314);
            btnHesapGuncelle.Name = "btnHesapGuncelle";
            btnHesapGuncelle.Size = new System.Drawing.Size(102, 36);
            btnHesapGuncelle.TabIndex = 24;
            btnHesapGuncelle.Text = "simpleButton1";
            btnHesapGuncelle.Click += btnHesapGuncelle_Click;
            // 
            // lblYeniSifreTekrar
            // 
            lblYeniSifreTekrar.Appearance.Options.UseTextOptions = true;
            lblYeniSifreTekrar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            lblYeniSifreTekrar.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblYeniSifreTekrar.Location = new System.Drawing.Point(60, 225);
            lblYeniSifreTekrar.Name = "lblYeniSifreTekrar";
            lblYeniSifreTekrar.Size = new System.Drawing.Size(130, 13);
            lblYeniSifreTekrar.TabIndex = 22;
            lblYeniSifreTekrar.Text = "yenisifretekrar";
            // 
            // lblYeniSifre
            // 
            lblYeniSifre.Appearance.Options.UseTextOptions = true;
            lblYeniSifre.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            lblYeniSifre.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblYeniSifre.Location = new System.Drawing.Point(89, 184);
            lblYeniSifre.Name = "lblYeniSifre";
            lblYeniSifre.Size = new System.Drawing.Size(101, 13);
            lblYeniSifre.TabIndex = 21;
            lblYeniSifre.Text = "yenisifre";
            // 
            // lblKullaniciAdi
            // 
            lblKullaniciAdi.Appearance.Options.UseTextOptions = true;
            lblKullaniciAdi.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            lblKullaniciAdi.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblKullaniciAdi.Location = new System.Drawing.Point(80, 143);
            lblKullaniciAdi.Name = "lblKullaniciAdi";
            lblKullaniciAdi.Size = new System.Drawing.Size(110, 13);
            lblKullaniciAdi.TabIndex = 20;
            lblKullaniciAdi.Text = "kullaniciadı";
            // 
            // lblEmail
            // 
            lblEmail.Appearance.Options.UseTextOptions = true;
            lblEmail.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            lblEmail.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblEmail.Location = new System.Drawing.Point(106, 102);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new System.Drawing.Size(84, 13);
            lblEmail.TabIndex = 19;
            lblEmail.Text = "email";
            // 
            // lblSoyad
            // 
            lblSoyad.Appearance.Options.UseTextOptions = true;
            lblSoyad.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            lblSoyad.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblSoyad.Location = new System.Drawing.Point(101, 61);
            lblSoyad.Name = "lblSoyad";
            lblSoyad.Size = new System.Drawing.Size(89, 13);
            lblSoyad.TabIndex = 18;
            lblSoyad.Text = "soyad";
            // 
            // lblAd
            // 
            lblAd.Appearance.Options.UseTextOptions = true;
            lblAd.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            lblAd.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblAd.Location = new System.Drawing.Point(118, 20);
            lblAd.Name = "lblAd";
            lblAd.Size = new System.Drawing.Size(72, 13);
            lblAd.TabIndex = 17;
            lblAd.Text = "ad";
            // 
            // lblKullaniciTipi
            // 
            lblKullaniciTipi.Location = new System.Drawing.Point(127, 268);
            lblKullaniciTipi.Name = "lblKullaniciTipi";
            lblKullaniciTipi.Size = new System.Drawing.Size(63, 13);
            lblKullaniciTipi.TabIndex = 34;
            lblKullaniciTipi.Text = "labelControl1";
            // 
            // cmbKullaniciTipi
            // 
            cmbKullaniciTipi.Location = new System.Drawing.Point(196, 265);
            cmbKullaniciTipi.Name = "cmbKullaniciTipi";
            cmbKullaniciTipi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbKullaniciTipi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbKullaniciTipi.Size = new System.Drawing.Size(139, 20);
            cmbKullaniciTipi.TabIndex = 35;
            // 
            // FormKullaniciDuzenle
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(447, 413);
            Controls.Add(cmbKullaniciTipi);
            Controls.Add(lblKullaniciTipi);
            Controls.Add(btnHesapTemizle);
            Controls.Add(txtYeniSifreTekrar);
            Controls.Add(txtYeniSifre);
            Controls.Add(txtKullaniciAdi);
            Controls.Add(txtEmail);
            Controls.Add(txtSoyad);
            Controls.Add(txtAd);
            Controls.Add(btnHesapGuncelle);
            Controls.Add(lblYeniSifreTekrar);
            Controls.Add(lblYeniSifre);
            Controls.Add(lblKullaniciAdi);
            Controls.Add(lblEmail);
            Controls.Add(lblSoyad);
            Controls.Add(lblAd);
            Name = "FormKullaniciDuzenle";
            Text = "FormKullaniciDuzenle";
            Load += FormKullaniciDuzenle_Load;
            ((System.ComponentModel.ISupportInitialize)txtYeniSifreTekrar.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtYeniSifre.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtKullaniciAdi.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtEmail.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSoyad.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtAd.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbKullaniciTipi.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnHesapTemizle;
        private DevExpress.XtraEditors.TextEdit txtYeniSifreTekrar;
        private DevExpress.XtraEditors.TextEdit txtYeniSifre;
        private DevExpress.XtraEditors.TextEdit txtKullaniciAdi;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.TextEdit txtSoyad;
        private DevExpress.XtraEditors.TextEdit txtAd;
        private DevExpress.XtraEditors.SimpleButton btnHesapGuncelle;
        private DevExpress.XtraEditors.LabelControl lblYeniSifreTekrar;
        private DevExpress.XtraEditors.LabelControl lblYeniSifre;
        private DevExpress.XtraEditors.LabelControl lblKullaniciAdi;
        private DevExpress.XtraEditors.LabelControl lblEmail;
        private DevExpress.XtraEditors.LabelControl lblSoyad;
        private DevExpress.XtraEditors.LabelControl lblAd;
        private DevExpress.XtraEditors.LabelControl lblKullaniciTipi;
        private DevExpress.XtraEditors.ComboBoxEdit cmbKullaniciTipi;
    }
}