namespace TohumBankasiOtomasyonu
{
    partial class UcKullaniciYonetimi
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
            btnKullaniciSil = new DevExpress.XtraEditors.SimpleButton();
            btnKullaniciDuzenle = new DevExpress.XtraEditors.SimpleButton();
            btnKullaniciEkle = new DevExpress.XtraEditors.SimpleButton();
            gridKullanicilar = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridKullanicilar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(btnKullaniciSil);
            panelControl1.Controls.Add(btnKullaniciDuzenle);
            panelControl1.Controls.Add(btnKullaniciEkle);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(573, 60);
            panelControl1.TabIndex = 2;
            // 
            // btnKullaniciSil
            // 
            btnKullaniciSil.Dock = System.Windows.Forms.DockStyle.Left;
            btnKullaniciSil.ImageOptions.Image = Properties.Resources.delete;
            btnKullaniciSil.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnKullaniciSil.Location = new System.Drawing.Point(152, 2);
            btnKullaniciSil.LookAndFeel.UseDefaultLookAndFeel = false;
            btnKullaniciSil.Name = "btnKullaniciSil";
            btnKullaniciSil.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnKullaniciSil.Size = new System.Drawing.Size(75, 56);
            btnKullaniciSil.TabIndex = 2;
            btnKullaniciSil.Click += btnKullaniciSil_Click;
            // 
            // btnKullaniciDuzenle
            // 
            btnKullaniciDuzenle.Dock = System.Windows.Forms.DockStyle.Left;
            btnKullaniciDuzenle.ImageOptions.Image = Properties.Resources.sync;
            btnKullaniciDuzenle.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnKullaniciDuzenle.Location = new System.Drawing.Point(77, 2);
            btnKullaniciDuzenle.LookAndFeel.UseDefaultLookAndFeel = false;
            btnKullaniciDuzenle.Name = "btnKullaniciDuzenle";
            btnKullaniciDuzenle.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnKullaniciDuzenle.Size = new System.Drawing.Size(75, 56);
            btnKullaniciDuzenle.TabIndex = 1;
            btnKullaniciDuzenle.Click += btnKullaniciDuzenle_Click;
            // 
            // btnKullaniciEkle
            // 
            btnKullaniciEkle.Dock = System.Windows.Forms.DockStyle.Left;
            btnKullaniciEkle.ImageOptions.Image = Properties.Resources.add;
            btnKullaniciEkle.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnKullaniciEkle.Location = new System.Drawing.Point(2, 2);
            btnKullaniciEkle.LookAndFeel.UseDefaultLookAndFeel = false;
            btnKullaniciEkle.Name = "btnKullaniciEkle";
            btnKullaniciEkle.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnKullaniciEkle.Size = new System.Drawing.Size(75, 56);
            btnKullaniciEkle.TabIndex = 0;
            btnKullaniciEkle.Click += btnKullaniciEkle_Click;
            // 
            // gridKullanicilar
            // 
            gridKullanicilar.Dock = System.Windows.Forms.DockStyle.Fill;
            gridKullanicilar.Location = new System.Drawing.Point(0, 60);
            gridKullanicilar.MainView = gridView1;
            gridKullanicilar.Name = "gridKullanicilar";
            gridKullanicilar.Size = new System.Drawing.Size(573, 311);
            gridKullanicilar.TabIndex = 3;
            gridKullanicilar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridKullanicilar;
            gridView1.Name = "gridView1";
            // 
            // UcKullaniciYonetimi
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gridKullanicilar);
            Controls.Add(panelControl1);
            Name = "UcKullaniciYonetimi";
            Size = new System.Drawing.Size(573, 371);
            Load += UcKullaniciYonetimi_Load_1;
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridKullanicilar).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnKullaniciSil;
        private DevExpress.XtraEditors.SimpleButton btnKullaniciDuzenle;
        private DevExpress.XtraEditors.SimpleButton btnKullaniciEkle;
        private DevExpress.XtraGrid.GridControl gridKullanicilar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
