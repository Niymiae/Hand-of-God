namespace HandofGod
{
    partial class dlg_select_element
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlg_select_element));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.list_flags = new System.Windows.Forms.CheckedListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radiomanual = new System.Windows.Forms.RadioButton();
            this.radiolist = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.spin_manual = new System.Windows.Forms.NumericUpDown();
            this.list_elements = new HandofGod.HoGListView();
            this.searchTextBox1 = new HandofGod.SearchTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_manual)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 236);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(611, 32);
            this.panel1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(84, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // list_flags
            // 
            this.list_flags.CheckOnClick = true;
            this.list_flags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_flags.FormattingEnabled = true;
            this.list_flags.Location = new System.Drawing.Point(0, 0);
            this.list_flags.MultiColumn = true;
            this.list_flags.Name = "list_flags";
            this.list_flags.Size = new System.Drawing.Size(611, 236);
            this.list_flags.TabIndex = 2;
            this.list_flags.SelectedIndexChanged += new System.EventHandler(this.list_flags_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.searchTextBox1);
            this.panel2.Controls.Add(this.radiomanual);
            this.panel2.Controls.Add(this.radiolist);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(611, 29);
            this.panel2.TabIndex = 3;
            // 
            // radiomanual
            // 
            this.radiomanual.AutoSize = true;
            this.radiomanual.Location = new System.Drawing.Point(84, 6);
            this.radiomanual.Name = "radiomanual";
            this.radiomanual.Size = new System.Drawing.Size(66, 17);
            this.radiomanual.TabIndex = 1;
            this.radiomanual.Text = "Manuale";
            this.radiomanual.UseVisualStyleBackColor = true;
            // 
            // radiolist
            // 
            this.radiolist.AutoSize = true;
            this.radiolist.Checked = true;
            this.radiolist.Location = new System.Drawing.Point(12, 6);
            this.radiolist.Name = "radiolist";
            this.radiolist.Size = new System.Drawing.Size(47, 17);
            this.radiolist.TabIndex = 0;
            this.radiolist.TabStop = true;
            this.radiolist.Text = "Lista";
            this.radiolist.UseVisualStyleBackColor = true;
            this.radiolist.CheckedChanged += new System.EventHandler(this.radiolist_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Vnum: ";
            this.label1.Visible = false;
            // 
            // spin_manual
            // 
            this.spin_manual.Location = new System.Drawing.Point(58, 41);
            this.spin_manual.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.spin_manual.Name = "spin_manual";
            this.spin_manual.Size = new System.Drawing.Size(120, 20);
            this.spin_manual.TabIndex = 6;
            this.spin_manual.Visible = false;
            // 
            // list_elements
            // 
            this.list_elements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_elements.Filter = "";
            this.list_elements.FullRowSelect = true;
            this.list_elements.GridLines = true;
            this.list_elements.Location = new System.Drawing.Point(0, 29);
            this.list_elements.MultiSelect = false;
            this.list_elements.Name = "list_elements";
            this.list_elements.Size = new System.Drawing.Size(611, 207);
            this.list_elements.TabIndex = 0;
            this.list_elements.UseCompatibleStateImageBehavior = false;
            this.list_elements.View = System.Windows.Forms.View.Details;
            this.list_elements.DoubleClick += new System.EventHandler(this.list_DoubleClick);
            // 
            // searchTextBox1
            // 
            this.searchTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox1.ButtonImage = ((System.Drawing.Image)(resources.GetObject("searchTextBox1.ButtonImage")));
            this.searchTextBox1.list = this.list_elements;
            this.searchTextBox1.Location = new System.Drawing.Point(483, 6);
            this.searchTextBox1.Name = "searchTextBox1";
            this.searchTextBox1.Size = new System.Drawing.Size(100, 20);
            this.searchTextBox1.TabIndex = 2;
            // 
            // dlg_select_element
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 268);
            this.Controls.Add(this.list_elements);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.spin_manual);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.list_flags);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "dlg_select_element";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleziona un elemento";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_manual)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HoGListView list_elements;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox list_flags;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radiomanual;
        private System.Windows.Forms.RadioButton radiolist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown spin_manual;
        private SearchTextBox searchTextBox1;
    }
}