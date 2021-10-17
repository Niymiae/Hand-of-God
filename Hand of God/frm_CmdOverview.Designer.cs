namespace HandofGod
{
    partial class frm_CmdOverview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_CmdOverview));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_area = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tab_elements = new System.Windows.Forms.TabPage();
            this.tab_zonelist = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tab_map = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tab_area.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tab_zonelist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 393);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 34);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Chiudi";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_area);
            this.tabControl1.Controls.Add(this.tab_elements);
            this.tabControl1.Controls.Add(this.tab_zonelist);
            this.tabControl1.Controls.Add(this.tab_map);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(540, 393);
            this.tabControl1.TabIndex = 1;
            // 
            // tab_area
            // 
            this.tab_area.BackColor = System.Drawing.SystemColors.Control;
            this.tab_area.Controls.Add(this.pictureBox1);
            this.tab_area.Location = new System.Drawing.Point(4, 22);
            this.tab_area.Name = "tab_area";
            this.tab_area.Padding = new System.Windows.Forms.Padding(3);
            this.tab_area.Size = new System.Drawing.Size(532, 367);
            this.tab_area.TabIndex = 1;
            this.tab_area.Text = "Area";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(526, 361);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tab_elements
            // 
            this.tab_elements.Location = new System.Drawing.Point(4, 22);
            this.tab_elements.Name = "tab_elements";
            this.tab_elements.Size = new System.Drawing.Size(532, 367);
            this.tab_elements.TabIndex = 2;
            this.tab_elements.Text = "Elementi";
            this.tab_elements.UseVisualStyleBackColor = true;
            // 
            // tab_zonelist
            // 
            this.tab_zonelist.BackColor = System.Drawing.SystemColors.Control;
            this.tab_zonelist.Controls.Add(this.pictureBox2);
            this.tab_zonelist.Location = new System.Drawing.Point(4, 22);
            this.tab_zonelist.Name = "tab_zonelist";
            this.tab_zonelist.Padding = new System.Windows.Forms.Padding(3);
            this.tab_zonelist.Size = new System.Drawing.Size(532, 367);
            this.tab_zonelist.TabIndex = 0;
            this.tab_zonelist.Text = "Zona";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(526, 361);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // tab_map
            // 
            this.tab_map.Location = new System.Drawing.Point(4, 22);
            this.tab_map.Name = "tab_map";
            this.tab_map.Size = new System.Drawing.Size(532, 367);
            this.tab_map.TabIndex = 3;
            this.tab_map.Text = "Editor Grafico";
            this.tab_map.UseVisualStyleBackColor = true;
            // 
            // frm_CmdOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 427);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frm_CmdOverview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comandi";
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tab_area.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tab_zonelist.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_area;
        private System.Windows.Forms.TabPage tab_elements;
        private System.Windows.Forms.TabPage tab_zonelist;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tab_map;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}