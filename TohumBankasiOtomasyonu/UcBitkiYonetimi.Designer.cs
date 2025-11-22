namespace TohumBankasiOtomasyonu
{
    partial class UcBitkiYonetimi
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
            btnBitkiSil = new DevExpress.XtraEditors.SimpleButton();
            btnBitkiDuzenle = new DevExpress.XtraEditors.SimpleButton();
            btnBitkiEkle = new DevExpress.XtraEditors.SimpleButton();
            gridBitkiler = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridBitkiler).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(btnBitkiSil);
            panelControl1.Controls.Add(btnBitkiDuzenle);
            panelControl1.Controls.Add(btnBitkiEkle);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(533, 60);
            panelControl1.TabIndex = 0;
            // 
            // btnBitkiSil
            // 
            btnBitkiSil.Dock = System.Windows.Forms.DockStyle.Left;
            btnBitkiSil.ImageOptions.Image = Properties.Resources.delete;
            btnBitkiSil.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnBitkiSil.Location = new System.Drawing.Point(152, 2);
            btnBitkiSil.LookAndFeel.UseDefaultLookAndFeel = false;
            btnBitkiSil.Name = "btnBitkiSil";
            btnBitkiSil.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnBitkiSil.Size = new System.Drawing.Size(75, 56);
            btnBitkiSil.TabIndex = 2;
            // 
            // btnBitkiDuzenle
            // 
            btnBitkiDuzenle.Dock = System.Windows.Forms.DockStyle.Left;
            btnBitkiDuzenle.ImageOptions.Image = Properties.Resources.sync;
            btnBitkiDuzenle.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnBitkiDuzenle.Location = new System.Drawing.Point(77, 2);
            btnBitkiDuzenle.LookAndFeel.UseDefaultLookAndFeel = false;
            btnBitkiDuzenle.Name = "btnBitkiDuzenle";
            btnBitkiDuzenle.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnBitkiDuzenle.Size = new System.Drawing.Size(75, 56);
            btnBitkiDuzenle.TabIndex = 1;
            // 
            // btnBitkiEkle
            // 
            btnBitkiEkle.Dock = System.Windows.Forms.DockStyle.Left;
            btnBitkiEkle.ImageOptions.Image = Properties.Resources.add;
            btnBitkiEkle.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnBitkiEkle.Location = new System.Drawing.Point(2, 2);
            btnBitkiEkle.LookAndFeel.UseDefaultLookAndFeel = false;
            btnBitkiEkle.Name = "btnBitkiEkle";
            btnBitkiEkle.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnBitkiEkle.Size = new System.Drawing.Size(75, 56);
            btnBitkiEkle.TabIndex = 0;
            btnBitkiEkle.Click += btnBitkiEkle_Click;
            // 
            // gridBitkiler
            // 
            gridBitkiler.Dock = System.Windows.Forms.DockStyle.Fill;
            gridBitkiler.Location = new System.Drawing.Point(0, 60);
            gridBitkiler.MainView = gridView1;
            gridBitkiler.Name = "gridBitkiler";
            gridBitkiler.Size = new System.Drawing.Size(533, 366);
            gridBitkiler.TabIndex = 1;
            gridBitkiler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridBitkiler;
            gridView1.Name = "gridView1";
            // 
            // UcBitkiYonetimi
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gridBitkiler);
            Controls.Add(panelControl1);
            Name = "UcBitkiYonetimi";
            Size = new System.Drawing.Size(533, 426);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridBitkiler).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnBitkiEkle;
        private DevExpress.XtraEditors.SimpleButton btnBitkiDuzenle;
        private DevExpress.XtraEditors.SimpleButton btnBitkiSil;
        private DevExpress.XtraGrid.GridControl gridBitkiler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
