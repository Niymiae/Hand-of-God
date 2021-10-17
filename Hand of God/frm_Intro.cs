using System;
using System.Drawing;
using System.Windows.Forms;

namespace HandofGod
{
    public partial class frm_Intro : Form
    {
        public frm_Intro()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
