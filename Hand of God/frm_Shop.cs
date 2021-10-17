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
    public partial class frm_Shop : frm_area_element
    {
        public Shop Data { get { return data as Shop; } set { data = value; } }
        public Shop Original { get { return original as Shop; } set { original = value; } }

        #region Constructor
        public frm_Shop()
        {
            title = "Negozio";
            data = new Shop();
            InitializeComponent();
            utils.AddFormEvents(Controls, SetModified);
            
            for (int i = 7; i <= 11; i++)
            {
                ComboBox combo = Controls.Find("prop" + i, true)[0] as ComboBox;
                combo.Items.Clear();
                foreach (string s in L.object_types)
                    combo.Items.Add(s);
            }
            prop12.Items.Clear();
            foreach (string s in L.shop_indigence)
                prop12.Items.Add(s);
            prop13.Items.Clear();
            foreach (string s in L.shop_attack)
                prop13.Items.Add(s);
        }
        #endregion

        #region Widgets <-> Data Transfers
        public override void Data2Widgets()
        {
            base.Data2Widgets();
            spin_mul_buy.Value = Math.Min(spin_mul_buy.Maximum, Data.mul_buy);
            spin_mul_sell.Value = Math.Min(spin_mul_sell.Maximum, Data.mul_sell);

            for (int i = 0; i <= C.shp_props_end; i++)
            {
                Control cnt = Controls.Find("prop" + i, true)[0];
                if (cnt.GetType() == typeof(NumericUpDown))
                    (cnt as NumericUpDown).Value = Data.properties[i];
                else if (cnt.GetType() == typeof(ComboBox))
                    (cnt as ComboBox).SelectedIndex = Data.properties[i];
            }

            for (int i = 0; i <= C.shp_speech_end; i++)
            {
                Control cnt = Controls.Find("speech" + i, true)[0];
                (cnt as MudlikeRichTextBox).SetText(Data.speech[i]);
            }
        }

        public override void Widgets2Data()
        {
            base.Widgets2Data();
            Data.mul_buy = spin_mul_buy.Value;
            Data.mul_sell = spin_mul_sell.Value;

            for (int i = 0; i <= C.shp_props_end; i++)
            {
                Control cnt = Controls.Find("prop" + i, true)[0];
                if (cnt.GetType() == typeof(NumericUpDown))
                    Data.properties[i] = Convert.ToInt32((cnt as NumericUpDown).Value);
                else if (cnt.GetType() == typeof(ComboBox))
                    Data.properties[i] = (cnt as ComboBox).SelectedIndex;
            }

            for (int i = 0; i <= C.shp_speech_end; i++)
            {
                Control cnt = Controls.Find("speech" + i, true)[0];
                Data.speech[i] = (cnt as MudlikeRichTextBox).GetText();
            }
        }
        #endregion

        private void findelementclick(object sender, EventArgs e)
        {
            using (dlg_select_element form = new dlg_select_element())
            {
                if (sender == btnfind_prop0)
                    form.SetElements<Mob>(ParentArea, C.i_mob, ParentArea.mobs, true);
                else if (sender == btnfind_prop1)
                    form.SetElements<Room>(ParentArea, C.i_room, ParentArea.rooms, true);
                else
                    form.SetElements<Obj>(ParentArea, C.i_obj, ParentArea.objects, true);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    NumericUpDown spin = Controls.Find((sender as Control).Name.Replace("btnfind_", ""), true)[0] as NumericUpDown;
                    if (sender == btnfind_prop0)
                        spin.Value = Convert.ToInt32((form.result.GetType() == typeof(Mob) ? (form.result as Mob).vnum : (int)form.result));
                    else if (sender == btnfind_prop1)
                        spin.Value = Convert.ToInt32((form.result.GetType() == typeof(Room) ? (form.result as Room).vnum : (int)form.result));
                    else
                        spin.Value = Convert.ToInt32((form.result.GetType() == typeof(Obj) ? (form.result as Obj).vnum : (int)form.result));
                }
            }
        }

        private void speech6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                ProcessTabKey(true);
                e.SuppressKeyPress = true;
            }
        }
    }
}
