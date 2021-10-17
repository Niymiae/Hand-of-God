using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace HandofGod
{
    // for debug purposes
    public class Log
    {
        public static void log(string s)
        {
            using (StreamWriter f = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + "\\hoglog.log", true))
                f.WriteLine(s);
        }
    }
}
