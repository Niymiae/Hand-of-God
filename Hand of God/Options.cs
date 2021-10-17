using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace HandofGod
{
    public class OptionsData
    {
        public string[] directories = new string[Options.max_dirs + 1];
        public bool save_confirm { get; set; }
        public bool del_confirm { get; set; }
        public bool init_automatic_comment { get; set; }
        public bool auto_load { get; set; }
        public bool check_samedir { get; set; }
        public bool init_opendoors { get; set; }
        public bool updatevnums_auto { get; set; }
        public bool preview_showvnums { get; set; }
        public bool preview_autoexits { get; set; }
        public bool dontsave_hogfile { get; set; }
        public bool format_helpzon { get; set; }
        public int  file_format { get; set; }
        public bool delete_vnum_refs { get; set; }
        public Font descriptions_font { get; set; }
        public Area Templates;

        #region Constructor
        public OptionsData()
        {
            for (int i = 0; i <= Options.max_dirs; i++)
                directories[i] = "";
            descriptions_font = new Font("Microsoft Sans Serif", 9);
            Templates = new Area();
        }
        #endregion

        #region Methods
        public void CopyFrom(OptionsData data)
        {
            init_automatic_comment = data.init_automatic_comment;
            save_confirm = data.save_confirm;
            del_confirm = data.del_confirm;
            auto_load = data.auto_load;
            check_samedir = data.check_samedir;
            init_opendoors = data.init_opendoors;
            updatevnums_auto = data.updatevnums_auto;
            preview_showvnums = data.preview_showvnums;
            preview_autoexits = data.preview_autoexits;
            dontsave_hogfile = data.dontsave_hogfile;
            format_helpzon = data.format_helpzon;
            file_format = data.file_format;
            delete_vnum_refs = data.delete_vnum_refs;

            for (int i = 0; i < Options.max_dirs; i++)
                directories[i] = data.directories[i];

            descriptions_font = data.descriptions_font;
        }

        private void changememosfont(Control parent, Font font)
        {
            foreach (Control c in parent.Controls)
                if (c.Controls.Count > 0)
                    changememosfont(c, font);
                else if (c.GetType() == typeof(MudlikeRichTextBox))
                {
                    (c as MudlikeRichTextBox).SetFont(font);
                }
        }

        public void ChangeDescriptionsFont()
        {
            using (FontDialog dlg = new FontDialog())
            {
                dlg.Font = descriptions_font;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    descriptions_font = dlg.Font;
                    foreach (Form form in Application.OpenForms)
                        foreach (Control c in form.Controls)
                            changememosfont(c, dlg.Font);
                }
            }
        }
        #endregion

    }

    public class Options
    {
        public const string default_font_desc = "Microsoft Sans Serif; 9pt";

        public const int max_dirs = 6;
        public static OptionsData data = new OptionsData();
        public static bool mode_console = false;

        #region Options I/O
        #region Load

        #region Parses
        private static int parseInt(string s)
        {
            int result = 0;
            int.TryParse(s, out result);
            return result;
        }

        private static bool parseBool(string s)
        {
            bool result = false;
            bool.TryParse(s, out result);
            return result;
        }

        private static void ParseOptionLine(string s)
        {
            string[] tmp = s.Split(new char[] {'='}, 2, StringSplitOptions.None);

            if (tmp.Length <= 1)
                return;

            if (tmp.Length > 0)
                switch (tmp[0])
                {
                    case "dir0":
                    case "dir1":
                    case "dir2":
                    case "dir3":
                    case "dir4":
                    case "dir5":
                    case "dir6":
                        int index;
                        int.TryParse(tmp[0].Replace("dir", ""), out index);
                        if (index > -1)
                            data.directories[index] = tmp[1];
                        break;
                    case "init_autocomment":
                        data.init_automatic_comment = parseBool(tmp[1]);
                        break;
                    case "save_confirm":
                        data.save_confirm = parseBool(tmp[1]);
                        break;
                    case "del_confirm":
                        data.del_confirm = parseBool(tmp[1]);
                        break;
                    case "auto_load":
                        data.auto_load = parseBool(tmp[1]);
                        break;
                    case "check_samedir":
                        data.check_samedir = parseBool(tmp[1]);
                        break;
                    case "font_descriptions":
                        data.descriptions_font = utils.StringToFont(tmp[1]);
                        break;
                    case "init_opendoors":
                        data.init_opendoors = parseBool(tmp[1]);
                        break;
                    case "updatevnums_auto":
                        data.updatevnums_auto = parseBool(tmp[1]);
                        break;
                    case "preview_showvnums":
                        data.preview_showvnums = parseBool(tmp[1]);
                        break;
                    case "preview_autoexits":
                        data.preview_autoexits = parseBool(tmp[1]);
                        break;
                    case "dontsave_hogfile":
                        data.dontsave_hogfile = parseBool(tmp[1]);
                        break;
                    case "format_helpzon":
                        data.format_helpzon = parseBool(tmp[1]);
                        break;
                    case "file_format":
                        data.file_format = parseInt(tmp[1]);
                        break;
                    case "delete_vnum_refs":
                        data.delete_vnum_refs = parseBool(tmp[1]);
                        break;
            }
        }
        #endregion 

        public static void CheckTemplateFiles()
        {
           string filename = Path.GetDirectoryName(Application.ExecutablePath) + "\\templates\\templates";
            for (int i = 0; i <= C.i_shop; i++)
                if (!File.Exists(filename + C.file_ext[i]))
                    using (StreamWriter f = new StreamWriter(filename + C.file_ext[i]))
                        f.Close();
        }

        public static void LoadTemplates()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\templates";

            if (!Directory.Exists(path))
                return;

            data.Templates.Clear();
            FileManager.LoadWld(data.Templates, path + "\\templates.wld");
            FileManager.LoadMob(data.Templates, path + "\\templates.mob");
            FileManager.LoadObj(data.Templates, path + "\\templates.obj");
            FileManager.LoadShop(data.Templates, path + "\\templates.shp");
            FileManager.LoadZon(data.Templates.zones, path + "\\templates.zon", true);
            data.Templates.PostLoad(null);
        }

        public static void Load(FileManager fm)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath);

           // Templates
            LoadTemplates();

           // Options Data
            if (!File.Exists(path + "\\config.cfg"))
                return;

            StreamReader file = new StreamReader(path + "\\config.cfg");

            while (!file.EndOfStream)
                ParseOptionLine(file.ReadLine());

            file.Close();
        }
        #endregion

        #region Save
        public static void SaveTemplates()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath);

            FileManager.SaveWld(data.Templates.rooms, path + "\\templates\\templates.wld");
            FileManager.SaveMob(data.Templates.mobs, path + "\\templates\\templates.mob");
            FileManager.SaveObj(data.Templates.objects, path + "\\templates\\templates.obj");
            FileManager.SaveShop(data.Templates.shops, path + "\\templates\\templates.shp");
            FileManager.SaveZon(data.Templates.zones, path + "\\templates\\templates.zon");
        }

        public static void Save(FileManager fm)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath);

            if (!Directory.Exists(path + "\\templates"))
                Directory.CreateDirectory( path + "\\templates");

            // Templates
            SaveTemplates();  

           // Options Data
            StreamWriter file = new StreamWriter(path + "\\config.cfg");
            int dircounter = 0;

            while (dircounter < max_dirs)
            {
                file.WriteLine("dir" + dircounter + "=" + data.directories[dircounter]);
                dircounter++;
            }
            file.WriteLine("init_autocomment=" + data.init_automatic_comment.ToString());
            file.WriteLine("save_confirm=" + data.save_confirm.ToString());
            file.WriteLine("del_confirm=" + data.del_confirm.ToString());
            file.WriteLine("auto_load=" + data.auto_load.ToString());
            file.WriteLine("check_samedir=" + data.check_samedir.ToString());
            file.WriteLine("font_descriptions=" + utils.FontToString(data.descriptions_font));
            file.WriteLine("init_opendoors=" + data.init_opendoors.ToString());
            file.WriteLine("updatevnums_auto=" + data.updatevnums_auto.ToString());
            file.WriteLine("preview_showvnums=" + data.preview_showvnums.ToString());
            file.WriteLine("preview_autoexits=" + data.preview_autoexits.ToString());
            file.WriteLine("dontsave_hogfile=" + data.dontsave_hogfile.ToString());
            file.WriteLine("format_helpzon=" + data.format_helpzon.ToString());
            file.WriteLine("file_format=" + data.file_format.ToString());
            file.WriteLine("delete_vnum_refs=" + data.delete_vnum_refs.ToString());
            file.Close();
        }
        #endregion
        #endregion
    }
}
