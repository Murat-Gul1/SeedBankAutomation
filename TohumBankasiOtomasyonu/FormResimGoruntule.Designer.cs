namespace TohumBankasiOtomasyonu
{
    partial class FormResimGoruntule
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
            picBuyukResim = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)picBuyukResim.Properties).BeginInit();
            SuspendLayout();
            // 
            // picBuyukResim
            // 
            picBuyukResim.Dock = System.Windows.Forms.DockStyle.Fill;
            picBuyukResim.Location = new System.Drawing.Point(0, 0);
            picBuyukResim.Name = "picBuyukResim";
            picBuyukResim.Properties.ReadOnly = true;
            picBuyukResim.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            picBuyukResim.Properties.ShowMenu = false;
            picBuyukResim.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            picBuyukResim.Size = new System.Drawing.Size(784, 552);
            picBuyukResim.TabIndex = 0;
            // 
            // FormResimGoruntule
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 552);
            Controls.Add(picBuyukResim);
            MinimizeBox = false;
            Name = "FormResimGoruntule";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "FormResimGoruntule";
            ((System.ComponentModel.ISupportInitialize)picBuyukResim.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit picBuyukResim;
    }
}