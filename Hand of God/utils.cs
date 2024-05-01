using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
using System.ComponentModel;

namespace HandofGod
{
    class utils
    {
        public static int GetOppositeDirection(int dir)
        {
            switch (dir)
            {
                case C.dir_north: return C.dir_south;
                case C.dir_south: return C.dir_north;
                case C.dir_east: return C.dir_west;
                case C.dir_west: return C.dir_east;
                case C.dir_up: return C.dir_down;
                case C.dir_down: return C.dir_up;
                default : return dir;
            }
        }

        public static int GetFirstAvailableVnum<T>(Area area, int start, List<T> list)
        {
            int end = area.vnum_max;

            int result = start;
            while (result <= end && list.Find(v => (v as area_element).vnum == result) != null)
                result++;

            if (result > end)
                return -1;

            return result;
        }

        public static void AddFormEvents(System.Windows.Forms.Control.ControlCollection Controls, Action<object, EventArgs> func)
        {
            foreach (Control c in Controls)
            {
                if (c.Controls.Count > 0)
                {
                    AddFormEvents(c.Controls, func);
                }
                else if (c is ComboBox)
                {
                    ((ComboBox)c).SelectedIndexChanged += new EventHandler(func);
                }
                else if (c is TextBox)
                {
                    //((TextBox)c).TextChanged += new EventHandler(func);
                    ((TextBox)c).KeyDown += new KeyEventHandler(func);
                }
                else if (c is RichTextBox)
                {
                    //((RichTextBox)c).TextChanged += new EventHandler(func);
                    ((RichTextBox)c).KeyDown += new KeyEventHandler(func);
                }
                else if (c is ListBox)
                {
                    ((ListBox)c).SelectedIndexChanged += new EventHandler(func);
                }
                else if (c is CheckBox)
                {
                    ((CheckBox)c).CheckedChanged += new EventHandler(func);
                }
            }
        }

        public static bool ValidateDiceText(string s, bool withplus)
        {
            String strpattern = withplus ? "^(\\d+)(d)(\\d+)(([-+])(\\d+))*$" : "^(\\d+)(d)(\\d+)*$";
            Regex regex = new Regex(strpattern);
            return regex.Match(s).Success;
        }

        public static string ProcessDiceText(string s)
        {
            if (ValidateDiceText(s, true))
            {
                if (s.IndexOf('+') == -1)
                    s = s + "+0";
                return s;
            }
            else return "1d1+0";
        }

        public static string FontToString(Font f)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
            return converter.ConvertToString(f);
        }

        public static Font StringToFont(string s)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
            return (Font)converter.ConvertFromString(s);
        }

        public static string CutColorCodes(string str)
        {
            String strpattern = @"[$]c[0-9][0-9][0-9][0-9]";
            String newStrPattern = @"[$]ch[k|b|f][0-F][0-F][0-F][0-F][0-F][0-F]";

            Regex regex = new Regex(strpattern);
            Regex regexTwo = new Regex(newStrPattern);

            regex.Replace(str, "");
            regexTwo.Replace(str, "");

            return str;
        }

        public static string ColorToCode(Color col)
        {
            if (col == Color.Black) return "$c0000";
            else if (col == Color.DarkRed) return "$c0001";
            else if (col == Color.DarkGreen) return "$c0002";
            else if (col == Color.DarkKhaki) return "$c0003";
            else if (col == Color.Blue) return "$c0004";
            else if (col == Color.Indigo) return "$c0005";
            else if (col == Color.DarkCyan) return "$c0006";
            else if (col == Color.DimGray) return "$c0008";
            else if (col == Color.Red) return "$c0009";
            else if (col == Color.Lime) return "$c0010";
            else if (col == Color.Yellow) return "$c0011";
            else if (col == Color.DodgerBlue) return "$c0012";
            else if (col == Color.Fuchsia) return "$c0013";
            else if (col == Color.Cyan) return "$c0014";
            else if (col == Color.White) return "$c0015";
            else return "$c0007";
        }

        public static Color CodeToColor(string code)
        {
            switch (code)
            {
                case "$c0000": return Color.Black;
                case "$c0001": return Color.DarkRed;
                case "$c0002": return Color.DarkGreen;
                case "$c0003": return Color.DarkKhaki;
                case "$c0004": return Color.Blue;
                case "$c0005": return Color.Indigo;
                case "$c0006": return Color.DarkCyan;
                default: return Color.LightGray;
                case "$c0008": return Color.DimGray;
                case "$c0009": return Color.Red;
                case "$c0010": return Color.Lime;
                case "$c0011": return Color.Yellow;
                case "$c0012": return Color.DodgerBlue;
                case "$c0013": return Color.Fuchsia;
                case "$c0014": return Color.Cyan;
                case "$c0015": return Color.White;
            }

        }

        public static List<colorindexer> ProcessTextColorCodes(string str, Color def)
        {
            String strpattern = @"[$]c[0-9][0-9][0-9][0-9]";
            String newStrPattern = @"[$]ch[k|b|f][0-F][0-F][0-F][0-F][0-F][0-F]";
            Regex regex = new Regex(strpattern);
            Regex regexTwo = new Regex(newStrPattern);

            Color col = def;

            List<colorindexer> result = new List<colorindexer>();

            Match m = null;
            while ((m = regex.Match(str)).Success)
            {
                string s = m.Value;

                result.Add(new colorindexer() { s = str.Substring(0, m.Index), c = col, i = m.Index });
                col = CodeToColor("$c00" + s[4] + s[5]);
                str = str.Substring(m.Index + 6, str.Length - (m.Index + 6));
            }

            m = null;
            while ((m = regexTwo.Match(str)).Success)
            {
                string s = m.Value;
                ColorConverter converter = new ColorConverter();
                result.Add(new colorindexer() { s = str.Substring(0, m.Index), c = col, i = m.Index });

                col = (Color)converter.ConvertFromString("#" + s.Substring(2, 6));
                str = str.Substring(m.Index + 6, str.Length - (m.Index + 6));
            }

            result.Add(new colorindexer() { s = str, c = col });

            return result;
        }
    }
}
