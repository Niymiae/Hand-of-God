namespace HandofGod
{
    partial class frm_main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuovaAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.caricaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.impostaDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.directoryPrefissateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.esciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sturmentiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminaDallaListaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aiutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.riepilogoComandiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.informazioniSuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.searchbox = new HandofGod.SearchTextBox();
            this.list_zones = new HandofGod.HoGListView();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.grp_console = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grp_console.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.sturmentiToolStripMenuItem,
            this.aiutoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(714, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuovaAreaToolStripMenuItem,
            this.caricaToolStripMenuItem,
            this.toolStripMenuItem1,
            this.impostaDirectoryToolStripMenuItem,
            this.importaToolStripMenuItem,
            this.toolStripMenuItem2,
            this.esciToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // nuovaAreaToolStripMenuItem
            // 
            this.nuovaAreaToolStripMenuItem.Name = "nuovaAreaToolStripMenuItem";
            this.nuovaAreaToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.nuovaAreaToolStripMenuItem.Text = "Nuova Area";
            this.nuovaAreaToolStripMenuItem.Click += new System.EventHandler(this.AddZone);
            // 
            // caricaToolStripMenuItem
            // 
            this.caricaToolStripMenuItem.Name = "caricaToolStripMenuItem";
            this.caricaToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.caricaToolStripMenuItem.Text = "Carica";
            this.caricaToolStripMenuItem.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 6);
            // 
            // impostaDirectoryToolStripMenuItem
            // 
            this.impostaDirectoryToolStripMenuItem.Name = "impostaDirectoryToolStripMenuItem";
            this.impostaDirectoryToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.impostaDirectoryToolStripMenuItem.Text = "Opzioni..";
            this.impostaDirectoryToolStripMenuItem.Click += new System.EventHandler(this.btn_options_Click);
            // 
            // importaToolStripMenuItem
            // 
            this.importaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.directoryPrefissateToolStripMenuItem});
            this.importaToolStripMenuItem.Name = "importaToolStripMenuItem";
            this.importaToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.importaToolStripMenuItem.Text = "Importa";
            // 
            // directoryPrefissateToolStripMenuItem
            // 
            this.directoryPrefissateToolStripMenuItem.Name = "directoryPrefissateToolStripMenuItem";
            this.directoryPrefissateToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.directoryPrefissateToolStripMenuItem.Text = "Carica dalle Directory Impostate";
            this.directoryPrefissateToolStripMenuItem.Click += new System.EventHandler(this.import_dirs);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(133, 6);
            // 
            // esciToolStripMenuItem
            // 
            this.esciToolStripMenuItem.Name = "esciToolStripMenuItem";
            this.esciToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.esciToolStripMenuItem.Text = "Esci";
            this.esciToolStripMenuItem.Click += new System.EventHandler(this.button3_Click);
            // 
            // sturmentiToolStripMenuItem
            // 
            this.sturmentiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificaToolStripMenuItem,
            this.eliminaDallaListaToolStripMenuItem});
            this.sturmentiToolStripMenuItem.Name = "sturmentiToolStripMenuItem";
            this.sturmentiToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.sturmentiToolStripMenuItem.Text = "Modifica";
            // 
            // modificaToolStripMenuItem
            // 
            this.modificaToolStripMenuItem.Name = "modificaToolStripMenuItem";
            this.modificaToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.modificaToolStripMenuItem.Text = "Modifica";
            this.modificaToolStripMenuItem.Click += new System.EventHandler(this.btnedit_Click);
            // 
            // eliminaDallaListaToolStripMenuItem
            // 
            this.eliminaDallaListaToolStripMenuItem.Name = "eliminaDallaListaToolStripMenuItem";
            this.eliminaDallaListaToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.eliminaDallaListaToolStripMenuItem.Text = "Elimina";
            this.eliminaDallaListaToolStripMenuItem.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // aiutoToolStripMenuItem
            // 
            this.aiutoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.riepilogoComandiToolStripMenuItem,
            this.toolStripMenuItem3,
            this.informazioniSuToolStripMenuItem});
            this.aiutoToolStripMenuItem.Name = "aiutoToolStripMenuItem";
            this.aiutoToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.aiutoToolStripMenuItem.Text = "Aiuto";
            // 
            // riepilogoComandiToolStripMenuItem
            // 
            this.riepilogoComandiToolStripMenuItem.Name = "riepilogoComandiToolStripMenuItem";
            this.riepilogoComandiToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.riepilogoComandiToolStripMenuItem.Text = "Riepilogo Comandi..";
            this.riepilogoComandiToolStripMenuItem.Visible = false;
            this.riepilogoComandiToolStripMenuItem.Click += new System.EventHandler(this.riepilogoComandiToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(179, 6);
            this.toolStripMenuItem3.Visible = false;
            // 
            // informazioniSuToolStripMenuItem
            // 
            this.informazioniSuToolStripMenuItem.Name = "informazioniSuToolStripMenuItem";
            this.informazioniSuToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.informazioniSuToolStripMenuItem.Text = "Informazioni su..";
            this.informazioniSuToolStripMenuItem.Click += new System.EventHandler(this.informazioniSuToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.searchbox);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 31);
            this.panel1.TabIndex = 3;
            // 
            // button5
            // 
            this.button5.ImageKey = "gear.png";
            this.button5.ImageList = this.imageList1;
            this.button5.Location = new System.Drawing.Point(381, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "Opzioni";
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btn_options_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "AddTable_5632_32.bmp");
            this.imageList1.Images.SetKeyName(1, "EditorZone_6025_24.bmp");
            this.imageList1.Images.SetKeyName(2, "gear.png");
            this.imageList1.Images.SetKeyName(3, "refresh.png");
            this.imageList1.Images.SetKeyName(4, "load.png");
            this.imageList1.Images.SetKeyName(5, "nuclear.png");
            // 
            // searchbox
            // 
            this.searchbox.ButtonImage = ((System.Drawing.Image)(resources.GetObject("searchbox.ButtonImage")));
            this.searchbox.list = this.list_zones;
            this.searchbox.Location = new System.Drawing.Point(556, 5);
            this.searchbox.Name = "searchbox";
            this.searchbox.Size = new System.Drawing.Size(133, 20);
            this.searchbox.TabIndex = 4;
            // 
            // list_zones
            // 
            this.list_zones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_zones.Filter = "";
            this.list_zones.FullRowSelect = true;
            this.list_zones.GridLines = true;
            this.list_zones.Location = new System.Drawing.Point(0, 55);
            this.list_zones.MultiSelect = false;
            this.list_zones.Name = "list_zones";
            this.list_zones.Size = new System.Drawing.Size(714, 328);
            this.list_zones.TabIndex = 0;
            this.list_zones.UseCompatibleStateImageBehavior = false;
            this.list_zones.View = System.Windows.Forms.View.Details;
            this.list_zones.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.list_zones_MouseDoubleClick);
            // 
            // button4
            // 
            this.button4.ImageKey = "EditorZone_6025_24.bmp";
            this.button4.ImageList = this.imageList1;
            this.button4.Location = new System.Drawing.Point(282, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Modifica";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnedit_Click);
            // 
            // button3
            // 
            this.button3.ImageKey = "load.png";
            this.button3.ImageList = this.imageList1;
            this.button3.Location = new System.Drawing.Point(201, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Carica";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // button2
            // 
            this.button2.ImageKey = "AddTable_5632_32.bmp";
            this.button2.ImageList = this.imageList1;
            this.button2.Location = new System.Drawing.Point(120, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Nuova";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.AddZone);
            // 
            // button1
            // 
            this.button1.ImageKey = "refresh.png";
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(6, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Importa";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.import_dirs);
            // 
            // grp_console
            // 
            this.grp_console.Controls.Add(this.label2);
            this.grp_console.Controls.Add(this.button7);
            this.grp_console.Controls.Add(this.label1);
            this.grp_console.Controls.Add(this.button6);
            this.grp_console.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grp_console.Location = new System.Drawing.Point(0, 383);
            this.grp_console.Name = "grp_console";
            this.grp_console.Size = new System.Drawing.Size(714, 81);
            this.grp_console.TabIndex = 4;
            this.grp_console.TabStop = false;
            this.grp_console.Text = "Console";
            this.grp_console.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Controlla i moltiplicatori e salva gli shop delle zone in lista";
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.LightYellow;
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.ImageIndex = 5;
            this.button7.ImageList = this.imageList1;
            this.button7.Location = new System.Drawing.Point(282, 36);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(53, 32);
            this.button7.TabIndex = 2;
            this.button7.Text = "Go!";
            this.button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Risalva tutte le zone in lista";
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.LightYellow;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.ImageIndex = 5;
            this.button6.ImageList = this.imageList1;
            this.button6.Location = new System.Drawing.Point(47, 36);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(53, 32);
            this.button6.TabIndex = 0;
            this.button6.Text = "Go!";
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 464);
            this.Controls.Add(this.list_zones);
            this.Controls.Add(this.grp_console);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frm_main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hand of God - LeU 3.0 (beta)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_main_FormClosing);
            this.Load += new System.EventHandler(this.frm_main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grp_console.ResumeLayout(false);
            this.grp_console.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HoGListView list_zones;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sturmentiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aiutoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informazioniSuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuovaAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem caricaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem importaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem directoryPrefissateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem esciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem impostaDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminaDallaListaToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private SearchTextBox searchbox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem riepilogoComandiToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox grp_console;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button7;
    }
}