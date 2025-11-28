namespace TohumBankasiOtomasyonu
{
    partial class UcStokYonetimi
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
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            gridStokListesi = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            btnStokAzalt = new DevExpress.XtraEditors.SimpleButton();
            btnStokGuncelle = new DevExpress.XtraEditors.SimpleButton();
            numStokEkle = new DevExpress.XtraEditors.SpinEdit();
            lblMevcutStok = new DevExpress.XtraEditors.LabelControl();
            lblUrunAdi = new DevExpress.XtraEditors.LabelControl();
            picUrunResmi = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridStokListesi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStokEkle.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picUrunResmi.Properties).BeginInit();
            SuspendLayout();
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Controls.Add(gridStokListesi);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(btnStokAzalt);
            splitContainerControl1.Panel2.Controls.Add(btnStokGuncelle);
            splitContainerControl1.Panel2.Controls.Add(numStokEkle);
            splitContainerControl1.Panel2.Controls.Add(lblMevcutStok);
            splitContainerControl1.Panel2.Controls.Add(lblUrunAdi);
            splitContainerControl1.Panel2.Controls.Add(picUrunResmi);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new System.Drawing.Size(846, 514);
            splitContainerControl1.SplitterPosition = 521;
            splitContainerControl1.TabIndex = 0;
            // 
            // gridStokListesi
            // 
            gridStokListesi.Dock = System.Windows.Forms.DockStyle.Fill;
            gridStokListesi.Location = new System.Drawing.Point(0, 0);
            gridStokListesi.MainView = gridView1;
            gridStokListesi.Name = "gridStokListesi";
            gridStokListesi.Size = new System.Drawing.Size(313, 514);
            gridStokListesi.TabIndex = 0;
            gridStokListesi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridStokListesi;
            gridView1.Name = "gridView1";
            gridView1.RowStyle += gridViewStok_RowStyle;
            gridView1.FocusedRowChanged += gridViewStok_FocusedRowChanged;
            // 
            // btnStokAzalt
            // 
            btnStokAzalt.ImageOptions.Image = Properties.Resources.minus;
            btnStokAzalt.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnStokAzalt.Location = new System.Drawing.Point(338, 384);
            btnStokAzalt.LookAndFeel.UseDefaultLookAndFeel = false;
            btnStokAzalt.Name = "btnStokAzalt";
            btnStokAzalt.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnStokAzalt.Size = new System.Drawing.Size(57, 32);
            btnStokAzalt.TabIndex = 5;
            btnStokAzalt.Click += btnStokAzalt_Click;
            // 
            // btnStokGuncelle
            // 
            btnStokGuncelle.ImageOptions.Image = Properties.Resources.add;
            btnStokGuncelle.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnStokGuncelle.Location = new System.Drawing.Point(276, 384);
            btnStokGuncelle.LookAndFeel.UseDefaultLookAndFeel = false;
            btnStokGuncelle.Name = "btnStokGuncelle";
            btnStokGuncelle.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnStokGuncelle.Size = new System.Drawing.Size(57, 32);
            btnStokGuncelle.TabIndex = 4;
            btnStokGuncelle.Click += btnStokGuncelle_Click;
            // 
            // numStokEkle
            // 
            numStokEkle.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            numStokEkle.Location = new System.Drawing.Point(276, 351);
            numStokEkle.Name = "numStokEkle";
            numStokEkle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            numStokEkle.Size = new System.Drawing.Size(119, 20);
            numStokEkle.TabIndex = 3;
            // 
            // lblMevcutStok
            // 
            lblMevcutStok.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblMevcutStok.Appearance.ForeColor = System.Drawing.Color.Red;
            lblMevcutStok.Appearance.Options.UseFont = true;
            lblMevcutStok.Appearance.Options.UseForeColor = true;
            lblMevcutStok.Location = new System.Drawing.Point(97, 384);
            lblMevcutStok.Name = "lblMevcutStok";
            lblMevcutStok.Size = new System.Drawing.Size(75, 13);
            lblMevcutStok.TabIndex = 2;
            lblMevcutStok.Text = "labelControl1";
            // 
            // lblUrunAdi
            // 
            lblUrunAdi.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblUrunAdi.Appearance.Options.UseFont = true;
            lblUrunAdi.Location = new System.Drawing.Point(97, 345);
            lblUrunAdi.Name = "lblUrunAdi";
            lblUrunAdi.Size = new System.Drawing.Size(110, 19);
            lblUrunAdi.TabIndex = 1;
            lblUrunAdi.Text = "labelControl1";
            // 
            // picUrunResmi
            // 
            picUrunResmi.Location = new System.Drawing.Point(97, 117);
            picUrunResmi.Name = "picUrunResmi";
            picUrunResmi.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            picUrunResmi.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            picUrunResmi.Size = new System.Drawing.Size(296, 228);
            picUrunResmi.TabIndex = 0;
            // 
            // UcStokYonetimi
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(splitContainerControl1);
            Name = "UcStokYonetimi";
            Size = new System.Drawing.Size(846, 514);
            Load += UcStokYonetimi_Load;
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            splitContainerControl1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridStokListesi).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStokEkle.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)picUrunResmi.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridStokListesi;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl lblMevcutStok;
        private DevExpress.XtraEditors.LabelControl lblUrunAdi;
        private DevExpress.XtraEditors.PictureEdit picUrunResmi;
        private DevExpress.XtraEditors.SimpleButton btnStokGuncelle;
        private DevExpress.XtraEditors.SpinEdit numStokEkle;
        private DevExpress.XtraEditors.SimpleButton btnStokAzalt;
    }
}
