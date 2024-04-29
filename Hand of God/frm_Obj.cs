using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace HandofGod
{
    public partial class frm_Obj : frm_area_element
    {
        public Obj Data { get { return data as Obj; } set { data = value; } }
        public Obj Original { get { return original as Obj; } set { original = value; } }

        List<CheckBox> chks_flags;
        List<Label> labels_val;
        List<ValAffEditbox> values;
        List<ValAffEditbox> extraValues;
        List<ComboBox> combos_aff;
        List<ValAffEditbox> affects;

        #region Constructor
        public frm_Obj()
        {
            title = "Oggetto";
            Data = new Obj();
            InitializeComponent();
            utils.AddFormEvents(Controls, SetModified);
            // custom components
            gaugecharcounter.AddEvents(memo_desc);
            gaugecharcounter.AddEvents(memo_actiondesc);
            gaugecharcounter.AddEvents(memo_extradesc);

            labels_val = new List<Label>() { lbl_val0, lbl_val1, lbl_val2, lbl_val3 };
            values = new List<ValAffEditbox>() { val0, val1, val2, val3 };
            extraValues = new List<ValAffEditbox>() { extraVal1, extraVal2, extraVal3, extraVal4 };
            combos_aff = new List<ComboBox>() { combo_aff0, combo_aff1, combo_aff2, combo_aff3, combo_aff4 };
            affects = new List<ValAffEditbox>() { aff0, aff1, aff2, aff3, aff4 };

            combo_type.Items.Clear();
            foreach (string s in L.object_types)
                combo_type.Items.Add(s);

            combo_rarity.Items.Clear();
            foreach (string s in L.object_rarity)
                combo_rarity.Items.Add(s);

            chks_flags = new List<CheckBox>();
            for (int i = 0; i <= C.of_end; i++)
            {
                CheckBox chk = tabControl2.Controls.Find("flg" + i, true)[0] as CheckBox;
                chk.Text = L.object_flags[i];
                chks_flags.Add(chk);
            }

            list_wear.Items.Clear();
            foreach (string s in L.object_wear_ita)
                list_wear.Items.Add(s);

            for (int i = 0; i <= 4; i++)
            {
                combos_aff[i].Items.Clear();
                foreach (string s in L.object_affects)
                    combos_aff[i].Items.Add(s);
            }
        }
        #endregion

        #region Methods
        protected override void SetModifiedTitle(object sender, EventArgs e)
        {
            Text = "Oggetto [" + original.vnum + "] " + original.shortdesc + (modified ? " *modificato*" : "");
        }
        #endregion 

        #region Widgets <-> Data Transfers
        public override void Data2Widgets()
        {
            base.Data2Widgets();

            edt_keys.Text = Data.keys;
            edt_shortdesc.SetText(Data.shortdesc);
            memo_desc.SetText(Data.description);
            memo_actiondesc.SetText(Data.actiondesc);

            combo_type.SelectedIndex = Data.properties[C.op_type];
            combo_rarity.SelectedIndex = Data.rarity;

            for (int i = 1; i <= 3; i++)
                (Controls.Find("prop" + i, false)[0] as NumericUpDown).Value = Data.properties[i];

            for (int i = 0; i <= 3; i++)
            {
                values[i].Value = Data.values[i];
                extraValues[i].Value = Data.extraValues[i];
            }


            // flags
            for (int i = 0; i <= C.of_end; i++)
                chks_flags[i].Checked = Data.flags[1 << i];

            for (int i = 0; i <= C.ow_end; i++)
                list_wear.SetItemCheckState(i, Data.wearpos[1 << i] ? CheckState.Checked : CheckState.Unchecked);

            for (int i = 0; i <= 4; i++)
            {
                if (i <= 3)
                {
                    values[i].SetArea(ParentArea);
                    values[i].Value = Data.values[i];
                    values[i].RefreshComponents();

                    extraValues[i].SetArea(ParentArea);
                    extraValues[i].Value = Data.extraValues[i];
                    extraValues[i].RefreshComponents();
                }

                combos_aff[i].SelectedIndex = Data.affects[i].index;
                affects[i].SetArea(ParentArea);
                affects[i].Value = Data.affects[i].value;
                
                if (ObjAffect.isComplex(Data.affects[i].index))
                {
                    affects[i].val2 = Data.affects[i].value2;
                    affects[i].val3 = Data.affects[i].value3;
                    affects[i].val4 = Data.affects[i].value4;
                }
                affects[i].RefreshComponents();
            }

            edt_extradesc.Text = "";
            memo_extradesc.SetText("");
            RefreshExtraDescListBox();

            if (ValAffEditbox.lastopenedmenu != null)
                ValAffEditbox.lastopenedmenu.Close();

            chkhtmlexport.Checked = Data.HTMLExportable;

            RefreshExtraDescListBox();
        }

        public override void Widgets2Data()
        {
            base.Widgets2Data();

            Data.keys = edt_keys.Text;
            Data.shortdesc = edt_shortdesc.GetText();
            Data.description = memo_desc.GetText();
            Data.actiondesc = memo_actiondesc.GetText();

            Data.rarity = combo_rarity.SelectedIndex;
            Data.properties[C.op_type] = combo_type.SelectedIndex;

            for (int i = 1; i <= 3; i++)
                Data.properties[i] = Convert.ToInt32((Controls.Find("prop" + i, false)[0] as NumericUpDown).Value);

            for (int i = 0; i <= 3; i++)
            {
                Data.values[i] = Convert.ToInt32(values[i].Value);
                Data.extraValues[i] = Convert.ToInt32(extraValues[i].Value);
            }

            for (int i = 0; i <= 4; i++)
            {
                Data.affects[i].index = combos_aff[i].SelectedIndex;
                Data.affects[i].value = Convert.ToInt32(affects[i].Value);

                if (ObjAffect.isComplex(Data.affects[i].index))
                {
                    Data.affects[i].value2 = affects[i].val2;
                    Data.affects[i].value3 = affects[i].val3;
                    Data.affects[i].value4 = affects[i].val4;
                }
            }

            // modified extradesc notification
            if (extra_modified && MessageBox.Show("Salvare la Extra Description modificata?", "Attenzione", MessageBoxButtons.YesNo) == DialogResult.Yes)
                btn_extradescAdd_Click(null, null);

            // flags
            for (int i = 0; i <= C.of_end; i++)
                Data.flags[1 << i] = chks_flags[i].Checked;

            for (int i = 0; i <= C.ow_end; i++)
                Data.wearpos[1 << i] = list_wear.GetItemCheckState(i) == CheckState.Checked ? true : false;

            Data.HTMLExportable = chkhtmlexport.Checked;
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
                ed.desc = memo_extradesc.GetText();
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

            tabControl1.SelectedIndex = 2;
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
        private void combo_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] labels_text = L.object_value_names[combo_type.SelectedIndex].Split('~');
            string s = C.objvalues[combo_type.SelectedIndex];
            int i;

            int max_width = 0;

            for (i = 0; i <= 3; i++)
            {
                labels_val[i].Text = labels_text[i];
                if (labels_val[i].Right > max_width)
                    max_width = labels_val[i].Right;
            }

            if (combo_type.SelectedIndex == C.ot_weapon)
            {
                extralbl1.Visible = true;
                extralbl2.Visible = true;
                extralbl3.Visible = true;
                extralbl4.Visible = true;

                extraVal1.Visible = true;
                extraVal2.Visible = true;
                extraVal3.Visible = true;
                extraVal4.Visible = true;
            }
            else 
            {
                extralbl1.Visible = false;
                extralbl2.Visible = false;
                extralbl3.Visible = false;
                extralbl4.Visible = false;

                extraVal1.Visible = false;
                extraVal2.Visible = false;
                extraVal3.Visible = false;
                extraVal4.Visible = false;
            }

            i = 0;
            foreach (char c in s)
            {
                values[i].ValueType = c;
                values[i].Left = max_width;
                i++;
            }

            pn_values.Width = max_width + val0.Width + 10;
            //pn_affects.Left = pn_values.Right;

            if (combo_type.SelectedIndex == C.ot_trap)
                lbltrapdamage.Show();
            else lbltrapdamage.Hide();
        }

        private void combo_aff0_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = (sender as ComboBox).SelectedIndex;
            int itemindex = int.Parse((sender as Control).Name[(sender as Control).Name.Length - 1].ToString());
            affects[itemindex].ValueType = C.objaffects[i];
        }

        private void list_flags_SelectedIndexChanged(object sender, EventArgs e)
        {
            (sender as CheckedListBox).ClearSelected();
        }
        #endregion

        private void combo_type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                ProcessTabKey(true);
                e.SuppressKeyPress = true;
            }
        }

        private void val2_LocationChanged(object sender, EventArgs e)
        {
            lbltrapdamage.Left = (sender as Control).Right + 10;
        }

        /*
         * 
void FindTrapDamage( struct char_data *v, struct obj_data *p_obj )
{
int trapLevel = p_obj->getTrapLevel();


         * 
         * */

        private void val2_ValueChanged(object sender, EventArgs e)
        {
            if (combo_type.SelectedIndex == C.ot_trap)
            {
                int dam = 1;
                int lev = (int)val2.Value;

                if (lev >= 50)
                    dam = 10 * lev;
                else if (lev > 40)
                    dam = 6 * lev;
                else if (lev > 30)
                    dam = 5 * lev;
                else if (lev > 20)
                    dam = 4 * lev;
                else dam = 3 * lev;

                lbltrapdamage.Text = "( " + dam + " Danni )";
            }
        }

        private void val0_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pn_values_Enter(object sender, EventArgs e)
        {

        }

        private void btnapply_Click_1(object sender, EventArgs e)
        {

        }
    }
}
