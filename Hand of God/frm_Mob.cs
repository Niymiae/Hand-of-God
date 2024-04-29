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
    public partial class frm_Mob : frm_area_element
    {
        public Mob Data { get { return data as Mob; } set { data = value; } }
        public Mob Original { get { return original as Mob; } set { original = value; } }
        private List<CheckBox> chks_act;
        private List<CheckBox> chks_aff;
        private int avghp { get; set; }
        private int avgxp { get; set; }


        #region Constructor
        public frm_Mob()
        {
            title = "Mob";
            Data = new Mob();
            InitializeComponent();
            utils.AddFormEvents(Controls, SetModified);
            gaugecharcounter.AddEvents(memo_desc);
            gaugecharcounter.AddEvents(memo_sound_adjacent);
            gaugecharcounter.AddEvents(memo_sound_sameroom);
            gaugecharcounter.AddEvents(memo_longdesc);

            val0.Items.Clear();
            foreach (string s in L.mob_types)
                val0.Items.Add(s);

            val17.Items.Clear();
            foreach (string s in L.mob_balance_types)
                val17.Items.Add(s);

            val4.Items.Clear();
            foreach (string s in L.mob_gender)
                val4.Items.Add(s);

            val9.Items.Clear();
            foreach (string s in L.mob_races)
                val9.Items.Add(s);

            val11.Items.Clear();
            val12.Items.Clear();
            foreach (string s in L.mob_positions)
            {
                val11.Items.Add(s);
                val12.Items.Add(s);
            }

            chks_act = new List<CheckBox>();
            for (int i = 0; i <= C.mf_end; i++)
            {
                CheckBox chk = tab_acts.Controls.Find("act" + i, true)[0] as CheckBox;
                chk.Text = L.mob_acts[i];
                chks_act.Add(chk);
            }

            chks_aff = new List<CheckBox>();
            for (int i = 0; i <= C.ma_end; i++)
            {
                CheckBox chk = tab_affects.Controls.Find("aff" + i, true)[0] as CheckBox;
                chk.Text = L.mob_affects[i];
                chks_aff.Add(chk);
            }

            list_immunities.Items.Clear();
            list_resistances.Items.Clear();
            list_suscept.Items.Clear();
            foreach (string s in L.damage_types)
            {
                list_immunities.Items.Add(s);
                list_resistances.Items.Add(s);
                list_suscept.Items.Add(s);
            }

            // mob dialogs
            list_dialog_questions.SetColumns(C.i_mobdialog_Q);
            list_dialog_questions.Sorting = SortOrder.None;
            (list_dialog_questions.ListViewItemSorter as Sorter).Order = SortOrder.Ascending;

            list_dialog_answers.SetColumns(C.i_mobdialog_A);
            list_dialog_answers.Sorting = SortOrder.None;
            (list_dialog_answers.ListViewItemSorter as Sorter).Order = SortOrder.Ascending;
        }
        #endregion

        #region Widgets <-> Data Transfers
        public override void Data2Widgets()
        {
            base.Data2Widgets();
            // keys
            edt_keys.Text = Data.keys;
            // damage
            edt_damage.Text = Data.damage;
            // short desc
            edt_shortdesc.SetText(Data.shortdesc);
            // long desc
            memo_longdesc.SetText(Data.longdesc);

            // long desc
            memo_desc.SetText(Data.description);
            // sound same room
            memo_sound_sameroom.SetText(Data.samesound);
            // sound adjacent room
            memo_sound_adjacent.SetText(Data.adjacentsound);

            spin_fame.Text = Data.fame;
            for (int i = 0; i <= C.gt_end; i++)
            {
                (tab_gemsfame.Controls.Find("gem_perc" + i, false)[0] as NumericUpDown).Value = Data.gems[i].percent;
                (tab_gemsfame.Controls.Find("gem_dice" + i, false)[0] as TextBox).Text = Data.gems[i].dice;
            }

            for (int i = 1; i <= C.et_end; i++)
                (tab_talents.Controls.Find("spin_epic" + i, false)[0] as NumericUpDown).Value = Data.epic_talents[i];

            // values
            for (int i = 0; i <= C.mv_end; i++)
            {
                Control c = Controls.Find("val" + i, true)[0];
                if (c.GetType() == typeof(NumericUpDown))
                {
                    (c as NumericUpDown).Value = Data.values[i];
                }
                else
                    if (c.GetType() == typeof(ComboBox))
                        (c as ComboBox).SelectedIndex = Data.values[i];
            }
            // acts
            for (int i = 0; i <= C.mf_end; i++)
                chks_act[i].Checked = Data.flags[1 << i];
            // affects
            for (int i = 0; i <= C.ma_end; i++)
                chks_aff[i].Checked = Data.affects[1 << i];
            // immunities
            for (int i = 0; i <= C.dt_end; i++)
                list_immunities.SetItemCheckState(i, Data.immunities[1 << i] ? CheckState.Checked : CheckState.Unchecked);
            // resistances
            for (int i = 0; i <= C.dt_end; i++)
                list_resistances.SetItemCheckState(i, Data.resistances[1 << i] ? CheckState.Checked : CheckState.Unchecked);
            // susceptibles
            for (int i = 0; i <= C.dt_end; i++)
                list_suscept.SetItemCheckState(i, Data.susceptibles[1 << i] ? CheckState.Checked : CheckState.Unchecked);

            // mob dialogs
            list_dialog_questions.Items.Clear();
            list_dialog_questions.AddToList(ParentArea, Data.dialogues, false, md => md.type == MobDialogueType.Domanda);
            list_dialog_answers.Items.Clear();

            UpdateButtonsState(null, null);
        }

        public override void Widgets2Data()
        {
            base.Widgets2Data();

            // keys
            Data.keys = edt_keys.Text;
            // damage
            Data.damage = utils.ProcessDiceText(edt_damage.Text);
            // short desc
            Data.shortdesc = edt_shortdesc.GetText();
            // long desc
            Data.longdesc = memo_longdesc.GetText();
            // description
            Data.description = memo_desc.GetText();
            // sound same room
            Data.samesound = memo_sound_sameroom.GetText();
            // sound adjacent room
            Data.adjacentsound = memo_sound_adjacent.GetText();

            Data.fame = spin_fame.Text;
            for (int i = 0; i <= C.gt_end; i++)
            {
                Data.gems[i].percent = Convert.ToInt32((tab_gemsfame.Controls.Find("gem_perc" + i, false)[0] as NumericUpDown).Value);
                Data.gems[i].dice = utils.ProcessDiceText((tab_gemsfame.Controls.Find("gem_dice" + i, false)[0] as TextBox).Text);
            }

            for (int i = 1; i <= C.et_end; i++)
                Data.epic_talents[i] = Convert.ToInt32((tab_talents.Controls.Find("spin_epic" + i, false)[0] as NumericUpDown).Value);

            // values
            for (int i = 0; i <= C.mv_end; i++)
            {
                Control c = Controls.Find("val" + i, true)[0];
                if (c.GetType() == typeof(NumericUpDown))
                    Data.values[i] = Convert.ToInt32((c as NumericUpDown).Value);
                else
                    if (c.GetType() == typeof(ComboBox))
                        Data.values[i] = Convert.ToInt32((c as ComboBox).SelectedIndex);
            }
            // flags
            for (int i = 0; i <= C.mf_end; i++)
                Data.flags[1 << i] = chks_act[i].Checked;
            // affects
            for (int i = 0; i <= C.ma_end; i++)
                Data.affects[1 << i] = chks_aff[i].Checked;
            // immunities
            for (int i = 0; i <= C.dt_end; i++)
                Data.immunities[1 << i] = list_immunities.GetItemCheckState(i) == CheckState.Checked ? true : false;
            // resistances
            for (int i = 0; i <= C.dt_end; i++)
                Data.resistances[1 << i] = list_resistances.GetItemCheckState(i) == CheckState.Checked ? true : false;
            // susceptibles
            for (int i = 0; i <= C.dt_end; i++)
                Data.susceptibles[1 << i] = list_suscept.GetItemCheckState(i) == CheckState.Checked ? true : false;
        }
        #endregion

        #region Methods
        #region Average XP Calculator (formulas are taken from LeuKreator)
        private void calcAvgXP()
        {
            int[] aiBase = new int[52]
            {
              10,     20,    40,    70,   120,   180,   300,   450,    600,    900,
              1100,   1300,  1550,  1800,  2100,  2400,  2700,  3000,   3500,   4000,
              4500,   5000,  6000,  7000,  8000,  9000, 10000, 12000,  14000,  16000,
              20000,  25000, 25000, 25000, 25000, 35000, 35000, 35000,  35000,  35000,
              45000,  45000, 55000, 55000, 75000, 75000, 95000, 95000, 125000, 125000,
              150000, 200000
            };

            int[] aiHit = new int[52]
            {
              1,   2,   3,   4,   5,   6,   7,   9,  12,  14,
              15,  16,  17,  18,  19,  20,  23,  25,  28,  30,
              33,  35,  40,  45,  50,  55,  60,  70,  80,  90,
              100, 120, 120, 120, 120, 140, 140, 140, 140, 140,
              160, 160, 180, 180, 200, 200, 225, 225, 250, 250,
              275, 300
            };

            int[] aiFlags = new int[52]
            {
              10,     15,    20,    25,    30,    40,    75,   125,   175,   300,
              450,   700,   800,   950,  1100,  1250,  1400,  1550,  1750,  2100,
              2300,  2600,  3000,  3500,  4000,  4500,  5000,  6000,  7000,  8000,
              10000, 12000, 12000, 12000, 12000, 14000, 14000, 14000, 14000, 14000,
              16000, 16000, 20000, 20000, 24000, 24000, 28000, 28000, 32000, 32000,
              36000, 40000
            };

            //if (m_xpBonus > 20)
                //qWarning("%s has an Experience Bonus > 20 (%ld).", dumpObject().toLatin1().data(), m_xpBonus);

            int iLevel = Convert.ToInt32(val2.Value);

            if (iLevel < 0)
            {
                avgxp = 1;
                return;
            }
            else if (iLevel > 51)
                iLevel = 51;

            avgxp = aiBase[iLevel] + (aiHit[iLevel] * avghp) + (aiFlags[iLevel] * Convert.ToInt32(val8.Value));

            if (act7.Checked)
                avgxp -= avgxp / 10;
            else
            {
                if (act5.Checked)
                    avgxp += avgxp / 10;

                if (act15.Checked)
                    avgxp += avgxp / 2;
            }

            if (avgxp <= 0)
                avgxp = 1;
        }
        #endregion

        private void UpdateButtonsState(object sender, EventArgs e)
        {
            // multi attacks 
            //val7.Enabled = val0.SelectedIndex == C.mt_multi_attacks || val0.SelectedIndex == C.mt_sound || val0.SelectedIndex == C.mt_unbashable;

            if (val0.SelectedIndex != C.mt_sound)
            {
                memo_sound_sameroom.Enabled = false;
                memo_sound_adjacent.Enabled = false;
            }
            else
            {
                memo_sound_sameroom.Enabled = true;
                memo_sound_adjacent.Enabled = true;
            }

            avghp =  (int)((Convert.ToInt32(val2.Value) * 4.5) + Convert.ToInt32(val6.Value));
            lblavghp.Text = "(Avg: " + avghp + ")";

            calcAvgXP();

            avgxp = Convert.ToInt32(val8.Value);
            if (val0.SelectedIndex == C.mt_simple)
            { 
                // do nothing 
            }
            else if (avgxp < 0)
                avgxp = -avgxp;
            else
                calcAvgXP();

            lblavgxp.Text = "(Avg: " + avgxp + ")";
        }
        #endregion

        #region Widgets' Methods
        private void gem_dice0_TextChanged(object sender, EventArgs e)
        {
            TextBox c = sender as TextBox;
            int i = c.SelectionStart;

            c.Text = c.Text.TrimEnd();

            if (i > c.Text.Length)
                c.SelectionStart = c.Text.Length;

            if (utils.ValidateDiceText(c.Text, true))
                c.BackColor = Color.White;
            else c.BackColor = Color.LightCoral;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            (sender as CheckedListBox).ClearSelected();
        }
        #endregion

        private void val7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                ProcessTabKey(true);
                e.SuppressKeyPress = true;
            }
        }

        private void val2_ValueChanged(object sender, EventArgs e)
        {
            UpdateButtonsState(null, null);
        }

        #region Mob Dialogs
        void refreshDialogLists()
        {
            object oldSelection = list_dialog_questions.SelectedItems.Count > 0 ? list_dialog_questions.SelectedItems[0].Tag : null;

            list_dialog_questions.Items.Clear();
            list_dialog_questions.AddToList(ParentArea, Data.dialogues, false, md => md.type == MobDialogueType.Domanda);
            list_dialog_answers.Items.Clear();

            if (oldSelection != null)
            {
                foreach (ListViewItem i in list_dialog_questions.Items)
                    if (i.Tag == oldSelection)
                    {
                        i.Selected = true;
                        return;
                    }
            }
        }

        private void btnadd_dialog_Click(object sender, EventArgs e)
        {
            MobDialogue d = new MobDialogue();

            if ((sender as Button).Name == "btnaddQ")
            {
                d.type = MobDialogueType.Domanda;
                d.vnum = d.FindFirstVNumAvailable(Data.dialogues);
            }
            else
            {
                if (list_dialog_questions.SelectedItems.Count <= 0)
                    return;

                MobDialogue question = list_dialog_questions.SelectedItems[0].Tag as MobDialogue;
                d.type = MobDialogueType.Risposta;
                d.vnum = question.vnum;
            }

            Data.dialogues.Add(d);

            if (d.Edit(ParentArea, Data, true))
                SetModified(sender, e);

            refreshDialogLists();
        }

        private void list_dialog_questions_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_dialog_answers.Items.Clear();
            if (list_dialog_questions.SelectedItems.Count == 0)
                return;

            MobDialogue question = list_dialog_questions.SelectedItems[0].Tag as MobDialogue;
            list_dialog_answers.AddToList(ParentArea, Data.dialogues, false, md => md.type == MobDialogueType.Risposta&& md.vnum == question.vnum);
        }

        private void btnedit_dialog_Click(object sender, EventArgs e)
        {
            HoGListView list = null;

            if ((sender as Button).Name == "btneditQ")
                list = list_dialog_questions;
            else list = list_dialog_answers;

            if (list.SelectedIndices.Count <= 0)
                return;

            MobDialogue x = list.SelectedItems[0].Tag as MobDialogue;
            if (x.Edit(ParentArea, Data))
                SetModified(sender, e);

            refreshDialogLists();
        }

        private void btndel_dialog_Click(object sender, EventArgs e)
        {
            HoGListView list = null;

            if ((sender as Button).Name == "btndelQ")
                list = list_dialog_questions;
            else list = list_dialog_answers;

            if (list.SelectedIndices.Count <= 0)
                return;

            MobDialogue x = list.SelectedItems[0].Tag as MobDialogue;
            Data.dialogues.Remove(x);

            SetModified(sender, e);

            refreshDialogLists();
        }
        #endregion

        private void list_dialog_questions_DoubleClick(object sender, EventArgs e)
        {
            HoGListView list = null;
            Button btn = null;

            if ((sender as HoGListView).Name == "list_dialog_questions")
            {
                btn = btneditQ;
                list = list_dialog_questions;
            }
            else
            {
                btn = btneditA;
                list = list_dialog_answers;
            }


            if (list.SelectedIndices.Count <= 0)
                return;

            btnedit_dialog_Click(btn, null);
        }

        private void btnapply_Click_1(object sender, EventArgs e)
        {

        }

        private void val3_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}