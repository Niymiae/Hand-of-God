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
    public partial class frm_MobDialogue : Form
    {
        public Area ParentArea;
        public Mob ParentMob;
        public MobDialogue Original;
        public MobDialogue Data;
        bool modified;
        bool wait_answer;

        List<CheckBox> checkEnablers;
        List<CheckBox> rollEnablers;
        List<NumericUpDown> checkValues;
        List<NumericUpDown> rollValues;
        List<NumericUpDown> rollFails;
        List<MudlikeRichTextBox> descriptions;

        #region Constructor
        public frm_MobDialogue()
        {
            InitializeComponent();

            checkEnablers = new List<CheckBox>();
            rollEnablers= new List<CheckBox>();
            checkValues = new List<NumericUpDown>();
            rollValues = new List<NumericUpDown>();
            rollFails = new List<NumericUpDown>();

            Data = new MobDialogue(null);
            descriptions = new List<MudlikeRichTextBox>();
            // descriptions
            for (int i = 0; i <= C.mdd_end; i++)
            {
                Control cnt = Controls.Find("desc" + i, true)[0];
                descriptions.Add(cnt as MudlikeRichTextBox);
                gaugecharcounter.AddEvents(cnt as MudlikeRichTextBox);
            }

            for (int i = 0; i <= C.ms_end; i++)
            {
                // checks
                Control cnt = Controls.Find("check" + i, true)[0];
                checkEnablers.Add(cnt as CheckBox);

                cnt = Controls.Find("check" + i + "_val", true)[0];
                checkValues.Add(cnt as NumericUpDown);
            }

            for (int i = 0; i <= C.ms_end; i++)
            {
                //rolls
                Control cnt = Controls.Find("roll" + i, true)[0];
                //cnt = Controls.Find("roll" + i, true)[0];
                rollEnablers.Add(cnt as CheckBox);

                cnt = Controls.Find("roll" + i + "_val", true)[0];
                rollValues.Add(cnt as NumericUpDown);

                cnt = Controls.Find("roll" + i + "_fail", true)[0];
                rollFails.Add(cnt as NumericUpDown);
            }


            func_success.ValueType = 'F';
            func_fail.ValueType = 'F';

            utils.AddFormEvents(Controls, SetModified);
        }
        #endregion

        #region *Modified* Status
        private void SetModified(object sender, EventArgs e)
        {
            btnapply.Enabled = true;
            modified = true;
            frm_MobDialog_Shown(null, null);
        }

        public void SetNotModified()
        {
            btnapply.Enabled = false;
            modified = false;
            frm_MobDialog_Shown(null, null);
        }

        private void frm_MobDialog_Shown(object sender, EventArgs e)
        {
            Text = "Dialogo (" + utils.CutColorCodes(ParentMob.shortdesc) + ")" + (modified ? " *modificata*" : "");
        }
        #endregion

        #region Widgets <-> Data Transfers
        public void Widgets2Data()
        {
            //no need to change type
            Data.vnum = (int)edt_id.Value;

            /*
            if (Data.type == MobDialogueType.Domanda)
            {
                Data.next = combo_Qnext.SelectedIndex;
            }
            else
            {
                Data.next = (int)edt_Anext.Value;
            }
            */
            Data.next = (int)edt_Anext.Value;

            for (int i = 0; i <= C.mdd_end; i++)
                Data.descriptions[i] = descriptions[i].GetText();

            // checks
            for (int i = 0; i < checkEnablers.Count(); i++)
            {
                if (checkEnablers[i].Checked)
                {
                    if (!Data.checkData.ContainsKey(i))
                        Data.checkData.Add(i, new MDCheckData());

                    Data.checkData[i].amount = (int)checkValues[i].Value;
                }
                else if (Data.checkData.ContainsKey(i))
                        Data.checkData.Remove(i);
            }

            // rolls
            for (int i = 0; i < rollEnablers.Count(); i++)
            {
                if (rollEnablers[i].Checked)
                {
                    if (!Data.rollData.ContainsKey(i))
                        Data.rollData.Add(i, new MDRollData());

                    Data.rollData[i].amount = (int)rollValues[i].Value;
                    Data.rollData[i].fail_next = (int)rollFails[i].Value;
                }
                else if (Data.rollData.ContainsKey(i))
                    Data.rollData.Remove(i);
            }

            Data.onSuccess.vnum = (int)func_success.Value;
            Data.onSuccess.param1 = edt1_func_success.Text;
            Data.onSuccess.param2 = edt2_func_success.Text;

            Data.onFail.vnum = (int)func_fail.Value;
            Data.onFail.param1 = edt1_func_fail.Text;
            Data.onFail.param2 = edt2_func_fail.Text;
        }

        public void Data2Widgets()
        {
            edt_type.Text = Enum.GetName(typeof(MobDialogueType), Data.type);
            edt_id.Value = Data.vnum;

            /*
            if (Data.type == MobDialogueType.Domanda)
            {
                edt_id.Enabled = true;
                combo_Qnext.Show();
                combo_Qnext.SelectedIndex = Data.next == 0 ? 0 : 1;
                edt_Anext.Hide();
                btn_Afindnext.Hide();
            }
            else
            {
                edt_id.Enabled = false;
                combo_Qnext.Hide();
                edt_Anext.Show();
                edt_Anext.Value = Data.next;
                btn_Afindnext.Show();
            }
            */
            edt_Anext.Value = Data.next;

            for (int i = 0; i <= C.mdd_end; i++)
                descriptions[i].SetText(Data.descriptions[i]);

            // checks
            for (int i = 0; i < checkEnablers.Count(); i++)
            {
                if (Data.checkData.ContainsKey(i))
                {
                    checkEnablers[i].Checked = true;
                    checkValues[i].Value = Data.checkData[i].amount;
                }
                else
                {
                    checkEnablers[i].Checked = false;
                    checkValues[i].Value = 0;
                }
            }

            // rolls
            for (int i = 0; i < rollEnablers.Count(); i++)
            {
                if (Data.rollData.ContainsKey(i))
                {
                    rollEnablers[i].Checked = true;
                    rollValues[i].Value = Data.rollData[i].amount;
                    rollFails[i].Value = Data.rollData[i].fail_next;
                }
                else
                {
                    rollEnablers[i].Checked = false;
                    rollValues[i].Value = 0;
                }
            }

            func_success.SetValue(Data.onSuccess.vnum);
            edt1_func_success.Text = Data.onSuccess.param1;
            edt2_func_success.Text = Data.onSuccess.param2;

            func_fail.SetValue(Data.onFail.vnum);
            edt1_func_fail.Text = Data.onFail.param1;
            edt2_func_fail.Text = Data.onFail.param2;
        }
        #endregion

        void toggle_control(string s, bool visibility)
        {
            Control[] list = Controls.Find(s, true);
            Control cnt = list.Count() > 0 ? list[0] : null;

            if (cnt == null)
                return;

            if (visibility)
                cnt.Show();
            else cnt.Hide();
        }

        void updateCheckBoxColor(CheckBox chk)
        {
            toggle_control(chk.Name + "_val", chk.Checked);
            toggle_control(chk.Name + "_fail", chk.Checked);
            toggle_control(chk.Name + "_failbtn", chk.Checked);
            toggle_control(chk.Name + "_faillbl", chk.Checked);

            if (chk.Checked)
                chk.BackColor = Color.LightGreen;
            else
                chk.BackColor = SystemColors.Control;
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void func_success_ValueChanged(object sender, EventArgs e)
        {
            ValAffEditbox snd = sender as ValAffEditbox;
            Label param1 = Controls.Find("param1_" + snd.Name, true)[0] as Label;
            Label param2 = Controls.Find("param2_" + snd.Name, true)[0] as Label;
            TextBox edt1 = Controls.Find("edt1_" + snd.Name, true)[0] as TextBox;
            TextBox edt2 = Controls.Find("edt2_" + snd.Name, true)[0] as TextBox;
            int value = Convert.ToInt32(snd.Value);
            MDFuncData f = Database.md_functions.Find(x => x.vnum == value);

            if (f != null)
            {
                if (f.desc1 != "")
                {
                    param1.Text = f.desc1;
                    param1.Show();
                    edt1.Show();
                }
                else
                {
                    edt1.Hide();
                    param1.Hide();
                }

                if (f.desc2 != "")
                {
                    param2.Text = f.desc2;
                    param2.Show();
                    edt2.Show();
                }
                else
                {
                    edt2.Hide();
                    param2.Hide();
                }
            }
            else
            {
                edt1.Hide();
                edt2.Hide();
                param1.Hide();
                param2.Hide();
            }
        }

        private void btn_findnext_Click(object sender, EventArgs e)
        {
            using (dlg_select_element form = new dlg_select_element())
            {
                Dictionary<int, string> next_top_values = new Dictionary<int, string>();
                next_top_values.Add(-1, "Esci dal Dialogo");
                if (Data.type == MobDialogueType.Domanda)
                    next_top_values.Add(-2, "Attendi Risposta");
                form.SetElements<MobDialogue>(ParentArea, C.i_mobdialog_Q, ParentMob.dialogues, false, x => (x as MobDialogue).type == MobDialogueType.Domanda, next_top_values, Original);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    int result = form.result.GetType() == typeof(MobDialogue) ? (form.result as MobDialogue).vnum : (int)form.result;
                    if (result == -2)
                        edt_Anext.Value = edt_id.Value;
                    else
                        edt_Anext.Value = result;
                }
            }
        }

        private void btnapply_Click(object sender, EventArgs e)
        {
            Widgets2Data();

            if (Data.vnum != Original.vnum)
            {
                int newVnum = Data.vnum;
                MobDialogue existing = ParentMob.dialogues.Find(x => x != Original && x.type == MobDialogueType.Domanda && x.vnum == newVnum);
                if (existing != null)
                {
                    if (Dialogs.Confirm("Cambiare l'ID comporterà la sovrascrizione della frase\n\"" + existing.descriptions[0] + "\"\nContinuare?"))
                        Original.updateVNum(ParentMob.dialogues, Original.vnum, newVnum);
                }
                else Original.updateVNum(ParentMob.dialogues, Original.vnum, newVnum);
            }

            Original.CopyFrom(Data, false);
            SetNotModified();
        }

        private void btnrestore_Click(object sender, EventArgs e)
        {
            Data.CopyFrom(Original);
            Data2Widgets();
            SetNotModified();
        }

        private void roll0_failbtn_Click(object sender, EventArgs e)
        {
            using (dlg_select_element form = new dlg_select_element())
            {
                form.SetElements<MobDialogue>(ParentArea, C.i_mobdialog_Q, ParentMob.dialogues, false, x => (x as MobDialogue).type == MobDialogueType.Domanda);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    
                    int index = (int)char.GetNumericValue((sender as Button).Name[4]);
                    int lmaoIndexTwo = (int)char.GetNumericValue((sender as Button).Name[5]);

                    if (lmaoIndexTwo > 0)
                        index = (index * 10) + lmaoIndexTwo;

                    rollFails[index].Value = form.result.GetType() == typeof(MobDialogue) ? (form.result as MobDialogue).vnum : (int)form.result;
                }
            }
        }

        private void check9_CheckedChanged(object sender, EventArgs e)
        {
            updateCheckBoxColor(sender as CheckBox);
        }

        private void combo_Qnext_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void edt_Anext_ValueChanged(object sender, EventArgs e)
        {
            if (Data.type != MobDialogueType.Domanda)
                return;

            wait_answer = edt_Anext.Value == edt_id.Value;
        }

        private void edt_id_ValueChanged(object sender, EventArgs e)
        {
            if (wait_answer)
                edt_Anext.Value = edt_id.Value;
        }

        private void edt1_func_success_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
