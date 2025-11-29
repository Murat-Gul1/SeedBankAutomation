namespace TohumBankasiOtomasyonu
{
    partial class UcBitkiBilgi
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
            listAramaSonuclari = new DevExpress.XtraEditors.ListBoxControl();
            txtArama = new DevExpress.XtraEditors.SearchControl();
            pnlBilgiKarti = new DevExpress.XtraEditors.PanelControl();
            tabBilgiDetay = new DevExpress.XtraTab.XtraTabControl();
            pageGenel = new DevExpress.XtraTab.XtraTabPage();
            memoGenelBilgi = new DevExpress.XtraEditors.MemoEdit();
            pageTeknik = new DevExpress.XtraTab.XtraTabPage();
            memoTeknikBilgi = new DevExpress.XtraEditors.MemoEdit();
            lblBilgiBilimsel = new DevExpress.XtraEditors.LabelControl();
            lblBilgiAd = new DevExpress.XtraEditors.LabelControl();
            picBilgiResim = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)listAramaSonuclari).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtArama.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pnlBilgiKarti).BeginInit();
            pnlBilgiKarti.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tabBilgiDetay).BeginInit();
            tabBilgiDetay.SuspendLayout();
            pageGenel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)memoGenelBilgi.Properties).BeginInit();
            pageTeknik.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)memoTeknikBilgi.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBilgiResim.Properties).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl1.Controls.Add(listAramaSonuclari);
            panelControl1.Controls.Add(txtArama);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1192, 150);
            panelControl1.TabIndex = 0;
            // 
            // listAramaSonuclari
            // 
            listAramaSonuclari.Anchor = System.Windows.Forms.AnchorStyles.Top;
            listAramaSonuclari.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            listAramaSonuclari.Appearance.Options.UseFont = true;
            listAramaSonuclari.Location = new System.Drawing.Point(422, 49);
            listAramaSonuclari.Name = "listAramaSonuclari";
            listAramaSonuclari.Size = new System.Drawing.Size(362, 95);
            listAramaSonuclari.TabIndex = 1;
            listAramaSonuclari.Visible = false;
            listAramaSonuclari.Click += listAramaSonuclari_Click;
            // 
            // txtArama
            // 
            txtArama.Anchor = System.Windows.Forms.AnchorStyles.Top;
            txtArama.Location = new System.Drawing.Point(422, 5);
            txtArama.Name = "txtArama";
            txtArama.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            txtArama.Properties.Appearance.Options.UseFont = true;
            txtArama.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            txtArama.Properties.NullValuePrompt = "Bitki adı veya bilimsel ad yazın...";
            txtArama.Size = new System.Drawing.Size(362, 30);
            txtArama.TabIndex = 0;
            txtArama.EditValueChanged += txtArama_EditValueChanged;
            // 
            // pnlBilgiKarti
            // 
            pnlBilgiKarti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pnlBilgiKarti.Controls.Add(tabBilgiDetay);
            pnlBilgiKarti.Controls.Add(lblBilgiBilimsel);
            pnlBilgiKarti.Controls.Add(lblBilgiAd);
            pnlBilgiKarti.Controls.Add(picBilgiResim);
            pnlBilgiKarti.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlBilgiKarti.Location = new System.Drawing.Point(0, 150);
            pnlBilgiKarti.Name = "pnlBilgiKarti";
            pnlBilgiKarti.Size = new System.Drawing.Size(1192, 458);
            pnlBilgiKarti.TabIndex = 2;
            // 
            // tabBilgiDetay
            // 
            tabBilgiDetay.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabBilgiDetay.Location = new System.Drawing.Point(359, 59);
            tabBilgiDetay.Name = "tabBilgiDetay";
            tabBilgiDetay.SelectedTabPage = pageGenel;
            tabBilgiDetay.Size = new System.Drawing.Size(830, 380);
            tabBilgiDetay.TabIndex = 3;
            tabBilgiDetay.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { pageGenel, pageTeknik });
            // 
            // pageGenel
            // 
            pageGenel.Controls.Add(memoGenelBilgi);
            pageGenel.Name = "pageGenel";
            pageGenel.Size = new System.Drawing.Size(828, 353);
            pageGenel.Text = "xtraTabPage1";
            // 
            // memoGenelBilgi
            // 
            memoGenelBilgi.Dock = System.Windows.Forms.DockStyle.Fill;
            memoGenelBilgi.Location = new System.Drawing.Point(0, 0);
            memoGenelBilgi.Name = "memoGenelBilgi";
            memoGenelBilgi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            memoGenelBilgi.Properties.Appearance.Options.UseFont = true;
            memoGenelBilgi.Properties.ReadOnly = true;
            memoGenelBilgi.Size = new System.Drawing.Size(828, 353);
            memoGenelBilgi.TabIndex = 0;
            // 
            // pageTeknik
            // 
            pageTeknik.Controls.Add(memoTeknikBilgi);
            pageTeknik.Name = "pageTeknik";
            pageTeknik.Size = new System.Drawing.Size(828, 355);
            pageTeknik.Text = "xtraTabPage2";
            // 
            // memoTeknikBilgi
            // 
            memoTeknikBilgi.Dock = System.Windows.Forms.DockStyle.Fill;
            memoTeknikBilgi.Location = new System.Drawing.Point(0, 0);
            memoTeknikBilgi.Name = "memoTeknikBilgi";
            memoTeknikBilgi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            memoTeknikBilgi.Properties.Appearance.Options.UseFont = true;
            memoTeknikBilgi.Properties.ReadOnly = true;
            memoTeknikBilgi.Size = new System.Drawing.Size(828, 355);
            memoTeknikBilgi.TabIndex = 0;
            // 
            // lblBilgiBilimsel
            // 
            lblBilgiBilimsel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 162);
            lblBilgiBilimsel.Appearance.ForeColor = System.Drawing.Color.Gray;
            lblBilgiBilimsel.Appearance.Options.UseFont = true;
            lblBilgiBilimsel.Appearance.Options.UseForeColor = true;
            lblBilgiBilimsel.Location = new System.Drawing.Point(359, 37);
            lblBilgiBilimsel.Name = "lblBilgiBilimsel";
            lblBilgiBilimsel.Size = new System.Drawing.Size(76, 16);
            lblBilgiBilimsel.TabIndex = 2;
            lblBilgiBilimsel.Text = "labelControl1";
            // 
            // lblBilgiAd
            // 
            lblBilgiAd.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblBilgiAd.Appearance.ForeColor = System.Drawing.Color.Green;
            lblBilgiAd.Appearance.Options.UseFont = true;
            lblBilgiAd.Appearance.Options.UseForeColor = true;
            lblBilgiAd.Location = new System.Drawing.Point(359, 6);
            lblBilgiAd.Name = "lblBilgiAd";
            lblBilgiAd.Size = new System.Drawing.Size(140, 25);
            lblBilgiAd.TabIndex = 1;
            lblBilgiAd.Text = "labelControl1";
            // 
            // picBilgiResim
            // 
            picBilgiResim.Anchor = System.Windows.Forms.AnchorStyles.Left;
            picBilgiResim.Location = new System.Drawing.Point(3, 6);
            picBilgiResim.Name = "picBilgiResim";
            picBilgiResim.Properties.ReadOnly = true;
            picBilgiResim.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            picBilgiResim.Properties.ShowMenu = false;
            picBilgiResim.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            picBilgiResim.Size = new System.Drawing.Size(350, 350);
            picBilgiResim.TabIndex = 0;
            // 
            // UcBitkiBilgi
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(pnlBilgiKarti);
            Controls.Add(panelControl1);
            Name = "UcBitkiBilgi";
            Size = new System.Drawing.Size(1192, 608);
            Load += UcBitkiBilgi_Load;
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)listAramaSonuclari).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtArama.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)pnlBilgiKarti).EndInit();
            pnlBilgiKarti.ResumeLayout(false);
            pnlBilgiKarti.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tabBilgiDetay).EndInit();
            tabBilgiDetay.ResumeLayout(false);
            pageGenel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)memoGenelBilgi.Properties).EndInit();
            pageTeknik.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)memoTeknikBilgi.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBilgiResim.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SearchControl txtArama;
        private DevExpress.XtraEditors.PanelControl pnlBilgiKarti;
        private DevExpress.XtraEditors.PictureEdit picBilgiResim;
        private DevExpress.XtraEditors.LabelControl lblBilgiBilimsel;
        private DevExpress.XtraEditors.LabelControl lblBilgiAd;
        private DevExpress.XtraTab.XtraTabControl tabBilgiDetay;
        private DevExpress.XtraTab.XtraTabPage pageGenel;
        private DevExpress.XtraTab.XtraTabPage pageTeknik;
        private DevExpress.XtraEditors.MemoEdit memoGenelBilgi;
        private DevExpress.XtraEditors.MemoEdit memoTeknikBilgi;
        private DevExpress.XtraEditors.ListBoxControl listAramaSonuclari;
    }
}
