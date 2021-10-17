using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HandofGod
{
    public partial class frm_Room : frm_area_element
    {
        public Room Data { get { return data as Room; } set { data = value; } }
        public Room Original { get { return original as Room; } set { original = value; } }
        private List<CheckBox> chks_flags;

        #region Constructor
        public frm_Room()
        {
            title = "Stanza";
            Data = new Room();
            InitializeComponent();
            utils.AddFormEvents(Controls, SetModified);
            gaugecharcounter.AddEvents(memo_longdesc);
            gaugecharcounter.AddEvents(memo_daydesc);
            gaugecharcounter.AddEvents(memo_nightdesc);
            gaugecharcounter.AddEvents(memo_extradesc);
            edt_shortdesc.DefaultCharacterColor = Color.Cyan;

            pn_watercurrent.Hide();
            pn_teleport.Hide();

            combo_watercurrent_dir.Items.Clear();
            for (int i = 0; i <= C.dir_down; i++)
                combo_watercurrent_dir.Items.Add(L.directions[i]);

            combo_sect.Items.Clear();
            combo_tel_sect.Items.Clear();
            foreach (string s in L.room_sectors)
            {
                combo_sect.Items.Add(s);
                if (s != L.room_sectors[C.rs_teleport]) // careful! it works only because teleport is the last one
                    combo_tel_sect.Items.Add(s);
            }

            for (int i = 0; i <= C.tf_end; i++)
                GetTeleportCheckbox(i).Text = L.teleport_flags[i];



            chks_flags = new List<CheckBox>();
            for (int i = 0; i <= C.rf_end; i++)
            {
                CheckBox chk = tabcontrol_flags.Controls.Find("flg" + i, true)[0] as CheckBox;
                chk.Text = L.room_flags[i];
                chks_flags.Add(chk);
            }

            list_exits.SetColumns(C.i_exit);
            list_exits.Sorting = SortOrder.None;
            (list_exits.ListViewItemSorter as Sorter).Order = SortOrder.None;
        }
        #endregion
        
        #region Methods
        private void UpdateButtonsState()
        {
            if (combo_sect.SelectedIndex == C.rs_teleport)
            {
                pn_watercurrent.Hide();
                pn_teleport.Show();
            }
            else
            {
                if (combo_sect.SelectedIndex == C.rs_waternoswim || combo_sect.SelectedIndex == C.rs_underwater)
                    pn_watercurrent.Show();
                else pn_watercurrent.Hide();
                pn_teleport.Hide();
            }

            lbl_maxpc.Enabled = chks_flags[C.rf_tunnel].Checked;
            spin_maxpc.Enabled = chks_flags[C.rf_tunnel].Checked;
            lbl_maxobj.Enabled = chks_flags[C.rf_saveroom].Checked;
            spin_maxobj.Enabled = chks_flags[C.rf_saveroom].Checked;

            if (list_exits.SelectedIndices.Count > 0)
            {
                Exit x = list_exits.SelectedItems[0].Tag as Exit;
                btndoorinit.BackColor = x.GetDoorInit(ParentArea) != null ? Color.LightGreen : Color.LightCoral;
            }
            else btndoorinit.BackColor = Color.LightCoral;
        }

        private CheckBox GetTeleportCheckbox(int i)
        {
            return (Controls.Find("chk_tel_flag" + i.ToString(), true)[0] as CheckBox);
        }
        #endregion

        #region Widgets <-> Data Transfers
        public override void Data2Widgets()
        {
            base.Data2Widgets();
           // short desc
            edt_shortdesc.SetText(Data.shortdesc);
           // sector
            combo_sect.SelectedIndex = Data.sect;

            // long desc
            memo_longdesc.SetText(Data.longdesc);
            // day desc
            memo_daydesc.SetText(Data.daydesc);
            // night desc
            memo_nightdesc.SetText(Data.nightdesc);

            spin_area.Value = Data.zone;
            spin_maxobj.Value = Data.max_obj;
            spin_maxpc.Value = Data.max_pc;
            spin_watercurrent_vel.Value = Data.water_current_vel;
            combo_watercurrent_dir.SelectedIndex = Data.water_current_dir;                

            // teleport flags
            edt_tel_counter.Value = Data.tel_counter;
            edt_tel_time.Value = Data.tel_time;
            edt_tel_toroom.Value = Data.tel_toroom;
            combo_tel_sect.SelectedIndex = Data.tel_sect;
            for (int i = 0; i <= C.tf_end; i++)
                GetTeleportCheckbox(i).Checked = Data.tel_flags[1 << i];

            edt_extradesc.Text = "";
            memo_extradesc.SetText("");
            RefreshExtraDescListBox();

           // exits
            foreach (Exit e in Data.exits)
                e.SetDoorStatus(ParentArea);
            list_exits.AddToList(ParentArea, Data.exits, false, true);

           // flags
            for (int i = 0; i <= C.rf_end; i++)
                chks_flags[i].Checked = Data.flags[1 << i];

            UpdateButtonsState();
        }

        public override void Widgets2Data()
        {
            base.Widgets2Data();

           // short desc
            Data.shortdesc = edt_shortdesc.GetText();
           // sector
            Data.sect = combo_sect.SelectedIndex;
           // long desc
            Data.longdesc = memo_longdesc.GetText();
           // area
            Data.zone = Convert.ToInt32(spin_area.Value);
           // day desc
            Data.daydesc = memo_daydesc.GetText();
           // night desc
            Data.nightdesc = memo_nightdesc.GetText();

            Data.max_obj = Convert.ToInt32(spin_maxobj.Value);
            Data.max_pc = Convert.ToInt32(spin_maxpc.Value);
            Data.water_current_vel = Convert.ToInt32(spin_watercurrent_vel.Value);
            Data.water_current_dir = combo_watercurrent_dir.SelectedIndex;

           // teleport flags
            Data.tel_counter = Convert.ToInt32(edt_tel_counter.Value);
            Data.tel_time = Convert.ToInt32(edt_tel_time.Value);
            Data.tel_toroom = Convert.ToInt32(edt_tel_toroom.Value);
            Data.tel_sect = combo_tel_sect.SelectedIndex;
            for (int i = 0; i <= C.tf_end; i++)
                Data.tel_flags[1 << i] = GetTeleportCheckbox(i).Checked;

            // modified extradesc notification
            if (extra_modified && MessageBox.Show("Salvare la Extra Description modificata?", "Attenzione", MessageBoxButtons.YesNo) == DialogResult.Yes)
                btn_extradescAdd_Click(null, null);

            // set the exits' parent in case of vnum change
            foreach (Exit e in Data.exits)
                e.parent = Original;

           // flags
            for (int i = 0; i <= C.rf_end; i++)
                Data.flags[1 << i] = chks_flags[i].Checked;
        }
        #endregion

        #region Extra Desc
        bool extra_modified;

        private void RefreshExtraDescListBox()
        {
            int i = list_extradesc.SelectedIndex;

            extra_modified = false;

            list_extradesc.Items.Clear();
            foreach (ExtraDesc ed in Data.extras)
                list_extradesc.Items.Add(ed.keys);

            if (i >= 0 && i < list_extradesc.Items.Count)
                list_extradesc.SelectedIndex = i;
        }

        private void btn_extradescAdd_Click(object sender, EventArgs e)
        {
            if (edt_extradesc.Text == "")
                return;

            ExtraDesc ed = Data.extras.Find(v => v.keys == edt_extradesc.Text);

            if (ed == null) // create a new extra desc
            {
                ed = new ExtraDesc();
                ed.keys = edt_extradesc.Text;
                ed.desc = memo_extradesc.GetText();

                Data.extras.Add(ed);
            }
            else // modify the existing extra desc
            {
                ed.desc = memo_extradesc.Text;
            }

            SetModified(sender, e);
            RefreshExtraDescListBox();
        }

        private void btn_extradescDel_Click(object sender, EventArgs e)
        {
            int i = list_extradesc.SelectedIndex;

            if (i == -1)
                return;

            Data.extras.Remove(Data.extras[i]);
            edt_extradesc.Text = "";
            memo_extradesc.SetText("");
            SetModified(sender, e);
            RefreshExtraDescListBox();
        }
        
        private void list_extradesc_SelectedValueChanged(object sender, EventArgs e)
        {
            if (list_extradesc.SelectedItem == null)
                return;

            // modified extradesc notification
            if (extra_modified && MessageBox.Show("Salvare la Extra Description modificata?", "Attenzione", MessageBoxButtons.YesNo) == DialogResult.Yes)
                btn_extradescAdd_Click(null, null);
            

            tabControl1.SelectedIndex = 3;
            edt_extradesc.Text = "";
            memo_extradesc.SetText("");
            edt_extradesc.Text = list_extradesc.SelectedItem.ToString();
            ExtraDesc ed = Data.extras.Find(v => v.keys == edt_extradesc.Text);

            if (ed != null)
                memo_extradesc.SetText(ed.desc.TrimEnd());
            extra_modified = false;
        }

        private void memo_extradesc_TextChanged(object sender, EventArgs e)
        {
            extra_modified = true;
        }
        #endregion

        #region Widgets' Methods
        private void flg13_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }

        private void btn_followexit_Click(object sender, EventArgs e)
        {
            if (list_exits.SelectedItems.Count <= 0)
                return;

            Exit ex = list_exits.SelectedItems[0].Tag as Exit;
            Room room = ParentArea.Get<Room>(ex.room);

            if (room != null)
                ChangeElement(room);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dlg_select_element dlg = new dlg_select_element();
            dlg.SetElements<Room>(ParentArea, C.i_room, ParentArea.rooms, true);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.result == null)
                    edt_tel_toroom.Value = -1;
                else
                    edt_tel_toroom.Value = (dlg.result as Room).vnum;
            }
        }

        private void combo_sect_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }
        #endregion

        #region Colors
        /*private void pn_cols_Click(object sender, EventArgs e)
        {
            if (ActiveControl.GetType() != typeof(RichTextBox))
                return;

            RichTextBox tb = ActiveControl as RichTextBox;
            string insertText = "$c00" + (sender as Panel).Name.Replace("pn_col", "");
            int selectionIndex = tb.SelectionStart;
            tb.Text = tb.Text.Insert(selectionIndex, insertText);
            tb.SelectionStart = selectionIndex + insertText.Length;
        }*/
        #endregion

        #region Exits
        // add
        private void btn_addexit_Click(object sender, EventArgs e)
        {
            Exit x = new Exit(Original);
            x.dir = Data.GetFreeDir();
            Data.SetExit(null, -1, x.dir, false, x);
            if (x.Edit(ParentArea, Data,  true))
                SetModified(sender, e);

            list_exits.Items.Clear();
            list_exits.AddToList(ParentArea, Data.exits, false, true);
        }

        // edit
        private void btn_editexit_Click(object sender, EventArgs e)
        {
            if (list_exits.SelectedIndices.Count <= 0)
                return;

            Exit x = list_exits.SelectedItems[0].Tag as Exit;
            if (x.Edit(ParentArea, Data))
                SetModified(sender, e);

            list_exits.Items.Clear();
            list_exits.AddToList(ParentArea, Data.exits, false, true);
        }

        // delete
        private void btn_delexit_Click(object sender, EventArgs e)
        {
            if (list_exits.SelectedIndices.Count <= 0)
                return;

            Exit x = list_exits.SelectedItems[0].Tag as Exit;
            Data.exits.Remove(x);

            if (x.GetDoorInit(ParentArea) != null)
                x.RemoveDoorInit(ParentArea);

            SetModified(sender, e);

            list_exits.Items.Clear();
            list_exits.AddToList(ParentArea, Data.exits, false, true);
        }
        #endregion

        private void list_exits_MouseUp(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hti = list_exits.HitTest(e.X, e.Y);
            if (hti.Item != null && (Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                btn_followexit_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (list_exits.SelectedIndices.Count <= 0)
                return;

            Exit x = list_exits.SelectedItems[0].Tag as Exit;
            if (x.GetDoorInit(ParentArea) == null)
                x.SetDoorInit(ParentArea);
            else x.RemoveDoorInit(ParentArea);

            UpdateButtonsState();

            list_exits.Items.Clear();
            list_exits.AddToList(ParentArea, Data.exits, false, true);
        }

        private void list_exits_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }
    }
}
