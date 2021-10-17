using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HandofGod
{
    class Dialogs
    {
        public static string TextboxDialog(string text, string caption)
        {
            string result = "";
            using (Form prompt = new Form())
            {
                prompt.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                prompt.ShowInTaskbar = false;
                prompt.Width = 300;
                prompt.Height = 150;
                prompt.Text = caption;
                prompt.StartPosition = FormStartPosition.CenterScreen;
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 200 };
                Button confirmation = new Button() { Text = "Ok", Left = 150, Width = 100, Top = 70 };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;
                prompt.ShowDialog();
                result = textBox.Text;
            }
            return result;
        }

        public static bool Confirm(string s)
        {
            return MessageBox.Show(s, "Attenzione", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK;
        }

        public static bool ConfirmAndDelete<T>(T e, List<T> list, string s)
        {
            if (!Options.data.del_confirm || MessageBox.Show(s, "Attenzione", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                list.Remove(e);
                return true;
            }
            else return false;
        }

        public static void Warning(string s)
        {
            MessageBox.Show(s, "Attenzione");
        }

        public const string msg_exitexists = "Impossibile creare un'uscita nella stanza #{0}, direzione {1}{2} perché già esistente.";
        public const string msg_confirmsave = "Salvare le modifiche?";
    }
}
