using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace HandofGod
{
    public partial class dlg_select_element : Form
    {
        private int mode = 0;
        public object result;
        private BitVector32 flagsData = new BitVector32();

        #region Constructor
        public dlg_select_element()
        {
            InitializeComponent();
            list_flags.Hide();
            list_elements.Hide();
        }
        #endregion

        #region Methods
        /// <summary>
        /// sets up the form to retrieve a specific zone_element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i">Column type index (i_zone, i_room, etc..)</param>
        /// <param name="l">the list used to fill the dialog</param>
        /// <param name="none">Adds the first -none- item with a null value</param>
        public void SetElements<T>(Area ParentArea, int i, List<T> l, bool none = false, Predicate<area_element> func = null, Dictionary<int, string> top_values = null, area_element omit = null)
        {
            list_elements.SetColumns(i);

            if (typeof(T) == typeof(Zone)) list_elements.AddToList(ParentArea, l as List<Zone>, none, func);
            else if (typeof(T) == typeof(Room)) list_elements.AddToList(ParentArea, l as List<Room>, none, func);
            else if (typeof(T) == typeof(Mob)) list_elements.AddToList(ParentArea, l as List<Mob>, none, func);
            else if (typeof(T) == typeof(Obj)) list_elements.AddToList(ParentArea, l as List<Obj>, none, func);
            else if (typeof(T) == typeof(Exit)) list_elements.AddToList(ParentArea, l as List<Exit>, none, true);
            else if (typeof(T) == typeof(Init)) list_elements.AddToList(ParentArea, l as List<Init>, none, func);
            else if (typeof(T) == typeof(Shop)) list_elements.AddToList(ParentArea, l as List<Shop>, none, func);
            else if (typeof(T) == typeof(Spell)) list_elements.AddToList(ParentArea, l as List<Spell>, none, func);
            else if (typeof(T) == typeof(MDFuncData)) list_elements.AddToList(ParentArea, l as List<MDFuncData>, none, func, top_values);
            else if (typeof(T) == typeof(MobDialogue)) list_elements.AddToList(ParentArea, l as List<MobDialogue>, none, func, top_values, omit as MobDialogue);
            list_elements.Show();
            list_flags.Hide();
            panel2.Show();
            mode = 0;
        }

        /// <summary>
        /// sets up the form to retrieve a variable number of given flags as one int
        /// </summary>
        /// <param name="content"></param>
        /// <param name="values"></param>
        public void SetItems(string[] content, int values)
        {
            list_flags.Items.Clear();

            for (int i = 0; i <= 31; i++)
                flagsData[1 << i] = false;

            flagsData[values] = true;
            result = values;

            for (int i = 0; i < content.Length; i++)
            {
                list_flags.Items.Add(content[i]);
                list_flags.SetItemCheckState(i, flagsData[1 << i] ? CheckState.Checked : CheckState.Unchecked);
            }

            list_flags.Show();
            list_elements.Hide();
            panel2.Hide();
            mode = 1;
        }
        #endregion

        #region Widgets' Methods
        private void button1_Click(object sender, EventArgs e)
        {
            switch (mode)
            {
                // elements list
                case 0:
                    if (list_elements.SelectedItems.Count > 0)
                        result = list_elements.SelectedItems[0].Tag;
                    else result = 0;
                    break;
                case 1:
                        result = flagsData.Data;
                        break;
                case 2:
                        
                        result = Convert.ToInt32(spin_manual.Value);
                        break;
            }

            //if ((list_elements.Visible && list_elements.SelectedIndices.Count > 0) || !list_elements.Visible)
           DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        #endregion

        private void list_DoubleClick(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void list_flags_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_flags.SelectedIndex < 0)
                return;

            flagsData[1 << list_flags.SelectedIndex] = list_flags.GetItemCheckState(list_flags.SelectedIndex) == CheckState.Checked ? true : false;
            (sender as CheckedListBox).ClearSelected();
        }

        private void radiolist_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                list_elements.Show();
                label1.Hide();
                spin_manual.Hide();
                mode = 0;
            }
            else
            {
                list_elements.Hide();
                label1.Show();
                spin_manual.Show();
                mode = 2;
            }
        }
    }
}
