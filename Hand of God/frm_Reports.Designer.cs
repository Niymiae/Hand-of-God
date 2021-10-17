namespace HandofGod
{
    partial class frm_Reports
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnclose = new System.Windows.Forms.Button();
            this.radiogroup = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioexternalexits = new System.Windows.Forms.RadioButton();
            this.radiodeathrooms = new System.Windows.Forms.RadioButton();
            this.radiokeys = new System.Windows.Forms.RadioButton();
            this.radiotreasures = new System.Windows.Forms.RadioButton();
            this.radiocoins = new System.Windows.Forms.RadioButton();
            this.radiogems = new System.Windows.Forms.RadioButton();
            this.list = new HandofGod.HoGListView();
            this.panel1.SuspendLayout();
            this.radiogroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 465);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 36);
            this.panel1.TabIndex = 0;
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(12, 6);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(75, 23);
            this.btnclose.TabIndex = 0;
            this.btnclose.TabStop = false;
            this.btnclose.Text = "Chiudi";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // radiogroup
            // 
            this.radiogroup.Controls.Add(this.radioButton3);
            this.radiogroup.Controls.Add(this.radioButton2);
            this.radiogroup.Controls.Add(this.radioButton1);
            this.radiogroup.Controls.Add(this.radioexternalexits);
            this.radiogroup.Controls.Add(this.radiodeathrooms);
            this.radiogroup.Controls.Add(this.radiokeys);
            this.radiogroup.Controls.Add(this.radiotreasures);
            this.radiogroup.Controls.Add(this.radiocoins);
            this.radiogroup.Controls.Add(this.radiogems);
            this.radiogroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.radiogroup.Location = new System.Drawing.Point(0, 0);
            this.radiogroup.Name = "radiogroup";
            this.radiogroup.Size = new System.Drawing.Size(696, 53);
            this.radiogroup.TabIndex = 1;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(304, 31);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(111, 17);
            this.radioButton3.TabIndex = 20;
            this.radioButton3.Text = "Oggetti non Inittati";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radiobtncheckedchanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(184, 31);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(98, 17);
            this.radioButton2.TabIndex = 19;
            this.radioButton2.Text = "Mob non Inittati";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radiobtncheckedchanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 31);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(125, 17);
            this.radioButton1.TabIndex = 18;
            this.radioButton1.Text = "Stanze non collegate";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radiobtncheckedchanged);
            // 
            // radioexternalexits
            // 
            this.radioexternalexits.AutoSize = true;
            this.radioexternalexits.Location = new System.Drawing.Point(423, 8);
            this.radioexternalexits.Name = "radioexternalexits";
            this.radioexternalexits.Size = new System.Drawing.Size(94, 17);
            this.radioexternalexits.TabIndex = 17;
            this.radioexternalexits.Text = "Uscite Esterne";
            this.radioexternalexits.UseVisualStyleBackColor = true;
            this.radioexternalexits.CheckedChanged += new System.EventHandler(this.radiobtncheckedchanged);
            // 
            // radiodeathrooms
            // 
            this.radiodeathrooms.AutoSize = true;
            this.radiodeathrooms.Location = new System.Drawing.Point(304, 8);
            this.radiodeathrooms.Name = "radiodeathrooms";
            this.radiodeathrooms.Size = new System.Drawing.Size(113, 17);
            this.radiodeathrooms.TabIndex = 16;
            this.radiodeathrooms.Text = "Stanze della Morte";
            this.radiodeathrooms.UseVisualStyleBackColor = true;
            this.radiodeathrooms.CheckedChanged += new System.EventHandler(this.radiobtncheckedchanged);
            // 
            // radiokeys
            // 
            this.radiokeys.AutoSize = true;
            this.radiokeys.Location = new System.Drawing.Point(244, 8);
            this.radiokeys.Name = "radiokeys";
            this.radiokeys.Size = new System.Drawing.Size(54, 17);
            this.radiokeys.TabIndex = 15;
            this.radiokeys.Text = "Chiavi";
            this.radiokeys.UseVisualStyleBackColor = true;
            this.radiokeys.CheckedChanged += new System.EventHandler(this.radiobtncheckedchanged);
            // 
            // radiotreasures
            // 
            this.radiotreasures.AutoSize = true;
            this.radiotreasures.Location = new System.Drawing.Point(184, 8);
            this.radiotreasures.Name = "radiotreasures";
            this.radiotreasures.Size = new System.Drawing.Size(54, 17);
            this.radiotreasures.TabIndex = 14;
            this.radiotreasures.Text = "Tesori";
            this.radiotreasures.UseVisualStyleBackColor = true;
            this.radiotreasures.CheckedChanged += new System.EventHandler(this.radiobtncheckedchanged);
            // 
            // radiocoins
            // 
            this.radiocoins.AutoSize = true;
            this.radiocoins.Location = new System.Drawing.Point(79, 8);
            this.radiocoins.Name = "radiocoins";
            this.radiocoins.Size = new System.Drawing.Size(99, 17);
            this.radiocoins.TabIndex = 13;
            this.radiocoins.Text = "Fama e Monete";
            this.radiocoins.UseVisualStyleBackColor = true;
            this.radiocoins.CheckedChanged += new System.EventHandler(this.radiobtncheckedchanged);
            // 
            // radiogems
            // 
            this.radiogems.AutoSize = true;
            this.radiogems.Checked = true;
            this.radiogems.Location = new System.Drawing.Point(12, 8);
            this.radiogems.Name = "radiogems";
            this.radiogems.Size = new System.Drawing.Size(61, 17);
            this.radiogems.TabIndex = 12;
            this.radiogems.TabStop = true;
            this.radiogems.Text = "Gemme";
            this.radiogems.UseVisualStyleBackColor = true;
            this.radiogems.CheckedChanged += new System.EventHandler(this.radiobtncheckedchanged);
            // 
            // list
            // 
            this.list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list.Filter = "";
            this.list.FullRowSelect = true;
            this.list.GridLines = true;
            this.list.Location = new System.Drawing.Point(0, 53);
            this.list.MultiSelect = false;
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(696, 412);
            this.list.TabIndex = 2;
            this.list.TabStop = false;
            this.list.UseCompatibleStateImageBehavior = false;
            this.list.View = System.Windows.Forms.View.Details;
            this.list.DoubleClick += new System.EventHandler(this.list_DoubleClick);
            // 
            // frm_Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 501);
            this.Controls.Add(this.list);
            this.Controls.Add(this.radiogroup);
            this.Controls.Add(this.panel1);
            this.Name = "frm_Reports";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reports";
            this.Shown += new System.EventHandler(this.frm_Reports_Shown);
            this.panel1.ResumeLayout(false);
            this.radiogroup.ResumeLayout(false);
            this.radiogroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Panel radiogroup;
        private HoGListView list;
        private System.Windows.Forms.RadioButton radiogems;
        private System.Windows.Forms.RadioButton radioexternalexits;
        private System.Windows.Forms.RadioButton radiodeathrooms;
        private System.Windows.Forms.RadioButton radiokeys;
        private System.Windows.Forms.RadioButton radiotreasures;
        private System.Windows.Forms.RadioButton radiocoins;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}