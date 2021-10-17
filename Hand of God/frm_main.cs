/* Hand of God
 * Copyright © 2014 Michele "Saregon" Pais Becher All Rights Reserved */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Specialized;

namespace HandofGod
{
    public partial class frm_main : Form
    {
        #region Vars
        FileManager fileManager;
        List<Zone> zones;
        List<frm_List> opened_forms;
        #endregion

        #region Constructor
        public frm_main()
        {
            InitializeComponent();
            opened_forms = new List<frm_List>();
            fileManager = new FileManager();
            zones = new List<Zone>();
            list_zones.SetColumns(C.i_zone);
            Text = C.AppName + " " + C.AppVersion;
        }
        #endregion

        #region Methods
        private void AddZone(object sender, EventArgs e)
        {
            Edit("nuova area", true, true);
            
            /*
            int vnum = 0;
            while (zones.Find(z => z.vnum == vnum) != null)
                vnum++;

            Zone zone = new Zone();
            zone.vnum = vnum;
            zone.vnum_max = vnum * 100 + 99;
            zones.Add(zone);
            ListViewItem item = list_zones.AddItem(null, zone, true);

            if (item != null)
            {
                item.Selected = true;
                item.EnsureVisible();
            }
            */
        }

        private void onFormClosed(object sender, EventArgs e)
        {
            if (opened_forms.Contains(sender as frm_List))
                opened_forms.Remove(sender as frm_List);
        }

        private void Edit(string fname = "", bool directload = false, bool newzone = false)
        {
            if (list_zones.SelectedItems.Count <= 0 && fname == "")
                return;

            Zone z = null;

            // if there is no given filename, open the selected zone
            if (fname == "")
            {
                z = list_zones.SelectedItems[0].Tag as Zone;

                if (z == null)
                    return;

                fname = z.filename;
            }
            
            frm_List form = null;

            // if the zone is already opened we don't need to create a new form
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frm_List) && (f as frm_List).HasFilename(fname))
                    form = (f as frm_List);
            }

            if (form == null)
            {
                form = new frm_List();
                form.FormClosed += new FormClosedEventHandler(onFormClosed);
            }

            if (form.OpenAreaFiles(fname, directload, newzone))
            {
                form.Show();
                form.BringToFront();
                if (!opened_forms.Contains(form))
                    opened_forms.Add(form);
            }
        }
        #endregion

        #region File I/O
        private IOResult OpenZoneFile(string file, bool inits)
        {
            return FileManager.LoadZon(zones, file, inits);
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.FileName = "";
                dlg.Filter = "File Area|*.zon;*.wld;*.mob;*.obj;*.shp|Zone|*.zon|Stanze|*.wld|Mobs|*.mob|Oggetti|*.obj|Negozi|*.shp";
                if (dlg.ShowDialog() == DialogResult.OK)
                    Edit(dlg.FileName, true);
            }
        }

        private void import_dirs(object sender, EventArgs e)
        {
            if (Directory.Exists(Options.data.directories[C.i_zone]))
            {
                string not_opened_zones = "Non è stato possibile caricare le seguenti zone:\n";
                bool err = false;
                string[] files = Directory.GetFiles(Options.data.directories[C.i_zone], "*.zon", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    if (OpenZoneFile(file, false) != IOResult.Ok)
                    {
                        not_opened_zones += file + "\n";
                        err = true;
                    }
                }
                list_zones.AddToList(null, zones);

                if (err)
                    MessageBox.Show(not_opened_zones);
            }
        }
        #endregion

        #region Widgets Methods
        private void informazioniSuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frm_Intro form = new frm_Intro())
                form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void list_zones_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Edit();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (list_zones.SelectedItems.Count <= 0)
                return;

            int i = list_zones.SelectedItems[0].Index;

            list_zones.Items.Remove(list_zones.SelectedItems[0]);
            zones.Remove(zones[i]);
        }

        private void btn_options_Click(object sender, EventArgs e)
        {
            using (dlg_setdirs form = new dlg_setdirs())
            {
                form.Data.CopyFrom(Options.data);
                if (form.ShowDialog() == DialogResult.OK)
                    Options.data.CopyFrom(form.Data);
            }
        }

        private void frm_main_Load(object sender, EventArgs e)
        {
            Database.Initialize();
            Options.Load(fileManager);
            if (Options.data.auto_load)
                import_dirs(sender, e);

            if (Options.mode_console)
                grp_console.Show();
        }

        private void frm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (frm_List f in opened_forms.ToArray())
            {
                f.Close();
                if (f.Visible)
                {
                    f.BringToFront();
                    e.Cancel = true;
                    return;
                }
            }

            Options.Save(fileManager);                
        }
        #endregion

        private void riepilogoComandiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frm_CmdOverview form = new frm_CmdOverview())
                form.ShowDialog();
        }

        #region Console
        private void button7_Click(object sender, EventArgs e)
        {
            string log = "";

            foreach (ListViewItem item in list_zones.Items)
            {
             Zone z = item.Tag as Zone;
             
             if (z != null)
             using (Area Data = new Area())
             {
                 string suff = "";
                 string fname = z.filename;
                 string s = Path.GetFileNameWithoutExtension(fname);
                 string fname_last = Options.data.directories[C.i_shop] + "\\" + s + C.file_ext[C.i_shop];

                 FileManager.LoadShop(Data, fname_last);
                 foreach (Shop shop in Data.shops)
                 {
                     suff = suff + "\r\n      Shop #" + shop.vnum;
                     suff = suff + "\r\n      " + " BUY " + shop.mul_buy.ToString() + " "
                                 + "- SELL " + shop.mul_sell.ToString();
                 }
                 FileManager.SaveShop(Data.shops, fname_last);
                 log = log + "ZONA> " + s + " - " + (suff == "" ? "Nessun negozio" : suff) + "\r\n";
             }
            }

            using (Form frmlog = new Form())
            {
                frmlog.Width = 500;
                frmlog.Height = 500;

                TextBox memo = new TextBox();

                frmlog.Controls.Add(memo);

                memo.Parent = frmlog;
                memo.Dock = DockStyle.Fill;
                memo.Multiline = true;
                memo.ReadOnly = true;
                memo.ScrollBars = ScrollBars.Both;
                memo.WordWrap = false;

                memo.Text = log;

                frmlog.ShowDialog();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int counter = 0;
            foreach (ListViewItem item in list_zones.Items)
            {
                Zone z = item.Tag as Zone;

                if (z != null)
                    using (Area Data = new Area())
                    {
                        string[] filenames = new string[Options.max_dirs + 1];
                        string fname = z.filename;
                        string s = Path.GetFileNameWithoutExtension(fname);
                        string fname_last = Options.data.directories[C.i_shop] + "\\" + s + C.file_ext[C.i_shop];

                        for (int i = 0; i < Options.max_dirs; i++)
                            if (File.Exists(Options.data.directories[i] + "\\" + s + C.file_ext[i]))
                                filenames[i] = Options.data.directories[i] + "\\" + s + C.file_ext[i];
                            else filenames[i] = "";

                        bool found = false;

                        if (filenames[C.i_room] != "")
                        {
                            FileManager.LoadWld(Data, filenames[C.i_room]);
                            found = true;
                        }

                        if (filenames[C.i_mob] != "")
                        {
                            FileManager.LoadMob(Data, filenames[C.i_mob]);
                            found = true;
                        }

                        if (filenames[C.i_obj] != "")
                        {
                            FileManager.LoadObj(Data, filenames[C.i_obj]);
                            found = true;
                        }

                        if (filenames[C.i_shop] != "")
                        {
                            FileManager.LoadShop(Data, filenames[C.i_shop]);
                            found = true;
                        }

                        if (filenames[C.i_zone] != "")
                        {
                            FileManager.LoadZon(Data.zones, filenames[C.i_zone], true);
                            found = true;
                        }

                        if (filenames[C.i_hogvisual] != "")
                        {
                            FileManager.LoadHog(Data, filenames[C.i_hogvisual]);
                            found = true;
                        }

                        Data.PostLoad(Data);


                        if (filenames[C.i_room] != "")
                            FileManager.SaveWld(Data.rooms, filenames[C.i_room]);

                        if (filenames[C.i_mob] != "")
                            FileManager.SaveMob(Data.mobs, filenames[C.i_mob]);

                        if (filenames[C.i_obj] != "")
                            FileManager.SaveObj(Data.objects, filenames[C.i_obj]);

                        if (filenames[C.i_shop] != "")
                            FileManager.SaveShop(Data.shops, filenames[C.i_shop]);

                        if (filenames[C.i_zone] != "")
                            FileManager.SaveZon(Data.zones, filenames[C.i_zone]);

                        if (filenames[C.i_hogvisual] != "")
                            FileManager.SaveHog(Data, filenames[C.i_hogvisual]);

                        if (found)
                            counter++;
                    }
            }

            MessageBox.Show(counter.ToString() + " zone salvate.");
        }
        #endregion
    }
}
