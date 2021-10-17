namespace HandofGod
{
    partial class frm_MudPreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnexec = new System.Windows.Forms.Button();
            this.txt_input = new System.Windows.Forms.TextBox();
            this.txt_preview = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnexec);
            this.panel1.Controls.Add(this.txt_input);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 481);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(765, 29);
            this.panel1.TabIndex = 1;
            // 
            // btnexec
            // 
            this.btnexec.Location = new System.Drawing.Point(678, 6);
            this.btnexec.Name = "btnexec";
            this.btnexec.Size = new System.Drawing.Size(75, 20);
            this.btnexec.TabIndex = 1;
            this.btnexec.Text = "Invia";
            this.btnexec.UseVisualStyleBackColor = true;
            this.btnexec.Click += new System.EventHandler(this.btnexec_Click);
            // 
            // txt_input
            // 
            this.txt_input.Location = new System.Drawing.Point(12, 6);
            this.txt_input.Name = "txt_input";
            this.txt_input.Size = new System.Drawing.Size(658, 20);
            this.txt_input.TabIndex = 0;
            this.txt_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_input_KeyDown);
            this.txt_input.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_input_KeyUp);
            // 
            // txt_preview
            // 
            this.txt_preview.BackColor = System.Drawing.Color.Black;
            this.txt_preview.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txt_preview.DetectUrls = false;
            this.txt_preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_preview.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_preview.ForeColor = System.Drawing.Color.LightGray;
            this.txt_preview.Location = new System.Drawing.Point(0, 0);
            this.txt_preview.Name = "txt_preview";
            this.txt_preview.ReadOnly = true;
            this.txt_preview.Size = new System.Drawing.Size(765, 481);
            this.txt_preview.TabIndex = 2;
            this.txt_preview.TabStop = false;
            this.txt_preview.Text = "";
            this.txt_preview.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_preview_KeyDown);
            // 
            // frm_MudPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 510);
            this.Controls.Add(this.txt_preview);
            this.Controls.Add(this.panel1);
            this.Name = "frm_MudPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preview";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_input;
        private System.Windows.Forms.Button btnexec;
        private System.Windows.Forms.RichTextBox txt_preview;
    }
}