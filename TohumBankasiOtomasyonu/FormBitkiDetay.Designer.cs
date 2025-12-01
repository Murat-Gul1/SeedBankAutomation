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
            btnRaporAl = new DevExpress.XtraEditors.SimpleButton();
            lblBitkiAdi = new DevExpress.XtraEditors.LabelControl();
            picBitkiResmi = new DevExpress.XtraEditors.PictureEdit();
            pnlAlt = new DevExpress.XtraEditors.PanelControl();
            btnGonder = new DevExpress.XtraEditors.SimpleButton();
            txtMesaj = new DevExpress.XtraEditors.MemoEdit();
            chatEkrani = new DevExpress.XtraRichEdit.RichEditControl();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBitkiResmi.Properties).BeginInit();
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
            splitContainerControl1.Panel1.Controls.Add(btnRaporAl);
            splitContainerControl1.Panel1.Controls.Add(lblBitkiAdi);
            splitContainerControl1.Panel1.Controls.Add(picBitkiResmi);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(pnlAlt);
            splitContainerControl1.Panel2.Controls.Add(chatEkrani);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new System.Drawing.Size(1058, 486);
            splitContainerControl1.SplitterPosition = 361;
            splitContainerControl1.TabIndex = 0;
            // 
            // btnRaporAl
            // 
            btnRaporAl.Dock = System.Windows.Forms.DockStyle.Top;
            btnRaporAl.ImageOptions.Image = Properties.Resources.report;
            btnRaporAl.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnRaporAl.Location = new System.Drawing.Point(0, 223);
            btnRaporAl.LookAndFeel.UseDefaultLookAndFeel = false;
            btnRaporAl.Name = "btnRaporAl";
            btnRaporAl.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnRaporAl.Size = new System.Drawing.Size(361, 77);
            btnRaporAl.TabIndex = 2;
            btnRaporAl.Click += btnRaporAl_Click;
            // 
            // lblBitkiAdi
            // 
            lblBitkiAdi.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblBitkiAdi.Appearance.Options.UseFont = true;
            lblBitkiAdi.Dock = System.Windows.Forms.DockStyle.Top;
            lblBitkiAdi.Location = new System.Drawing.Point(0, 200);
            lblBitkiAdi.Name = "lblBitkiAdi";
            lblBitkiAdi.Size = new System.Drawing.Size(129, 23);
            lblBitkiAdi.TabIndex = 1;
            lblBitkiAdi.Text = "labelControl1";
            // 
            // picBitkiResmi
            // 
            picBitkiResmi.Dock = System.Windows.Forms.DockStyle.Top;
            picBitkiResmi.Location = new System.Drawing.Point(0, 0);
            picBitkiResmi.Name = "picBitkiResmi";
            picBitkiResmi.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            picBitkiResmi.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            picBitkiResmi.Size = new System.Drawing.Size(361, 200);
            picBitkiResmi.TabIndex = 0;
            // 
            // pnlAlt
            // 
            pnlAlt.Controls.Add(btnGonder);
            pnlAlt.Controls.Add(txtMesaj);
            pnlAlt.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlAlt.Location = new System.Drawing.Point(0, 351);
            pnlAlt.Name = "pnlAlt";
            pnlAlt.Size = new System.Drawing.Size(685, 135);
            pnlAlt.TabIndex = 1;
            // 
            // btnGonder
            // 
            btnGonder.Dock = System.Windows.Forms.DockStyle.Fill;
            btnGonder.ImageOptions.Image = Properties.Resources.send;
            btnGonder.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnGonder.Location = new System.Drawing.Point(2, 98);
            btnGonder.Name = "btnGonder";
            btnGonder.Size = new System.Drawing.Size(681, 35);
            btnGonder.TabIndex = 1;
            btnGonder.Click += btnGonder_Click;
            // 
            // txtMesaj
            // 
            txtMesaj.Dock = System.Windows.Forms.DockStyle.Top;
            txtMesaj.Location = new System.Drawing.Point(2, 2);
            txtMesaj.Name = "txtMesaj";
            txtMesaj.Size = new System.Drawing.Size(681, 96);
            txtMesaj.TabIndex = 0;
            // 
            // chatEkrani
            // 
            chatEkrani.Dock = System.Windows.Forms.DockStyle.Top;
            chatEkrani.Location = new System.Drawing.Point(0, 0);
            chatEkrani.Name = "chatEkrani";
            chatEkrani.Options.DocumentSaveOptions.CurrentFormat = DevExpress.XtraRichEdit.DocumentFormat.PlainText;
            chatEkrani.Options.HorizontalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            chatEkrani.Options.HorizontalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden;
            chatEkrani.Options.VerticalRuler.Visibility = DevExpress.XtraRichEdit.RichEditRulerVisibility.Hidden;
            chatEkrani.Options.VerticalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden;
            chatEkrani.ReadOnly = true;
            chatEkrani.Size = new System.Drawing.Size(685, 351);
            chatEkrani.TabIndex = 0;
            // 
            // FormBitkiDetay
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1058, 486);
            Controls.Add(splitContainerControl1);
            Name = "FormBitkiDetay";
            Text = "FormBitkiDetay";
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            splitContainerControl1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picBitkiResmi.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)pnlAlt).EndInit();
            pnlAlt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtMesaj.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SimpleButton btnRaporAl;
        private DevExpress.XtraEditors.LabelControl lblBitkiAdi;
        private DevExpress.XtraEditors.PictureEdit picBitkiResmi;
        private DevExpress.XtraRichEdit.RichEditControl chatEkrani;
        private DevExpress.XtraEditors.PanelControl pnlAlt;
        private DevExpress.XtraEditors.MemoEdit txtMesaj;
        private DevExpress.XtraEditors.SimpleButton btnGonder;
    }
}