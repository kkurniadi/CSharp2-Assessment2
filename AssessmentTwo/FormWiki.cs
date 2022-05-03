using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssessmentTwo
{
    public partial class FormWiki : Form
    {
        public FormWiki()
        {
            InitializeComponent();
        }
        List<Information> Wiki = new List<Information>();

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Information info = new Information();
            info.setName(textBoxName.Text);
            info.setCategory(comboBoxCategory.SelectedItem.ToString());
            info.setStructure(CheckRadioButtons());
            info.setDefinition(textBoxDefinition.Text);
            Wiki.Add(info);
            ResetInputs();
            DisplayAllData();
            statusStrip.Text = "New entry added.";
        }

        private void DisplayAllData()
        {
            listViewDisplay.Items.Clear();
            foreach(Information info in Wiki)
            {
                ListViewItem lvi = new ListViewItem(info.getName());
                lvi.SubItems.Add(info.getCategory());
                listViewDisplay.Items.Add(lvi);
            }
        }

        private void ResetInputs()
        {
            textBoxName.Clear();
            comboBoxCategory.ResetText();
            radioButtonLinear.Checked = false;
            radioButtonNonLin.Checked = false;
            textBoxDefinition.Clear();
        }

        private string CheckRadioButtons()
        {
            if (radioButtonLinear.Checked)
                return radioButtonLinear.Text;
            else if (radioButtonNonLin.Checked)
                return radioButtonNonLin.Text;
            else
                return "";
        }
    }
}
