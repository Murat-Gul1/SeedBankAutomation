namespace TohumBankasiOtomasyonu
{
    partial class UcAnaSayfa
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            memoAnaAciklama = new DevExpress.XtraEditors.MemoEdit();
            lblAnaBaslik = new DevExpress.XtraEditors.LabelControl();
            sliderBitkiler = new DevExpress.XtraEditors.Controls.ImageSlider();
            flowPanelUrunler = new System.Windows.Forms.FlowLayoutPanel();
            ucUrunKarti1 = new UcUrunKarti();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)memoAnaAciklama.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sliderBitkiler).BeginInit();
            flowPanelUrunler.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.69664F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.30336F));
            tableLayoutPanel1.Controls.Add(panelControl1, 1, 0);
            tableLayoutPanel1.Controls.Add(sliderBitkiler, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1190, 350);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(memoAnaAciklama);
            panelControl1.Controls.Add(lblAnaBaslik);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Location = new System.Drawing.Point(713, 3);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(474, 306);
            panelControl1.TabIndex = 1;
            // 
            // memoAnaAciklama
            // 
            memoAnaAciklama.Dock = System.Windows.Forms.DockStyle.Bottom;
            memoAnaAciklama.Location = new System.Drawing.Point(2, 36);
            memoAnaAciklama.Name = "memoAnaAciklama";
            memoAnaAciklama.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            memoAnaAciklama.Properties.Appearance.Options.UseFont = true;
            memoAnaAciklama.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            memoAnaAciklama.Properties.ReadOnly = true;
            memoAnaAciklama.Size = new System.Drawing.Size(470, 268);
            memoAnaAciklama.TabIndex = 1;
            // 
            // lblAnaBaslik
            // 
            lblAnaBaslik.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblAnaBaslik.Appearance.Options.UseFont = true;
            lblAnaBaslik.Location = new System.Drawing.Point(5, 5);
            lblAnaBaslik.Name = "lblAnaBaslik";
            lblAnaBaslik.Size = new System.Drawing.Size(121, 25);
            lblAnaBaslik.TabIndex = 0;
            lblAnaBaslik.Text = "labelControl1";
            // 
            // sliderBitkiler
            // 
            sliderBitkiler.AllowLooping = true;
            sliderBitkiler.AutoSlide = DevExpress.XtraEditors.Controls.AutoSlide.Forward;
            sliderBitkiler.AutoSlideInterval = 3000;
            sliderBitkiler.Dock = System.Windows.Forms.DockStyle.Fill;
            sliderBitkiler.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            sliderBitkiler.Location = new System.Drawing.Point(3, 3);
            sliderBitkiler.Name = "sliderBitkiler";
            sliderBitkiler.Size = new System.Drawing.Size(704, 306);
            sliderBitkiler.TabIndex = 0;
            sliderBitkiler.Text = "ımageSlider1";
            // 
            // flowPanelUrunler
            // 
            flowPanelUrunler.AutoScroll = true;
            flowPanelUrunler.Controls.Add(ucUrunKarti1);
            flowPanelUrunler.Dock = System.Windows.Forms.DockStyle.Fill;
            flowPanelUrunler.Location = new System.Drawing.Point(0, 350);
            flowPanelUrunler.Name = "flowPanelUrunler";
            flowPanelUrunler.Size = new System.Drawing.Size(1190, 218);
            flowPanelUrunler.TabIndex = 1;
            // 
            // ucUrunKarti1
            // 
            ucUrunKarti1.Appearance.BackColor = System.Drawing.Color.White;
            ucUrunKarti1.Appearance.Options.UseBackColor = true;
            ucUrunKarti1.Location = new System.Drawing.Point(3, 3);
            ucUrunKarti1.Name = "ucUrunKarti1";
            ucUrunKarti1.Size = new System.Drawing.Size(250, 300);
            ucUrunKarti1.TabIndex = 0;
            // 
            // UcAnaSayfa
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(flowPanelUrunler);
            Controls.Add(tableLayoutPanel1);
            Name = "UcAnaSayfa";
            Size = new System.Drawing.Size(1190, 568);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)memoAnaAciklama.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)sliderBitkiler).EndInit();
            flowPanelUrunler.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.Controls.ImageSlider sliderBitkiler;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblAnaBaslik;
        private DevExpress.XtraEditors.MemoEdit memoAnaAciklama;
        private System.Windows.Forms.FlowLayoutPanel flowPanelUrunler;
        private UcUrunKarti ucUrunKarti1;
    }
}
