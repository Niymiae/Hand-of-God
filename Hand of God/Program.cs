using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HandofGod
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            foreach (string s in args)
                if (s == "console")
                    Options.mode_console = true;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (frm_Intro introform = new frm_Intro())
                introform.ShowDialog();
            Application.Run(new frm_main());
        }
    }
}
