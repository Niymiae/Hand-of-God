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
    public partial class frm_area_element : Form
    {
        protected string title = "";
        public Area ParentArea;
        public area_element data;
        public area_element original;
        protected bool modified;
        protected bool applied;

        #region Constructor
        public frm_area_element()
        {
            InitializeComponent();
        }
        #endregion
        #region *Modified* Status
        public void SetModified(object sender, EventArgs e)
        {
            btnapply.Enabled = true;
            modified = true;
            SetModifiedTitle(sender, e);
        }

        public void SetNotModified()
        {
            btnapply.Enabled = false;
            modified = false;
            SetModifiedTitle(null, null);
        }

        protected virtual void SetModifiedTitle(object sender, EventArgs e)
        {
            Text = title + " [" + original.vnum + "] " + original.shortdesc + (modified ? " *modificato*" : "");
        }
        #endregion

        #region Data <-> Widgets Transfers
        public virtual void Data2Widgets()
        {
            spin_vnum.Value = data.vnum;
        }

        public virtual void Widgets2Data()
        {
            int newvnum = Convert.ToInt32(spin_vnum.Value);
            bool valid = false;

            if (data.GetType() == typeof(Zone)) 
            {
                Zone item = ParentArea.Get<Zone>(newvnum);
                if (item != null && item != original)
                    valid = Dialogs.ConfirmAndDelete<Zone>(item, ParentArea.zones, "Una zona con questo VNum esiste già, vuoi eliminarla?");
                else valid = true;
            }
            else if (data.GetType() == typeof(Room))
            {
                Room item = ParentArea.Get<Room>(newvnum);
                if (item != null && item != original)
                    valid = Dialogs.ConfirmAndDelete<Room>(item, ParentArea.rooms, "Una stanza con questo VNum esiste già, vuoi eliminarla?");
                else valid = true;
            }
            else if (data.GetType() == typeof(Mob))
            {
                Mob item = ParentArea.Get<Mob>(newvnum);
                if (item != null && item != original)
                    valid = Dialogs.ConfirmAndDelete<Mob>(item, ParentArea.mobs, "Un mob con questo VNum esiste già, vuoi eliminarlo?");
                else valid = true;
            }
            else if (data.GetType() == typeof(Obj))
            {
                Obj item = ParentArea.Get<Obj>(newvnum);
                if (item != null && item != original)
                    valid = Dialogs.ConfirmAndDelete<Obj>(item, ParentArea.objects, "Un oggetto con questo VNum esiste già, vuoi eliminarlo?");
                else valid = true;
            }
            else if (data.GetType() == typeof(Shop))
            {
                Shop item = ParentArea.Get<Shop>(newvnum);
                if (item != null && item != original)
                    valid = Dialogs.ConfirmAndDelete<Shop>(item, ParentArea.shops, "Un negozio con questo VNum esiste già, vuoi eliminarlo?");
                else valid = true;
            }

            if (valid)
                data.vnum = newvnum;
            else spin_vnum.Value = data.vnum;
        }
        #endregion

        #region Methods
        protected void ChangeElement(area_element s)
        {
            if (modified)
            {
                DialogResult result;
                if (Options.data.save_confirm)
                    result = MessageBox.Show(Dialogs.msg_confirmsave, "Conferma", MessageBoxButtons.YesNoCancel);
                else result = DialogResult.Yes;

                if (result == DialogResult.Cancel)
                    return;

                else
                    if (result == DialogResult.Yes)
                        btnapply_Click(null, null);
            }

            original = s;
            data.CopyFrom(s);
            Data2Widgets();
            SetNotModified();
        }

        public void NextVnum(bool decrement = false)
        {
            area_element ae = null;

            if (data.GetType() == typeof(Zone)) ae = ParentArea.GetNextUsed<Zone>(data.vnum, decrement);
            if (data.GetType() == typeof(Room)) ae = ParentArea.GetNextUsed<Room>(data.vnum, decrement);
            if (data.GetType() == typeof(Mob)) ae = ParentArea.GetNextUsed<Mob>(data.vnum, decrement);
            if (data.GetType() == typeof(Obj)) ae = ParentArea.GetNextUsed<Obj>(data.vnum, decrement);
            if (data.GetType() == typeof(Shop)) ae = ParentArea.GetNextUsed<Shop>(data.vnum, decrement);
            
            if (ae != null)
                ChangeElement(ae);
        }

        public void SetFixedVnum()
        {
            spin_vnum.Enabled = false;
            btnprev.Enabled = false;
            btnnext.Enabled = false;
        }

        public void SetVnumRange()
        {
            spin_vnum.Minimum = 0;
            spin_vnum.Maximum = 9999999;
            //spin_vnum.Minimum = ParentArea.vnum_min;
            //spin_vnum.Maximum = ParentArea.vnum_max;
        }
        #endregion

        #region Widgets' Methods
        private void btnnext_Click(object sender, EventArgs e)
        {
            NextVnum();
        }

        private void btnprev_Click(object sender, EventArgs e)
        {
            NextVnum(true);
        }

        private void btnrestore_Click(object sender, EventArgs e)
        {
            data.CopyFrom(original);
            Data2Widgets();
            SetNotModified();
        }

        public void btnapply_Click(object sender, EventArgs e)
        {
            Widgets2Data();

            if (original.vnum != data.vnum)
                if (data.GetType() == typeof(Zone)) ParentArea.UpdateVNumReferences<Zone>(original.vnum, data.vnum);
                else if (data.GetType() == typeof(Room)) ParentArea.UpdateVNumReferences<Room>(original.vnum, data.vnum);
                else if (data.GetType() == typeof(Mob)) ParentArea.UpdateVNumReferences<Mob>(original.vnum, data.vnum);
                else if (data.GetType() == typeof(Obj)) ParentArea.UpdateVNumReferences<Obj>(original.vnum, data.vnum);
                else if (data.GetType() == typeof(Shop)) ParentArea.UpdateVNumReferences<Shop>(original.vnum, data.vnum);
                else if (data.GetType() == typeof(Init)) ParentArea.UpdateVNumReferences<Init>(original.vnum, data.vnum);
            //cambiare il vnum prima dell'update per le exits?
            original.CopyFrom(data);
            SetNotModified();
            applied = true;
        }
        #endregion

        private void spin_vnum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                ProcessTabKey(true);
                e.SuppressKeyPress = true;
            }
        }

        private object closesender;
        private void frm_area_element_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closesender == btnok)
                return;

            if (modified && Options.data.save_confirm)
            {
                DialogResult result = MessageBox.Show(Dialogs.msg_confirmsave, "Conferma", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    btnapply_Click(null, null);
                    DialogResult = DialogResult.OK;
                }
            }

            if (applied)
                DialogResult = DialogResult.Retry; // force a list refresh if the element was modified and saved in this form
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            closesender = sender;
        }
    }
}
