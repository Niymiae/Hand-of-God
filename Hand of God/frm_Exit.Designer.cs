namespace HandofGod
{
    partial class frm_Exit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.edt_to = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.edt_from = new System.Windows.Forms.TextBox();
            this.edt_inverse = new System.Windows.Forms.TextBox();
            this.edt_nameinlist = new System.Windows.Forms.TextBox();
            this.edt_name = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gaugecharcounter = new HandofGod.CharCounterProgressBar();
            this.memo_desc = new HandofGod.MudlikeRichTextBox();
            this.lblinverse = new System.Windows.Forms.Label();
            this.btn_editkey = new System.Windows.Forms.Button();
            this.btneditroom = new System.Windows.Forms.Button();
            this.btninverse = new System.Windows.Forms.Button();
            this.lbl_roomshort = new System.Windows.Forms.Label();
            this.edt_toroom = new System.Windows.Forms.NumericUpDown();
            this.lbl_keyshort = new System.Windows.Forms.Label();
            this.edt_doorobjkey = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.btnfindkey = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btnfindroom = new System.Windows.Forms.Button();
            this.combo_doorstatus = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.combo_dooract = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.list_flags = new System.Windows.Forms.CheckedListBox();
            this.edt_doorkeys = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.combo_type = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnrestore = new System.Windows.Forms.Button();
            this.btnapply = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edt_toroom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt_doorobjkey)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.edt_to);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.edt_from);
            this.groupBox1.Controls.Add(this.edt_inverse);
            this.groupBox1.Controls.Add(this.edt_nameinlist);
            this.groupBox1.Controls.Add(this.edt_name);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(542, 90);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Uscita Speciale";
            // 
            // edt_to
            // 
            this.edt_to.Location = new System.Drawing.Point(318, 39);
            this.edt_to.Name = "edt_to";
            this.edt_to.Size = new System.Drawing.Size(151, 20);
            this.edt_to.TabIndex = 15;
            this.edt_to.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(278, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Verso: ";
            // 
            // edt_from
            // 
            this.edt_from.Location = new System.Drawing.Point(318, 13);
            this.edt_from.Name = "edt_from";
            this.edt_from.Size = new System.Drawing.Size(151, 20);
            this.edt_from.TabIndex = 14;
            this.edt_from.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // edt_inverse
            // 
            this.edt_inverse.Location = new System.Drawing.Point(121, 65);
            this.edt_inverse.Name = "edt_inverse";
            this.edt_inverse.Size = new System.Drawing.Size(151, 20);
            this.edt_inverse.TabIndex = 13;
            this.edt_inverse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // edt_nameinlist
            // 
            this.edt_nameinlist.Location = new System.Drawing.Point(121, 39);
            this.edt_nameinlist.Name = "edt_nameinlist";
            this.edt_nameinlist.Size = new System.Drawing.Size(151, 20);
            this.edt_nameinlist.TabIndex = 12;
            this.edt_nameinlist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // edt_name
            // 
            this.edt_name.Location = new System.Drawing.Point(121, 13);
            this.edt_name.Name = "edt_name";
            this.edt_name.Size = new System.Drawing.Size(151, 20);
            this.edt_name.TabIndex = 11;
            this.edt_name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Nome uscita inversa: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(278, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Da: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nome in lista: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nome (Univoco): ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gaugecharcounter);
            this.groupBox2.Controls.Add(this.memo_desc);
            this.groupBox2.Controls.Add(this.lblinverse);
            this.groupBox2.Controls.Add(this.btn_editkey);
            this.groupBox2.Controls.Add(this.btneditroom);
            this.groupBox2.Controls.Add(this.btninverse);
            this.groupBox2.Controls.Add(this.lbl_roomshort);
            this.groupBox2.Controls.Add(this.edt_toroom);
            this.groupBox2.Controls.Add(this.lbl_keyshort);
            this.groupBox2.Controls.Add(this.edt_doorobjkey);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.btnfindkey);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.btnfindroom);
            this.groupBox2.Controls.Add(this.combo_doorstatus);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.combo_dooract);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.list_flags);
            this.groupBox2.Controls.Add(this.edt_doorkeys);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(542, 292);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Attributi";
            // 
            // gaugecharcounter
            // 
            this.gaugecharcounter.Location = new System.Drawing.Point(6, 263);
            this.gaugecharcounter.Name = "gaugecharcounter";
            this.gaugecharcounter.Size = new System.Drawing.Size(425, 14);
            this.gaugecharcounter.TabIndex = 39;
            // 
            // memo_desc
            // 
            this.memo_desc.DefaultCharacterColor = System.Drawing.Color.LightGray;
            this.memo_desc.DetectUrls = false;
            this.memo_desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.memo_desc.Location = new System.Drawing.Point(9, 148);
            this.memo_desc.Name = "memo_desc";
            this.memo_desc.Size = new System.Drawing.Size(425, 118);
            this.memo_desc.TabIndex = 38;
            this.memo_desc.Text = "";
            // 
            // lblinverse
            // 
            this.lblinverse.AutoSize = true;
            this.lblinverse.Location = new System.Drawing.Point(378, 17);
            this.lblinverse.Name = "lblinverse";
            this.lblinverse.Size = new System.Drawing.Size(127, 13);
            this.lblinverse.TabIndex = 37;
            this.lblinverse.Text = "Uscita Inversa Impostata.";
            this.lblinverse.Visible = false;
            // 
            // btn_editkey
            // 
            this.btn_editkey.Location = new System.Drawing.Point(197, 110);
            this.btn_editkey.Name = "btn_editkey";
            this.btn_editkey.Size = new System.Drawing.Size(45, 23);
            this.btn_editkey.TabIndex = 32;
            this.btn_editkey.TabStop = false;
            this.btn_editkey.Text = "Edit";
            this.btn_editkey.UseVisualStyleBackColor = true;
            this.btn_editkey.Click += new System.EventHandler(this.button8_Click);
            // 
            // btneditroom
            // 
            this.btneditroom.Location = new System.Drawing.Point(197, 35);
            this.btneditroom.Name = "btneditroom";
            this.btneditroom.Size = new System.Drawing.Size(45, 23);
            this.btneditroom.TabIndex = 35;
            this.btneditroom.TabStop = false;
            this.btneditroom.Text = "Edit";
            this.btneditroom.UseVisualStyleBackColor = true;
            this.btneditroom.Click += new System.EventHandler(this.button8_Click);
            // 
            // btninverse
            // 
            this.btninverse.Location = new System.Drawing.Point(248, 12);
            this.btninverse.Name = "btninverse";
            this.btninverse.Size = new System.Drawing.Size(124, 23);
            this.btninverse.TabIndex = 26;
            this.btninverse.Text = "Imposta Uscita Inversa";
            this.btninverse.UseVisualStyleBackColor = true;
            this.btninverse.Click += new System.EventHandler(this.btninverse_Click);
            // 
            // lbl_roomshort
            // 
            this.lbl_roomshort.AutoSize = true;
            this.lbl_roomshort.Location = new System.Drawing.Point(35, 35);
            this.lbl_roomshort.Name = "lbl_roomshort";
            this.lbl_roomshort.Size = new System.Drawing.Size(10, 13);
            this.lbl_roomshort.TabIndex = 36;
            this.lbl_roomshort.Text = " ";
            // 
            // edt_toroom
            // 
            this.edt_toroom.Location = new System.Drawing.Point(83, 15);
            this.edt_toroom.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.edt_toroom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.edt_toroom.Name = "edt_toroom";
            this.edt_toroom.Size = new System.Drawing.Size(108, 20);
            this.edt_toroom.TabIndex = 33;
            this.edt_toroom.ValueChanged += new System.EventHandler(this.UpdateButtonsState);
            // 
            // lbl_keyshort
            // 
            this.lbl_keyshort.AutoSize = true;
            this.lbl_keyshort.Location = new System.Drawing.Point(35, 115);
            this.lbl_keyshort.Name = "lbl_keyshort";
            this.lbl_keyshort.Size = new System.Drawing.Size(10, 13);
            this.lbl_keyshort.TabIndex = 31;
            this.lbl_keyshort.Text = " ";
            // 
            // edt_doorobjkey
            // 
            this.edt_doorobjkey.Location = new System.Drawing.Point(83, 90);
            this.edt_doorobjkey.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.edt_doorobjkey.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.edt_doorobjkey.Name = "edt_doorobjkey";
            this.edt_doorobjkey.Size = new System.Drawing.Size(108, 20);
            this.edt_doorobjkey.TabIndex = 30;
            this.edt_doorobjkey.ValueChanged += new System.EventHandler(this.UpdateButtonsState);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 132);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "Descrizione: ";
            // 
            // btnfindkey
            // 
            this.btnfindkey.Location = new System.Drawing.Point(197, 88);
            this.btnfindkey.Name = "btnfindkey";
            this.btnfindkey.Size = new System.Drawing.Size(45, 23);
            this.btnfindkey.TabIndex = 27;
            this.btnfindkey.TabStop = false;
            this.btnfindkey.Text = "Trova";
            this.btnfindkey.UseVisualStyleBackColor = true;
            this.btnfindkey.Click += new System.EventHandler(this.button2_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 91);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Obj chiave: #";
            // 
            // btnfindroom
            // 
            this.btnfindroom.Location = new System.Drawing.Point(197, 12);
            this.btnfindroom.Name = "btnfindroom";
            this.btnfindroom.Size = new System.Drawing.Size(45, 23);
            this.btnfindroom.TabIndex = 24;
            this.btnfindroom.TabStop = false;
            this.btnfindroom.Text = "Trova";
            this.btnfindroom.UseVisualStyleBackColor = true;
            this.btnfindroom.Click += new System.EventHandler(this.button1_Click);
            // 
            // combo_doorstatus
            // 
            this.combo_doorstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_doorstatus.FormattingEnabled = true;
            this.combo_doorstatus.Items.AddRange(new object[] {
            "Aperta",
            "Chiusa",
            "Chiusa a chiave"});
            this.combo_doorstatus.Location = new System.Drawing.Point(327, 92);
            this.combo_doorstatus.Name = "combo_doorstatus";
            this.combo_doorstatus.Size = new System.Drawing.Size(108, 21);
            this.combo_doorstatus.TabIndex = 20;
            this.combo_doorstatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(249, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Inizializzazione: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(263, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Comando: ";
            // 
            // combo_dooract
            // 
            this.combo_dooract.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_dooract.FormattingEnabled = true;
            this.combo_dooract.Items.AddRange(new object[] {
            "cmd_pull ",
            "cmd_twist",
            "cmd_turn",
            "cmd_lift  ",
            "cmd_push",
            "cmd_dig  ",
            "cmd_cut  "});
            this.combo_dooract.Location = new System.Drawing.Point(327, 64);
            this.combo_dooract.Name = "combo_dooract";
            this.combo_dooract.Size = new System.Drawing.Size(107, 21);
            this.combo_dooract.TabIndex = 19;
            this.combo_dooract.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(437, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Flags:";
            // 
            // list_flags
            // 
            this.list_flags.CheckOnClick = true;
            this.list_flags.FormattingEnabled = true;
            this.list_flags.Items.AddRange(new object[] {
            "df_door       ",
            "df_closed     ",
            "df_locked     ",
            "df_secret     ",
            "df_nobash     ",
            "df_nopick     ",
            "df_climb      ",
            "df_male       ",
            "df_nolook     ",
            "df_noknock    ",
            "df_invisible  "});
            this.list_flags.Location = new System.Drawing.Point(440, 108);
            this.list_flags.Name = "list_flags";
            this.list_flags.Size = new System.Drawing.Size(97, 184);
            this.list_flags.TabIndex = 21;
            this.list_flags.SelectedIndexChanged += new System.EventHandler(this.list_flags_SelectedIndexChanged);
            this.list_flags.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // edt_doorkeys
            // 
            this.edt_doorkeys.Location = new System.Drawing.Point(83, 64);
            this.edt_doorkeys.Name = "edt_doorkeys";
            this.edt_doorkeys.Size = new System.Drawing.Size(159, 20);
            this.edt_doorkeys.TabIndex = 17;
            this.edt_doorkeys.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Alla stanza: #";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Parole chiave:  ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.combo_type);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 28);
            this.panel1.TabIndex = 16;
            // 
            // combo_type
            // 
            this.combo_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_type.FormattingEnabled = true;
            this.combo_type.Items.AddRange(new object[] {
            "Nord",
            "Est",
            "Sud",
            "Ovest",
            "Alto",
            "Basso",
            "Speciale"});
            this.combo_type.Location = new System.Drawing.Point(51, 4);
            this.combo_type.Name = "combo_type";
            this.combo_type.Size = new System.Drawing.Size(121, 21);
            this.combo_type.TabIndex = 0;
            this.combo_type.SelectedIndexChanged += new System.EventHandler(this.UpdateButtonsState);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tipo: ";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 416);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "Ok";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(90, 416);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 18;
            this.button4.Text = "Cancel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnrestore
            // 
            this.btnrestore.Location = new System.Drawing.Point(455, 416);
            this.btnrestore.Name = "btnrestore";
            this.btnrestore.Size = new System.Drawing.Size(75, 23);
            this.btnrestore.TabIndex = 19;
            this.btnrestore.Text = "Ripristina";
            this.btnrestore.UseVisualStyleBackColor = true;
            this.btnrestore.Click += new System.EventHandler(this.btnrestore_Click);
            // 
            // btnapply
            // 
            this.btnapply.Location = new System.Drawing.Point(374, 416);
            this.btnapply.Name = "btnapply";
            this.btnapply.Size = new System.Drawing.Size(75, 23);
            this.btnapply.TabIndex = 20;
            this.btnapply.Text = "Applica";
            this.btnapply.UseVisualStyleBackColor = true;
            this.btnapply.Click += new System.EventHandler(this.btnapply_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frm_Exit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 444);
            this.Controls.Add(this.btnapply);
            this.Controls.Add(this.btnrestore);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frm_Exit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modifica Uscita";
            this.Shown += new System.EventHandler(this.frm_Exit_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edt_toroom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edt_doorobjkey)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox edt_to;
        private System.Windows.Forms.TextBox edt_from;
        private System.Windows.Forms.TextBox edt_inverse;
        private System.Windows.Forms.TextBox edt_nameinlist;
        private System.Windows.Forms.TextBox edt_name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox edt_doorkeys;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox combo_type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox list_flags;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox combo_dooract;
        private System.Windows.Forms.Button btnfindkey;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnfindroom;
        private System.Windows.Forms.ComboBox combo_doorstatus;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.NumericUpDown edt_doorobjkey;
        private System.Windows.Forms.Label lbl_keyshort;
        private System.Windows.Forms.Button btn_editkey;
        private System.Windows.Forms.NumericUpDown edt_toroom;
        private System.Windows.Forms.Button btninverse;
        private System.Windows.Forms.Label lbl_roomshort;
        private System.Windows.Forms.Button btneditroom;
        private System.Windows.Forms.Button btnrestore;
        private System.Windows.Forms.Button btnapply;
        private System.Windows.Forms.Label lblinverse;
        private System.Windows.Forms.Timer timer1;
        private CharCounterProgressBar gaugecharcounter;
        private MudlikeRichTextBox memo_desc;
    }
}