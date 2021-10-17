namespace HandofGod
{
    partial class dlg_setdirs
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_dirs = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.edt5 = new System.Windows.Forms.TextBox();
            this.btn5 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.edt4 = new System.Windows.Forms.TextBox();
            this.btn4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.edt3 = new System.Windows.Forms.TextBox();
            this.btn3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.edt2 = new System.Windows.Forms.TextBox();
            this.btn2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.edt1 = new System.Windows.Forms.TextBox();
            this.btn1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.edt0 = new System.Windows.Forms.TextBox();
            this.btn0 = new System.Windows.Forms.Button();
            this.tab_general = new System.Windows.Forms.TabPage();
            this.chk_delete_vnum_refs = new System.Windows.Forms.CheckBox();
            this.chk_updatevnums = new System.Windows.Forms.CheckBox();
            this.chk_initdopenoors = new System.Windows.Forms.CheckBox();
            this.btnfontdesc_def = new System.Windows.Forms.Button();
            this.fontdesc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnfontdesc = new System.Windows.Forms.Button();
            this.chk_samedir = new System.Windows.Forms.CheckBox();
            this.chk_delconfirm = new System.Windows.Forms.CheckBox();
            this.chk_saveconfirm = new System.Windows.Forms.CheckBox();
            this.chk_autoload = new System.Windows.Forms.CheckBox();
            this.tab_advanced = new System.Windows.Forms.TabPage();
            this.chk_format_helpzon = new System.Windows.Forms.CheckBox();
            this.chk_dontsavehogfile = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radio_fileformat1 = new System.Windows.Forms.RadioButton();
            this.radio_fileformat0 = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tab_dirs.SuspendLayout();
            this.tab_general.SuspendLayout();
            this.tab_advanced.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 294);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 294);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_dirs);
            this.tabControl1.Controls.Add(this.tab_general);
            this.tabControl1.Controls.Add(this.tab_advanced);
            this.tabControl1.Location = new System.Drawing.Point(12, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(305, 285);
            this.tabControl1.TabIndex = 2;
            // 
            // tab_dirs
            // 
            this.tab_dirs.BackColor = System.Drawing.SystemColors.Control;
            this.tab_dirs.Controls.Add(this.label6);
            this.tab_dirs.Controls.Add(this.edt5);
            this.tab_dirs.Controls.Add(this.btn5);
            this.tab_dirs.Controls.Add(this.label5);
            this.tab_dirs.Controls.Add(this.edt4);
            this.tab_dirs.Controls.Add(this.btn4);
            this.tab_dirs.Controls.Add(this.label4);
            this.tab_dirs.Controls.Add(this.edt3);
            this.tab_dirs.Controls.Add(this.btn3);
            this.tab_dirs.Controls.Add(this.label3);
            this.tab_dirs.Controls.Add(this.edt2);
            this.tab_dirs.Controls.Add(this.btn2);
            this.tab_dirs.Controls.Add(this.label2);
            this.tab_dirs.Controls.Add(this.edt1);
            this.tab_dirs.Controls.Add(this.btn1);
            this.tab_dirs.Controls.Add(this.label1);
            this.tab_dirs.Controls.Add(this.edt0);
            this.tab_dirs.Controls.Add(this.btn0);
            this.tab_dirs.Location = new System.Drawing.Point(4, 22);
            this.tab_dirs.Name = "tab_dirs";
            this.tab_dirs.Padding = new System.Windows.Forms.Padding(3);
            this.tab_dirs.Size = new System.Drawing.Size(297, 259);
            this.tab_dirs.TabIndex = 0;
            this.tab_dirs.Text = "Directory";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Dati visivi di Hand of God:";
            // 
            // edt5
            // 
            this.edt5.Location = new System.Drawing.Point(13, 222);
            this.edt5.Name = "edt5";
            this.edt5.Size = new System.Drawing.Size(241, 20);
            this.edt5.TabIndex = 36;
            // 
            // btn5
            // 
            this.btn5.Location = new System.Drawing.Point(260, 220);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(26, 23);
            this.btn5.TabIndex = 35;
            this.btn5.Text = "...";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.btn0_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "Negozi: ";
            // 
            // edt4
            // 
            this.edt4.Location = new System.Drawing.Point(13, 183);
            this.edt4.Name = "edt4";
            this.edt4.Size = new System.Drawing.Size(241, 20);
            this.edt4.TabIndex = 33;
            // 
            // btn4
            // 
            this.btn4.Location = new System.Drawing.Point(260, 181);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(26, 23);
            this.btn4.TabIndex = 32;
            this.btn4.Text = "...";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.btn0_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Oggetti: ";
            // 
            // edt3
            // 
            this.edt3.Location = new System.Drawing.Point(13, 144);
            this.edt3.Name = "edt3";
            this.edt3.Size = new System.Drawing.Size(241, 20);
            this.edt3.TabIndex = 30;
            // 
            // btn3
            // 
            this.btn3.Location = new System.Drawing.Point(260, 142);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(26, 23);
            this.btn3.TabIndex = 29;
            this.btn3.Text = "...";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btn0_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Mobs: ";
            // 
            // edt2
            // 
            this.edt2.Location = new System.Drawing.Point(13, 105);
            this.edt2.Name = "edt2";
            this.edt2.Size = new System.Drawing.Size(241, 20);
            this.edt2.TabIndex = 27;
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(260, 103);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(26, 23);
            this.btn2.TabIndex = 26;
            this.btn2.Text = "...";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn0_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Stanze: ";
            // 
            // edt1
            // 
            this.edt1.Location = new System.Drawing.Point(13, 66);
            this.edt1.Name = "edt1";
            this.edt1.Size = new System.Drawing.Size(241, 20);
            this.edt1.TabIndex = 24;
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(260, 64);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(26, 23);
            this.btn1.TabIndex = 23;
            this.btn1.Text = "...";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn0_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Zone: ";
            // 
            // edt0
            // 
            this.edt0.Location = new System.Drawing.Point(13, 27);
            this.edt0.Name = "edt0";
            this.edt0.Size = new System.Drawing.Size(241, 20);
            this.edt0.TabIndex = 21;
            // 
            // btn0
            // 
            this.btn0.Location = new System.Drawing.Point(260, 25);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(26, 23);
            this.btn0.TabIndex = 20;
            this.btn0.Text = "...";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.btn0_Click);
            // 
            // tab_general
            // 
            this.tab_general.BackColor = System.Drawing.SystemColors.Control;
            this.tab_general.Controls.Add(this.chk_delete_vnum_refs);
            this.tab_general.Controls.Add(this.chk_updatevnums);
            this.tab_general.Controls.Add(this.chk_initdopenoors);
            this.tab_general.Controls.Add(this.btnfontdesc_def);
            this.tab_general.Controls.Add(this.fontdesc);
            this.tab_general.Controls.Add(this.label7);
            this.tab_general.Controls.Add(this.btnfontdesc);
            this.tab_general.Controls.Add(this.chk_samedir);
            this.tab_general.Controls.Add(this.chk_delconfirm);
            this.tab_general.Controls.Add(this.chk_saveconfirm);
            this.tab_general.Controls.Add(this.chk_autoload);
            this.tab_general.Location = new System.Drawing.Point(4, 22);
            this.tab_general.Name = "tab_general";
            this.tab_general.Padding = new System.Windows.Forms.Padding(3);
            this.tab_general.Size = new System.Drawing.Size(297, 259);
            this.tab_general.TabIndex = 1;
            this.tab_general.Text = "Generale";
            // 
            // chk_delete_vnum_refs
            // 
            this.chk_delete_vnum_refs.AutoSize = true;
            this.chk_delete_vnum_refs.Location = new System.Drawing.Point(6, 149);
            this.chk_delete_vnum_refs.Name = "chk_delete_vnum_refs";
            this.chk_delete_vnum_refs.Size = new System.Drawing.Size(233, 17);
            this.chk_delete_vnum_refs.TabIndex = 11;
            this.chk_delete_vnum_refs.Text = "Elimina anche gli init relativi all\'entità rimossa";
            this.chk_delete_vnum_refs.UseVisualStyleBackColor = true;
            // 
            // chk_updatevnums
            // 
            this.chk_updatevnums.AutoSize = true;
            this.chk_updatevnums.Location = new System.Drawing.Point(6, 107);
            this.chk_updatevnums.Name = "chk_updatevnums";
            this.chk_updatevnums.Size = new System.Drawing.Size(252, 17);
            this.chk_updatevnums.TabIndex = 10;
            this.chk_updatevnums.Text = "Conferma l\'aggiornamento dei riferimenti ai vnum";
            this.chk_updatevnums.UseVisualStyleBackColor = true;
            // 
            // chk_initdopenoors
            // 
            this.chk_initdopenoors.AutoSize = true;
            this.chk_initdopenoors.Location = new System.Drawing.Point(6, 128);
            this.chk_initdopenoors.Name = "chk_initdopenoors";
            this.chk_initdopenoors.Size = new System.Drawing.Size(271, 17);
            this.chk_initdopenoors.TabIndex = 8;
            this.chk_initdopenoors.Text = "Crea Init automaticamente anche per le porte aperte";
            this.chk_initdopenoors.UseVisualStyleBackColor = true;
            // 
            // btnfontdesc_def
            // 
            this.btnfontdesc_def.Location = new System.Drawing.Point(237, 189);
            this.btnfontdesc_def.Name = "btnfontdesc_def";
            this.btnfontdesc_def.Size = new System.Drawing.Size(51, 23);
            this.btnfontdesc_def.TabIndex = 7;
            this.btnfontdesc_def.Text = "Default";
            this.btnfontdesc_def.UseVisualStyleBackColor = true;
            this.btnfontdesc_def.Click += new System.EventHandler(this.btnfontdesc_def_Click);
            // 
            // fontdesc
            // 
            this.fontdesc.Location = new System.Drawing.Point(6, 191);
            this.fontdesc.Name = "fontdesc";
            this.fontdesc.ReadOnly = true;
            this.fontdesc.Size = new System.Drawing.Size(195, 20);
            this.fontdesc.TabIndex = 6;
            this.fontdesc.TextChanged += new System.EventHandler(this.fontdesc_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Font delle Descrizioni: ";
            // 
            // btnfontdesc
            // 
            this.btnfontdesc.Location = new System.Drawing.Point(207, 189);
            this.btnfontdesc.Name = "btnfontdesc";
            this.btnfontdesc.Size = new System.Drawing.Size(25, 23);
            this.btnfontdesc.TabIndex = 4;
            this.btnfontdesc.Text = "...";
            this.btnfontdesc.UseVisualStyleBackColor = true;
            this.btnfontdesc.Click += new System.EventHandler(this.btnfontdesc_Click);
            // 
            // chk_samedir
            // 
            this.chk_samedir.AutoSize = true;
            this.chk_samedir.Location = new System.Drawing.Point(6, 44);
            this.chk_samedir.Name = "chk_samedir";
            this.chk_samedir.Size = new System.Drawing.Size(289, 17);
            this.chk_samedir.TabIndex = 3;
            this.chk_samedir.Text = "Cerca i file di un\'area nelle dir. impostate se non presenti";
            this.chk_samedir.UseVisualStyleBackColor = true;
            // 
            // chk_delconfirm
            // 
            this.chk_delconfirm.AutoSize = true;
            this.chk_delconfirm.Location = new System.Drawing.Point(6, 86);
            this.chk_delconfirm.Name = "chk_delconfirm";
            this.chk_delconfirm.Size = new System.Drawing.Size(135, 17);
            this.chk_delconfirm.TabIndex = 2;
            this.chk_delconfirm.Text = "Conferma l\'eliminazione";
            this.chk_delconfirm.UseVisualStyleBackColor = true;
            // 
            // chk_saveconfirm
            // 
            this.chk_saveconfirm.AutoSize = true;
            this.chk_saveconfirm.Location = new System.Drawing.Point(6, 65);
            this.chk_saveconfirm.Name = "chk_saveconfirm";
            this.chk_saveconfirm.Size = new System.Drawing.Size(135, 17);
            this.chk_saveconfirm.TabIndex = 1;
            this.chk_saveconfirm.Text = "Conferma il salvataggio";
            this.chk_saveconfirm.UseVisualStyleBackColor = true;
            // 
            // chk_autoload
            // 
            this.chk_autoload.AutoSize = true;
            this.chk_autoload.Location = new System.Drawing.Point(6, 22);
            this.chk_autoload.Name = "chk_autoload";
            this.chk_autoload.Size = new System.Drawing.Size(221, 17);
            this.chk_autoload.TabIndex = 0;
            this.chk_autoload.Text = "Importa automaticamente le aree all\'avvio";
            this.chk_autoload.UseVisualStyleBackColor = true;
            // 
            // tab_advanced
            // 
            this.tab_advanced.BackColor = System.Drawing.SystemColors.Control;
            this.tab_advanced.Controls.Add(this.chk_format_helpzon);
            this.tab_advanced.Controls.Add(this.chk_dontsavehogfile);
            this.tab_advanced.Controls.Add(this.groupBox1);
            this.tab_advanced.Location = new System.Drawing.Point(4, 22);
            this.tab_advanced.Name = "tab_advanced";
            this.tab_advanced.Padding = new System.Windows.Forms.Padding(3);
            this.tab_advanced.Size = new System.Drawing.Size(297, 259);
            this.tab_advanced.TabIndex = 2;
            this.tab_advanced.Text = "Avanzate";
            // 
            // chk_format_helpzon
            // 
            this.chk_format_helpzon.AutoSize = true;
            this.chk_format_helpzon.Location = new System.Drawing.Point(6, 45);
            this.chk_format_helpzon.Name = "chk_format_helpzon";
            this.chk_format_helpzon.Size = new System.Drawing.Size(173, 17);
            this.chk_format_helpzon.TabIndex = 13;
            this.chk_format_helpzon.Text = "Salva .ZON in formato Helpzon";
            this.chk_format_helpzon.UseVisualStyleBackColor = true;
            // 
            // chk_dontsavehogfile
            // 
            this.chk_dontsavehogfile.AutoSize = true;
            this.chk_dontsavehogfile.Location = new System.Drawing.Point(6, 22);
            this.chk_dontsavehogfile.Name = "chk_dontsavehogfile";
            this.chk_dontsavehogfile.Size = new System.Drawing.Size(193, 17);
            this.chk_dontsavehogfile.TabIndex = 12;
            this.chk_dontsavehogfile.Text = "Non salvare file dati opzionali .HOG";
            this.chk_dontsavehogfile.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radio_fileformat1);
            this.groupBox1.Controls.Add(this.radio_fileformat0);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 188);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Codifica dei file salvati:";
            // 
            // radio_fileformat1
            // 
            this.radio_fileformat1.AutoSize = true;
            this.radio_fileformat1.Location = new System.Drawing.Point(6, 42);
            this.radio_fileformat1.Name = "radio_fileformat1";
            this.radio_fileformat1.Size = new System.Drawing.Size(103, 17);
            this.radio_fileformat1.TabIndex = 1;
            this.radio_fileformat1.Text = "UTF-8 (no BOM)";
            this.radio_fileformat1.UseVisualStyleBackColor = true;
            // 
            // radio_fileformat0
            // 
            this.radio_fileformat0.AutoSize = true;
            this.radio_fileformat0.Checked = true;
            this.radio_fileformat0.Location = new System.Drawing.Point(6, 19);
            this.radio_fileformat0.Name = "radio_fileformat0";
            this.radio_fileformat0.Size = new System.Drawing.Size(50, 17);
            this.radio_fileformat0.TabIndex = 0;
            this.radio_fileformat0.TabStop = true;
            this.radio_fileformat0.Text = "ANSI";
            this.radio_fileformat0.UseVisualStyleBackColor = true;
            // 
            // dlg_setdirs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 329);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "dlg_setdirs";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opzioni";
            this.Shown += new System.EventHandler(this.frm_options_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tab_dirs.ResumeLayout(false);
            this.tab_dirs.PerformLayout();
            this.tab_general.ResumeLayout(false);
            this.tab_general.PerformLayout();
            this.tab_advanced.ResumeLayout(false);
            this.tab_advanced.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_dirs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox edt5;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox edt4;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox edt3;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox edt2;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edt1;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edt0;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.TabPage tab_general;
        private System.Windows.Forms.CheckBox chk_delconfirm;
        private System.Windows.Forms.CheckBox chk_saveconfirm;
        private System.Windows.Forms.CheckBox chk_autoload;
        private System.Windows.Forms.CheckBox chk_samedir;
        private System.Windows.Forms.Button btnfontdesc_def;
        private System.Windows.Forms.TextBox fontdesc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnfontdesc;
        private System.Windows.Forms.CheckBox chk_initdopenoors;
        private System.Windows.Forms.CheckBox chk_updatevnums;
        private System.Windows.Forms.TabPage tab_advanced;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radio_fileformat1;
        private System.Windows.Forms.RadioButton radio_fileformat0;
        private System.Windows.Forms.CheckBox chk_dontsavehogfile;
        private System.Windows.Forms.CheckBox chk_format_helpzon;
        private System.Windows.Forms.CheckBox chk_delete_vnum_refs;
    }
}