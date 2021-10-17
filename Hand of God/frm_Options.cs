using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HandofGod
{
    public partial class dlg_setdirs : Form
    {
        public OptionsData Data;
        public dlg_setdirs()
        {
            InitializeComponent();
            Data = new OptionsData();
        }

        private void Widgets2Data()
        {
            for (int i = 0; i < Options.max_dirs; i++)
                Data.directories[i] = (tab_dirs.Controls.Find("edt" + i.ToString(), false)[0] as TextBox).Text;

            Data.auto_load = chk_autoload.Checked;
            Data.save_confirm = chk_saveconfirm.Checked;
            Data.del_confirm = chk_delconfirm.Checked;
            Data.check_samedir = chk_samedir.Checked;
            Data.init_opendoors = chk_initdopenoors.Checked;
            Data.updatevnums_auto = chk_updatevnums.Checked;
            Data.dontsave_hogfile = chk_dontsavehogfile.Checked;
            Data.format_helpzon = chk_format_helpzon.Checked;
            Data.descriptions_font = utils.StringToFont(fontdesc.Text);
            Data.file_format = radio_fileformat1.Checked ? 1 : 0;
            Data.delete_vnum_refs = chk_delete_vnum_refs.Checked;
        }

        private void Data2Widgets()
        {
            for (int i = 0; i < Options.max_dirs; i++)
                (tab_dirs.Controls.Find("edt" + i.ToString(), false)[0] as TextBox).Text = Data.directories[i];

            chk_autoload.Checked = Data.auto_load;
            chk_saveconfirm.Checked = Data.save_confirm;
            chk_delconfirm.Checked = Data.del_confirm;
            chk_samedir.Checked = Data.check_samedir;
            chk_initdopenoors.Checked = Data.init_opendoors;
            chk_updatevnums.Checked = Data.updatevnums_auto;
            chk_dontsavehogfile.Checked = Data.dontsave_hogfile;
            chk_format_helpzon.Checked = Data.format_helpzon;
            chk_delete_vnum_refs.Checked = Data.delete_vnum_refs;
            fontdesc.Text = utils.FontToString(Data.descriptions_font);
            if (Data.file_format == 1)
                radio_fileformat1.Checked = true;
            else radio_fileformat0.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Widgets2Data();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    int i = Convert.ToInt32((sender as Button).Name.Replace("btn", ""));
                    Data.directories[i] = fbd.SelectedPath;
                }
            Data2Widgets();
        }

        private void frm_options_Shown(object sender, EventArgs e)
        {
            Data2Widgets();
        }

        private void btnfontdesc_def_Click(object sender, EventArgs e)
        {
            fontdesc.Text = Options.default_font_desc;
        }

        private void fontdesc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fontdesc.Font = utils.StringToFont(fontdesc.Text);
            }
            catch
            {
                fontdesc.Font = new Font("Microsoft Sans Serif", 9);
            }
        }

        private void btnfontdesc_Click(object sender, EventArgs e)
        {
            Font tmp = Options.data.descriptions_font;
            Options.data.ChangeDescriptionsFont();
            fontdesc.Text = utils.FontToString(Options.data.descriptions_font);
            Options.data.descriptions_font = tmp;
        }
    }
}
