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
    public partial class frm_Zone : frm_area_element
    {
        public Zone Data { get { return data as Zone; } set { data = value; } }
        public Zone Original { get { return original as Zone; } set { original = value; } }
        private List<CheckBox> chk_flags;
        private List<Control> edt_descriptions;
        private frm_Reports frm_reports;

        private struct animdata
        {
            public TextBox control;
            public Point dest,size;
        }

        // descriptions animation vars
        private List<animdata> anim_desc;

        #region Constructor
        public frm_Zone()
        {
            Data = new Zone();
            InitializeComponent();
            utils.AddFormEvents(Controls, SetModified);

            combo_repop.Items.Clear();
            foreach (string s in L.repop_types)
                combo_repop.Items.Add(s);

            chk_flags = new List<CheckBox>() { flg0, flg1, flg2, flg3, flg4, flg5, flg6 };
            edt_descriptions = new List<Control> { edt_desc0, edt_desc1, edt_desc2, edt_desc3, edt_desc4 };

            for (int i = 0; i <= C.zf_end; i++)
                chk_flags[i].Text = L.Get(L.zone_flags_complete, i);

            for (int i = 0; i <= C.zlr_end; i++)
                edt_desc2.Items.Add(L.Get(L.zone_level_ranges, i));

            edt_desc3.GotFocus += new EventHandler(quest_desc_gotfocus);
            edt_desc3.LostFocus += new EventHandler(quest_desc_lostfocus);
            edt_desc4.GotFocus += new EventHandler(quest_desc_gotfocus);
            edt_desc4.LostFocus += new EventHandler(quest_desc_lostfocus);
            anim_desc = new List<animdata>();

            pn_initdetails.Height = 0;

            list_inits.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        #endregion

        #region Widgets <-> Data Transfers
        public override void Data2Widgets()
        {
            base.Data2Widgets();
            maxvnum.Value = Data.vnum_max;
            //minvnum.Text = (Data.vnum * 100).ToString();

            for (int i = 0; i <= C.zd_end; i++)
                edt_descriptions[i].Text = Data.descriptions[i];
            edt_desc0.Text = Data.shortdesc;

            spin_limitXP.Value = Data.limit_xp;
            spin_limitgems.Value = Data.limit_gems;
            mapCoordMap.Value = Data.map_id;
            mapCoordX.Value = Data.map_x;
            mapCoordY.Value = Data.map_y;
            combo_repop.SelectedIndex = Data.repop_type;
            spin_interval.Value = Data.repop_interval;
            for (int i = 0; i <= C.zf_end; i++)
                chk_flags[i].Checked = Data.flags[1 << i];

            chk_vnumlimits.Checked = Data.hogdata_manual_vunm_limits;
            chk_vnumlimits_CheckedChanged(null, null);

            spin_vnum_ValueChanged(null, null);

            list_inits.InitsToList(Data.inits);
        }

        public override void Widgets2Data()
        {
            base.Widgets2Data();
            Data.vnum_max = Convert.ToInt32(maxvnum.Value);

            for (int i = 0; i <= C.zd_end; i++)
                Data.descriptions[i] = edt_descriptions[i].Text;
            Data.shortdesc = edt_desc0.Text;
            
            Data.limit_xp = Convert.ToInt32(spin_limitXP.Value);
            Data.limit_gems = Convert.ToInt32(spin_limitgems.Value);
            Data.map_id = Convert.ToInt32(mapCoordMap.Value);
            Data.map_x = Convert.ToInt32(mapCoordX.Value);
            Data.map_y = Convert.ToInt32(mapCoordY.Value);
            Data.repop_type = combo_repop.SelectedIndex;
            Data.repop_interval = Convert.ToInt32(spin_interval.Value);
            for (int i = 0; i <= C.zf_end; i++)
                Data.flags[1 << i] = chk_flags[i].Checked;

            Data.hogdata_manual_vunm_limits = chk_vnumlimits.Checked;

            list_inits.ListToInits(Data.inits);
        }
        #endregion

        #region Init Details Panel
        private bool details_closed = true;
        private void details_timer_Tick(object sender, EventArgs e)
        {
            if (pn_initdetails.Height < 158 && !details_closed)
            {
                pn_initdetails.Height += 5;
            }
            else if (pn_initdetails.Width > 10 && details_closed)
            {
                pn_initdetails.Height -= 5;
            }
            else
            {
                pn_initdetails.Visible = !details_closed;
                t_details.Enabled = false;
            }
        }

        private void btndetails_Click(object sender, EventArgs e)
        {
            // setup vnum enumerator panel
            pn_initdetails.Visible = true;
            //pn_utils.Width = 10;
            t_details.Enabled = true;
            details_closed = !details_closed;
            chkinitmode.Checked = !details_closed;

            if (!details_closed)
                pn_initdetails.BringToFront();

            if (chkinitmode.Checked)
                chkinitmode.BackColor = Color.LightGreen;
            else chkinitmode.BackColor = Color.LightYellow;
        }
        #endregion

        #region Widgets' Methods
        private void spin_vnum_ValueChanged(object sender, EventArgs e)
        {
            minvnum.Text = (spin_vnum.Value * 100).ToString();

            if (!chk_vnumlimits.Checked)
                maxvnum.Text = (spin_vnum.Value * 100 + 99).ToString();

            if (spin_vnum.Value != original.vnum)
                spin_vnum.BackColor = Color.LightYellow;
            else spin_vnum.BackColor = Color.White;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in list_inits.Items)
                if (i.SubItems[0].Text.Length > 0)
                {
                    if (Init.CharToIndex(i.SubItems[0].Text[0]) != C.it_comment)
                        list_inits.GenerateComment(i);
                }
        }

        private void initellisenselist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            list_inits.SelectItem();
        }

        private void chkautocomment_CheckedChanged(object sender, EventArgs e)
        {
            Options.data.init_automatic_comment = chkautocomment.Checked;
        }

        private void btnremcontainers_Click(object sender, EventArgs e)
        {
            list_inits.CreateContainersObjRem();
        }

        private void combosep_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_inits.separator = combosep.SelectedIndex;
        }

        private void chk_vnumlimits_CheckedChanged(object sender, EventArgs e)
        {
            maxvnum.Enabled = chk_vnumlimits.Checked;
            if (!chk_vnumlimits.Checked)
                spin_vnum_ValueChanged(null, null);
        }

        private void btnaddinit_Click(object sender, EventArgs e)
        {
            list_inits.Do_Add();
        }

        private void btndelinit_Click(object sender, EventArgs e)
        {
            list_inits.Do_Del();
        }

        private void btntrim_Click(object sender, EventArgs e)
        {
            list_inits.TrimSelection();
        }

        private void btnapply_Click_1(object sender, EventArgs e)
        {
            spin_vnum_ValueChanged(null, null);
        }


        private void list_flags_SelectedIndexChanged(object sender, EventArgs e)
        {
            (sender as CheckedListBox).ClearSelected();
        }
        #endregion

        private void frm_Zone_Shown(object sender, EventArgs e)
        {
            list_inits.SetComponents(ParentArea, initellisenselist, edt, statusStrip1);
            chkautocomment.Checked = Options.data.init_automatic_comment;
            combosep.SelectedIndex = 0;
        }
  
        private void button1_Click(object sender, EventArgs e)
        {
            int quantity = Convert.ToInt32(spin_mgqnt.Value);
            string[] l = new string[8];
            for (int i = 0; i < quantity; i++)
            {
                l[0] = "M";
                l[1] = spin_mgpercent.Value.ToString();
                l[2] = spin_mgmob.Value.ToString();
                l[3] = spin_mgmax.Value.ToString();
                l[4] = spin_mgroom.Value.ToString();

                ListViewItem item = list_inits.Do_Add(-1, l);
            }
            list_inits.Redraw();
        }

        private void btnfindmob_Click(object sender, EventArgs e)
        {
            using (dlg_select_element form = new dlg_select_element())
            {
                if (sender == btnfindmob)
                    form.SetElements<Mob>(ParentArea, C.i_mob, ParentArea.mobs, true);
                else if (sender == btnfindroom)
                    form.SetElements<Room>(ParentArea, C.i_room, ParentArea.rooms, true);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (sender == btnfindmob)
                        spin_mgmob.Value = Convert.ToInt32((form.result.GetType() == typeof(Mob) ? (form.result as Mob).vnum : (int)form.result));
                    else if (sender == btnfindroom)
                        spin_mgroom.Value = Convert.ToInt32((form.result.GetType() == typeof(Room) ? (form.result as Room).vnum : (int)form.result));
                }
            }
        }

        private void btnreports_Click(object sender, EventArgs e)
        {
            if (frm_reports != null && frm_reports.Visible)
            {
                frm_reports.BringToFront();
                return;
            }

            frm_reports = new frm_Reports();
            frm_reports.ParentArea = ParentArea;
            frm_reports.Show();
        }

        #region Description Textboxes Animation
        private void t_memos_Tick(object sender, EventArgs e)
        {
            const int speed = 10;

            if (anim_desc.Count <= 0)
            {
                t_memos.Enabled = false;
                return;
            }

            foreach (animdata a in anim_desc)
            {
                Control cnt = a.control;
                int x = 0;
                int y = 0;
                int rx = 0;
                int ry = 0;

                if (cnt.Left < a.dest.X)
                    x = speed;
                else if (cnt.Left > a.dest.X)
                    x = -speed;

                if (cnt.Top < a.dest.Y)
                    y = speed;
                else if (cnt.Top > a.dest.Y)
                    y = -speed;

                rx = Math.Abs(cnt.Left - a.dest.X) < speed ? a.dest.X : cnt.Left + x;
                ry = Math.Abs(cnt.Top - a.dest.Y) < speed ? a.dest.Y : cnt.Top + y;
                cnt.Location = new Point(rx, ry);

                x = 0;
                y = 0;

                if (cnt.Width < a.size.X)
                    x = speed;
                else if (cnt.Width > a.size.X)
                    x = -speed;

                if (cnt.Height < a.size.Y)
                    y = speed;
                else if (cnt.Height > a.size.Y)
                    y = -speed;

                rx = Math.Abs(cnt.Width - a.size.X) < speed ? a.size.X : cnt.Width + x;
                ry = Math.Abs(cnt.Height - a.size.Y) < speed ? a.size.Y : cnt.Height + y;
                cnt.Width = rx;
                cnt.Height = ry;
            }

            anim_desc = anim_desc.Where(a => a.control.Location != a.dest || a.control.Width != a.size.X || a.control.Height != a.size.Y).ToList();
        }

        private void anim_Start(animdata a)
        {
            bool found = false;
            for (int i = 0; i < anim_desc.Count(); i++)
                if (anim_desc[i].control == a.control)
                {
                    found = true;
                    anim_desc[i] = a;
                }

            if (!found) 
                anim_desc.Add(a);

            a.control.Multiline = true;
            a.control.BringToFront();
            t_memos.Enabled = true;
        }

        private void anim_End(animdata a)
        {
            a.control.Multiline = false;
        }

        private void quest_desc_gotfocus(object sender, EventArgs e)
        {
            if (!details_closed)
                return;

            animdata a;
            a.control = sender as TextBox;
            a.dest = grpflags.Location;
            a.size = new Point(grpflags.Width, grpflags.Height);
            //a.dest = edt_desc0.Location;
            //a.size = new Point(a.control.Width, edt_desc4.Bottom - edt_desc0.Top);
            anim_Start(a);
        }

        private void quest_desc_lostfocus(object sender, EventArgs e)
        {
            if (!details_closed)
                return;

            animdata a;
            a.control = sender as TextBox;
            Point dest = sender == edt_desc3 ? new Point(99, 111) : new Point(99, 137);
            a.dest = dest;
            a.size = new Point(295, 20);
            anim_Start(a);
        }
        #endregion
    }
}
