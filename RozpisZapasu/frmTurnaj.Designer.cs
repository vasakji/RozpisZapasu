﻿
namespace RozpisZapasu
{
    partial class frmTurnaj
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTurnaj));
            this.clbTymy = new System.Windows.Forms.CheckedListBox();
            this.lblTymy = new System.Windows.Forms.Label();
            this.lblHriste = new System.Windows.Forms.Label();
            this.lblSkupiny = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnStorno = new System.Windows.Forms.Button();
            this.clbHriste = new System.Windows.Forms.CheckedListBox();
            this.clbSkupiny = new System.Windows.Forms.CheckedListBox();
            this.lblBarvaPozadi = new System.Windows.Forms.Label();
            this.btnBarvaZapasu = new System.Windows.Forms.Button();
            this.picBarvaZapasu = new System.Windows.Forms.PictureBox();
            this.lblUkazka = new System.Windows.Forms.Label();
            this.lblVitezneSety = new System.Windows.Forms.Label();
            this.numVitezneSety = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAutomaticky = new System.Windows.Forms.RadioButton();
            this.rbRucne = new System.Windows.Forms.RadioButton();
            this.picInfo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBarvaZapasu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVitezneSety)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // clbTymy
            // 
            this.clbTymy.FormattingEnabled = true;
            this.clbTymy.Location = new System.Drawing.Point(11, 23);
            this.clbTymy.Margin = new System.Windows.Forms.Padding(2);
            this.clbTymy.Name = "clbTymy";
            this.clbTymy.Size = new System.Drawing.Size(223, 124);
            this.clbTymy.TabIndex = 0;
            // 
            // lblTymy
            // 
            this.lblTymy.AutoSize = true;
            this.lblTymy.Location = new System.Drawing.Point(9, 7);
            this.lblTymy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTymy.Name = "lblTymy";
            this.lblTymy.Size = new System.Drawing.Size(79, 13);
            this.lblTymy.TabIndex = 1;
            this.lblTymy.Text = "Týmy na výběr:";
            // 
            // lblHriste
            // 
            this.lblHriste.AutoSize = true;
            this.lblHriste.Location = new System.Drawing.Point(8, 157);
            this.lblHriste.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHriste.Name = "lblHriste";
            this.lblHriste.Size = new System.Drawing.Size(82, 13);
            this.lblHriste.TabIndex = 2;
            this.lblHriste.Text = "Hřiště na výběr:";
            // 
            // lblSkupiny
            // 
            this.lblSkupiny.AutoSize = true;
            this.lblSkupiny.Location = new System.Drawing.Point(247, 7);
            this.lblSkupiny.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSkupiny.Name = "lblSkupiny";
            this.lblSkupiny.Size = new System.Drawing.Size(92, 13);
            this.lblSkupiny.TabIndex = 3;
            this.lblSkupiny.Text = "Skupiny na výběr:";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(300, 350);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnStorno
            // 
            this.btnStorno.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStorno.Location = new System.Drawing.Point(384, 350);
            this.btnStorno.Margin = new System.Windows.Forms.Padding(2);
            this.btnStorno.Name = "btnStorno";
            this.btnStorno.Size = new System.Drawing.Size(80, 25);
            this.btnStorno.TabIndex = 7;
            this.btnStorno.Text = "Storno";
            this.btnStorno.UseVisualStyleBackColor = true;
            this.btnStorno.Click += new System.EventHandler(this.btnStorno_Click);
            // 
            // clbHriste
            // 
            this.clbHriste.FormattingEnabled = true;
            this.clbHriste.Location = new System.Drawing.Point(11, 172);
            this.clbHriste.Margin = new System.Windows.Forms.Padding(2);
            this.clbHriste.Name = "clbHriste";
            this.clbHriste.Size = new System.Drawing.Size(223, 199);
            this.clbHriste.TabIndex = 8;
            // 
            // clbSkupiny
            // 
            this.clbSkupiny.FormattingEnabled = true;
            this.clbSkupiny.Location = new System.Drawing.Point(250, 23);
            this.clbSkupiny.Margin = new System.Windows.Forms.Padding(2);
            this.clbSkupiny.Name = "clbSkupiny";
            this.clbSkupiny.Size = new System.Drawing.Size(214, 124);
            this.clbSkupiny.TabIndex = 9;
            // 
            // lblBarvaPozadi
            // 
            this.lblBarvaPozadi.AutoSize = true;
            this.lblBarvaPozadi.Location = new System.Drawing.Point(269, 222);
            this.lblBarvaPozadi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBarvaPozadi.Name = "lblBarvaPozadi";
            this.lblBarvaPozadi.Size = new System.Drawing.Size(166, 13);
            this.lblBarvaPozadi.TabIndex = 10;
            this.lblBarvaPozadi.Text = "Barva pozadí přehledu a exportu:";
            // 
            // btnBarvaZapasu
            // 
            this.btnBarvaZapasu.Location = new System.Drawing.Point(343, 238);
            this.btnBarvaZapasu.Margin = new System.Windows.Forms.Padding(2);
            this.btnBarvaZapasu.Name = "btnBarvaZapasu";
            this.btnBarvaZapasu.Size = new System.Drawing.Size(74, 24);
            this.btnBarvaZapasu.TabIndex = 11;
            this.btnBarvaZapasu.Text = "Zvolit barvu";
            this.btnBarvaZapasu.UseVisualStyleBackColor = true;
            this.btnBarvaZapasu.Click += new System.EventHandler(this.btnZvolitBarvu_Click);
            // 
            // picBarvaZapasu
            // 
            this.picBarvaZapasu.BackColor = System.Drawing.Color.White;
            this.picBarvaZapasu.Location = new System.Drawing.Point(271, 238);
            this.picBarvaZapasu.Margin = new System.Windows.Forms.Padding(2);
            this.picBarvaZapasu.Name = "picBarvaZapasu";
            this.picBarvaZapasu.Size = new System.Drawing.Size(68, 24);
            this.picBarvaZapasu.TabIndex = 12;
            this.picBarvaZapasu.TabStop = false;
            // 
            // lblUkazka
            // 
            this.lblUkazka.AutoSize = true;
            this.lblUkazka.BackColor = System.Drawing.Color.White;
            this.lblUkazka.Location = new System.Drawing.Point(279, 244);
            this.lblUkazka.Name = "lblUkazka";
            this.lblUkazka.Size = new System.Drawing.Size(44, 13);
            this.lblUkazka.TabIndex = 13;
            this.lblUkazka.Text = "Ukázka";
            // 
            // lblVitezneSety
            // 
            this.lblVitezneSety.AutoSize = true;
            this.lblVitezneSety.Location = new System.Drawing.Point(269, 172);
            this.lblVitezneSety.Name = "lblVitezneSety";
            this.lblVitezneSety.Size = new System.Drawing.Size(111, 13);
            this.lblVitezneSety.TabIndex = 14;
            this.lblVitezneSety.Text = "Počet vítěžných setů:";
            // 
            // numVitezneSety
            // 
            this.numVitezneSety.Location = new System.Drawing.Point(271, 188);
            this.numVitezneSety.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numVitezneSety.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numVitezneSety.Name = "numVitezneSety";
            this.numVitezneSety.Size = new System.Drawing.Size(145, 20);
            this.numVitezneSety.TabIndex = 15;
            this.numVitezneSety.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbAutomaticky);
            this.groupBox1.Controls.Add(this.rbRucne);
            this.groupBox1.Location = new System.Drawing.Point(260, 276);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 45);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Způsob naplnění skupin";
            // 
            // rbAutomaticky
            // 
            this.rbAutomaticky.AutoSize = true;
            this.rbAutomaticky.Checked = true;
            this.rbAutomaticky.Location = new System.Drawing.Point(92, 19);
            this.rbAutomaticky.Name = "rbAutomaticky";
            this.rbAutomaticky.Size = new System.Drawing.Size(83, 17);
            this.rbAutomaticky.TabIndex = 1;
            this.rbAutomaticky.TabStop = true;
            this.rbAutomaticky.Text = "Automaticky";
            this.rbAutomaticky.UseVisualStyleBackColor = true;
            // 
            // rbRucne
            // 
            this.rbRucne.AutoSize = true;
            this.rbRucne.Location = new System.Drawing.Point(10, 19);
            this.rbRucne.Name = "rbRucne";
            this.rbRucne.Size = new System.Drawing.Size(57, 17);
            this.rbRucne.TabIndex = 0;
            this.rbRucne.Text = "Ručně";
            this.rbRucne.UseVisualStyleBackColor = true;
            // 
            // picInfo
            // 
            this.picInfo.Image = ((System.Drawing.Image)(resources.GetObject("picInfo.Image")));
            this.picInfo.Location = new System.Drawing.Point(260, 350);
            this.picInfo.Name = "picInfo";
            this.picInfo.Size = new System.Drawing.Size(25, 25);
            this.picInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInfo.TabIndex = 17;
            this.picInfo.TabStop = false;
            this.picInfo.Click += new System.EventHandler(this.picInfo_Click);
            // 
            // frmTurnaj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 386);
            this.Controls.Add(this.picInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numVitezneSety);
            this.Controls.Add(this.lblVitezneSety);
            this.Controls.Add(this.lblUkazka);
            this.Controls.Add(this.picBarvaZapasu);
            this.Controls.Add(this.btnBarvaZapasu);
            this.Controls.Add(this.lblBarvaPozadi);
            this.Controls.Add(this.clbSkupiny);
            this.Controls.Add(this.clbHriste);
            this.Controls.Add(this.btnStorno);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblSkupiny);
            this.Controls.Add(this.lblHriste);
            this.Controls.Add(this.lblTymy);
            this.Controls.Add(this.clbTymy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTurnaj";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parametry turnaje";
            this.Load += new System.EventHandler(this.frmTurnaj_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBarvaZapasu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVitezneSety)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbTymy;
        private System.Windows.Forms.Label lblTymy;
        private System.Windows.Forms.Label lblHriste;
        private System.Windows.Forms.Label lblSkupiny;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnStorno;
        private System.Windows.Forms.CheckedListBox clbHriste;
        private System.Windows.Forms.CheckedListBox clbSkupiny;
        private System.Windows.Forms.Label lblBarvaPozadi;
        private System.Windows.Forms.Button btnBarvaZapasu;
        private System.Windows.Forms.PictureBox picBarvaZapasu;
        private System.Windows.Forms.Label lblUkazka;
        private System.Windows.Forms.Label lblVitezneSety;
        private System.Windows.Forms.NumericUpDown numVitezneSety;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbAutomaticky;
        private System.Windows.Forms.RadioButton rbRucne;
        private System.Windows.Forms.PictureBox picInfo;
    }
}