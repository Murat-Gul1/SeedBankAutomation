namespace TohumBankasiOtomasyonu
{
    partial class FormSepet
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
            gridSepet = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            btnSatinAl = new DevExpress.XtraEditors.SimpleButton();
            btnSepetTemizle = new DevExpress.XtraEditors.SimpleButton();
            btnUrunSil = new DevExpress.XtraEditors.SimpleButton();
            lblGenelToplam = new DevExpress.XtraEditors.LabelControl();
            lblBaslikToplam = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)gridSepet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            SuspendLayout();
            // 
            // gridSepet
            // 
            gridSepet.Dock = System.Windows.Forms.DockStyle.Fill;
            gridSepet.Location = new System.Drawing.Point(0, 0);
            gridSepet.MainView = gridView1;
            gridSepet.Name = "gridSepet";
            gridSepet.Size = new System.Drawing.Size(470, 736);
            gridSepet.TabIndex = 0;
            gridSepet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridSepet;
            gridView1.Name = "gridView1";
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(btnSatinAl);
            panelControl1.Controls.Add(btnSepetTemizle);
            panelControl1.Controls.Add(btnUrunSil);
            panelControl1.Controls.Add(lblGenelToplam);
            panelControl1.Controls.Add(lblBaslikToplam);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelControl1.Location = new System.Drawing.Point(0, 656);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(470, 80);
            panelControl1.TabIndex = 1;
            // 
            // btnSatinAl
            // 
            btnSatinAl.Dock = System.Windows.Forms.DockStyle.Right;
            btnSatinAl.ImageOptions.Image = Properties.Resources.Shop_cart_apply;
            btnSatinAl.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnSatinAl.Location = new System.Drawing.Point(198, 2);
            btnSatinAl.LookAndFeel.UseDefaultLookAndFeel = false;
            btnSatinAl.Name = "btnSatinAl";
            btnSatinAl.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnSatinAl.Size = new System.Drawing.Size(90, 76);
            btnSatinAl.TabIndex = 2;
            btnSatinAl.Click += btnSatinAl_Click;
            // 
            // btnSepetTemizle
            // 
            btnSepetTemizle.Dock = System.Windows.Forms.DockStyle.Right;
            btnSepetTemizle.ImageOptions.Image = Properties.Resources.Shop_cart_exclude;
            btnSepetTemizle.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnSepetTemizle.Location = new System.Drawing.Point(288, 2);
            btnSepetTemizle.LookAndFeel.UseDefaultLookAndFeel = false;
            btnSepetTemizle.Name = "btnSepetTemizle";
            btnSepetTemizle.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnSepetTemizle.Size = new System.Drawing.Size(90, 76);
            btnSepetTemizle.TabIndex = 3;
            btnSepetTemizle.Click += btnSepetTemizle_Click;
            // 
            // btnUrunSil
            // 
            btnUrunSil.Dock = System.Windows.Forms.DockStyle.Right;
            btnUrunSil.ImageOptions.Image = Properties.Resources.minus;
            btnUrunSil.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnUrunSil.Location = new System.Drawing.Point(378, 2);
            btnUrunSil.LookAndFeel.UseDefaultLookAndFeel = false;
            btnUrunSil.Name = "btnUrunSil";
            btnUrunSil.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnUrunSil.Size = new System.Drawing.Size(90, 76);
            btnUrunSil.TabIndex = 4;
            btnUrunSil.Click += btnUrunSil_Click;
            // 
            // lblGenelToplam
            // 
            lblGenelToplam.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 162);
            lblGenelToplam.Appearance.Options.UseFont = true;
            lblGenelToplam.Location = new System.Drawing.Point(86, 5);
            lblGenelToplam.Name = "lblGenelToplam";
            lblGenelToplam.Size = new System.Drawing.Size(102, 18);
            lblGenelToplam.TabIndex = 1;
            lblGenelToplam.Text = "labelControl1";
            // 
            // lblBaslikToplam
            // 
            lblBaslikToplam.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 162);
            lblBaslikToplam.Appearance.Options.UseFont = true;
            lblBaslikToplam.Location = new System.Drawing.Point(5, 6);
            lblBaslikToplam.Name = "lblBaslikToplam";
            lblBaslikToplam.Size = new System.Drawing.Size(75, 16);
            lblBaslikToplam.TabIndex = 0;
            lblBaslikToplam.Text = "labelControl1";
            // 
            // FormSepet
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(470, 736);
            Controls.Add(panelControl1);
            Controls.Add(gridSepet);
            MinimizeBox = false;
            Name = "FormSepet";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FormSepet";
            Load += FormSepet_Load;
            ((System.ComponentModel.ISupportInitialize)gridSepet).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridSepet;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblGenelToplam;
        private DevExpress.XtraEditors.LabelControl lblBaslikToplam;
        private DevExpress.XtraEditors.SimpleButton btnSatinAl;
        private DevExpress.XtraEditors.SimpleButton btnSepetTemizle;
        private DevExpress.XtraEditors.SimpleButton btnUrunSil;
    }
}