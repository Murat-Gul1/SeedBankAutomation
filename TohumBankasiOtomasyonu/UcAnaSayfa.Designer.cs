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
            pnlZemin = new DevExpress.XtraEditors.XtraScrollableControl();
            pnlMerkezKutu = new DevExpress.XtraEditors.PanelControl();
            flowPanelUrunler = new System.Windows.Forms.FlowLayoutPanel();
            pnlUstKisim = new DevExpress.XtraEditors.PanelControl();
            memoAnaAciklama = new DevExpress.XtraEditors.MemoEdit();
            lblAnaBaslik = new DevExpress.XtraEditors.LabelControl();
            sliderBitkiler = new DevExpress.XtraEditors.Controls.ImageSlider();
            pnlZemin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pnlMerkezKutu).BeginInit();
            pnlMerkezKutu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pnlUstKisim).BeginInit();
            pnlUstKisim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)memoAnaAciklama.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sliderBitkiler).BeginInit();
            SuspendLayout();
            // 
            // pnlZemin
            // 
            pnlZemin.Controls.Add(pnlMerkezKutu);
            pnlZemin.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlZemin.Location = new System.Drawing.Point(0, 0);
            pnlZemin.Name = "pnlZemin";
            pnlZemin.Size = new System.Drawing.Size(1190, 568);
            pnlZemin.TabIndex = 0;
            // 
            // pnlMerkezKutu
            // 
            pnlMerkezKutu.AutoSize = true;
            pnlMerkezKutu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pnlMerkezKutu.Controls.Add(flowPanelUrunler);
            pnlMerkezKutu.Controls.Add(pnlUstKisim);
            pnlMerkezKutu.Location = new System.Drawing.Point(38, 3);
            pnlMerkezKutu.Name = "pnlMerkezKutu";
            pnlMerkezKutu.Size = new System.Drawing.Size(1100, 1100);
            pnlMerkezKutu.TabIndex = 0;
            // 
            // flowPanelUrunler
            // 
            flowPanelUrunler.AutoSize = true;
            flowPanelUrunler.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowPanelUrunler.Dock = System.Windows.Forms.DockStyle.Top;
            flowPanelUrunler.Location = new System.Drawing.Point(0, 280);
            flowPanelUrunler.Name = "flowPanelUrunler";
            flowPanelUrunler.Size = new System.Drawing.Size(1100, 0);
            flowPanelUrunler.TabIndex = 1;
            // 
            // pnlUstKisim
            // 
            pnlUstKisim.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pnlUstKisim.Controls.Add(memoAnaAciklama);
            pnlUstKisim.Controls.Add(lblAnaBaslik);
            pnlUstKisim.Controls.Add(sliderBitkiler);
            pnlUstKisim.Dock = System.Windows.Forms.DockStyle.Top;
            pnlUstKisim.Location = new System.Drawing.Point(0, 0);
            pnlUstKisim.Name = "pnlUstKisim";
            pnlUstKisim.Size = new System.Drawing.Size(1100, 280);
            pnlUstKisim.TabIndex = 0;
            // 
            // memoAnaAciklama
            // 
            memoAnaAciklama.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            memoAnaAciklama.Location = new System.Drawing.Point(516, 37);
            memoAnaAciklama.Name = "memoAnaAciklama";
            memoAnaAciklama.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(214, 219, 233);
            memoAnaAciklama.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            memoAnaAciklama.Properties.Appearance.Options.UseBackColor = true;
            memoAnaAciklama.Properties.Appearance.Options.UseFont = true;
            memoAnaAciklama.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            memoAnaAciklama.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            memoAnaAciklama.Size = new System.Drawing.Size(584, 243);
            memoAnaAciklama.TabIndex = 2;
            // 
            // lblAnaBaslik
            // 
            lblAnaBaslik.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblAnaBaslik.Appearance.Options.UseFont = true;
            lblAnaBaslik.Location = new System.Drawing.Point(516, 3);
            lblAnaBaslik.Name = "lblAnaBaslik";
            lblAnaBaslik.Size = new System.Drawing.Size(140, 25);
            lblAnaBaslik.TabIndex = 1;
            lblAnaBaslik.Text = "labelControl1";
            // 
            // sliderBitkiler
            // 
            sliderBitkiler.AllowLooping = true;
            sliderBitkiler.Appearance.BackColor = System.Drawing.Color.FromArgb(214, 219, 233);
            sliderBitkiler.Appearance.Options.UseBackColor = true;
            sliderBitkiler.Dock = System.Windows.Forms.DockStyle.Left;
            sliderBitkiler.Location = new System.Drawing.Point(0, 0);
            sliderBitkiler.Name = "sliderBitkiler";
            sliderBitkiler.Size = new System.Drawing.Size(500, 280);
            sliderBitkiler.TabIndex = 0;
            sliderBitkiler.Text = "ımageSlider1";
            // 
            // UcAnaSayfa
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(pnlZemin);
            Name = "UcAnaSayfa";
            Size = new System.Drawing.Size(1190, 568);
            Resize += UcAnaSayfa_Resize;
            pnlZemin.ResumeLayout(false);
            pnlZemin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pnlMerkezKutu).EndInit();
            pnlMerkezKutu.ResumeLayout(false);
            pnlMerkezKutu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pnlUstKisim).EndInit();
            pnlUstKisim.ResumeLayout(false);
            pnlUstKisim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)memoAnaAciklama.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)sliderBitkiler).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl pnlZemin;
        private DevExpress.XtraEditors.PanelControl pnlMerkezKutu;
        private DevExpress.XtraEditors.PanelControl pnlUstKisim;
        private DevExpress.XtraEditors.Controls.ImageSlider sliderBitkiler;
        private DevExpress.XtraEditors.MemoEdit memoAnaAciklama;
        private DevExpress.XtraEditors.LabelControl lblAnaBaslik;
        private System.Windows.Forms.FlowLayoutPanel flowPanelUrunler;
    }
}
