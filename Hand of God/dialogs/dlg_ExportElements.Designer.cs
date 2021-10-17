namespace HandofGod
{
    partial class dlg_ExportElements
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
            this.chklist = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkcontainers = new System.Windows.Forms.CheckBox();
            this.chkkeys = new System.Windows.Forms.CheckBox();
            this.chkarmors = new System.Windows.Forms.CheckBox();
            this.chkweapons = new System.Windows.Forms.CheckBox();
            this.chkselall = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chkpersonal = new System.Windows.Forms.CheckBox();
            this.chkcrstl = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chklist
            // 
            this.chklist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chklist.FormattingEnabled = true;
            this.chklist.Location = new System.Drawing.Point(0, 28);
            this.chklist.Name = "chklist";
            this.chklist.Size = new System.Drawing.Size(687, 411);
            this.chklist.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkpersonal);
            this.panel1.Controls.Add(this.chkcrstl);
            this.panel1.Controls.Add(this.chkcontainers);
            this.panel1.Controls.Add(this.chkkeys);
            this.panel1.Controls.Add(this.chkarmors);
            this.panel1.Controls.Add(this.chkweapons);
            this.panel1.Controls.Add(this.chkselall);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(687, 28);
            this.panel1.TabIndex = 1;
            // 
            // chkcontainers
            // 
            this.chkcontainers.AutoSize = true;
            this.chkcontainers.Location = new System.Drawing.Point(373, 8);
            this.chkcontainers.Name = "chkcontainers";
            this.chkcontainers.Size = new System.Drawing.Size(76, 17);
            this.chkcontainers.TabIndex = 5;
            this.chkcontainers.Tag = "4";
            this.chkcontainers.Text = "Contenitori";
            this.chkcontainers.UseVisualStyleBackColor = true;
            this.chkcontainers.Click += new System.EventHandler(this.chkselallClick);
            // 
            // chkkeys
            // 
            this.chkkeys.AutoSize = true;
            this.chkkeys.Location = new System.Drawing.Point(312, 8);
            this.chkkeys.Name = "chkkeys";
            this.chkkeys.Size = new System.Drawing.Size(55, 17);
            this.chkkeys.TabIndex = 4;
            this.chkkeys.Tag = "3";
            this.chkkeys.Text = "Chiavi";
            this.chkkeys.UseVisualStyleBackColor = true;
            this.chkkeys.Click += new System.EventHandler(this.chkselallClick);
            // 
            // chkarmors
            // 
            this.chkarmors.AutoSize = true;
            this.chkarmors.Location = new System.Drawing.Point(238, 8);
            this.chkarmors.Name = "chkarmors";
            this.chkarmors.Size = new System.Drawing.Size(68, 17);
            this.chkarmors.TabIndex = 3;
            this.chkarmors.Tag = "2";
            this.chkarmors.Text = "Armature";
            this.chkarmors.UseVisualStyleBackColor = true;
            this.chkarmors.Click += new System.EventHandler(this.chkselallClick);
            // 
            // chkweapons
            // 
            this.chkweapons.AutoSize = true;
            this.chkweapons.Location = new System.Drawing.Point(186, 8);
            this.chkweapons.Name = "chkweapons";
            this.chkweapons.Size = new System.Drawing.Size(46, 17);
            this.chkweapons.TabIndex = 2;
            this.chkweapons.Tag = "1";
            this.chkweapons.Text = "Armi";
            this.chkweapons.UseVisualStyleBackColor = true;
            this.chkweapons.Click += new System.EventHandler(this.chkselallClick);
            // 
            // chkselall
            // 
            this.chkselall.AutoSize = true;
            this.chkselall.Location = new System.Drawing.Point(133, 8);
            this.chkselall.Name = "chkselall";
            this.chkselall.Size = new System.Drawing.Size(47, 17);
            this.chkselall.TabIndex = 1;
            this.chkselall.Tag = "0";
            this.chkselall.Text = "Tutti";
            this.chkselall.UseVisualStyleBackColor = true;
            this.chkselall.Click += new System.EventHandler(this.chkselallClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Elementi da Esportare: ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 439);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(687, 31);
            this.panel2.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(91, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Annulla";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkpersonal
            // 
            this.chkpersonal.AutoSize = true;
            this.chkpersonal.Location = new System.Drawing.Point(539, 8);
            this.chkpersonal.Name = "chkpersonal";
            this.chkpersonal.Size = new System.Drawing.Size(67, 17);
            this.chkpersonal.TabIndex = 7;
            this.chkpersonal.Tag = "6";
            this.chkpersonal.Text = "Personal";
            this.chkpersonal.UseVisualStyleBackColor = true;
            // 
            // chkcrstl
            // 
            this.chkcrstl.AutoSize = true;
            this.chkcrstl.Location = new System.Drawing.Point(455, 8);
            this.chkcrstl.Name = "chkcrstl";
            this.chkcrstl.Size = new System.Drawing.Size(78, 17);
            this.chkcrstl.TabIndex = 6;
            this.chkcrstl.Tag = "5";
            this.chkcrstl.Text = "Crstl_value";
            this.chkcrstl.UseVisualStyleBackColor = true;
            // 
            // dlg_ExportElements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 470);
            this.Controls.Add(this.chklist);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "dlg_ExportElements";
            this.Text = "Esporta in Html";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chklist;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkselall;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkcontainers;
        private System.Windows.Forms.CheckBox chkkeys;
        private System.Windows.Forms.CheckBox chkarmors;
        private System.Windows.Forms.CheckBox chkweapons;
        private System.Windows.Forms.CheckBox chkpersonal;
        private System.Windows.Forms.CheckBox chkcrstl;
    }
}