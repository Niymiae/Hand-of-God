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
    public partial class dlg_ExportElements : Form
    {
        private List<Obj> data;
        public List<bool> result;

        public dlg_ExportElements()
        {
            InitializeComponent();
            result = new List<bool>();
        }

        public void FillList<T>(List<T> list)
        {
            List<T> SortedList = list.OrderBy(x => (x as area_element).vnum).ToList();
            // da generalizzare
            data = SortedList as List<Obj>;
            chklist.Items.Clear();
            foreach (T item in list)
            {
                area_element e = item as area_element;
                int index = chklist.Items.Add("#" + e.vnum + " " + utils.CutColorCodes(e.shortdesc));
                chklist.SetItemChecked(index, (e as Obj).HTMLExportable);
            }
        }

        private void chkselallClick(object sender, EventArgs e)
        {
            CheckBox cnt = sender as CheckBox;
            for (int i = 0; i < chklist.Items.Count; i++)
            {
                switch (Convert.ToInt32(cnt.Tag))
                {
                    case 0:
                        chkweapons.Checked = cnt.Checked;
                        chkarmors.Checked = cnt.Checked;
                        chkkeys.Checked = cnt.Checked;
                        chkcontainers.Checked = cnt.Checked;
                        chkcrstl.Checked = cnt.Checked;
                        chkpersonal.Checked = cnt.Checked;
                        break;
                    case 1:
                            if (data == null) continue;
                            if (data[i].properties[C.op_type] == C.ot_weapon || data[i].properties[C.op_type] == C.ot_missile ||
                                data[i].properties[C.op_type] == C.ot_wand || data[i].properties[C.op_type] == C.ot_staff ||
                                data[i].properties[C.op_type] == C.ot_fireweapon)
                                break;
                            else continue;
                    case 2:
                            if (data == null) continue;
                            if (data[i].properties[C.op_type] == C.ot_armor)
                                break;
                            else continue;
                    case 3:
                            if (data == null) continue;
                            if (data[i].properties[C.op_type] == C.ot_key)
                                break;
                            else continue;
                    case 4:
                             if (data == null) continue;
                             if (data[i].properties[C.op_type] == C.ot_container || data[i].properties[C.op_type] == C.ot_liquidcontainer)
                                 break;
                             else continue;
                    case 5:
                             if (data[i].extras.Find(x => x.keys.Contains("crstl_value")) != null)
                                 break;
                             else continue;
                    case 6:
                             if (data[i].wearpos[1 << C.ow_personal])
                                 break;
                             else continue;
                }
                chklist.SetItemChecked(i, cnt.Checked);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result.Clear();
            for (int i = 0; i < chklist.Items.Count; i++)
                result.Add(chklist.GetItemChecked(i));
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result.Clear();
            DialogResult = DialogResult.Cancel;
        }
    }
}
