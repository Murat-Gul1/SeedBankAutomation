namespace TohumBankasiOtomasyonu
{
    partial class UcBitkiTakip
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
            btnYeniEkle = new DevExpress.XtraEditors.SimpleButton();
            lblBaslik = new DevExpress.XtraEditors.LabelControl();
            flowPanelBitkilerim = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(btnYeniEkle);
            panelControl1.Controls.Add(lblBaslik);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(802, 80);
            panelControl1.TabIndex = 0;
            // 
            // btnYeniEkle
            // 
            btnYeniEkle.Dock = System.Windows.Forms.DockStyle.Right;
            btnYeniEkle.ImageOptions.Image = Properties.Resources.plant1;
            btnYeniEkle.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnYeniEkle.Location = new System.Drawing.Point(559, 2);
            btnYeniEkle.LookAndFeel.UseDefaultLookAndFeel = false;
            btnYeniEkle.Name = "btnYeniEkle";
            btnYeniEkle.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnYeniEkle.Size = new System.Drawing.Size(241, 76);
            btnYeniEkle.TabIndex = 1;
            btnYeniEkle.Click += btnYeniEkle_Click;
            // 
            // lblBaslik
            // 
            lblBaslik.Appearance.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblBaslik.Appearance.Options.UseFont = true;
            lblBaslik.Location = new System.Drawing.Point(5, 5);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Size = new System.Drawing.Size(140, 25);
            lblBaslik.TabIndex = 0;
            lblBaslik.Text = "labelControl1";
            // 
            // flowPanelBitkilerim
            // 
            flowPanelBitkilerim.AutoScroll = true;
            flowPanelBitkilerim.BackColor = System.Drawing.Color.White;
            flowPanelBitkilerim.Dock = System.Windows.Forms.DockStyle.Fill;
            flowPanelBitkilerim.Location = new System.Drawing.Point(0, 80);
            flowPanelBitkilerim.Name = "flowPanelBitkilerim";
            flowPanelBitkilerim.Size = new System.Drawing.Size(802, 380);
            flowPanelBitkilerim.TabIndex = 1;
            // 
            // UcBitkiTakip
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(flowPanelBitkilerim);
            Controls.Add(panelControl1);
            Name = "UcBitkiTakip";
            Size = new System.Drawing.Size(802, 460);
            Load += UcBitkiTakip_Load;
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnYeniEkle;
        private DevExpress.XtraEditors.LabelControl lblBaslik;
        private System.Windows.Forms.FlowLayoutPanel flowPanelBitkilerim;
    }
}
