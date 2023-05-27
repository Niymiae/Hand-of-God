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
    public partial class frm_Exit : Form
    {
        public Area ParentArea;
        public Room ParentRoom;
        public Exit Data;
        bool modified;

        #region Constructor
        public frm_Exit()
        {
            InitializeComponent();
            utils.AddFormEvents(Controls, SetModified);
            Data = new Exit(null);

            gaugecharcounter.AddEvents(memo_desc);

            combo_type.Items.Clear();
            for (int i = 0; i <= 6; i++)
                combo_type.Items.Add(L.directions[i]);

            combo_dooract.Items.Clear();
            for (int i = 0; i <= C.cmd_end; i++)
                combo_dooract.Items.Add(L.door_commands[i]);

            list_flags.Items.Clear();
            for (int i = 0; i <= C.df_end; i++)
                list_flags.Items.Add(L.door_flags[i]);

            combo_doorstatus.Items.Clear();
            for (int i = 0; i <= 2; i++)
                combo_doorstatus.Items.Add(L.door_status[i]);
        }
        #endregion
        #region *Modified* Status
        private void SetModified(object sender, EventArgs e)
        {
            btnapply.Enabled = true;
            modified = true;
            frm_Exit_Shown(null, null);
        }

        public void SetNotModified()
        {
            btnapply.Enabled = false;
            modified = false;
            frm_Exit_Shown(null, null);
        }

        private void frm_Exit_Shown(object sender, EventArgs e)
        {
            Text = "Uscita " + L.directions[Data.dir] + (modified ? " *modificata*" : "");
        }
        #endregion

        #region Widgets <-> Data Transfers
        public void Widgets2Data()
        {
            //Data.area ?
            Data.dir = combo_type.SelectedIndex;
            Data.room = Convert.ToInt32(edt_toroom.Value);
            Data.name = edt_name.Text;
            Data.str_to = edt_to.Text;
            Data.str_from = edt_from.Text;
            Data.nameinlist = edt_nameinlist.Text;
            Data.inverse = edt_inverse.Text;
            Data.door.keys = edt_doorkeys.Text;
            Data.door.objkey = Convert.ToInt32(edt_doorobjkey.Value);
            Data.door.status = combo_doorstatus.SelectedIndex;
            Data.door.cmd = combo_dooract.SelectedIndex;
            Data.doorDescription = edt_doorDesc.Text;
            Data.openToChar = edt_OpenChar.Text;
            Data.closeToChar = edt_CloseChar.Text;
            Data.openToRoom = edt_OpenRoom.Text;
            Data.closeToRoom = edt_CloseRoom.Text;

            Data.pickDiff = Convert.ToInt32(edt_PickCheck.Value);
            Data.bashDiff = Convert.ToInt32(edt_BashCheck.Value);
            Data.climbDiff = Convert.ToInt32(edt_ClimbCheck.Value);
            Data.knockDiff = Convert.ToInt32(edt_KnockCheck.Value);
            Data.percDiff = Convert.ToInt32(edt_PercCheck.Value);
            Data.strDiff = Convert.ToInt32(edt_StrCheck.Value);
            Data.dexDiff = Convert.ToInt32(edt_DexCheck.Value);
            Data.intDiff = Convert.ToInt32(edt_IntCheck.Value);
            Data.wisDiff = Convert.ToInt32(edt_WisCheck.Value);
            Data.conDiff = Convert.ToInt32(edt_ConCheck.Value);
            Data.chrDiff = Convert.ToInt32(edt_ChrCheck.Value);

            Data.desc = memo_desc.Text;

            // flags
            for (int i = 0; i <= C.df_end; i++)
                Data.flags[1 << i] = list_flags.GetItemCheckState(i) == CheckState.Checked ? true : false;
        }

        public void Data2Widgets()
        {
            combo_type.SelectedIndex = Data.dir;
            edt_toroom.Value = Data.room;
            edt_name.Text = Data.name;
            edt_to.Text = Data.str_to;
            edt_from.Text = Data.str_from;
            edt_nameinlist.Text = Data.nameinlist;
            edt_inverse.Text = Data.inverse;
            edt_doorkeys.Text = Data.door.keys;
            edt_doorobjkey.Value = Data.door.objkey;
            combo_doorstatus.SelectedIndex = Data.door.status;
            combo_dooract.SelectedIndex = Data.door.cmd;
            memo_desc.Text = Data.desc;
            edt_doorDesc.Text = Data.doorDescription;
            edt_OpenChar.Text = Data.openToChar;
            edt_OpenRoom.Text = Data.openToRoom;
            edt_CloseChar.Text = Data.closeToChar;
            edt_CloseRoom.Text = Data.closeToRoom;
            edt_PickCheck.Value = Data.pickDiff;
            edt_BashCheck.Value = Data.bashDiff;
            edt_ClimbCheck.Value = Data.climbDiff;
            edt_KnockCheck.Value = Data.knockDiff;
            edt_PercCheck.Value = Data.percDiff;
            edt_StrCheck.Value = Data.strDiff;
            edt_DexCheck.Value = Data.dexDiff;
            edt_IntCheck.Value = Data.intDiff;
            edt_WisCheck.Value = Data.wisDiff;
            edt_ConCheck.Value = Data.conDiff;
            edt_ChrCheck.Value = Data.chrDiff;

            // flags
            for (int i = 0; i <= C.df_end; i++)
                list_flags.SetItemCheckState(i, Data.flags[1 << i] ? CheckState.Checked : CheckState.Unchecked);
        }
        #endregion

        #region Methods
        private void UpdateButtonsState(object sender, EventArgs e)
        {
            // to room
            Room r = ParentArea.Get<Room>(Convert.ToInt32(edt_toroom.Value));
            lbl_roomshort.Text = r == null ? "<stanza non trovata>" : r.shortdesc;

            if (r == null/* || r.GetExit(utils.GetOppositeDirection(combo_type.SelectedIndex), edt_name.Text) != null*/)
                btninverse.Enabled = false;
            else btninverse.Enabled = true;

            // key object
            Obj o = ParentArea.Get<Obj>(Convert.ToInt32(edt_doorobjkey.Value));
            lbl_keyshort.Text = edt_doorobjkey.Value > 0 ? (o == null ? "<oggetto non trovato>" : o.shortdesc) :
                                (edt_doorobjkey.Value == -1 ? "[Nessuna Serratura]" : "[Serratura senza Chiave]");

            // special exit values
            bool enabled = combo_type.SelectedIndex == C.dir_special;
            edt_name.Enabled = enabled;
            edt_nameinlist.Enabled = enabled;
            edt_to.Enabled = enabled;
            edt_from.Enabled = enabled;
            edt_inverse.Enabled = enabled;
        }
        #endregion

        #region Widgets' Methods
        private void button1_Click(object sender, EventArgs e)
        {
            dlg_select_element form = new dlg_select_element();
            form.SetElements<Room>(ParentArea, C.i_room, ParentArea.rooms, true);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.result == null)
                    edt_toroom.Value = -1;
                else
                    edt_toroom.Value = (form.result.GetType() == typeof(Room) ? (form.result as Room).vnum : (int)form.result);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int vnum = 0;
            area_element r = null;

            if (sender == btneditroom)
            {
                vnum = Convert.ToInt32(edt_toroom.Value);
                r = ParentArea.rooms.Find(x => x.vnum == vnum);
            }
            else
            {
                vnum = Convert.ToInt32(edt_doorobjkey.Value);
                r = ParentArea.objects.Find(x => x.vnum == vnum);
            }

            if (r == null)
                return;

            r.Edit(ParentArea, null, false, true);
            UpdateButtonsState(sender, e);
        }

        private void btninverse_Click(object sender, EventArgs e)
        {
            Room r = ParentArea.Get<Room>(Convert.ToInt32(edt_toroom.Value));
            Exit ex = r.GetExit(utils.GetOppositeDirection(combo_type.SelectedIndex), edt_inverse.Text);
            if (r != null)
            {
                if (ex == null)
                    ex = r.SetExit(ParentRoom, ParentRoom.vnum, utils.GetOppositeDirection(combo_type.SelectedIndex), false);
                else if (!Dialogs.Confirm("Sovrascrivere l'uscita inversa già esistente?"))
                    return;
            }
            UpdateButtonsState(sender, e);

            if (ex == null)
                return;

            ex.door.keys = edt_doorkeys.Text;
            ex.door.objkey = Convert.ToInt32(edt_doorobjkey.Value);
            ex.door.cmd = combo_dooract.SelectedIndex;
            ex.door.status = combo_doorstatus.SelectedIndex;
            ex.desc = memo_desc.Text;

            ex.doorDescription = edt_doorDesc.Text;
            ex.openToChar = edt_OpenChar.Text;
            ex.closeToChar = edt_CloseChar.Text;
            ex.openToRoom = edt_OpenRoom.Text;
            ex.closeToRoom = edt_CloseRoom.Text;

            ex.pickDiff = Convert.ToInt32(edt_PickCheck.Value);
            ex.bashDiff = Convert.ToInt32(edt_BashCheck.Value);
            ex.climbDiff = Convert.ToInt32(edt_ClimbCheck.Value);
            ex.knockDiff = Convert.ToInt32(edt_KnockCheck.Value);
            ex.percDiff = Convert.ToInt32(edt_PercCheck.Value);

            ex.strDiff = Convert.ToInt32(edt_StrCheck.Value);
            ex.dexDiff = Convert.ToInt32(edt_DexCheck.Value);
            ex.intDiff = Convert.ToInt32(edt_IntCheck.Value);
            ex.wisDiff = Convert.ToInt32(edt_WisCheck.Value);
            ex.conDiff = Convert.ToInt32(edt_ConCheck.Value);
            ex.chrDiff = Convert.ToInt32(edt_ChrCheck.Value);
            
            for (int i = 0; i <= C.df_end; i++)
                ex.flags[1 << i] = list_flags.GetItemChecked(i);

            ex.name = edt_inverse.Text;
            ex.nameinlist = edt_nameinlist.Text;
            ex.inverse = edt_name.Text;
            ex.str_to = edt_from.Text;
            ex.str_from = edt_to.Text;

            //if (ex.GetDoorInit(ParentArea) == null)
            ex.SetDoorInit(ParentArea);

            timer1.Enabled = true;
            lblinverse.Visible = true;
        }

        private void btnrestore_Click(object sender, EventArgs e)
        {
            Data2Widgets();
            UpdateButtonsState(sender, e);
            SetNotModified();
        }

        private void btnapply_Click(object sender, EventArgs e)
        {
            Widgets2Data();
            SetNotModified();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (list_flags.GetItemChecked(C.df_door) &&
               ((combo_type.SelectedIndex == C.dir_special && edt_name.Text == "") || edt_doorkeys.Text == ""))
                Dialogs.Warning("I campi 'Parole Chiave' e 'Nome (Univoco)' (per uscite speciali) sono obbligatori con il flag 'isDoor'!");
            else
                DialogResult = DialogResult.OK;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                ProcessTabKey(true);
                e.SuppressKeyPress = true;
            }
        }

        private void list_flags_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox list = sender as CheckedListBox;
            list.ClearSelected();

            // exits can only be invisible if they are already flagged secret
            // if (list.GetItemCheckState(C.df_invisible) == CheckState.Checked && list.GetItemCheckState(C.df_secret) == CheckState.Unchecked)
            //     list.SetItemCheckState(C.df_invisible, CheckState.Unchecked);
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            dlg_select_element form = new dlg_select_element();
            form.SetElements<Obj>(ParentArea, C.i_obj, ParentArea.objects, true);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.result == null)
                    edt_doorobjkey.Value = -1;
                else
                    edt_doorobjkey.Value = (form.result.GetType() == typeof(Obj) ? (form.result as Obj).vnum : (int)form.result);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblinverse.Visible = false;
            timer1.Enabled = false;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
