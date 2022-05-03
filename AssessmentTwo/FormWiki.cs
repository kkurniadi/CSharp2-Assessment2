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
        // 6.2 Create a global List<T> of type Information called Wiki.
        List<Information> Wiki = new List<Information>();
        string[] categories = { "Array", "List", "Tree", "Graphs", "Abstract", "Hash" };

        // 6.3 Create a button method to ADD a new item to the list.
        // Use a TextBox for the Name input, ComboBox for the Category,
        // Radio group for the Structure and Multiline TextBox for the Definition.
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Information info = new Information();
            if (ValidName(textBoxName.Text))
            {
                info.setName(textBoxName.Text);
                info.setCategory(comboBoxCategory.SelectedItem.ToString());
                info.setStructure(CheckRadioButtons());
                info.setDefinition(textBoxDefinition.Text);
                Wiki.Add(info);
                ResetInputs();
                DisplayAllData();
                statusStrip.Text = "New entry added.";
            }
        }
        // 6.4 Create and initialise a global string array
        // with the six categories as indicated in the Data Structure Matrix.
        // Create a custom method to populate the ComboBox when the Form Load method is called.
        private void FormWiki_Load(object sender, EventArgs e)
        {
            foreach (string cat in categories)
            {
                comboBoxCategory.Items.Add(cat);
            }
        }
        // TODO: 6.5 Create a custom ValidName method which will take a parameter string value
        // from the Textbox Name and returns a Boolean after checking for duplicates.
        // Use the built in List<T> method “Exists” to answer this requirement.
        private bool ValidName(string checkName)
        {
            if (Wiki.Exists(duplicate => duplicate.getName().Equals(checkName)))
            {
                statusStrip.Text = "Name already in use";
                return false;
            }
            else
                return true;
        }
        // 6.6 Create two methods to highlight and return the values from the Radio button GroupBox.
        // The first method must return a string value from the selected radio button (Linear or Non-Linear).
        // The second method must send an integer index which will highlight an appropriate radio button.
        private string CheckRadioButtons()
        {
            if (radioButtonLinear.Checked)
                return radioButtonLinear.Text;
            else if (radioButtonNonLin.Checked)
                return radioButtonNonLin.Text;
            else
                return "";
        }
        // TODO: 6.7 Create a button method that will delete the currently selected record in the ListView.
        // Ensure the user has the option to backout of this action by using a dialog box.
        // Display an updated version of the sorted list at the end of this process.

        // TODO: 6.8 Create a button method that will save the edited record of the currently selected item in the ListView.
        // All the changes in the input controls will be written back to the list.
        // Display an updated version of the sorted list at the end of this process.

        // 6.9 Create a single custom method that will sort and then display
        // the Name and Category from the wiki information in the list.
        private void DisplayAllData()
        {
            Wiki.Sort();
            listViewDisplay.Items.Clear();
            foreach (Information info in Wiki)
            {
                ListViewItem lvi = new ListViewItem(info.getName());
                lvi.SubItems.Add(info.getCategory());
                listViewDisplay.Items.Add(lvi);
            }
        }
        // TODO: 6.10 Create a button method that will use the builtin binary search to find a Data Structure name.
        // If the record is found the associated details will populate
        // the appropriate input controls and highlight the name in the ListView.
        // At the end of the search process the search input TextBox must be cleared.

        // TODO: 6.11 Create a ListView event so a user can select a Data Structure Name from the list of Names
        // and the associated information will be displayed in the related text boxes combo box and radio button.

        // 6.12 Create a custom method that will clear and reset the TextBoxes, ComboBox and Radio button
        private void ResetInputs()
        {
            textBoxName.Clear();
            comboBoxCategory.ResetText();
            radioButtonLinear.Checked = false;
            radioButtonNonLin.Checked = false;
            textBoxDefinition.Clear();
        }

        // TODO: 6.13 Create a double click event on the Name TextBox to clear the TextBboxes, ComboBox and Radio button.

        // TODO: 6.14 Create two buttons for the manual open and save option;
        // this must use a dialog box to select a file or rename a saved file.
        // All Wiki data is stored/retrieved using a binary file format.

        // TODO: 6.15 The Wiki application will save data when the form closes.

    }
}
