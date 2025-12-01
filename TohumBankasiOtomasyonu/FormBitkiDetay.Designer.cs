namespace TohumBankasiOtomasyonu
{
    partial class FormBitkiDetay
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
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            btnRaporArsivi = new DevExpress.XtraEditors.SimpleButton();
            btnRaporAl = new DevExpress.XtraEditors.SimpleButton();
            btnResimEkle = new DevExpress.XtraEditors.SimpleButton();
            lblBitkiAdi = new DevExpress.XtraEditors.LabelControl();
            flowResimPaneli = new System.Windows.Forms.FlowLayoutPanel();
            pnlAlt = new DevExpress.XtraEditors.PanelControl();
            chatEkrani = new DevExpress.XtraRichEdit.RichEditControl();
            txtMesaj = new DevExpress.XtraEditors.MemoEdit();
            btnGonder = new DevExpress.XtraEditors.SimpleButton();
            lblHafizaUyarisi = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pnlAlt).BeginInit();
            pnlAlt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtMesaj.Properties).BeginInit();
            SuspendLayout();
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Controls.Add(btnRaporArsivi);
            splitContainerControl1.Panel1.Controls.Add(btnRaporAl);
            splitContainerControl1.Panel1.Controls.Add(btnResimEkle);
            splitContainerControl1.Panel1.Controls.Add(lblBitkiAdi);
            splitContainerControl1.Panel1.Controls.Add(flowResimPaneli);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(pnlAlt);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new System.Drawing.Size(1156, 548);
            splitContainerControl1.SplitterPosition = 361;
            splitContainerControl1.TabIndex = 0;
            // 
            // btnRaporArsivi
            // 
            btnRaporArsivi.Dock = System.Windows.Forms.DockStyle.Top;
            btnRaporArsivi.ImageOptions.Image = Properties.Resources.book;
            btnRaporArsivi.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnRaporArsivi.Location = new System.Drawing.Point(0, 420);
            btnRaporArsivi.LookAndFeel.UseDefaultLookAndFeel = false;
            btnRaporArsivi.Name = "btnRaporArsivi";
            btnRaporArsivi.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnRaporArsivi.Size = new System.Drawing.Size(361, 77);
            btnRaporArsivi.TabIndex = 5;
            btnRaporArsivi.Click += btnRaporArsivi_Click;
            // 
            // btnRaporAl
            // 
            btnRaporAl.Dock = System.Windows.Forms.DockStyle.Top;
            btnRaporAl.ImageOptions.Image = Properties.Resources.report;
            btnRaporAl.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnRaporAl.Location = new System.Drawing.Point(0, 343);
            btnRaporAl.LookAndFeel.UseDefaultLookAndFeel = false;
            btnRaporAl.Name = "btnRaporAl";
            btnRaporAl.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnRaporAl.Size = new System.Drawing.Size(361, 77);
            btnRaporAl.TabIndex = 2;
            btnRaporAl.Click += btnRaporAl_Click;
            // 
            // btnResimEkle
            // 
            btnResimEkle.Dock = System.Windows.Forms.DockStyle.Top;
            btnResimEkle.ImageOptions.Image = Properties.Resources.add;
            btnResimEkle.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnResimEkle.Location = new System.Drawing.Point(0, 266);
            btnResimEkle.LookAndFeel.UseDefaultLookAndFeel = false;
            btnResimEkle.Name = "btnResimEkle";
            btnResimEkle.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnResimEkle.Size = new System.Drawing.Size(361, 77);
            btnResimEkle.TabIndex = 4;
            btnResimEkle.Click += btnResimEkle_Click;
            // 
            // lblBitkiAdi
            // 
            lblBitkiAdi.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblBitkiAdi.Appearance.Options.UseFont = true;
            lblBitkiAdi.Appearance.Options.UseTextOptions = true;
            lblBitkiAdi.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            lblBitkiAdi.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            lblBitkiAdi.Dock = System.Windows.Forms.DockStyle.Top;
            lblBitkiAdi.Location = new System.Drawing.Point(0, 243);
            lblBitkiAdi.Name = "lblBitkiAdi";
            lblBitkiAdi.Size = new System.Drawing.Size(361, 23);
            lblBitkiAdi.TabIndex = 1;
            lblBitkiAdi.Text = "labelControl1";
            // 
            // flowResimPaneli
            // 
            flowResimPaneli.AutoScroll = true;
            flowResimPaneli.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            flowResimPaneli.Dock = System.Windows.Forms.DockStyle.Top;
            flowResimPaneli.Location = new System.Drawing.Point(0, 0);
            flowResimPaneli.Name = "flowResimPaneli";
            flowResimPaneli.Size = new System.Drawing.Size(361, 243);
            flowResimPaneli.TabIndex = 3;
            // 
            // pnlAlt
            // 
            pnlAlt.Controls.Add(chatEkrani);
            pnlAlt.Controls.Add(txtMesaj);
            pnlAlt.Controls.Add(btnGonder);
            pnlAlt.Controls.Add(lblHafizaUyarisi);
            pnlAlt.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlAlt.Location = new System.Drawing.Point(0, 0);
            pnlAlt.Name = "pnlAlt";
            pnlAlt.Size = new System.Drawing.Size(783, 548);
            pnlAlt.TabIndex = 1;
            // 
            // chatEkrani
            // 
            chatEkrani.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            chatEkrani.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            chatEkrani.Dock = System.Windows.Forms.DockStyle.Fill;
            chatEkrani.LayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;
            chatEkrani.Location = new System.Drawing.Point(2, 2);
            chatEkrani.Name = "chatEkrani";
            chatEkrani.Options.DocumentSaveOptions.CurrentFormat = DevExpress.XtraRichEdit.DocumentFormat.PlainText;
            chatEkrani.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            chatEkrani.Options.HorizontalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden;
            chatEkrani.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            chatEkrani.Options.VerticalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden;
            chatEkrani.ReadOnly = true;
            chatEkrani.Size = new System.Drawing.Size(779, 416);
            chatEkrani.TabIndex = 0;
            // 
            // txtMesaj
            // 
            txtMesaj.Dock = System.Windows.Forms.DockStyle.Bottom;
            txtMesaj.Location = new System.Drawing.Point(2, 418);
            txtMesaj.Name = "txtMesaj";
            txtMesaj.Size = new System.Drawing.Size(779, 64);
            txtMesaj.TabIndex = 0;
            // 
            // btnGonder
            // 
            btnGonder.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnGonder.ImageOptions.Image = Properties.Resources.send;
            btnGonder.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnGonder.Location = new System.Drawing.Point(2, 482);
            btnGonder.Name = "btnGonder";
            btnGonder.Size = new System.Drawing.Size(779, 51);
            btnGonder.TabIndex = 1;
            btnGonder.Click += btnGonder_Click;
            // 
            // lblHafizaUyarisi
            // 
            lblHafizaUyarisi.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblHafizaUyarisi.Appearance.ForeColor = System.Drawing.Color.Red;
            lblHafizaUyarisi.Appearance.Options.UseFont = true;
            lblHafizaUyarisi.Appearance.Options.UseForeColor = true;
            lblHafizaUyarisi.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            lblHafizaUyarisi.Dock = System.Windows.Forms.DockStyle.Bottom;
            lblHafizaUyarisi.Location = new System.Drawing.Point(2, 533);
            lblHafizaUyarisi.Name = "lblHafizaUyarisi";
            lblHafizaUyarisi.Size = new System.Drawing.Size(779, 13);
            lblHafizaUyarisi.TabIndex = 2;
            lblHafizaUyarisi.Text = "labelControl1";
            // 
            // FormBitkiDetay
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1156, 548);
            Controls.Add(splitContainerControl1);
            Name = "FormBitkiDetay";
            Text = "FormBitkiDetay";
            Load += FormBitkiDetay_Load;
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pnlAlt).EndInit();
            pnlAlt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtMesaj.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SimpleButton btnRaporAl;
        private DevExpress.XtraEditors.LabelControl lblBitkiAdi;
        private DevExpress.XtraRichEdit.RichEditControl chatEkrani;
        private DevExpress.XtraEditors.PanelControl pnlAlt;
        private DevExpress.XtraEditors.MemoEdit txtMesaj;
        private DevExpress.XtraEditors.SimpleButton btnGonder;
        private DevExpress.XtraEditors.SimpleButton btnResimEkle;
        private System.Windows.Forms.FlowLayoutPanel flowResimPaneli;
        private DevExpress.XtraEditors.LabelControl lblHafizaUyarisi;
        private DevExpress.XtraEditors.SimpleButton btnRaporArsivi;
    }
}