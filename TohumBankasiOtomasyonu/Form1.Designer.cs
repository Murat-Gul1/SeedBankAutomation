﻿namespace TohumBankasiOtomasyonu
{
    partial class Form1
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
            pnlToolbar = new DevExpress.XtraEditors.PanelControl();
            btnDil = new DevExpress.XtraEditors.SimpleButton();
            pnlDilSecenekleri = new DevExpress.XtraEditors.PanelControl();
            btnIngilizce = new DevExpress.XtraEditors.SimpleButton();
            btnTurkce = new DevExpress.XtraEditors.SimpleButton();
            btnGiris = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)pnlToolbar).BeginInit();
            pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pnlDilSecenekleri).BeginInit();
            pnlDilSecenekleri.SuspendLayout();
            SuspendLayout();
            // 
            // pnlToolbar
            // 
            pnlToolbar.Controls.Add(btnGiris);
            pnlToolbar.Controls.Add(btnDil);
            pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            pnlToolbar.Location = new System.Drawing.Point(0, 0);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Size = new System.Drawing.Size(668, 56);
            pnlToolbar.TabIndex = 0;
            pnlToolbar.Click += pnlToolbar_Click;
            // 
            // btnDil
            // 
            btnDil.Dock = System.Windows.Forms.DockStyle.Right;
            btnDil.ImageOptions.Image = Properties.Resources.language;
            btnDil.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnDil.Location = new System.Drawing.Point(611, 2);
            btnDil.LookAndFeel.UseDefaultLookAndFeel = false;
            btnDil.Name = "btnDil";
            btnDil.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnDil.Size = new System.Drawing.Size(55, 52);
            btnDil.TabIndex = 0;
            btnDil.Click += btnDil_Click;
            // 
            // pnlDilSecenekleri
            // 
            pnlDilSecenekleri.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            pnlDilSecenekleri.Controls.Add(btnIngilizce);
            pnlDilSecenekleri.Controls.Add(btnTurkce);
            pnlDilSecenekleri.Location = new System.Drawing.Point(539, 62);
            pnlDilSecenekleri.Name = "pnlDilSecenekleri";
            pnlDilSecenekleri.Size = new System.Drawing.Size(131, 50);
            pnlDilSecenekleri.TabIndex = 1;
            pnlDilSecenekleri.Visible = false;
            pnlDilSecenekleri.Click += pnlDilSecenekleri_Click;
            // 
            // btnIngilizce
            // 
            btnIngilizce.Dock = System.Windows.Forms.DockStyle.Top;
            btnIngilizce.Location = new System.Drawing.Point(2, 25);
            btnIngilizce.Name = "btnIngilizce";
            btnIngilizce.Size = new System.Drawing.Size(127, 23);
            btnIngilizce.TabIndex = 1;
            btnIngilizce.Text = "English";
            btnIngilizce.Click += btnIngilizce_Click;
            // 
            // btnTurkce
            // 
            btnTurkce.Dock = System.Windows.Forms.DockStyle.Top;
            btnTurkce.Location = new System.Drawing.Point(2, 2);
            btnTurkce.Name = "btnTurkce";
            btnTurkce.Size = new System.Drawing.Size(127, 23);
            btnTurkce.TabIndex = 0;
            btnTurkce.Text = "Türkçe";
            btnTurkce.Click += btnTurkce_Click;
            // 
            // btnGiris
            // 
            btnGiris.Dock = System.Windows.Forms.DockStyle.Left;
            btnGiris.ImageOptions.Image = Properties.Resources.useraccount;
            btnGiris.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnGiris.Location = new System.Drawing.Point(2, 2);
            btnGiris.LookAndFeel.UseDefaultLookAndFeel = false;
            btnGiris.Name = "btnGiris";
            btnGiris.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            btnGiris.Size = new System.Drawing.Size(61, 52);
            btnGiris.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(668, 216);
            Controls.Add(pnlDilSecenekleri);
            Controls.Add(pnlToolbar);
            Name = "Form1";
            Text = "a";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Click += Form1_Click;
            ((System.ComponentModel.ISupportInitialize)pnlToolbar).EndInit();
            pnlToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pnlDilSecenekleri).EndInit();
            pnlDilSecenekleri.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pnlToolbar;
        private DevExpress.XtraEditors.SimpleButton btnDil;
        private DevExpress.XtraEditors.PanelControl pnlDilSecenekleri;
        private DevExpress.XtraEditors.SimpleButton btnIngilizce;
        private DevExpress.XtraEditors.SimpleButton btnTurkce;
        private DevExpress.XtraEditors.SimpleButton btnGiris;
    }
}