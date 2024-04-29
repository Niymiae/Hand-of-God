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
    public partial class frm_Reports : Form
    {
        public Area ParentArea;
        public EventHandler areachanged;

        #region Constructor
        public frm_Reports()
        {
            InitializeComponent();
        }
        #endregion

        private void radiobtncheckedchanged(object sender, EventArgs e)
        {
            List<Exit> exits = new List<Exit>();
            int index = (sender as RadioButton).TabIndex;
            switch (index)
            {
                case C.i_report_nolinkedrooms: list.SetColumns(C.i_room); break;
                case C.i_report_noinitmob: list.SetColumns(C.i_mob); break;
                case C.i_report_noinitobj: list.SetColumns(C.i_obj); break;
                default: list.SetColumns(index); break;
            }
            switch (index)
            {
                case C.i_report_gems:
                    list.AddToList(ParentArea, ParentArea.mobs, false, x => 
                                                                            {
                                                                                for (int i = 0; i <= C.gt_end; i++)
                                                                                    if (x.gems[i].percent > 0)
                                                                                        return true;
                                                                                return false;
                                                                            }, null, null, true, true);
                    break;
                case C.i_report_coinsfame:
                    list.AddToList(ParentArea, ParentArea.mobs, false, x => (x.values[C.mv_gold] > 0), null, null, true, true);
                    break;
                case C.i_report_treasures:
                    list.AddToList(ParentArea, ParentArea.objects, false, x => (x.properties[C.op_type] == C.ot_money && x.values[0] > 0), null, null, true, true);
                    break;
                case C.i_report_keys:
                    List<int> keys = new List<int>();
                    foreach (Obj obj in ParentArea.objects)
                        if (obj.properties[C.op_type] == C.ot_container &&
                            obj.values[2] > 0)
                            keys.Add(obj.vnum);
                    list.AddToList(ParentArea, ParentArea.objects, false, x => keys.Contains(x.vnum));

                    exits = new List<Exit>();
                    foreach (Room r in ParentArea.rooms)
                        foreach (Exit ex in r.exits.Where<Exit>(x => x.door.objkey > 0))
                            exits.Add(ex);
                    list.AddToList(ParentArea, exits, false, false);
                    break;
                case C.i_report_deathrooms:
                    list.AddToList(ParentArea, ParentArea.rooms, false, x => (x.flags[1 << C.rf_death]));
                    break;
                case C.i_report_externalexits:
                    foreach (Room r in ParentArea.rooms)
                        foreach (Exit ex in r.exits.Where<Exit>(x => x.room > 0 && ParentArea.Get<Room>(x.room) == null))
                            exits.Add(ex);
                    list.AddToList(ParentArea, exits, false, false);
                    break;

                case C.i_report_nolinkedrooms:
                    foreach (Room r in ParentArea.rooms)
                    {
                        bool found = false;
                        foreach (Room x in ParentArea.rooms)
                        {
                            if (r == x)
                                continue;

                            foreach (Exit ex in x.exits)
                                if (ex.room == r.vnum)
                                    found = true;

                            if (x.sect == C.rs_teleport && x.tel_toroom == r.vnum)
                                found = true;

                            if (found)
                                break;
                        }

                        if (!found)
                            list.AddItem(ParentArea, r, false);
                    }
                    list.Redraw();
                    break;

                case C.i_report_noinitmob:
                    foreach (Mob m in ParentArea.mobs)
                    {
                        bool found = false;

                        int z = -1;
                        while ((z = z + 1) < ParentArea.zones.Count && !found)
                        {
                            int i = -1;
                            while ((i = i + 1) < ParentArea.zones[z].inits.Count && !found)
                            {
                                for (int k = C.iv_value0; k <= C.iv_value3; k++)
                                    if (ParentArea.zones[z].inits[i].GetElementType(k) == C.i_mob && ParentArea.zones[z].inits[i].values[k] == m.vnum)
                                        found = true;
                            }
                        }

                        if (!found)
                            list.AddItem(ParentArea, m, false);
                    }
                    list.Redraw();
                    break;

                case C.i_report_noinitobj:
                    foreach (Obj o in ParentArea.objects)
                    {
                        bool found = false;

                        int z = -1;
                        while ((z = z + 1) < ParentArea.zones.Count && !found)
                        {
                            int i = -1;
                            while ((i = i + 1) < ParentArea.zones[z].inits.Count && !found)
                            {
                                for (int k = C.iv_value0; k <= C.iv_value3; k++)
                                    if (ParentArea.zones[z].inits[i].GetElementType(k) == C.i_obj && ParentArea.zones[z].inits[i].values[k] == o.vnum)
                                        found = true;
                            }
                        }

                        if (!found)
                            list.AddItem(ParentArea, o, false);
                    }
                    list.Redraw();
                    break;
            }
        }

        #region Widgets' Methods
        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void frm_Reports_Shown(object sender, EventArgs e)
        {
            radiobtncheckedchanged(radiogems, null);
        }

        private void list_DoubleClick(object sender, EventArgs e)
        {
            if (list.SelectedIndices.Count <= 0)
                return;

            Exit ex;
            area_element selected = list.SelectedItems[0].Tag as area_element;

            if (selected != null)
                selected.Edit(ParentArea, null, false, true);
            else if ((ex = list.SelectedItems[0].Tag as Exit) != null)
                ex.Edit(ParentArea, ex.parent);

            radiobtncheckedchanged(radiogroup.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked), null);

            if (areachanged != null)
                areachanged(null, null);
        }
    }
}
