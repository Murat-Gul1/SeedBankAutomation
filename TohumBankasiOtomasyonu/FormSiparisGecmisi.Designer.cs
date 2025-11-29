namespace TohumBankasiOtomasyonu
{
    partial class FormSiparisGecmisi
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
            gridGecmis = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)gridGecmis).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // gridGecmis
            // 
            gridGecmis.Dock = System.Windows.Forms.DockStyle.Fill;
            gridGecmis.Location = new System.Drawing.Point(0, 0);
            gridGecmis.MainView = gridView1;
            gridGecmis.Name = "gridGecmis";
            gridGecmis.Size = new System.Drawing.Size(684, 402);
            gridGecmis.TabIndex = 0;
            gridGecmis.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridGecmis;
            gridView1.Name = "gridView1";
            // 
            // FormSiparisGecmisi
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(684, 402);
            Controls.Add(gridGecmis);
            Name = "FormSiparisGecmisi";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FormSiparisGecmisi";
            Load += FormSiparisGecmisi_Load;
            ((System.ComponentModel.ISupportInitialize)gridGecmis).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridGecmis;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}