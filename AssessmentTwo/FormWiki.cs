using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Kirsten Kurniadi, ID: 30045816
// Date: 10/05/2022
// Assessment Task Two (Individual Project)
// Using List<T>, Classes and Windows Forms
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
                info.setStructure(GetRadioButtons());
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
            FillComboBox();
        }
        private void FillComboBox()
        {
            foreach (string cat in categories)
            {
                comboBoxCategory.Items.Add(cat);
            }
        }
        // 6.5 Create a custom ValidName method which will take a parameter string value
        // from the Textbox Name and returns a Boolean after checking for duplicates.
        // Use the built in List<T> method “Exists” to answer this requirement.
        private bool ValidName(string checkName)
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            if (Wiki.Exists(duplicate => duplicate.getName().Equals(checkName)))
            {
                statusStrip.Text = "Name already in use";
                Trace.WriteLine(checkName + " is not a valid name");
                return false;
            }
            else
            {
                Trace.WriteLine(checkName + " is valid");
                return true;
            }
        }
        // 6.6 Create two methods to highlight and return the values from the Radio button GroupBox.
        // The first method must return a string value from the selected radio button (Linear or Non-Linear).
        // The second method must send an integer index which will highlight an appropriate radio button.
        private string GetRadioButtons()
        {
            if (radioButtonLinear.Checked)
                return radioButtonLinear.Text;
            else if (radioButtonNonLin.Checked)
                return radioButtonNonLin.Text;
            else
                return "";
        }
        private void SetRadioButtons(int i)
        {
            if (i == 0)
                radioButtonLinear.Checked = true;
            else if (i == 1)
                radioButtonNonLin.Checked = true;
            else
                MessageBox.Show("Structure undefined", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        // TODO: 6.7 Create a button method that will delete the currently selected record in the ListView.
        // Ensure the user has the option to backout of this action by using a dialog box.
        // Display an updated version of the sorted list at the end of this process.
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listViewDisplay.SelectedIndices[0] == -1)
            {
                statusStrip.Text = "Please select an entry to delete";
            }
            else
            {
                int delIndex = listViewDisplay.SelectedIndices[0];
                MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.OK)
                {
                    Wiki.RemoveAt(delIndex);
                    statusStrip.Text = "Entry has been deleted.";
                    ResetInputs();
                    DisplayAllData();
                }
            }
        }
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
        private void listViewDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            int currentEntry = listViewDisplay.SelectedIndices[0];
            textBoxName.Text = Wiki[currentEntry].getName();
            int catIndex = comboBoxCategory.FindString(Wiki[currentEntry].getCategory());
            if (catIndex != -1)
                comboBoxCategory.SelectedIndex = catIndex;
            if (Wiki[currentEntry].getStructure() == "Linear")
                SetRadioButtons(0);
            else if (Wiki[currentEntry].getStructure() == "Non-Linear")
                SetRadioButtons(1);
            textBoxDefinition.Text = Wiki[currentEntry].getDefinition();
        }
        // 6.12 Create a custom method that will clear and reset the TextBoxes, ComboBox and Radio button
        private void ResetInputs()
        {
            textBoxName.Clear();
            comboBoxCategory.ResetText();
            radioButtonLinear.Checked = false;
            radioButtonNonLin.Checked = false;
            textBoxDefinition.Clear();
        }

        // 6.13 Create a double click event on the Name TextBox to clear the TextBoxes, ComboBox and Radio button.
        private void textBoxName_DoubleClick(object sender, EventArgs e)
        {
            ResetInputs();
        }

        // TODO: 6.14 Create two buttons for the manual open and save option;
        // this must use a dialog box to select a file or rename a saved file.
        // All Wiki data is stored/retrieved using a binary file format.

        // TODO: 6.15 The Wiki application will save data when the form closes.

    }
}
