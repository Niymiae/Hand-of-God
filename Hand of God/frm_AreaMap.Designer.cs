namespace HandofGod
{
    partial class frm_AreaMap
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.roomfloorplus = new System.Windows.Forms.ToolStripMenuItem();
            this.roomfloorminus = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundpanel = new HandofGod.NoFlickerPanel();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roomfloorplus,
            this.roomfloorminus});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(248, 48);
            // 
            // roomfloorplus
            // 
            this.roomfloorplus.Name = "roomfloorplus";
            this.roomfloorplus.Size = new System.Drawing.Size(247, 22);
            this.roomfloorplus.Text = "Sposta la stanza al piano di sopra";
            this.roomfloorplus.Click += new System.EventHandler(this.changeroomfloor);
            // 
            // roomfloorminus
            // 
            this.roomfloorminus.Name = "roomfloorminus";
            this.roomfloorminus.Size = new System.Drawing.Size(247, 22);
            this.roomfloorminus.Text = "Sposta la stanza al piano di sotto";
            this.roomfloorminus.Click += new System.EventHandler(this.changeroomfloor);
            // 
            // backgroundpanel
            // 
            this.backgroundpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundpanel.Location = new System.Drawing.Point(0, 0);
            this.backgroundpanel.Name = "backgroundpanel";
            this.backgroundpanel.Size = new System.Drawing.Size(775, 547);
            this.backgroundpanel.TabIndex = 8;
            this.backgroundpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.backgroundpanel_Paint);
            this.backgroundpanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.backgroundpanel_MouseDown);
            this.backgroundpanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.backgroundpanel_MouseMove);
            this.backgroundpanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.backgroundpanel_MouseUp);
            // 
            // frm_AreaMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(775, 547);
            this.Controls.Add(this.backgroundpanel);
            this.KeyPreview = true;
            this.Name = "frm_AreaMap";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor Grafico";
            this.Load += new System.EventHandler(this.frm_editArea_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_main_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frm_main_KeyUp);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private NoFlickerPanel backgroundpanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem roomfloorplus;
        private System.Windows.Forms.ToolStripMenuItem roomfloorminus;
    }
}

