namespace HandofGod
{
    partial class frm_Room
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.page_longdesc = new System.Windows.Forms.TabPage();
            this.memo_longdesc = new HandofGod.MudlikeRichTextBox();
            this.page_daydesc = new System.Windows.Forms.TabPage();
            this.memo_daydesc = new HandofGod.MudlikeRichTextBox();
            this.page_nightdesc = new System.Windows.Forms.TabPage();
            this.memo_nightdesc = new HandofGod.MudlikeRichTextBox();
            this.page_extradesc = new System.Windows.Forms.TabPage();
            this.memo_extradesc = new HandofGod.MudlikeRichTextBox();
            this.btn_extradescDel = new System.Windows.Forms.Button();
            this.btn_extradescAdd = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.edt_extradesc = new System.Windows.Forms.TextBox();
            this.edt_shortdesc = new HandofGod.MudlikeRichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.combo_sect = new System.Windows.Forms.ComboBox();
            this.list_extradesc = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btndoorinit = new System.Windows.Forms.Button();
            this.btn_followexit = new System.Windows.Forms.Button();
            this.list_exits = new HandofGod.HoGListView();
            this.btn_editexit = new System.Windows.Forms.Button();
            this.btn_delexit = new System.Windows.Forms.Button();
            this.btn_addexit = new System.Windows.Forms.Button();
            this.pn_teleport = new System.Windows.Forms.Panel();
            this.edt_tel_toroom = new System.Windows.Forms.NumericUpDown();
            this.edt_tel_counter = new System.Windows.Forms.NumericUpDown();
            this.combo_tel_sect = new System.Windows.Forms.ComboBox();
            this.edt_tel_time = new System.Windows.Forms.NumericUpDown();
            this.chk_tel_flag4 = new System.Windows.Forms.CheckBox();
            this.chk_tel_flag3 = new System.Windows.Forms.CheckBox();
            this.chk_tel_flag1 = new System.Windows.Forms.CheckBox();
            this.chk_tel_flag2 = new System.Windows.Forms.CheckBox();
            this.chk_tel_flag0 = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.pn_watercurrent = new System.Windows.Forms.Panel();
            this.combo_watercurrent_dir = new System.Windows.Forms.ComboBox();
            this.spin_watercurrent_vel = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.gaugecharcounter = new HandofGod.CharCounterProgressBar();
            this.spin_maxpc = new System.Windows.Forms.NumericUpDown();
            this.lbl_maxpc = new System.Windows.Forms.Label();
            this.spin_maxobj = new System.Windows.Forms.NumericUpDown();
            this.lbl_maxobj = new System.Windows.Forms.Label();
            this.tabcontrol_flags = new System.Windows.Forms.TabControl();
            this.tab_flags0 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.flg6 = new System.Windows.Forms.CheckBox();
            this.flg26 = new System.Windows.Forms.CheckBox();
            this.flg24 = new System.Windows.Forms.CheckBox();
            this.flg28 = new System.Windows.Forms.CheckBox();
            this.flg27 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flg21 = new System.Windows.Forms.CheckBox();
            this.flg20 = new System.Windows.Forms.CheckBox();
            this.flg22 = new System.Windows.Forms.CheckBox();
            this.flg10 = new System.Windows.Forms.CheckBox();
            this.flg15 = new System.Windows.Forms.CheckBox();
            this.flg1 = new System.Windows.Forms.CheckBox();
            this.flg14 = new System.Windows.Forms.CheckBox();
            this.flg25 = new System.Windows.Forms.CheckBox();
            this.flg2 = new System.Windows.Forms.CheckBox();
            this.flg4 = new System.Windows.Forms.CheckBox();
            this.flg7 = new System.Windows.Forms.CheckBox();
            this.flg5 = new System.Windows.Forms.CheckBox();
            this.tab_flags1 = new System.Windows.Forms.TabPage();
            this.flg13 = new System.Windows.Forms.CheckBox();
            this.flg12 = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.flg11 = new System.Windows.Forms.CheckBox();
            this.flg9 = new System.Windows.Forms.CheckBox();
            this.flg8 = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.flg23 = new System.Windows.Forms.CheckBox();
            this.flg0 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flg17 = new System.Windows.Forms.CheckBox();
            this.flg16 = new System.Windows.Forms.CheckBox();
            this.flg3 = new System.Windows.Forms.CheckBox();
            this.flg19 = new System.Windows.Forms.CheckBox();
            this.flg18 = new System.Windows.Forms.CheckBox();
            this.spin_area = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spin_vnum)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.page_longdesc.SuspendLayout();
            this.page_daydesc.SuspendLayout();
            this.page_nightdesc.SuspendLayout();
            this.page_extradesc.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pn_teleport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edt_tel_toroom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt_tel_counter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt_tel_time)).BeginInit();
            this.pn_watercurrent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_watercurrent_vel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_maxpc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_maxobj)).BeginInit();
            this.tabcontrol_flags.SuspendLayout();
            this.tab_flags0.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tab_flags1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_area)).BeginInit();
            this.SuspendLayout();
            // 
            // spin_vnum
            // 
            this.spin_vnum.Location = new System.Drawing.Point(10, 20);
            this.spin_vnum.Size = new System.Drawing.Size(92, 20);
            // 
            // lblvnum
            // 
            this.lblvnum.Location = new System.Drawing.Point(11, 4);
            // 
            // btnapply
            // 
            this.btnapply.Location = new System.Drawing.Point(676, 641);
            // 
            // btnrestore
            // 
            this.btnrestore.Location = new System.Drawing.Point(757, 641);
            // 
            // btnprev
            // 
            this.btnprev.Location = new System.Drawing.Point(891, 641);
            // 
            // btnnext
            // 
            this.btnnext.Location = new System.Drawing.Point(924, 641);
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(88, 641);
            // 
            // btnok
            // 
            this.btnok.Location = new System.Drawing.Point(7, 641);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.page_longdesc);
            this.tabControl1.Controls.Add(this.page_daydesc);
            this.tabControl1.Controls.Add(this.page_nightdesc);
            this.tabControl1.Controls.Add(this.page_extradesc);
            this.tabControl1.Location = new System.Drawing.Point(19, 260);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(651, 360);
            this.tabControl1.TabIndex = 11;
            // 
            // page_longdesc
            // 
            this.page_longdesc.Controls.Add(this.memo_longdesc);
            this.page_longdesc.Location = new System.Drawing.Point(4, 22);
            this.page_longdesc.Name = "page_longdesc";
            this.page_longdesc.Padding = new System.Windows.Forms.Padding(3);
            this.page_longdesc.Size = new System.Drawing.Size(643, 334);
            this.page_longdesc.TabIndex = 0;
            this.page_longdesc.Text = "Long Desc";
            this.page_longdesc.UseVisualStyleBackColor = true;
            // 
            // memo_longdesc
            // 
            this.memo_longdesc.DefaultCharacterColor = System.Drawing.Color.LightGray;
            this.memo_longdesc.DetectUrls = false;
            this.memo_longdesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memo_longdesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.memo_longdesc.Location = new System.Drawing.Point(3, 3);
            this.memo_longdesc.Name = "memo_longdesc";
            this.memo_longdesc.Size = new System.Drawing.Size(637, 328);
            this.memo_longdesc.TabIndex = 0;
            this.memo_longdesc.Text = "";
            this.memo_longdesc.WordWrap = false;
            // 
            // page_daydesc
            // 
            this.page_daydesc.Controls.Add(this.memo_daydesc);
            this.page_daydesc.Location = new System.Drawing.Point(4, 22);
            this.page_daydesc.Name = "page_daydesc";
            this.page_daydesc.Padding = new System.Windows.Forms.Padding(3);
            this.page_daydesc.Size = new System.Drawing.Size(643, 334);
            this.page_daydesc.TabIndex = 1;
            this.page_daydesc.Text = "Day Desc";
            this.page_daydesc.UseVisualStyleBackColor = true;
            // 
            // memo_daydesc
            // 
            this.memo_daydesc.DefaultCharacterColor = System.Drawing.Color.LightGray;
            this.memo_daydesc.DetectUrls = false;
            this.memo_daydesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memo_daydesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.memo_daydesc.Location = new System.Drawing.Point(3, 3);
            this.memo_daydesc.Name = "memo_daydesc";
            this.memo_daydesc.Size = new System.Drawing.Size(637, 328);
            this.memo_daydesc.TabIndex = 1;
            this.memo_daydesc.Text = "";
            this.memo_daydesc.WordWrap = false;
            // 
            // page_nightdesc
            // 
            this.page_nightdesc.Controls.Add(this.memo_nightdesc);
            this.page_nightdesc.Location = new System.Drawing.Point(4, 22);
            this.page_nightdesc.Name = "page_nightdesc";
            this.page_nightdesc.Size = new System.Drawing.Size(643, 334);
            this.page_nightdesc.TabIndex = 2;
            this.page_nightdesc.Text = "Night Desc";
            this.page_nightdesc.UseVisualStyleBackColor = true;
            // 
            // memo_nightdesc
            // 
            this.memo_nightdesc.DefaultCharacterColor = System.Drawing.Color.LightGray;
            this.memo_nightdesc.DetectUrls = false;
            this.memo_nightdesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memo_nightdesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.memo_nightdesc.Location = new System.Drawing.Point(0, 0);
            this.memo_nightdesc.Name = "memo_nightdesc";
            this.memo_nightdesc.Size = new System.Drawing.Size(643, 334);
            this.memo_nightdesc.TabIndex = 1;
            this.memo_nightdesc.Text = "";
            this.memo_nightdesc.WordWrap = false;
            // 
            // page_extradesc
            // 
            this.page_extradesc.Controls.Add(this.memo_extradesc);
            this.page_extradesc.Controls.Add(this.btn_extradescDel);
            this.page_extradesc.Controls.Add(this.btn_extradescAdd);
            this.page_extradesc.Controls.Add(this.label6);
            this.page_extradesc.Controls.Add(this.label5);
            this.page_extradesc.Controls.Add(this.edt_extradesc);
            this.page_extradesc.Location = new System.Drawing.Point(4, 22);
            this.page_extradesc.Name = "page_extradesc";
            this.page_extradesc.Size = new System.Drawing.Size(643, 334);
            this.page_extradesc.TabIndex = 3;
            this.page_extradesc.Text = "Extra Desc";
            this.page_extradesc.UseVisualStyleBackColor = true;
            // 
            // memo_extradesc
            // 
            this.memo_extradesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memo_extradesc.DefaultCharacterColor = System.Drawing.Color.LightGray;
            this.memo_extradesc.DetectUrls = false;
            this.memo_extradesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.memo_extradesc.Location = new System.Drawing.Point(8, 38);
            this.memo_extradesc.Name = "memo_extradesc";
            this.memo_extradesc.Size = new System.Drawing.Size(410, 201);
            this.memo_extradesc.TabIndex = 8;
            this.memo_extradesc.Text = "";
            this.memo_extradesc.WordWrap = false;
            this.memo_extradesc.TextChanged += new System.EventHandler(this.memo_extradesc_TextChanged);
            // 
            // btn_extradescDel
            // 
            this.btn_extradescDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_extradescDel.Location = new System.Drawing.Point(424, 132);
            this.btn_extradescDel.Name = "btn_extradescDel";
            this.btn_extradescDel.Size = new System.Drawing.Size(37, 23);
            this.btn_extradescDel.TabIndex = 7;
            this.btn_extradescDel.Text = "Del";
            this.btn_extradescDel.UseVisualStyleBackColor = true;
            this.btn_extradescDel.Click += new System.EventHandler(this.btn_extradescDel_Click);
            // 
            // btn_extradescAdd
            // 
            this.btn_extradescAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_extradescAdd.Location = new System.Drawing.Point(424, 103);
            this.btn_extradescAdd.Name = "btn_extradescAdd";
            this.btn_extradescAdd.Size = new System.Drawing.Size(37, 23);
            this.btn_extradescAdd.TabIndex = 6;
            this.btn_extradescAdd.Text = "-->";
            this.btn_extradescAdd.UseVisualStyleBackColor = true;
            this.btn_extradescAdd.Click += new System.EventHandler(this.btn_extradescAdd_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Desc:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Keys:";
            // 
            // edt_extradesc
            // 
            this.edt_extradesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edt_extradesc.Location = new System.Drawing.Point(44, 3);
            this.edt_extradesc.Name = "edt_extradesc";
            this.edt_extradesc.Size = new System.Drawing.Size(377, 20);
            this.edt_extradesc.TabIndex = 2;
            this.edt_extradesc.TextChanged += new System.EventHandler(this.memo_extradesc_TextChanged);
            // 
            // edt_shortdesc
            // 
            this.edt_shortdesc.DefaultCharacterColor = System.Drawing.Color.LightGray;
            this.edt_shortdesc.DetectUrls = false;
            this.edt_shortdesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.edt_shortdesc.Location = new System.Drawing.Point(12, 61);
            this.edt_shortdesc.Multiline = false;
            this.edt_shortdesc.Name = "edt_shortdesc";
            this.edt_shortdesc.Size = new System.Drawing.Size(537, 20);
            this.edt_shortdesc.TabIndex = 2;
            this.edt_shortdesc.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Short Desc:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(108, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Settore:";
            // 
            // combo_sect
            // 
            this.combo_sect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_sect.FormattingEnabled = true;
            this.combo_sect.Items.AddRange(new object[] {
            "Inside",
            "City streets",
            "Field",
            "Forest",
            "Hills",
            "Mountain",
            "Water,swimmable",
            "Water,not swimmable",
            "Air",
            "Underwater",
            "Desert (riservato?)",
            "Tree",
            "Darkcity",
            "Teleport"});
            this.combo_sect.Location = new System.Drawing.Point(158, 34);
            this.combo_sect.Name = "combo_sect";
            this.combo_sect.Size = new System.Drawing.Size(91, 21);
            this.combo_sect.TabIndex = 1;
            this.combo_sect.SelectedIndexChanged += new System.EventHandler(this.combo_sect_SelectedIndexChanged);
            // 
            // list_extradesc
            // 
            this.list_extradesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_extradesc.FormattingEnabled = true;
            this.list_extradesc.Location = new System.Drawing.Point(679, 292);
            this.list_extradesc.Name = "list_extradesc";
            this.list_extradesc.Size = new System.Drawing.Size(114, 290);
            this.list_extradesc.TabIndex = 12;
            this.list_extradesc.SelectedValueChanged += new System.EventHandler(this.list_extradesc_SelectedValueChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(676, 276);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Extra Desc: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btndoorinit);
            this.groupBox1.Controls.Add(this.btn_followexit);
            this.groupBox1.Controls.Add(this.list_exits);
            this.groupBox1.Controls.Add(this.btn_editexit);
            this.groupBox1.Controls.Add(this.btn_delexit);
            this.groupBox1.Controls.Add(this.btn_addexit);
            this.groupBox1.Location = new System.Drawing.Point(16, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(777, 170);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Exits";
            // 
            // btndoorinit
            // 
            this.btndoorinit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btndoorinit.BackColor = System.Drawing.Color.LightCoral;
            this.btndoorinit.Location = new System.Drawing.Point(725, 136);
            this.btndoorinit.Name = "btndoorinit";
            this.btndoorinit.Size = new System.Drawing.Size(46, 28);
            this.btndoorinit.TabIndex = 10;
            this.btndoorinit.Text = "Init";
            this.btndoorinit.UseVisualStyleBackColor = false;
            this.btndoorinit.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_followexit
            // 
            this.btn_followexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_followexit.Location = new System.Drawing.Point(725, 106);
            this.btn_followexit.Name = "btn_followexit";
            this.btn_followexit.Size = new System.Drawing.Size(46, 28);
            this.btn_followexit.TabIndex = 8;
            this.btn_followexit.Text = "Segui";
            this.btn_followexit.UseVisualStyleBackColor = true;
            this.btn_followexit.Click += new System.EventHandler(this.btn_followexit_Click);
            // 
            // list_exits
            // 
            this.list_exits.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_exits.Filter = "";
            this.list_exits.FullRowSelect = true;
            this.list_exits.GridLines = true;
            this.list_exits.HideSelection = false;
            this.list_exits.Location = new System.Drawing.Point(3, 16);
            this.list_exits.MultiSelect = false;
            this.list_exits.Name = "list_exits";
            this.list_exits.Size = new System.Drawing.Size(716, 151);
            this.list_exits.TabIndex = 9;
            this.list_exits.UseCompatibleStateImageBehavior = false;
            this.list_exits.View = System.Windows.Forms.View.Details;
            this.list_exits.SelectedIndexChanged += new System.EventHandler(this.list_exits_SelectedIndexChanged);
            this.list_exits.DoubleClick += new System.EventHandler(this.btn_editexit_Click);
            this.list_exits.MouseUp += new System.Windows.Forms.MouseEventHandler(this.list_exits_MouseUp);
            // 
            // btn_editexit
            // 
            this.btn_editexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_editexit.Location = new System.Drawing.Point(725, 46);
            this.btn_editexit.Name = "btn_editexit";
            this.btn_editexit.Size = new System.Drawing.Size(46, 28);
            this.btn_editexit.TabIndex = 6;
            this.btn_editexit.Text = "Edit";
            this.btn_editexit.UseVisualStyleBackColor = true;
            this.btn_editexit.Click += new System.EventHandler(this.btn_editexit_Click);
            // 
            // btn_delexit
            // 
            this.btn_delexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delexit.Location = new System.Drawing.Point(725, 76);
            this.btn_delexit.Name = "btn_delexit";
            this.btn_delexit.Size = new System.Drawing.Size(46, 28);
            this.btn_delexit.TabIndex = 7;
            this.btn_delexit.Text = "Del";
            this.btn_delexit.UseVisualStyleBackColor = true;
            this.btn_delexit.Click += new System.EventHandler(this.btn_delexit_Click);
            // 
            // btn_addexit
            // 
            this.btn_addexit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_addexit.Location = new System.Drawing.Point(725, 16);
            this.btn_addexit.Name = "btn_addexit";
            this.btn_addexit.Size = new System.Drawing.Size(46, 28);
            this.btn_addexit.TabIndex = 5;
            this.btn_addexit.Text = "Add";
            this.btn_addexit.UseVisualStyleBackColor = true;
            this.btn_addexit.Click += new System.EventHandler(this.btn_addexit_Click);
            // 
            // pn_teleport
            // 
            this.pn_teleport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pn_teleport.Controls.Add(this.edt_tel_toroom);
            this.pn_teleport.Controls.Add(this.edt_tel_counter);
            this.pn_teleport.Controls.Add(this.combo_tel_sect);
            this.pn_teleport.Controls.Add(this.edt_tel_time);
            this.pn_teleport.Controls.Add(this.chk_tel_flag4);
            this.pn_teleport.Controls.Add(this.chk_tel_flag3);
            this.pn_teleport.Controls.Add(this.chk_tel_flag1);
            this.pn_teleport.Controls.Add(this.chk_tel_flag2);
            this.pn_teleport.Controls.Add(this.chk_tel_flag0);
            this.pn_teleport.Controls.Add(this.label12);
            this.pn_teleport.Controls.Add(this.label11);
            this.pn_teleport.Controls.Add(this.label10);
            this.pn_teleport.Controls.Add(this.label9);
            this.pn_teleport.Controls.Add(this.label8);
            this.pn_teleport.Location = new System.Drawing.Point(255, -3);
            this.pn_teleport.Name = "pn_teleport";
            this.pn_teleport.Size = new System.Drawing.Size(538, 61);
            this.pn_teleport.TabIndex = 69;
            // 
            // edt_tel_toroom
            // 
            this.edt_tel_toroom.Location = new System.Drawing.Point(209, 38);
            this.edt_tel_toroom.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.edt_tel_toroom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.edt_tel_toroom.Name = "edt_tel_toroom";
            this.edt_tel_toroom.Size = new System.Drawing.Size(89, 20);
            this.edt_tel_toroom.TabIndex = 100;
            // 
            // edt_tel_counter
            // 
            this.edt_tel_counter.Location = new System.Drawing.Point(80, 38);
            this.edt_tel_counter.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.edt_tel_counter.Name = "edt_tel_counter";
            this.edt_tel_counter.Size = new System.Drawing.Size(44, 20);
            this.edt_tel_counter.TabIndex = 99;
            // 
            // combo_tel_sect
            // 
            this.combo_tel_sect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_tel_sect.FormattingEnabled = true;
            this.combo_tel_sect.Items.AddRange(new object[] {
            "Inside",
            "City streets",
            "Field",
            "Forest",
            "Hills",
            "Mountain",
            "Water,swimmable",
            "Water,not swimmable",
            "Air",
            "Underwater",
            "Desert (riservato?)",
            "Tree",
            "Darkcity",
            "Teleport"});
            this.combo_tel_sect.Location = new System.Drawing.Point(209, 7);
            this.combo_tel_sect.Name = "combo_tel_sect";
            this.combo_tel_sect.Size = new System.Drawing.Size(130, 21);
            this.combo_tel_sect.TabIndex = 98;
            this.combo_tel_sect.SelectedIndexChanged += new System.EventHandler(this.combo_sect_SelectedIndexChanged);
            // 
            // edt_tel_time
            // 
            this.edt_tel_time.Location = new System.Drawing.Point(80, 8);
            this.edt_tel_time.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.edt_tel_time.Name = "edt_tel_time";
            this.edt_tel_time.Size = new System.Drawing.Size(44, 20);
            this.edt_tel_time.TabIndex = 97;
            // 
            // chk_tel_flag4
            // 
            this.chk_tel_flag4.AutoSize = true;
            this.chk_tel_flag4.Location = new System.Drawing.Point(434, 41);
            this.chk_tel_flag4.Name = "chk_tel_flag4";
            this.chk_tel_flag4.Size = new System.Drawing.Size(64, 17);
            this.chk_tel_flag4.TabIndex = 96;
            this.chk_tel_flag4.Text = "No Mob";
            this.chk_tel_flag4.UseVisualStyleBackColor = true;
            // 
            // chk_tel_flag3
            // 
            this.chk_tel_flag3.AutoSize = true;
            this.chk_tel_flag3.Location = new System.Drawing.Point(434, 26);
            this.chk_tel_flag3.Name = "chk_tel_flag3";
            this.chk_tel_flag3.Size = new System.Drawing.Size(47, 17);
            this.chk_tel_flag3.TabIndex = 95;
            this.chk_tel_flag3.Text = "Spin";
            this.chk_tel_flag3.UseVisualStyleBackColor = true;
            // 
            // chk_tel_flag1
            // 
            this.chk_tel_flag1.AutoSize = true;
            this.chk_tel_flag1.Location = new System.Drawing.Point(348, 40);
            this.chk_tel_flag1.Name = "chk_tel_flag1";
            this.chk_tel_flag1.Size = new System.Drawing.Size(66, 17);
            this.chk_tel_flag1.TabIndex = 94;
            this.chk_tel_flag1.Text = "Random";
            this.chk_tel_flag1.UseVisualStyleBackColor = true;
            // 
            // chk_tel_flag2
            // 
            this.chk_tel_flag2.AutoSize = true;
            this.chk_tel_flag2.Location = new System.Drawing.Point(434, 11);
            this.chk_tel_flag2.Name = "chk_tel_flag2";
            this.chk_tel_flag2.Size = new System.Drawing.Size(54, 17);
            this.chk_tel_flag2.TabIndex = 93;
            this.chk_tel_flag2.Text = "Count";
            this.chk_tel_flag2.UseVisualStyleBackColor = true;
            // 
            // chk_tel_flag0
            // 
            this.chk_tel_flag0.AutoSize = true;
            this.chk_tel_flag0.Location = new System.Drawing.Point(348, 26);
            this.chk_tel_flag0.Name = "chk_tel_flag0";
            this.chk_tel_flag0.Size = new System.Drawing.Size(50, 17);
            this.chk_tel_flag0.TabIndex = 92;
            this.chk_tel_flag0.Text = "Look";
            this.chk_tel_flag0.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(345, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 13);
            this.label12.TabIndex = 91;
            this.label12.Text = "Flags Teleport: ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(130, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 90;
            this.label11.Text = "Settore reale: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(132, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 89;
            this.label10.Text = "Alla stanza: #";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 88;
            this.label9.Text = "Contatore: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 87;
            this.label8.Text = "Tempo: ";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(297, 35);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(45, 23);
            this.button3.TabIndex = 101;
            this.button3.TabStop = false;
            this.button3.Text = "Trova";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pn_watercurrent
            // 
            this.pn_watercurrent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pn_watercurrent.Controls.Add(this.combo_watercurrent_dir);
            this.pn_watercurrent.Controls.Add(this.spin_watercurrent_vel);
            this.pn_watercurrent.Controls.Add(this.label18);
            this.pn_watercurrent.Controls.Add(this.label19);
            this.pn_watercurrent.Location = new System.Drawing.Point(771, -3);
            this.pn_watercurrent.Name = "pn_watercurrent";
            this.pn_watercurrent.Size = new System.Drawing.Size(180, 61);
            this.pn_watercurrent.TabIndex = 73;
            // 
            // combo_watercurrent_dir
            // 
            this.combo_watercurrent_dir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_watercurrent_dir.FormattingEnabled = true;
            this.combo_watercurrent_dir.Items.AddRange(new object[] {
            "Inside",
            "City streets",
            "Field",
            "Forest",
            "Hills",
            "Mountain",
            "Water,swimmable",
            "Water,not swimmable",
            "Air",
            "Underwater",
            "Desert (riservato?)",
            "Tree",
            "Darkcity",
            "Teleport"});
            this.combo_watercurrent_dir.Location = new System.Drawing.Point(121, 8);
            this.combo_watercurrent_dir.Name = "combo_watercurrent_dir";
            this.combo_watercurrent_dir.Size = new System.Drawing.Size(56, 21);
            this.combo_watercurrent_dir.TabIndex = 103;
            // 
            // spin_watercurrent_vel
            // 
            this.spin_watercurrent_vel.Location = new System.Drawing.Point(121, 38);
            this.spin_watercurrent_vel.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spin_watercurrent_vel.Name = "spin_watercurrent_vel";
            this.spin_watercurrent_vel.Size = new System.Drawing.Size(56, 20);
            this.spin_watercurrent_vel.TabIndex = 99;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(21, 40);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(94, 13);
            this.label18.TabIndex = 88;
            this.label18.Text = "Velocità Corrente: ";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(15, 12);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(100, 13);
            this.label19.TabIndex = 87;
            this.label19.Text = "Direzione Corrente: ";
            // 
            // gaugecharcounter
            // 
            this.gaugecharcounter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gaugecharcounter.Location = new System.Drawing.Point(23, 622);
            this.gaugecharcounter.Name = "gaugecharcounter";
            this.gaugecharcounter.Size = new System.Drawing.Size(640, 15);
            this.gaugecharcounter.TabIndex = 70;
            // 
            // spin_maxpc
            // 
            this.spin_maxpc.Location = new System.Drawing.Point(726, 61);
            this.spin_maxpc.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spin_maxpc.Name = "spin_maxpc";
            this.spin_maxpc.Size = new System.Drawing.Size(46, 20);
            this.spin_maxpc.TabIndex = 4;
            // 
            // lbl_maxpc
            // 
            this.lbl_maxpc.AutoSize = true;
            this.lbl_maxpc.Location = new System.Drawing.Point(670, 63);
            this.lbl_maxpc.Name = "lbl_maxpc";
            this.lbl_maxpc.Size = new System.Drawing.Size(50, 13);
            this.lbl_maxpc.TabIndex = 90;
            this.lbl_maxpc.Text = "Max PC: ";
            // 
            // spin_maxobj
            // 
            this.spin_maxobj.Location = new System.Drawing.Point(617, 61);
            this.spin_maxobj.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spin_maxobj.Name = "spin_maxobj";
            this.spin_maxobj.Size = new System.Drawing.Size(43, 20);
            this.spin_maxobj.TabIndex = 3;
            // 
            // lbl_maxobj
            // 
            this.lbl_maxobj.AutoSize = true;
            this.lbl_maxobj.Location = new System.Drawing.Point(559, 64);
            this.lbl_maxobj.Name = "lbl_maxobj";
            this.lbl_maxobj.Size = new System.Drawing.Size(52, 13);
            this.lbl_maxobj.TabIndex = 88;
            this.lbl_maxobj.Text = "Max Obj: ";
            // 
            // tabcontrol_flags
            // 
            this.tabcontrol_flags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabcontrol_flags.Controls.Add(this.tab_flags0);
            this.tabcontrol_flags.Controls.Add(this.tab_flags1);
            this.tabcontrol_flags.Location = new System.Drawing.Point(799, 87);
            this.tabcontrol_flags.Name = "tabcontrol_flags";
            this.tabcontrol_flags.SelectedIndex = 0;
            this.tabcontrol_flags.Size = new System.Drawing.Size(152, 526);
            this.tabcontrol_flags.TabIndex = 10;
            // 
            // tab_flags0
            // 
            this.tab_flags0.BackColor = System.Drawing.SystemColors.Control;
            this.tab_flags0.Controls.Add(this.groupBox5);
            this.tab_flags0.Controls.Add(this.groupBox3);
            this.tab_flags0.Location = new System.Drawing.Point(4, 22);
            this.tab_flags0.Name = "tab_flags0";
            this.tab_flags0.Padding = new System.Windows.Forms.Padding(3);
            this.tab_flags0.Size = new System.Drawing.Size(144, 500);
            this.tab_flags0.TabIndex = 0;
            this.tab_flags0.Text = "Gameplay";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.BackColor = System.Drawing.Color.Cornsilk;
            this.groupBox5.Controls.Add(this.flg6);
            this.groupBox5.Controls.Add(this.flg26);
            this.groupBox5.Controls.Add(this.flg24);
            this.groupBox5.Controls.Add(this.flg28);
            this.groupBox5.Controls.Add(this.flg27);
            this.groupBox5.Location = new System.Drawing.Point(6, 250);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(132, 244);
            this.groupBox5.TabIndex = 89;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Movimento";
            // 
            // flg6
            // 
            this.flg6.Location = new System.Drawing.Point(8, 48);
            this.flg6.Name = "flg6";
            this.flg6.Size = new System.Drawing.Size(110, 17);
            this.flg6.TabIndex = 25;
            this.flg6.Text = "nosummon";
            this.flg6.UseVisualStyleBackColor = true;
            // 
            // flg26
            // 
            this.flg26.Location = new System.Drawing.Point(8, 31);
            this.flg26.Name = "flg26";
            this.flg26.Size = new System.Drawing.Size(110, 17);
            this.flg26.TabIndex = 24;
            this.flg26.Text = "notargetportal";
            this.flg26.UseVisualStyleBackColor = true;
            // 
            // flg24
            // 
            this.flg24.Location = new System.Drawing.Point(8, 15);
            this.flg24.Name = "flg24";
            this.flg24.Size = new System.Drawing.Size(110, 17);
            this.flg24.TabIndex = 23;
            this.flg24.Text = "noastral";
            this.flg24.UseVisualStyleBackColor = true;
            // 
            // flg28
            // 
            this.flg28.Location = new System.Drawing.Point(8, 82);
            this.flg28.Name = "flg28";
            this.flg28.Size = new System.Drawing.Size(110, 17);
            this.flg28.TabIndex = 27;
            this.flg28.Text = "nosummonout";
            this.flg28.UseVisualStyleBackColor = true;
            // 
            // flg27
            // 
            this.flg27.Location = new System.Drawing.Point(8, 65);
            this.flg27.Name = "flg27";
            this.flg27.Size = new System.Drawing.Size(110, 17);
            this.flg27.TabIndex = 26;
            this.flg27.Text = "nosummonin";
            this.flg27.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.SeaShell;
            this.groupBox3.Controls.Add(this.flg21);
            this.groupBox3.Controls.Add(this.flg20);
            this.groupBox3.Controls.Add(this.flg22);
            this.groupBox3.Controls.Add(this.flg10);
            this.groupBox3.Controls.Add(this.flg15);
            this.groupBox3.Controls.Add(this.flg1);
            this.groupBox3.Controls.Add(this.flg14);
            this.groupBox3.Controls.Add(this.flg25);
            this.groupBox3.Controls.Add(this.flg2);
            this.groupBox3.Controls.Add(this.flg4);
            this.groupBox3.Controls.Add(this.flg7);
            this.groupBox3.Controls.Add(this.flg5);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(132, 238);
            this.groupBox3.TabIndex = 87;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Gameplay";
            // 
            // flg21
            // 
            this.flg21.Location = new System.Drawing.Point(9, 204);
            this.flg21.Name = "flg21";
            this.flg21.Size = new System.Drawing.Size(110, 17);
            this.flg21.TabIndex = 24;
            this.flg21.Text = "extra regain";
            this.flg21.UseVisualStyleBackColor = true;
            // 
            // flg20
            // 
            this.flg20.Location = new System.Drawing.Point(9, 187);
            this.flg20.Name = "flg20";
            this.flg20.Size = new System.Drawing.Size(110, 17);
            this.flg20.TabIndex = 23;
            this.flg20.Text = "battleground";
            this.flg20.UseVisualStyleBackColor = true;
            // 
            // flg22
            // 
            this.flg22.Location = new System.Drawing.Point(9, 170);
            this.flg22.Name = "flg22";
            this.flg22.Size = new System.Drawing.Size(110, 17);
            this.flg22.TabIndex = 22;
            this.flg22.Text = "warzone";
            this.flg22.UseVisualStyleBackColor = true;
            // 
            // flg10
            // 
            this.flg10.Location = new System.Drawing.Point(9, 119);
            this.flg10.Name = "flg10";
            this.flg10.Size = new System.Drawing.Size(110, 17);
            this.flg10.TabIndex = 19;
            this.flg10.Text = "nosound";
            this.flg10.UseVisualStyleBackColor = true;
            // 
            // flg15
            // 
            this.flg15.Location = new System.Drawing.Point(9, 102);
            this.flg15.Name = "flg15";
            this.flg15.Size = new System.Drawing.Size(110, 17);
            this.flg15.TabIndex = 18;
            this.flg15.Text = "nomind";
            this.flg15.UseVisualStyleBackColor = true;
            // 
            // flg1
            // 
            this.flg1.Location = new System.Drawing.Point(9, 153);
            this.flg1.Name = "flg1";
            this.flg1.Size = new System.Drawing.Size(110, 17);
            this.flg1.TabIndex = 21;
            this.flg1.Text = "death";
            this.flg1.UseVisualStyleBackColor = true;
            // 
            // flg14
            // 
            this.flg14.Location = new System.Drawing.Point(9, 136);
            this.flg14.Name = "flg14";
            this.flg14.Size = new System.Drawing.Size(110, 17);
            this.flg14.TabIndex = 20;
            this.flg14.Text = "notrack";
            this.flg14.UseVisualStyleBackColor = true;
            // 
            // flg25
            // 
            this.flg25.Location = new System.Drawing.Point(9, 51);
            this.flg25.Name = "flg25";
            this.flg25.Size = new System.Drawing.Size(110, 17);
            this.flg25.TabIndex = 15;
            this.flg25.Text = "noregain";
            this.flg25.UseVisualStyleBackColor = true;
            // 
            // flg2
            // 
            this.flg2.Location = new System.Drawing.Point(9, 34);
            this.flg2.Name = "flg2";
            this.flg2.Size = new System.Drawing.Size(110, 17);
            this.flg2.TabIndex = 14;
            this.flg2.Text = "nomob";
            this.flg2.UseVisualStyleBackColor = true;
            // 
            // flg4
            // 
            this.flg4.Location = new System.Drawing.Point(9, 17);
            this.flg4.Name = "flg4";
            this.flg4.Size = new System.Drawing.Size(110, 17);
            this.flg4.TabIndex = 13;
            this.flg4.Text = "peaceful";
            this.flg4.UseVisualStyleBackColor = true;
            // 
            // flg7
            // 
            this.flg7.Location = new System.Drawing.Point(9, 85);
            this.flg7.Name = "flg7";
            this.flg7.Size = new System.Drawing.Size(110, 17);
            this.flg7.TabIndex = 17;
            this.flg7.Text = "nomagic";
            this.flg7.UseVisualStyleBackColor = true;
            // 
            // flg5
            // 
            this.flg5.Location = new System.Drawing.Point(9, 68);
            this.flg5.Name = "flg5";
            this.flg5.Size = new System.Drawing.Size(110, 17);
            this.flg5.TabIndex = 16;
            this.flg5.Text = "nosteal";
            this.flg5.UseVisualStyleBackColor = true;
            // 
            // tab_flags1
            // 
            this.tab_flags1.BackColor = System.Drawing.SystemColors.Control;
            this.tab_flags1.Controls.Add(this.flg13);
            this.tab_flags1.Controls.Add(this.flg12);
            this.tab_flags1.Controls.Add(this.groupBox4);
            this.tab_flags1.Controls.Add(this.groupBox6);
            this.tab_flags1.Controls.Add(this.groupBox2);
            this.tab_flags1.Location = new System.Drawing.Point(4, 22);
            this.tab_flags1.Name = "tab_flags1";
            this.tab_flags1.Padding = new System.Windows.Forms.Padding(3);
            this.tab_flags1.Size = new System.Drawing.Size(144, 500);
            this.tab_flags1.TabIndex = 1;
            this.tab_flags1.Text = "Proprietà";
            // 
            // flg13
            // 
            this.flg13.Location = new System.Drawing.Point(15, 377);
            this.flg13.Name = "flg13";
            this.flg13.Size = new System.Drawing.Size(110, 17);
            this.flg13.TabIndex = 26;
            this.flg13.Text = "saveroom";
            this.flg13.UseVisualStyleBackColor = true;
            this.flg13.CheckedChanged += new System.EventHandler(this.flg13_CheckedChanged);
            // 
            // flg12
            // 
            this.flg12.Location = new System.Drawing.Point(15, 359);
            this.flg12.Name = "flg12";
            this.flg12.Size = new System.Drawing.Size(110, 17);
            this.flg12.TabIndex = 25;
            this.flg12.Text = "nodeath";
            this.flg12.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.MintCream;
            this.groupBox4.Controls.Add(this.flg11);
            this.groupBox4.Controls.Add(this.flg9);
            this.groupBox4.Controls.Add(this.flg8);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(132, 93);
            this.groupBox4.TabIndex = 90;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Dimensioni";
            // 
            // flg11
            // 
            this.flg11.Location = new System.Drawing.Point(9, 53);
            this.flg11.Name = "flg11";
            this.flg11.Size = new System.Drawing.Size(110, 17);
            this.flg11.TabIndex = 15;
            this.flg11.Text = "large";
            this.flg11.UseVisualStyleBackColor = true;
            // 
            // flg9
            // 
            this.flg9.Location = new System.Drawing.Point(9, 36);
            this.flg9.Name = "flg9";
            this.flg9.Size = new System.Drawing.Size(110, 17);
            this.flg9.TabIndex = 14;
            this.flg9.Text = "private";
            this.flg9.UseVisualStyleBackColor = true;
            // 
            // flg8
            // 
            this.flg8.Location = new System.Drawing.Point(9, 19);
            this.flg8.Name = "flg8";
            this.flg8.Size = new System.Drawing.Size(110, 17);
            this.flg8.TabIndex = 13;
            this.flg8.Text = "tunnel";
            this.flg8.UseVisualStyleBackColor = true;
            this.flg8.Click += new System.EventHandler(this.flg13_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.Lavender;
            this.groupBox6.Controls.Add(this.flg23);
            this.groupBox6.Controls.Add(this.flg0);
            this.groupBox6.Location = new System.Drawing.Point(6, 263);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(132, 77);
            this.groupBox6.TabIndex = 89;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Luce";
            // 
            // flg23
            // 
            this.flg23.Location = new System.Drawing.Point(9, 36);
            this.flg23.Name = "flg23";
            this.flg23.Size = new System.Drawing.Size(110, 17);
            this.flg23.TabIndex = 24;
            this.flg23.Text = "bright";
            this.flg23.UseVisualStyleBackColor = true;
            // 
            // flg0
            // 
            this.flg0.Location = new System.Drawing.Point(9, 19);
            this.flg0.Name = "flg0";
            this.flg0.Size = new System.Drawing.Size(110, 17);
            this.flg0.TabIndex = 23;
            this.flg0.Text = "dark";
            this.flg0.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.AliceBlue;
            this.groupBox2.Controls.Add(this.flg17);
            this.groupBox2.Controls.Add(this.flg16);
            this.groupBox2.Controls.Add(this.flg3);
            this.groupBox2.Controls.Add(this.flg19);
            this.groupBox2.Controls.Add(this.flg18);
            this.groupBox2.Location = new System.Drawing.Point(6, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(132, 152);
            this.groupBox2.TabIndex = 88;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settore";
            // 
            // flg17
            // 
            this.flg17.Location = new System.Drawing.Point(9, 52);
            this.flg17.Name = "flg17";
            this.flg17.Size = new System.Drawing.Size(110, 17);
            this.flg17.TabIndex = 18;
            this.flg17.Text = "artic";
            this.flg17.UseVisualStyleBackColor = true;
            // 
            // flg16
            // 
            this.flg16.Location = new System.Drawing.Point(9, 35);
            this.flg16.Name = "flg16";
            this.flg16.Size = new System.Drawing.Size(110, 17);
            this.flg16.TabIndex = 17;
            this.flg16.Text = "luminous";
            this.flg16.UseVisualStyleBackColor = true;
            // 
            // flg3
            // 
            this.flg3.Location = new System.Drawing.Point(9, 18);
            this.flg3.Name = "flg3";
            this.flg3.Size = new System.Drawing.Size(110, 17);
            this.flg3.TabIndex = 16;
            this.flg3.Text = "inside";
            this.flg3.UseVisualStyleBackColor = true;
            // 
            // flg19
            // 
            this.flg19.Location = new System.Drawing.Point(9, 86);
            this.flg19.Name = "flg19";
            this.flg19.Size = new System.Drawing.Size(110, 17);
            this.flg19.TabIndex = 20;
            this.flg19.Text = "unreachable";
            this.flg19.UseVisualStyleBackColor = true;
            // 
            // flg18
            // 
            this.flg18.Location = new System.Drawing.Point(9, 69);
            this.flg18.Name = "flg18";
            this.flg18.Size = new System.Drawing.Size(110, 17);
            this.flg18.TabIndex = 19;
            this.flg18.Text = "underground";
            this.flg18.UseVisualStyleBackColor = true;
            // 
            // spin_area
            // 
            this.spin_area.Location = new System.Drawing.Point(158, 8);
            this.spin_area.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.spin_area.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.spin_area.Name = "spin_area";
            this.spin_area.Size = new System.Drawing.Size(47, 20);
            this.spin_area.TabIndex = 108;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(117, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 109;
            this.label1.Text = "Area: ";
            // 
            // frm_Room
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 676);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.spin_area);
            this.Controls.Add(this.tabcontrol_flags);
            this.Controls.Add(this.spin_maxpc);
            this.Controls.Add(this.lbl_maxpc);
            this.Controls.Add(this.spin_maxobj);
            this.Controls.Add(this.pn_watercurrent);
            this.Controls.Add(this.lbl_maxobj);
            this.Controls.Add(this.gaugecharcounter);
            this.Controls.Add(this.pn_teleport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.list_extradesc);
            this.Controls.Add(this.combo_sect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edt_shortdesc);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(800, 626);
            this.Name = "frm_Room";
            this.Text = "Room Editor";
            this.Controls.SetChildIndex(this.btnok, 0);
            this.Controls.SetChildIndex(this.btncancel, 0);
            this.Controls.SetChildIndex(this.btnnext, 0);
            this.Controls.SetChildIndex(this.btnprev, 0);
            this.Controls.SetChildIndex(this.btnrestore, 0);
            this.Controls.SetChildIndex(this.btnapply, 0);
            this.Controls.SetChildIndex(this.lblvnum, 0);
            this.Controls.SetChildIndex(this.spin_vnum, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.edt_shortdesc, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.combo_sect, 0);
            this.Controls.SetChildIndex(this.list_extradesc, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.pn_teleport, 0);
            this.Controls.SetChildIndex(this.gaugecharcounter, 0);
            this.Controls.SetChildIndex(this.lbl_maxobj, 0);
            this.Controls.SetChildIndex(this.pn_watercurrent, 0);
            this.Controls.SetChildIndex(this.spin_maxobj, 0);
            this.Controls.SetChildIndex(this.lbl_maxpc, 0);
            this.Controls.SetChildIndex(this.spin_maxpc, 0);
            this.Controls.SetChildIndex(this.tabcontrol_flags, 0);
            this.Controls.SetChildIndex(this.spin_area, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.spin_vnum)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.page_longdesc.ResumeLayout(false);
            this.page_daydesc.ResumeLayout(false);
            this.page_nightdesc.ResumeLayout(false);
            this.page_extradesc.ResumeLayout(false);
            this.page_extradesc.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.pn_teleport.ResumeLayout(false);
            this.pn_teleport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edt_tel_toroom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt_tel_counter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt_tel_time)).EndInit();
            this.pn_watercurrent.ResumeLayout(false);
            this.pn_watercurrent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spin_watercurrent_vel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_maxpc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spin_maxobj)).EndInit();
            this.tabcontrol_flags.ResumeLayout(false);
            this.tab_flags0.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tab_flags1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spin_area)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage page_longdesc;
        private System.Windows.Forms.TabPage page_daydesc;
        private MudlikeRichTextBox edt_shortdesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combo_sect;
        private System.Windows.Forms.TabPage page_nightdesc;
        private System.Windows.Forms.TabPage page_extradesc;
        private System.Windows.Forms.Button btn_extradescDel;
        private System.Windows.Forms.Button btn_extradescAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox edt_extradesc;
        private System.Windows.Forms.ListBox list_extradesc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private HoGListView list_exits;
        private System.Windows.Forms.Button btn_editexit;
        private System.Windows.Forms.Button btn_delexit;
        private System.Windows.Forms.Button btn_addexit;
        private System.Windows.Forms.Panel pn_teleport;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown edt_tel_toroom;
        private System.Windows.Forms.NumericUpDown edt_tel_counter;
        private System.Windows.Forms.ComboBox combo_tel_sect;
        private System.Windows.Forms.NumericUpDown edt_tel_time;
        private System.Windows.Forms.CheckBox chk_tel_flag4;
        private System.Windows.Forms.CheckBox chk_tel_flag3;
        private System.Windows.Forms.CheckBox chk_tel_flag1;
        private System.Windows.Forms.CheckBox chk_tel_flag2;
        private System.Windows.Forms.CheckBox chk_tel_flag0;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_followexit;
        private MudlikeRichTextBox memo_longdesc;
        private MudlikeRichTextBox memo_extradesc;
        private MudlikeRichTextBox memo_daydesc;
        private MudlikeRichTextBox memo_nightdesc;
        private CharCounterProgressBar gaugecharcounter;
        private System.Windows.Forms.Panel pn_watercurrent;
        private System.Windows.Forms.NumericUpDown spin_watercurrent_vel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox combo_watercurrent_dir;
        private System.Windows.Forms.NumericUpDown spin_maxpc;
        private System.Windows.Forms.Label lbl_maxpc;
        private System.Windows.Forms.NumericUpDown spin_maxobj;
        private System.Windows.Forms.Label lbl_maxobj;
        private System.Windows.Forms.TabControl tabcontrol_flags;
        private System.Windows.Forms.TabPage tab_flags0;
        private System.Windows.Forms.TabPage tab_flags1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox flg6;
        private System.Windows.Forms.CheckBox flg26;
        private System.Windows.Forms.CheckBox flg24;
        private System.Windows.Forms.CheckBox flg28;
        private System.Windows.Forms.CheckBox flg27;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox flg22;
        private System.Windows.Forms.CheckBox flg10;
        private System.Windows.Forms.CheckBox flg15;
        private System.Windows.Forms.CheckBox flg1;
        private System.Windows.Forms.CheckBox flg14;
        private System.Windows.Forms.CheckBox flg25;
        private System.Windows.Forms.CheckBox flg2;
        private System.Windows.Forms.CheckBox flg4;
        private System.Windows.Forms.CheckBox flg7;
        private System.Windows.Forms.CheckBox flg5;
        private System.Windows.Forms.CheckBox flg13;
        private System.Windows.Forms.CheckBox flg12;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox flg11;
        private System.Windows.Forms.CheckBox flg9;
        private System.Windows.Forms.CheckBox flg8;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox flg23;
        private System.Windows.Forms.CheckBox flg0;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox flg17;
        private System.Windows.Forms.CheckBox flg16;
        private System.Windows.Forms.CheckBox flg3;
        private System.Windows.Forms.CheckBox flg19;
        private System.Windows.Forms.CheckBox flg18;
        private System.Windows.Forms.NumericUpDown spin_area;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btndoorinit;
        private System.Windows.Forms.CheckBox flg21;
        private System.Windows.Forms.CheckBox flg20;
    }
}