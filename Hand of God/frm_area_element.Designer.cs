namespace HandofGod
{
    partial class frm_area_element
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
            this.btnapply = new System.Windows.Forms.Button();
            this.btnrestore = new System.Windows.Forms.Button();
            this.btnprev = new System.Windows.Forms.Button();
            this.btnnext = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnok = new System.Windows.Forms.Button();
            this.spin_vnum = new System.Windows.Forms.NumericUpDown();
            this.lblvnum = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spin_vnum)).BeginInit();
            this.SuspendLayout();
            // 
            // btnapply
            // 
            this.btnapply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnapply.Enabled = false;
            this.btnapply.Location = new System.Drawing.Point(404, 512);
            this.btnapply.Name = "btnapply";
            this.btnapply.Size = new System.Drawing.Size(75, 23);
            this.btnapply.TabIndex = 102;
            this.btnapply.Text = "Applica";
            this.btnapply.UseVisualStyleBackColor = true;
            this.btnapply.Click += new System.EventHandler(this.btnapply_Click);
            // 
            // btnrestore
            // 
            this.btnrestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnrestore.Location = new System.Drawing.Point(485, 512);
            this.btnrestore.Name = "btnrestore";
            this.btnrestore.Size = new System.Drawing.Size(75, 23);
            this.btnrestore.TabIndex = 103;
            this.btnrestore.Text = "Ripristina";
            this.btnrestore.UseVisualStyleBackColor = true;
            this.btnrestore.Click += new System.EventHandler(this.btnrestore_Click);
            // 
            // btnprev
            // 
            this.btnprev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnprev.Location = new System.Drawing.Point(581, 512);
            this.btnprev.Name = "btnprev";
            this.btnprev.Size = new System.Drawing.Size(27, 23);
            this.btnprev.TabIndex = 104;
            this.btnprev.Text = "<-";
            this.btnprev.UseVisualStyleBackColor = true;
            this.btnprev.Click += new System.EventHandler(this.btnprev_Click);
            // 
            // btnnext
            // 
            this.btnnext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnnext.Location = new System.Drawing.Point(614, 512);
            this.btnnext.Name = "btnnext";
            this.btnnext.Size = new System.Drawing.Size(27, 23);
            this.btnnext.TabIndex = 105;
            this.btnnext.Text = "->";
            this.btnnext.UseVisualStyleBackColor = true;
            this.btnnext.Click += new System.EventHandler(this.btnnext_Click);
            // 
            // btncancel
            // 
            this.btncancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btncancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncancel.Location = new System.Drawing.Point(83, 512);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 23);
            this.btncancel.TabIndex = 101;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btnok
            // 
            this.btnok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnok.Location = new System.Drawing.Point(2, 512);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(75, 23);
            this.btnok.TabIndex = 100;
            this.btnok.Text = "Ok";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // spin_vnum
            // 
            this.spin_vnum.Location = new System.Drawing.Point(57, 17);
            this.spin_vnum.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.spin_vnum.Name = "spin_vnum";
            this.spin_vnum.Size = new System.Drawing.Size(88, 20);
            this.spin_vnum.TabIndex = 0;
            this.spin_vnum.TabStop = false;
            this.spin_vnum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spin_vnum_KeyDown);
            // 
            // lblvnum
            // 
            this.lblvnum.AutoSize = true;
            this.lblvnum.Location = new System.Drawing.Point(12, 19);
            this.lblvnum.Name = "lblvnum";
            this.lblvnum.Size = new System.Drawing.Size(39, 13);
            this.lblvnum.TabIndex = 79;
            this.lblvnum.Text = "VNum:";
            // 
            // frm_area_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 547);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnnext);
            this.Controls.Add(this.spin_vnum);
            this.Controls.Add(this.btnapply);
            this.Controls.Add(this.lblvnum);
            this.Controls.Add(this.btnprev);
            this.Controls.Add(this.btnrestore);
            this.MinimumSize = new System.Drawing.Size(577, 586);
            this.Name = "frm_area_element";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Basic Area Element Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_area_element_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.spin_vnum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.NumericUpDown spin_vnum;
        public System.Windows.Forms.Label lblvnum;
        public System.Windows.Forms.Button btnapply;
        public System.Windows.Forms.Button btnrestore;
        public System.Windows.Forms.Button btnprev;
        public System.Windows.Forms.Button btnnext;
        public System.Windows.Forms.Button btncancel;
        public System.Windows.Forms.Button btnok;
    }
}