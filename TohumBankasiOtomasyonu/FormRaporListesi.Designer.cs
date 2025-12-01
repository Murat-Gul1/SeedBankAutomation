namespace TohumBankasiOtomasyonu
{
    partial class FormRaporListesi
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
            gridRaporlar = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)gridRaporlar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // gridRaporlar
            // 
            gridRaporlar.Dock = System.Windows.Forms.DockStyle.Fill;
            gridRaporlar.Location = new System.Drawing.Point(0, 0);
            gridRaporlar.MainView = gridView1;
            gridRaporlar.Name = "gridRaporlar";
            gridRaporlar.Size = new System.Drawing.Size(784, 452);
            gridRaporlar.TabIndex = 0;
            gridRaporlar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            gridRaporlar.Load += FormRaporListesi_Load;
            // 
            // gridView1
            // 
            gridView1.GridControl = gridRaporlar;
            gridView1.Name = "gridView1";
            // 
            // FormRaporListesi
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 452);
            Controls.Add(gridRaporlar);
            Name = "FormRaporListesi";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FormRaporListesi";
            ((System.ComponentModel.ISupportInitialize)gridRaporlar).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridRaporlar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}