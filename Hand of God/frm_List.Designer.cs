namespace HandofGod
{
    partial class frm_List
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_List));
            this.radiogroup = new System.Windows.Forms.Panel();
            this.searchbox = new HandofGod.SearchTextBox();
            this.list = new HandofGod.HoGListView();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.rd_zones = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblstatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnedittemplates = new System.Windows.Forms.Button();
            this.btndeltemplate = new System.Windows.Forms.Button();
            this.btnsavein = new HandofGod.NoselButton();
            this.btnaddsel = new HandofGod.NoselButton();
            this.btnenum = new System.Windows.Forms.CheckBox();
            this.btnsave = new HandofGod.NoselButton();
            this.t_panel = new System.Windows.Forms.Timer(this.components);
            this.pn_utils = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnstartenum = new System.Windows.Forms.Button();
            this.chkshops = new System.Windows.Forms.CheckBox();
            this.chkobjects = new System.Windows.Forms.CheckBox();
            this.chkmobs = new System.Windows.Forms.CheckBox();
            this.chkrooms = new System.Windows.Forms.CheckBox();
            this.spin_fromvnum = new System.Windows.Forms.NumericUpDown();
            this.spin_newvnum = new System.Windows.Forms.NumericUpDown();
            this.spin_tovnum = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.popupsave = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnsavechoosedir = new System.Windows.Forms.ToolStripMenuItem();
            this.mnsavesetdirs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.esportaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileZonazonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileStanzewldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMobsmobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOggettiobjToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNegozishpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.esportaInHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oggettiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btndel = new HandofGod.NoselButton();
            this.btnoptions = new HandofGod.NoselButton();
            this.btnreports = new HandofGod.NoselButton();
            this.btnadd = new HandofGod.NoselButton();
            this.btneditvis = new HandofGod.NoselButton();
            this.btnedit = new HandofGod.NoselButton();
            this.btnclone = new HandofGod.NoselButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkDuplicate = new System.Windows.Forms.CheckBox();
            this.radiogroup.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pn_utils.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_fromvnum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_newvnum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_tovnum)).BeginInit();
            this.popupsave.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // radiogroup
            // 
            this.radiogroup.Controls.Add(this.searchbox);
            this.radiogroup.Controls.Add(this.radioButton5);
            this.radiogroup.Controls.Add(this.radioButton4);
            this.radiogroup.Controls.Add(this.radioButton3);
            this.radiogroup.Controls.Add(this.radioButton2);
            this.radiogroup.Controls.Add(this.rd_zones);
            this.radiogroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.radiogroup.Location = new System.Drawing.Point(0, 41);
            this.radiogroup.Name = "radiogroup";
            this.radiogroup.Size = new System.Drawing.Size(1067, 25);
            this.radiogroup.TabIndex = 3;
            // 
            // searchbox
            // 
            this.searchbox.ButtonImage = ((System.Drawing.Image)(resources.GetObject("searchbox.ButtonImage")));
            this.searchbox.list = this.list;
            this.searchbox.Location = new System.Drawing.Point(698, 3);
            this.searchbox.Name = "searchbox";
            this.searchbox.Size = new System.Drawing.Size(150, 20);
            this.searchbox.TabIndex = 5;
            // 
            // list
            // 
            this.list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list.Filter = "";
            this.list.FullRowSelect = true;
            this.list.GridLines = true;
            this.list.HideSelection = false;
            this.list.Location = new System.Drawing.Point(34, 66);
            this.list.MultiSelect = false;
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(898, 415);
            this.list.TabIndex = 2;
            this.list.UseCompatibleStateImageBehavior = false;
            this.list.View = System.Windows.Forms.View.Details;
            this.list.DoubleClick += new System.EventHandler(this.EditSelected);
            // 
            // radioButton5
            // 
            this.radioButton5.Location = new System.Drawing.Point(277, 4);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(75, 17);
            this.radioButton5.TabIndex = 4;
            this.radioButton5.Text = "Negozi";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.Location = new System.Drawing.Point(196, 4);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(75, 17);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.Text = "Oggetti";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.Location = new System.Drawing.Point(115, 4);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(75, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "Mobs";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(34, 4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(75, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Stanze";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // rd_zones
            // 
            this.rd_zones.Checked = true;
            this.rd_zones.Location = new System.Drawing.Point(358, 4);
            this.rd_zones.Name = "rd_zones";
            this.rd_zones.Size = new System.Drawing.Size(75, 17);
            this.rd_zones.TabIndex = 0;
            this.rd_zones.TabStop = true;
            this.rd_zones.Text = "Zone";
            this.rd_zones.UseVisualStyleBackColor = true;
            this.rd_zones.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // button1
            // 
            this.button1.ImageKey = "magnifier.png";
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(3, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 26);
            this.button1.TabIndex = 8;
            this.toolTip1.SetToolTip(this.button1, "Anteprima in stile MUD");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Saveall_6518_24.bmp");
            this.imageList1.Images.SetKeyName(1, "add.png");
            this.imageList1.Images.SetKeyName(2, "EditorZone_6025_24.bmp");
            this.imageList1.Images.SetKeyName(3, "TableMissing_8931_24.bmp");
            this.imageList1.Images.SetKeyName(4, "imagelis_web[15].bmp");
            this.imageList1.Images.SetKeyName(5, "Reports-collapsed_12995_24.bmp");
            this.imageList1.Images.SetKeyName(6, "Tables_8928_24.bmp");
            this.imageList1.Images.SetKeyName(7, "Template_514_24.bmp");
            this.imageList1.Images.SetKeyName(8, "CopyWebSite_340_32.bmp");
            this.imageList1.Images.SetKeyName(9, "cross.png");
            this.imageList1.Images.SetKeyName(10, "magnifier.png");
            this.imageList1.Images.SetKeyName(11, "gear.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblstatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 481);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1067, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblstatus
            // 
            this.lblstatus.Name = "lblstatus";
            this.lblstatus.Size = new System.Drawing.Size(10, 17);
            this.lblstatus.Text = " ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnedittemplates);
            this.panel1.Controls.Add(this.btndeltemplate);
            this.panel1.Controls.Add(this.btnsavein);
            this.panel1.Controls.Add(this.btnaddsel);
            this.panel1.Controls.Add(this.btnenum);
            this.panel1.Controls.Add(this.btnsave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1067, 41);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Strumenti Template: ";
            // 
            // btnedittemplates
            // 
            this.btnedittemplates.ImageList = this.imageList1;
            this.btnedittemplates.Location = new System.Drawing.Point(553, 10);
            this.btnedittemplates.Name = "btnedittemplates";
            this.btnedittemplates.Size = new System.Drawing.Size(98, 24);
            this.btnedittemplates.TabIndex = 23;
            this.btnedittemplates.Text = "Crea/Modifica";
            this.btnedittemplates.UseVisualStyleBackColor = true;
            this.btnedittemplates.Click += new System.EventHandler(this.edittemplates_Click);
            // 
            // btndeltemplate
            // 
            this.btndeltemplate.ImageKey = "cross.png";
            this.btndeltemplate.ImageList = this.imageList1;
            this.btndeltemplate.Location = new System.Drawing.Point(407, 10);
            this.btndeltemplate.Name = "btndeltemplate";
            this.btndeltemplate.Size = new System.Drawing.Size(140, 24);
            this.btndeltemplate.TabIndex = 22;
            this.btndeltemplate.Text = "Resetta Selezione";
            this.btndeltemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btndeltemplate.UseVisualStyleBackColor = true;
            this.btndeltemplate.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnsavein
            // 
            this.btnsavein.ImageKey = "Saveall_6518_24.bmp";
            this.btnsavein.ImageList = this.imageList1;
            this.btnsavein.Location = new System.Drawing.Point(39, 10);
            this.btnsavein.Name = "btnsavein";
            this.btnsavein.Size = new System.Drawing.Size(84, 24);
            this.btnsavein.TabIndex = 7;
            this.btnsavein.Text = "Salva in...";
            this.btnsavein.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnsavein.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnsavein.UseVisualStyleBackColor = true;
            this.btnsavein.Click += new System.EventHandler(this.noselButton1_Click_2);
            // 
            // btnaddsel
            // 
            this.btnaddsel.ImageKey = "Template_514_24.bmp";
            this.btnaddsel.ImageList = this.imageList1;
            this.btnaddsel.Location = new System.Drawing.Point(255, 10);
            this.btnaddsel.Name = "btnaddsel";
            this.btnaddsel.Size = new System.Drawing.Size(146, 24);
            this.btnaddsel.TabIndex = 21;
            this.btnaddsel.Text = "nuovo elemento";
            this.btnaddsel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnaddsel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnaddsel, "Determina cosa inserire con il pulsante \"Crea\"");
            this.btnaddsel.UseVisualStyleBackColor = true;
            this.btnaddsel.Click += new System.EventHandler(this.btnaddsel_Click);
            // 
            // btnenum
            // 
            this.btnenum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnenum.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnenum.AutoSize = true;
            this.btnenum.ImageKey = "Tables_8928_24.bmp";
            this.btnenum.ImageList = this.imageList1;
            this.btnenum.Location = new System.Drawing.Point(977, 10);
            this.btnenum.Name = "btnenum";
            this.btnenum.Size = new System.Drawing.Size(78, 23);
            this.btnenum.TabIndex = 4;
            this.btnenum.Text = "Rinumera";
            this.btnenum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnenum.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnenum.UseVisualStyleBackColor = true;
            this.btnenum.Click += new System.EventHandler(this.btnutils_Click);
            // 
            // btnsave
            // 
            this.btnsave.ImageKey = "Saveall_6518_24.bmp";
            this.btnsave.ImageList = this.imageList1;
            this.btnsave.Location = new System.Drawing.Point(9, 10);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(24, 24);
            this.btnsave.TabIndex = 0;
            this.btnsave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnsave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnsave.UseVisualStyleBackColor = true;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // t_panel
            // 
            this.t_panel.Interval = 10;
            this.t_panel.Tick += new System.EventHandler(this.utils_timer_Tick);
            // 
            // pn_utils
            // 
            this.pn_utils.Controls.Add(this.groupBox1);
            this.pn_utils.Dock = System.Windows.Forms.DockStyle.Right;
            this.pn_utils.Location = new System.Drawing.Point(932, 66);
            this.pn_utils.Name = "pn_utils";
            this.pn_utils.Size = new System.Drawing.Size(135, 415);
            this.pn_utils.TabIndex = 9;
            this.pn_utils.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDuplicate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnstartenum);
            this.groupBox1.Controls.Add(this.chkshops);
            this.groupBox1.Controls.Add(this.chkobjects);
            this.groupBox1.Controls.Add(this.chkmobs);
            this.groupBox1.Controls.Add(this.chkrooms);
            this.groupBox1.Controls.Add(this.spin_fromvnum);
            this.groupBox1.Controls.Add(this.spin_newvnum);
            this.groupBox1.Controls.Add(this.spin_tovnum);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 415);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Rinumera VNum";
            // 
            // btnstartenum
            // 
            this.btnstartenum.Location = new System.Drawing.Point(39, 335);
            this.btnstartenum.Name = "btnstartenum";
            this.btnstartenum.Size = new System.Drawing.Size(73, 23);
            this.btnstartenum.TabIndex = 13;
            this.btnstartenum.Text = "Rinumera";
            this.btnstartenum.UseVisualStyleBackColor = true;
            this.btnstartenum.Click += new System.EventHandler(this.btnstartenum_Click);
            // 
            // chkshops
            // 
            this.chkshops.AutoSize = true;
            this.chkshops.Checked = true;
            this.chkshops.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkshops.Location = new System.Drawing.Point(9, 257);
            this.chkshops.Name = "chkshops";
            this.chkshops.Size = new System.Drawing.Size(59, 17);
            this.chkshops.TabIndex = 12;
            this.chkshops.Text = "Negozi";
            this.chkshops.UseVisualStyleBackColor = true;
            // 
            // chkobjects
            // 
            this.chkobjects.AutoSize = true;
            this.chkobjects.Checked = true;
            this.chkobjects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkobjects.Location = new System.Drawing.Point(9, 240);
            this.chkobjects.Name = "chkobjects";
            this.chkobjects.Size = new System.Drawing.Size(60, 17);
            this.chkobjects.TabIndex = 11;
            this.chkobjects.Text = "Oggetti";
            this.chkobjects.UseVisualStyleBackColor = true;
            // 
            // chkmobs
            // 
            this.chkmobs.AutoSize = true;
            this.chkmobs.Checked = true;
            this.chkmobs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkmobs.Location = new System.Drawing.Point(9, 223);
            this.chkmobs.Name = "chkmobs";
            this.chkmobs.Size = new System.Drawing.Size(52, 17);
            this.chkmobs.TabIndex = 10;
            this.chkmobs.Text = "Mobs";
            this.chkmobs.UseVisualStyleBackColor = true;
            // 
            // chkrooms
            // 
            this.chkrooms.AutoSize = true;
            this.chkrooms.Checked = true;
            this.chkrooms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkrooms.Location = new System.Drawing.Point(9, 206);
            this.chkrooms.Name = "chkrooms";
            this.chkrooms.Size = new System.Drawing.Size(59, 17);
            this.chkrooms.TabIndex = 9;
            this.chkrooms.Text = "Stanze";
            this.chkrooms.UseVisualStyleBackColor = true;
            // 
            // spin_fromvnum
            // 
            this.spin_fromvnum.Location = new System.Drawing.Point(6, 64);
            this.spin_fromvnum.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.spin_fromvnum.Name = "spin_fromvnum";
            this.spin_fromvnum.Size = new System.Drawing.Size(120, 20);
            this.spin_fromvnum.TabIndex = 6;
            this.spin_fromvnum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spin_fromvnum_KeyDown);
            // 
            // spin_newvnum
            // 
            this.spin_newvnum.Location = new System.Drawing.Point(6, 163);
            this.spin_newvnum.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.spin_newvnum.Name = "spin_newvnum";
            this.spin_newvnum.Size = new System.Drawing.Size(120, 20);
            this.spin_newvnum.TabIndex = 8;
            this.spin_newvnum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spin_fromvnum_KeyDown);
            // 
            // spin_tovnum
            // 
            this.spin_tovnum.Location = new System.Drawing.Point(6, 113);
            this.spin_tovnum.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.spin_tovnum.Name = "spin_tovnum";
            this.spin_tovnum.Size = new System.Drawing.Size(120, 20);
            this.spin_tovnum.TabIndex = 7;
            this.spin_tovnum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spin_fromvnum_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Nuovo VNum Iniziale";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Al VNum";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Dal VNum";
            // 
            // popupsave
            // 
            this.popupsave.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnsavechoosedir,
            this.mnsavesetdirs,
            this.toolStripMenuItem1,
            this.esportaToolStripMenuItem,
            this.toolStripSeparator1,
            this.esportaInHTMLToolStripMenuItem});
            this.popupsave.Name = "popupsave";
            this.popupsave.Size = new System.Drawing.Size(236, 104);
            // 
            // mnsavechoosedir
            // 
            this.mnsavechoosedir.Name = "mnsavechoosedir";
            this.mnsavechoosedir.Size = new System.Drawing.Size(235, 22);
            this.mnsavechoosedir.Text = "Salva nella directory...";
            this.mnsavechoosedir.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // mnsavesetdirs
            // 
            this.mnsavesetdirs.Name = "mnsavesetdirs";
            this.mnsavesetdirs.Size = new System.Drawing.Size(235, 22);
            this.mnsavesetdirs.Text = "Salva nelle directory impostate";
            this.mnsavesetdirs.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(232, 6);
            // 
            // esportaToolStripMenuItem
            // 
            this.esportaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileZonazonToolStripMenuItem,
            this.fileStanzewldToolStripMenuItem,
            this.fileMobsmobToolStripMenuItem,
            this.fileOggettiobjToolStripMenuItem,
            this.fileNegozishpToolStripMenuItem});
            this.esportaToolStripMenuItem.Name = "esportaToolStripMenuItem";
            this.esportaToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.esportaToolStripMenuItem.Text = "Esporta file singolo";
            // 
            // fileZonazonToolStripMenuItem
            // 
            this.fileZonazonToolStripMenuItem.Name = "fileZonazonToolStripMenuItem";
            this.fileZonazonToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.fileZonazonToolStripMenuItem.Tag = "0";
            this.fileZonazonToolStripMenuItem.Text = "File Zona (.zon)";
            this.fileZonazonToolStripMenuItem.Click += new System.EventHandler(this.fileZonazonToolStripMenuItem_Click);
            // 
            // fileStanzewldToolStripMenuItem
            // 
            this.fileStanzewldToolStripMenuItem.Name = "fileStanzewldToolStripMenuItem";
            this.fileStanzewldToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.fileStanzewldToolStripMenuItem.Tag = "1";
            this.fileStanzewldToolStripMenuItem.Text = "File Stanze (.wld)";
            this.fileStanzewldToolStripMenuItem.Click += new System.EventHandler(this.fileZonazonToolStripMenuItem_Click);
            // 
            // fileMobsmobToolStripMenuItem
            // 
            this.fileMobsmobToolStripMenuItem.Name = "fileMobsmobToolStripMenuItem";
            this.fileMobsmobToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.fileMobsmobToolStripMenuItem.Tag = "2";
            this.fileMobsmobToolStripMenuItem.Text = "File Mobs (.mob)";
            this.fileMobsmobToolStripMenuItem.Click += new System.EventHandler(this.fileZonazonToolStripMenuItem_Click);
            // 
            // fileOggettiobjToolStripMenuItem
            // 
            this.fileOggettiobjToolStripMenuItem.Name = "fileOggettiobjToolStripMenuItem";
            this.fileOggettiobjToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.fileOggettiobjToolStripMenuItem.Tag = "3";
            this.fileOggettiobjToolStripMenuItem.Text = "File Oggetti (.obj)";
            this.fileOggettiobjToolStripMenuItem.Click += new System.EventHandler(this.fileZonazonToolStripMenuItem_Click);
            // 
            // fileNegozishpToolStripMenuItem
            // 
            this.fileNegozishpToolStripMenuItem.Name = "fileNegozishpToolStripMenuItem";
            this.fileNegozishpToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.fileNegozishpToolStripMenuItem.Tag = "4";
            this.fileNegozishpToolStripMenuItem.Text = "File Negozi (.shp)";
            this.fileNegozishpToolStripMenuItem.Click += new System.EventHandler(this.fileZonazonToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(232, 6);
            // 
            // esportaInHTMLToolStripMenuItem
            // 
            this.esportaInHTMLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oggettiToolStripMenuItem});
            this.esportaInHTMLToolStripMenuItem.Name = "esportaInHTMLToolStripMenuItem";
            this.esportaInHTMLToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.esportaInHTMLToolStripMenuItem.Text = "Esporta in HTML";
            // 
            // oggettiToolStripMenuItem
            // 
            this.oggettiToolStripMenuItem.Name = "oggettiToolStripMenuItem";
            this.oggettiToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.oggettiToolStripMenuItem.Tag = "3";
            this.oggettiToolStripMenuItem.Text = "File Oggetti";
            this.oggettiToolStripMenuItem.Click += new System.EventHandler(this.oggettiToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btndel);
            this.panel2.Controls.Add(this.btnoptions);
            this.panel2.Controls.Add(this.btnreports);
            this.panel2.Controls.Add(this.btnadd);
            this.panel2.Controls.Add(this.btneditvis);
            this.panel2.Controls.Add(this.btnedit);
            this.panel2.Controls.Add(this.btnclone);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 66);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(34, 415);
            this.panel2.TabIndex = 10;
            // 
            // btndel
            // 
            this.btndel.ImageKey = "TableMissing_8931_24.bmp";
            this.btndel.ImageList = this.imageList1;
            this.btndel.Location = new System.Drawing.Point(3, 99);
            this.btndel.Name = "btndel";
            this.btndel.Size = new System.Drawing.Size(26, 26);
            this.btndel.TabIndex = 19;
            this.btndel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btndel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btndel, "Elimina l\'elemento selezionato");
            this.btndel.UseVisualStyleBackColor = true;
            this.btndel.Click += new System.EventHandler(this.eliminaToolStripMenuItem_Click);
            // 
            // btnoptions
            // 
            this.btnoptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnoptions.ImageKey = "gear.png";
            this.btnoptions.ImageList = this.imageList1;
            this.btnoptions.Location = new System.Drawing.Point(3, 386);
            this.btnoptions.Name = "btnoptions";
            this.btnoptions.Size = new System.Drawing.Size(26, 26);
            this.btnoptions.TabIndex = 8;
            this.btnoptions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnoptions, "Opzioni");
            this.btnoptions.UseVisualStyleBackColor = true;
            this.btnoptions.Click += new System.EventHandler(this.noselButton1_Click);
            // 
            // btnreports
            // 
            this.btnreports.ImageKey = "Reports-collapsed_12995_24.bmp";
            this.btnreports.ImageList = this.imageList1;
            this.btnreports.Location = new System.Drawing.Point(3, 231);
            this.btnreports.Name = "btnreports";
            this.btnreports.Size = new System.Drawing.Size(26, 26);
            this.btnreports.TabIndex = 6;
            this.btnreports.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnreports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnreports, "Reports");
            this.btnreports.UseVisualStyleBackColor = true;
            this.btnreports.Click += new System.EventHandler(this.btnreportsclick);
            // 
            // btnadd
            // 
            this.btnadd.ImageKey = "add.png";
            this.btnadd.ImageList = this.imageList1;
            this.btnadd.Location = new System.Drawing.Point(3, 6);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(26, 26);
            this.btnadd.TabIndex = 18;
            this.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnadd, "Crea una nuova stanza o inserisce il template selezionato");
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.CreateElement);
            // 
            // btneditvis
            // 
            this.btneditvis.ImageKey = "imagelis_web[15].bmp";
            this.btneditvis.ImageList = this.imageList1;
            this.btneditvis.Location = new System.Drawing.Point(3, 200);
            this.btneditvis.Name = "btneditvis";
            this.btneditvis.Size = new System.Drawing.Size(26, 26);
            this.btneditvis.TabIndex = 5;
            this.btneditvis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btneditvis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btneditvis, "Mappa Grafica");
            this.btneditvis.UseVisualStyleBackColor = true;
            this.btneditvis.Click += new System.EventHandler(this.btnmapclick);
            // 
            // btnedit
            // 
            this.btnedit.ImageKey = "EditorZone_6025_24.bmp";
            this.btnedit.ImageList = this.imageList1;
            this.btnedit.Location = new System.Drawing.Point(3, 67);
            this.btnedit.Name = "btnedit";
            this.btnedit.Size = new System.Drawing.Size(26, 26);
            this.btnedit.TabIndex = 18;
            this.btnedit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnedit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnedit, "Modifica l\'elemento selezionato");
            this.btnedit.UseVisualStyleBackColor = true;
            this.btnedit.Click += new System.EventHandler(this.EditSelected);
            // 
            // btnclone
            // 
            this.btnclone.ImageKey = "CopyWebSite_340_32.bmp";
            this.btnclone.ImageList = this.imageList1;
            this.btnclone.Location = new System.Drawing.Point(3, 35);
            this.btnclone.Name = "btnclone";
            this.btnclone.Size = new System.Drawing.Size(26, 26);
            this.btnclone.TabIndex = 21;
            this.btnclone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnclone.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnclone, "Duplica l\'elemento selezionato");
            this.btnclone.UseVisualStyleBackColor = true;
            this.btnclone.Click += new System.EventHandler(this.btnclone_Click);
            // 
            // chkDuplicate
            // 
            this.chkDuplicate.AutoSize = true;
            this.chkDuplicate.Location = new System.Drawing.Point(10, 312);
            this.chkDuplicate.Name = "chkDuplicate";
            this.chkDuplicate.Size = new System.Drawing.Size(62, 17);
            this.chkDuplicate.TabIndex = 14;
            this.chkDuplicate.Text = "Duplica";
            this.chkDuplicate.UseVisualStyleBackColor = true;
            // 
            // frm_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 503);
            this.Controls.Add(this.list);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pn_utils);
            this.Controls.Add(this.radiogroup);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(794, 542);
            this.Name = "frm_List";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liste";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_List_FormClosing);
            this.Shown += new System.EventHandler(this.frm_List_Shown);
            this.radiogroup.ResumeLayout(false);
            this.radiogroup.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pn_utils.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_fromvnum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_newvnum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_tovnum)).EndInit();
            this.popupsave.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HoGListView list;
        private System.Windows.Forms.Panel radiogroup;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton rd_zones;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblstatus;
        private System.Windows.Forms.Panel panel1;
        private NoselButton btnsave;
        private System.Windows.Forms.Timer t_panel;
        private System.Windows.Forms.Panel pn_utils;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown spin_fromvnum;
        private System.Windows.Forms.NumericUpDown spin_newvnum;
        private System.Windows.Forms.NumericUpDown spin_tovnum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnstartenum;
        private System.Windows.Forms.CheckBox chkshops;
        private System.Windows.Forms.CheckBox chkobjects;
        private System.Windows.Forms.CheckBox chkmobs;
        private System.Windows.Forms.CheckBox chkrooms;
        private System.Windows.Forms.CheckBox btnenum;
        private System.Windows.Forms.Label label5;
        private NoselButton btneditvis;
        private NoselButton btnreports;
        private NoselButton btnsavein;
        private System.Windows.Forms.ContextMenuStrip popupsave;
        private System.Windows.Forms.ToolStripMenuItem esportaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileZonazonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileStanzewldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMobsmobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileOggettiobjToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileNegozishpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnsavechoosedir;
        private System.Windows.Forms.ToolStripMenuItem mnsavesetdirs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private NoselButton btnclone;
        private NoselButton btndel;
        private NoselButton btnedit;
        private NoselButton btnaddsel;
        private NoselButton btnadd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip1;
        private NoselButton btnoptions;
        private System.Windows.Forms.Button btndeltemplate;
        private System.Windows.Forms.Button btnedittemplates;
        private SearchTextBox searchbox;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem esportaInHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oggettiToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkDuplicate;
    }
}