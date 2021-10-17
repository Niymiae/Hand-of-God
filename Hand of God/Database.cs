using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;

namespace HandofGod
{
    public class Database
    {
        public static readonly List<Spell> spells = new List<Spell>();
        public static readonly List<MDFuncData> md_functions = new List<MDFuncData>();

        #region Gets
        // Spells
        public static Spell GetSpell(int index)
        {
            return spells.Find(x => x.vnum == index);
        }

        // Mob Dialog Functions
        public static MDFuncData GetMDFunction(int index)
        {
            return md_functions.Find(x => x.vnum == index);
        }
        #endregion

        public static void Initialize()
        {
            string[] desc;

            spells.Clear();
            if (File.Exists(Path.GetDirectoryName(Application.ExecutablePath) + "\\spells.dat"))
            {
                using (StreamReader file = new StreamReader(Path.GetDirectoryName(Application.ExecutablePath) + "\\spells.dat"))
                {
                    while (!file.EndOfStream)
                    {
                        Spell s = new Spell();
                        desc = file.ReadLine().Split(';');
                        if (desc.Length > 0)
                        {
                            s.vnum = int.Parse(desc[0]);
                            s.shortdesc = desc.Length > 1 ? desc[1] : "";
                            spells.Add(s);
                        }
                    }
                    file.Close();
                }
            }
            else
            {
                Spell s = new Spell();
                s.vnum = -1;
                s.shortdesc = "nessuna spell";
                spells.Add(s);
            }
     
            md_functions.Clear();
            if (File.Exists(Path.GetDirectoryName(Application.ExecutablePath) + "\\md_functions.dat"))
            {
                using (StreamReader file = new StreamReader(Path.GetDirectoryName(Application.ExecutablePath) + "\\md_functions.dat"))
                {
                    while (!file.EndOfStream)
                    {
                        MDFuncData s = new MDFuncData();
                        desc = file.ReadLine().Split(';');
                        if (desc.Length > 0)
                        {
                            s.vnum = int.Parse(desc[0]);
                            s.shortdesc = desc.Length > 1 ? desc[1] : "";
                            s.desc = desc.Length > 2 ? desc[2] : "";
                            s.desc1 = desc.Length > 3 ? desc[3] : "";
                            s.desc2 = desc.Length > 4 ? desc[4] : "";
                            md_functions.Add(s);
                        }
                    }
                    file.Close();
                }
            }
            else
            {
                MDFuncData f = new MDFuncData();
                f.vnum = -1;
                f.shortdesc = "nessuna funzione";
                md_functions.Add(f);
            }
        }
    }
}
