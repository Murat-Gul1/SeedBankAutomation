namespace TohumBankasiOtomasyonu
{
    partial class UcSatisYonetimi
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
            btnSatisDetay = new DevExpress.XtraEditors.SimpleButton();
            gridSatislar = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridSatislar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(btnSatisDetay);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(432, 57);
            panelControl1.TabIndex = 0;
            // 
            // btnSatisDetay
            // 
            btnSatisDetay.Dock = System.Windows.Forms.DockStyle.Left;
            btnSatisDetay.ImageOptions.Image = Properties.Resources.zoom;
            btnSatisDetay.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnSatisDetay.Location = new System.Drawing.Point(2, 2);
            btnSatisDetay.LookAndFeel.UseDefaultLookAndFeel = false;
            btnSatisDetay.Name = "btnSatisDetay";
            btnSatisDetay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnSatisDetay.Size = new System.Drawing.Size(75, 53);
            btnSatisDetay.TabIndex = 0;
            btnSatisDetay.Click += btnSatisDetay_Click;
            // 
            // gridSatislar
            // 
            gridSatislar.Dock = System.Windows.Forms.DockStyle.Fill;
            gridSatislar.Location = new System.Drawing.Point(0, 57);
            gridSatislar.MainView = gridView1;
            gridSatislar.Name = "gridSatislar";
            gridSatislar.Size = new System.Drawing.Size(432, 273);
            gridSatislar.TabIndex = 1;
            gridSatislar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridSatislar;
            gridView1.Name = "gridView1";
            // 
            // UcSatisYonetimi
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gridSatislar);
            Controls.Add(panelControl1);
            Name = "UcSatisYonetimi";
            Size = new System.Drawing.Size(432, 330);
            Load += UcSatisYonetimi_Load;
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridSatislar).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSatisDetay;
        private DevExpress.XtraGrid.GridControl gridSatislar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
