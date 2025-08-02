using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HandofGod
{
    public partial class frm_List : Form
    {
        private frm_Reports frm_reports;
        private frm_AreaMap frm_editvis;
        private bool directsave;
        private bool templatesmode;
        private area_element[] defaults;
        public Area Data;
        public string filename = "";
        public string[] filenames = new string[Options.max_dirs + 1];

        #region Constructor
        public frm_List()
        {
            InitializeComponent();
            Data = new Area();
            defaults = new area_element[5];
            for (int i = 0; i <= 4; i++)
                filenames[i] = "";
            pn_utils.Width = 10;
            radioButton2.Checked = true;
        }
        #endregion
        #region Modified Status

        private bool modified;
        private bool Modified
        {
            get { return modified; }
            set
            {
                modified = value;
                SetModifiedStatus();
            }
        }

        private void SetModifiedStatus()
        {
            UpdateText();
            btnsave.Enabled = modified;
        }
        #endregion

        #region Templates
        public void SetTemplatesMode()
        {
            btnreports.Enabled = false;
            btnenum.Enabled = false;
            btnsavein.Enabled = false;
            btneditvis.Enabled = false;
            btnaddsel.Enabled = false;
            btnoptions.Enabled = false;
            btnedittemplates.Enabled = false;
            btndeltemplate.Enabled = false;
            templatesmode = true;
        }

        private void UpdateTemplateButton(int i)
        {
            if (defaults[i] != null)
            {
                btnaddsel.BackColor = Color.LightGreen;
                btnaddsel.Text = defaults[i].shortdesc;
            }
            else
            {
                btnaddsel.BackColor = SystemColors.Control;
                btnaddsel.Text = "<nuovo elemento>";
            }
        }
        #endregion

        #region Updates
        private void UpdateText()
        {
            var checkedButton = radiogroup.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            int i = checkedButton.TabIndex;
            
            lblstatus.Text = filenames[i] != "" ? filenames[i] : "";

            if (templatesmode)
                Text = C.AppName + " - Modifica Templates";

            else Text = C.AppName + " - " + Path.GetFileNameWithoutExtension(filename) + (modified ? " *Modificata*" : "");
        }

        public void RefreshList(int i)
        {
            int index = -1;

            if (list.SelectedIndices.Count > 0)
                index = list.SelectedIndices[0];


            bool columns_changed = list.SetColumns(i);

            switch (i)
            {
                case C.i_zone: list.AddToList(Data, Data.zones);
                                       break;
                case C.i_room: list.AddToList(Data, Data.rooms);
                                       break;
                case C.i_mob: list.AddToList(Data, Data.mobs);
                                       break;
                case C.i_obj: list.AddToList(Data, Data.objects);
                                       break;
                case C.i_shop: list.AddToList(Data, Data.shops);
                                       break;
                default : return;
            }

            UpdateText();
            UpdateTemplateButton(i);

            if (!columns_changed && index > -1 && index < list.Items.Count)
            {
                list.Items[index].Selected = true;
                list.Items[index].EnsureVisible();
                list.Select();
            }
        }

        public void RefreshSelectedList()
        {
            var checkedButton = radiogroup.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            RefreshList(checkedButton.TabIndex);
        }
        #endregion

        #region File I/O
        /// <summary>
        /// retrieves the file type by its extension
        /// </summary>
        /// <param name="f">filename</param>
        /// <returns></returns>
        private int GetTypeByExt(string f)
        {
            switch (Path.GetExtension(f))
            {   
                case ".zon": return C.i_zone;
                case ".wld": return C.i_room;
                case ".mob": return C.i_mob;
                case ".obj": return C.i_obj;
                case ".shp": return C.i_shop;
                case ".hog": return C.i_hogvisual;
                default: return -1;
            }
        }

        private void SetFileName()
        {
            filename = Dialogs.TextboxDialog("Specifica il nome deila Zona", "Nome del file (senza directory)");
        }

        public bool OpenAreaFiles(string fname, bool directload = false, bool newzone = false)
        {
            if (Modified && ConfirmSave() == DialogResult.Cancel)
                return false;

            filename = fname;

            if (fname == "")
                return false;

            string s = Path.GetFileNameWithoutExtension(fname);

            for (int i = 0; i < Options.max_dirs; i++)
                // set the base filenames..
                if (File.Exists(Path.GetDirectoryName(fname) + "\\" + s + C.file_ext[i]))
                    filenames[i] = Path.GetDirectoryName(fname) + "\\" + s + C.file_ext[i];
                else if ((!directload || Options.data.check_samedir) && File.Exists(Options.data.directories[i] + "\\" + s + C.file_ext[i]))
                    filenames[i] = Options.data.directories[i] + "\\" + s + C.file_ext[i];
                else filenames[i] = "";

            // .. then overwrite the loaded one
            if (GetTypeByExt(fname) > -1)
                filenames[GetTypeByExt(fname)] = fname;

            FileManager.LoadWld(Data, filenames[C.i_room]);
            FileManager.LoadMob(Data, filenames[C.i_mob]);
            FileManager.LoadObj(Data, filenames[C.i_obj]);
            FileManager.LoadShop(Data, filenames[C.i_shop]);
            if (FileManager.LoadZon(Data.zones, filenames[C.i_zone], true) == IOResult.FileCorrupted)
            {
                MessageBox.Show("Non è stato possibile aprire il file .ZON (formato non corretto)\n" + filenames[C.i_zone]);
                filenames[C.i_zone] = "";
                //Close();
                //return false;
            }
            FileManager.LoadHog(Data, filenames[C.i_hogvisual]);
            Data.PostLoad(Data);

            if (newzone)
                Data.Create<Zone>();

            directsave = directload;

            RefreshSelectedList();
            Modified = false;
            return true;
        }

        private DialogResult ConfirmSave()
        {
            DialogResult result = MessageBox.Show(Dialogs.msg_confirmsave, "Conferma", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
                btnsave_Click(null, null);

            return result;
        }

        private void SaveFile(int i)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filenames[i])))
                Directory.CreateDirectory(Path.GetDirectoryName(filenames[i]));

            switch (i)
            {
                case C.i_zone: 
                    FileManager.SaveZon(Data.zones, filenames[i]);
                    break;
                case C.i_room:
                    FileManager.SaveWld(Data.rooms, filenames[i]);
                    break;
                case C.i_mob:
                    FileManager.SaveMob(Data.mobs, filenames[i]);
                    break;
                case C.i_obj:
                    FileManager.SaveObj(Data.objects, filenames[i]);
                    break;
                case C.i_shop:
                    FileManager.SaveShop(Data.shops, filenames[i]);
                    break;
                case C.i_hogvisual:
                    FileManager.SaveHog(Data, filenames[i]);
                    break;
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            int index = sender == mnsavesetdirs ? 1 : 0;
            string dir = "";
            string fname = filename;

            bool save_hogfile = !Options.data.dontsave_hogfile;

            switch (index)
            {
                // same dir
                case 0:
                    bool filenameset = false;

                    // the user chose to "Save as.."
                    if (sender == mnsavechoosedir)
                    {
                        using (SaveFileDialog sfd = new SaveFileDialog())
                        {
                            sfd.Filter = "Area files|*.zon;*.wld;*.mob;*.obj;*.shp";
                            sfd.Title = "Specifica dove salvare i file dell'area";
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                fname = Path.GetFileNameWithoutExtension(sfd.FileName);
                                dir = Path.GetDirectoryName(sfd.FileName);
                                Path.GetFileNameWithoutExtension(sfd.FileName);
                                filenameset = true;
                                directsave = true;
                            }
                            else return;
                        }
                    }
                    else // else find a suitable directory and filename for a direct save
                        for (int i = 0; i < Options.max_dirs; i++)
                        {
                            switch (i)
                            {
                                case C.i_zone: if (Data.zones.Count <= 0) continue; break;
                                case C.i_room: if (Data.rooms.Count <= 0) continue; break;
                                case C.i_hogvisual: if (!save_hogfile || (Data.rooms.Count <= 0 && Data.objects.Count <= 0)) continue; break;
                                case C.i_mob: if (Data.mobs.Count <= 0) continue; break;
                                case C.i_obj: if (Data.objects.Count <= 0) continue; break;
                                case C.i_shop: if (Data.shops.Count <= 0) continue; break;
                            }

                            if (File.Exists(filenames[i]))
                            {
                                dir = Path.GetDirectoryName(filenames[i]);
                                fname = Path.GetFileNameWithoutExtension(filenames[i]);
                                filenameset = true;
                                break;
                            }
                        }

                    // go for it
                    for (int i = 0; i < Options.max_dirs; i++)
                    {
                        switch (i)
                        {
                            case C.i_zone: if (Data.zones.Count <= 0) continue; break;
                            case C.i_room: if (Data.rooms.Count <= 0) continue; break;
                            case C.i_hogvisual: if (!save_hogfile || (Data.rooms.Count <= 0 && Data.objects.Count <= 0)) continue; break;
                            case C.i_mob: if (Data.mobs.Count <= 0) continue; break;
                            case C.i_obj: if (Data.objects.Count <= 0) continue; break;
                            case C.i_shop: if (Data.shops.Count <= 0) continue; break;
                        }

                        // if we didn't find a suitable directory or filename then ask for it
                        if (!filenameset)
                            using (SaveFileDialog sfd = new SaveFileDialog())
                            {
                                sfd.Filter = "Area file|*" + C.file_ext[i];
                                sfd.Title = "Specifica il nome dell'area";
                                if (sfd.ShowDialog() == DialogResult.OK)
                                {
                                    fname = Path.GetFileNameWithoutExtension(sfd.FileName);
                                    filenames[i] = sfd.FileName;
                                    dir = Path.GetDirectoryName(sfd.FileName);
                                    Path.GetFileNameWithoutExtension(sfd.FileName);
                                    filenameset = true;
                                }
                                else return;
                            }

                        if (filenames[i] == "" || sender == mnsavechoosedir)
                        {
                            if (directsave)
                                filenames[i] = dir + "\\" + fname + C.file_ext[i];
                            else filenames[i] = (Options.data.directories[i] != "" ? Options.data.directories[i] : Path.GetDirectoryName(Application.ExecutablePath))
                                                 + "\\" + fname + C.file_ext[i];
                        }
                        SaveFile(i);
                    }

                    break;

                // set directories
                case 1:
                    fname = filename;
                    if (fname == "")
                        SetFileName();

                    if (fname == "")
                        return;

                    fname = Path.GetFileNameWithoutExtension(fname);

                    for (int i = 0; i < Options.max_dirs; i++)
                    {
                        switch (i)
                        {
                            case C.i_zone: if (Data.zones.Count <= 0) continue; break;
                            case C.i_room: if (Data.rooms.Count <= 0) continue; break;
                            case C.i_hogvisual: if (!save_hogfile || (Data.rooms.Count <= 0 && Data.objects.Count <= 0)) continue; break;
                            case C.i_mob: if (Data.mobs.Count <= 0) continue; break;
                            case C.i_obj: if (Data.objects.Count <= 0) continue; break;
                            case C.i_shop: if (Data.shops.Count <= 0) continue; break;
                        }

                        filenames[i] = (Options.data.directories[i] != "" ? Options.data.directories[i] : Path.GetDirectoryName(Application.ExecutablePath))
                                       + "\\" + fname + C.file_ext[i];
                        SaveFile(i);
                    }

                    break;
            }
            UpdateText();
            Modified = false;
        }


        private void fileZonazonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32((sender as ToolStripMenuItem).Tag);
            if (Data.RetrieveList(index).Length <= 0)
                return;

            string ext = C.file_ext[index];

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "File " + ext + "|*" + ext;
                sfd.Title = "Esporta file " + ext;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filenames[index] = sfd.FileName;
                    SaveFile(index);
                }
                else return;
            }
        }
        #endregion

        #region HTML Export
        private void ExportToHTML(int i)
        {
            using (dlg_ExportElements dlg = new dlg_ExportElements())
            {
                //temporaneo
                switch (i)
                {
                    case C.i_obj: dlg.FillList<Obj>(Data.objects); break;
                    default: return;
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "Html file|*.html";
                        sfd.Title = "Esporta in Html";
                        if (sfd.ShowDialog() == DialogResult.OK)
                            switch (i)
                            {
                                case C.i_obj: FileManager.ObjToHTML(Data, sfd.FileName, dlg.result); return;
                                default: return;
                            }
                    }
            }
        }
        #endregion

        #region Methods

        public bool HasFilename(string f)
        {
            foreach (string s in filenames)
                if (s == f)
                    return true;
            return false;
        }

        #region Add
        private void CreateElement(object sender, EventArgs e)
        {
            var checkedButton = radiogroup.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            int i = checkedButton.TabIndex;

            area_element element = null;

            switch (i)
            {
                case C.i_zone:
                    element = Data.Create<Zone>();
                    break;
                case C.i_room:
                    element = Data.Create<Room>();
                    break;
                case C.i_mob:
                    element = Data.Create<Mob>();
                    break;
                case C.i_obj:
                    element = Data.Create<Obj>();
                    break;
                case C.i_shop:
                    element = Data.Create<Shop>();
                    break;
            }

            if (element == null)
                return;

            int vnum = element.vnum;
            if (defaults[i] != null)
                element.CopyFrom(defaults[i]);
            element.vnum = vnum;
            if (element.GetType() == typeof(Zone))
                (element as Zone).vnum_max = vnum * 100 + 99;

            ListViewItem item = list.AddItem(Data, element, true);

            if (item != null)
            {
                item.Selected = true;
                list.Focus();
                item.EnsureVisible();
            }
            Modified = true;
        }
        #endregion

        #region Edit
        public void EditSelected(object sender, EventArgs e)
        {
            if (list.SelectedIndices.Count <= 0)
                return;

            area_element selected = list.SelectedItems[0].Tag as area_element;

            if (selected == null)
                return;

            if (selected.Edit(Data, null))
            {
                RefreshSelectedList();
                Modified = true;
            }
        }
        #endregion

        #region Delete
        private void eliminaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list.SelectedIndices.Count <= 0)
                return;
            
            area_element selected = list.SelectedItems[0].Tag as area_element;
            Data.RemoveElement(selected);
            RefreshSelectedList();
            Modified = true;
        }
        #endregion

        #endregion

        #region Widgets' Methods
        private void rd_CheckedChanged(object sender, EventArgs e)
        {
            RefreshSelectedList();
        }

        private void impostaDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (dlg_setdirs form = new dlg_setdirs())
            {
                form.Data.CopyFrom(Options.data);
                if (form.ShowDialog() == DialogResult.OK)
                    Options.data.CopyFrom(form.Data);
            }
        }

        private void frm_List_Shown(object sender, EventArgs e)
        {
            RefreshSelectedList();
            list.SetComponents();
        }
        #endregion

        #region Utils Panel
        private bool utils_closed = true;
        private void utils_timer_Tick(object sender, EventArgs e)
        {
            if (pn_utils.Width < 135 && !utils_closed)
            {
                pn_utils.Width += 5;
            }
            else if (pn_utils.Width > 10 && utils_closed)
            {
                pn_utils.Width -= 5;
            }
            else
            {
                pn_utils.Visible = !utils_closed;
                t_panel.Enabled = false;
            }
        }

        private void btnutils_Click(object sender, EventArgs e)
        {
            // setup vnum enumerator panel
            pn_utils.Visible = true;
            //pn_utils.Width = 10;
            t_panel.Enabled = true;
            utils_closed = !utils_closed;
            btnenum.Checked = !utils_closed;
        }

        #region VNum Enumerator
        private void setVNumEnumBtnsEnabled(bool enabled)
        {
            spin_fromvnum.Enabled = enabled;
            spin_tovnum.Enabled = enabled;
            spin_newvnum.Enabled = enabled;
            chkrooms.Enabled = enabled;
            chkmobs.Enabled = enabled;
            chkobjects.Enabled = enabled;
            chkshops.Enabled = enabled;
            btnstartenum.Enabled = enabled;
        }

        private void btnstartenum_Click(object sender, EventArgs e)
        {
            spin_fromvnum.BackColor = Color.White;
            spin_tovnum.BackColor = Color.White;
            spin_newvnum.BackColor = Color.White;

            int from = Convert.ToInt32(spin_fromvnum.Value > spin_tovnum.Value ? spin_tovnum.Value : spin_fromvnum.Value);
            int to = Convert.ToInt32(spin_fromvnum.Value > spin_tovnum.Value ? spin_fromvnum.Value : spin_tovnum.Value);
            int newvnum = Convert.ToInt32(spin_newvnum.Value);
            
            setVNumEnumBtnsEnabled(false);

            if (chkrooms.Checked)
            {
                List<Room> itList = Data.rooms.ToList();

                foreach (Room el in itList.Where<Room>(x => x.vnum >= from && x.vnum <= to))
                {
                    int lastvnum = el.vnum;
                    int truevnum = newvnum + (el.vnum - from);

                    if (chkDuplicate.Checked)
                    {
                        area_element duplicateRoom = null;
                        duplicateRoom = Data.Create<Room>();
                        duplicateRoom.CopyFrom(el);
                        duplicateRoom.vnum = truevnum;
                        Data.UpdateVNumReferences<Room>(el.vnum, duplicateRoom.vnum, true);
                    }
                    else
                    {
                        if (Data.Get<Room>(truevnum) != null)
                        {
                            Data.Get<Room>(truevnum).vnum = -1;
                            Data.UpdateVNumReferences<Room>(truevnum, -1, true);
                        }

                        el.vnum = truevnum;
                        Data.UpdateVNumReferences<Room>(lastvnum, el.vnum);
                    }
                }

                while (Data.Get<Room>(-1) != null)
                    Data.rooms.Remove(Data.Get<Room>(-1));
            }

            if (chkmobs.Checked)
            {
                foreach (Mob el in Data.mobs.Where<Mob>(x => x.vnum >= from && x.vnum <= to))
                {
                    int lastvnum = el.vnum;
                    int truevnum = newvnum + (el.vnum - from);

                    if (Data.Get<Mob>(truevnum) != null)
                    {
                        Data.Get<Mob>(truevnum).vnum = -1;
                        Data.UpdateVNumReferences<Mob>(truevnum, -1, true);
                    }

                    el.vnum = truevnum;
                    Data.UpdateVNumReferences<Mob>(lastvnum, el.vnum);
                }

                while (Data.Get<Mob>(-1) != null)
                    Data.mobs.Remove(Data.Get<Mob>(-1));
            }

            if (chkobjects.Checked)
            {
                foreach (Obj el in Data.objects.Where<Obj>(x => x.vnum >= from && x.vnum <= to))
                {
                    int lastvnum = el.vnum;
                    int truevnum = newvnum + (el.vnum - from);

                    if (Data.Get<Obj>(truevnum) != null)
                    {
                        Data.Get<Obj>(truevnum).vnum = -1;
                        Data.UpdateVNumReferences<Obj>(truevnum, -1, true);
                    }

                    el.vnum = truevnum;
                    Data.UpdateVNumReferences<Obj>(lastvnum, el.vnum);
                }

                while (Data.Get<Obj>(-1) != null)
                    Data.objects.Remove(Data.Get<Obj>(-1));
            }

            if (chkshops.Checked)
            {
                foreach (Shop el in Data.shops.Where<Shop>(x => x.vnum >= from && x.vnum <= to))
                {
                    int lastvnum = el.vnum;
                    int truevnum = newvnum + (el.vnum - from);

                    if (Data.Get<Shop>(truevnum) != null)
                    {
                        Data.Get<Shop>(truevnum).vnum = -1;
                        Data.UpdateVNumReferences<Shop>(truevnum, -1, true);
                    }

                    el.vnum = truevnum;
                    Data.UpdateVNumReferences<Shop>(lastvnum, el.vnum);
                }

                while (Data.Get<Shop>(-1) != null)
                    Data.shops.Remove(Data.Get<Shop>(-1));
            }

            setVNumEnumBtnsEnabled(true);
            RefreshSelectedList();
        }
        #endregion

        private void spin_fromvnum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                ProcessTabKey(true);
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        private void refresh_event(object sender, EventArgs e)
        {
            RefreshSelectedList();
            Modified = true;
        }

        private void btnmapclick(object sender, EventArgs e)
        {
            if (frm_editvis != null && frm_editvis.Visible)
            {
                frm_editvis.BringToFront();
                return;
            }

            frm_editvis = new frm_AreaMap();
            frm_editvis.areachanged += new EventHandler(refresh_event);
            frm_editvis.Data = Data;
            frm_editvis.Show();
        }

        private void frm_List_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Modified && ConfirmSave() == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            if (frm_editvis != null)
            {
                frm_editvis.Close();
                frm_editvis.Dispose();
            }

            if (frm_reports != null)
            {
                frm_reports.Close();
                frm_reports.Dispose();
            }

            if (templatesmode)
                Options.LoadTemplates();
        }

        private void btnreportsclick(object sender, EventArgs e)
        {
            if (frm_reports != null && frm_reports.Visible)
            {
                frm_reports.BringToFront();
                return;
            }

            frm_reports = new frm_Reports();
            frm_reports.areachanged += new EventHandler(refresh_event);
            frm_reports.ParentArea = Data;
            frm_reports.Show();
        }

        private void noselButton1_Click_2(object sender, EventArgs e)
        {
            popupsave.Show(MousePosition);
        }

        private void btnaddsel_Click(object sender, EventArgs e)
        {
            var checkedButton = radiogroup.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            int i = checkedButton.TabIndex;

            using (dlg_select_element form = new dlg_select_element())
            {
                switch (i)
                {
                    case C.i_zone:
                        form.SetElements<Zone>(Data, i, Options.data.Templates.zones);
                        break;
                    case C.i_room:
                        form.SetElements<Room>(Data, i, Options.data.Templates.rooms);
                        break;
                    case C.i_mob:
                        form.SetElements<Mob>(Data, i, Options.data.Templates.mobs);
                        break;
                    case C.i_obj:
                        form.SetElements<Obj>(Data, i, Options.data.Templates.objects);
                        break;
                    case C.i_shop:
                        form.SetElements<Shop>(Data, i, Options.data.Templates.shops);
                        break;
                }
                
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    defaults[i] = form.result as area_element;
                }
            }
            UpdateTemplateButton(i);
        }

        private void noselButton1_Click(object sender, EventArgs e)
        {
            using (dlg_setdirs form = new dlg_setdirs())
            {
                form.Data.CopyFrom(Options.data);
                if (form.ShowDialog() == DialogResult.OK)
                    Options.data.CopyFrom(form.Data);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = radiogroup.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).TabIndex;
            defaults[i] = null;
            UpdateTemplateButton(i);
        }

        private void btnclone_Click(object sender, EventArgs e)
        {
            if (list.SelectedIndices.Count <= 0)
                return;

            var checkedButton = radiogroup.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            int i = checkedButton.TabIndex;

            area_element element = null;

            switch (i)
            {
                case C.i_zone:
                    element = Data.Create<Zone>();
                    break;
                case C.i_room:
                    element = Data.Create<Room>();
                    break;
                case C.i_mob:
                    element = Data.Create<Mob>();
                    break;
                case C.i_obj:
                    element = Data.Create<Obj>();
                    break;
                case C.i_shop:
                    element = Data.Create<Shop>();
                    break;
            }

            if (element == null)
                return;

            area_element selected = list.SelectedItems[0].Tag as area_element;

            int vnum = element.vnum;
            element.CopyFrom(selected);
            element.vnum = vnum;
            if (element.GetType() == typeof(Zone))
                (element as Zone).vnum_max = vnum * 100 + 99;

            ListViewItem item = list.AddItem(Data, element, true);

            item.Selected = true;
            item.EnsureVisible();
            Modified = true;
        }

        private void edittemplates_Click(object sender, EventArgs e)
        {
            Options.CheckTemplateFiles();
            string filename = Path.GetDirectoryName(Application.ExecutablePath) + "\\templates\\templates.zon";

            frm_List form = null;

            // if the zone is already opened we don't need to create a new form
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(frm_List) && (f as frm_List).HasFilename(filename))
                    form = (f as frm_List);
            }

            if (form == null)
                form = new frm_List();
            form.OpenAreaFiles(filename, true);
            form.SetTemplatesMode();
            form.Show();
            form.BringToFront();
        }

        private void oggettiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToHTML(Convert.ToInt32((sender as ToolStripMenuItem).Tag));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Data.rooms.Count <= 0)
            {
                MessageBox.Show("L'area non ha stanze!");
                return;
            }

            using (frm_MudPreview frm = new frm_MudPreview())
            {
                Room r = list.SelectedItems.Count > 0 ? list.SelectedItems[0].Tag as Room : null;
                frm.SetArea(Data, r);
                frm.ShowDialog();
            }
        }
    }
}
