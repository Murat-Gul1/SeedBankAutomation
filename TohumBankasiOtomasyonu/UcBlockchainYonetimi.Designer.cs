namespace TohumBankasiOtomasyonu
{
    partial class UcBlockchainYonetimi
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
            gridBlockchain = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)gridBlockchain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // gridBlockchain
            // 
            gridBlockchain.Dock = System.Windows.Forms.DockStyle.Fill;
            gridBlockchain.Location = new System.Drawing.Point(0, 0);
            gridBlockchain.MainView = gridView1;
            gridBlockchain.Name = "gridBlockchain";
            gridBlockchain.Size = new System.Drawing.Size(533, 403);
            gridBlockchain.TabIndex = 0;
            gridBlockchain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridBlockchain;
            gridView1.Name = "gridView1";
            // 
            // UcBlockchainYonetimi
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gridBlockchain);
            Name = "UcBlockchainYonetimi";
            Size = new System.Drawing.Size(533, 403);
            Load += UcBlockchainYonetimi_Load;
            ((System.ComponentModel.ISupportInitialize)gridBlockchain).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridBlockchain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
