using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        
        string defaultFileName = "Wiki.bin";
        // 6.3 Create a button method to ADD a new item to the list.
        // Use a TextBox for the Name input, ComboBox for the Category,
        // Radio group for the Structure and Multiline TextBox for the Definition.
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Information info = new Information();
            if (!string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                if (comboBoxCategory.SelectedItem != null)
                {
                    if (!string.IsNullOrEmpty(GetRadioButtons()))
                    {
                        if (!string.IsNullOrWhiteSpace(textBoxDefinition.Text))
                        {
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
                        else
                        {
                            MessageBox.Show("Please enter a definition.", "Add Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a structure.", "Add Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                } 
                else
                {
                    MessageBox.Show("Please select a category from the box.", "Add Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter a name.", "Add Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string[] categories = File.ReadAllLines(@"categories.txt");
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
                MessageBox.Show("Name already in use", "Add Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Trace.WriteLine(checkName + " is not a valid name");
                return false;
            }
            else
            {
                Trace.WriteLine(checkName + " is a valid name");
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
            {
                radioButtonLinear.Checked = false;
                radioButtonNonLin.Checked = false;
            }
        }
        // 6.7 Create a button method that will delete the currently selected record in the ListView.
        // Ensure the user has the option to backout of this action by using a dialog box.
        // Display an updated version of the sorted list at the end of this process.
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            if (listViewDisplay.SelectedIndices.Count > 0)
            {
                int delIndex = listViewDisplay.SelectedIndices[0];
                DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    Wiki.RemoveAt(delIndex);
                    statusStrip.Text = "Entry has been deleted.";
                    ResetInputs();
                    DisplayAllData();
                    Trace.WriteLine("Deleted at index " + delIndex);
                }
                else
                {
                    statusStrip.Text = "Cancelled entry deletion.";
                    Trace.WriteLine("Cancelled deletion at index " + delIndex);
                }
            }
            else
            {
                MessageBox.Show("Please select an entry to delete", "Delete Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Trace.WriteLine("Pressed delete but nothing selected");
            }
        }
        // 6.8 Create a button method that will save the edited record of the currently selected item in the ListView.
        // All the changes in the input controls will be written back to the list.
        // Display an updated version of the sorted list at the end of this process.
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            if (listViewDisplay.SelectedIndices.Count > 0)
            {
                int editIndex = listViewDisplay.SelectedIndices[0];
                Information editInfo = Wiki.ElementAt(editIndex);
                editInfo.setName(textBoxName.Text);
                editInfo.setCategory(comboBoxCategory.SelectedItem.ToString());
                editInfo.setStructure(GetRadioButtons());
                editInfo.setDefinition(textBoxDefinition.Text);
                ResetInputs();
                DisplayAllData();
                statusStrip.Text = "Entry edited.";
                Trace.WriteLine("Edited entry at index " + editIndex);
            }
            else
            {
                MessageBox.Show("Please select an entry to edit", "Edit Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Trace.WriteLine("Pressed edit but nothing selected");
            }
        }
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
        // 6.10 Create a button method that will use the builtin binary search to find a Data Structure name.
        // If the record is found the associated details will populate
        // the appropriate input controls and highlight the name in the ListView.
        // At the end of the search process the search input TextBox must be cleared.
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Information target = new Information(textBoxSearch.Text);
            int found = Wiki.BinarySearch(target);
            if (found >= 0)
            {
                listViewDisplay.SelectedItems.Clear();
                listViewDisplay.Items[found].Selected = true;
                DisplayForOne(found);
                statusStrip.Text = "Entry found";
            }
            else
            {
                ResetInputs();
                statusStrip.Text = "Not found";
            }
            textBoxSearch.Clear();
        }
        // 6.11 Create a ListView event so a user can select a Data Structure Name from the list of Names
        // and the associated information will be displayed in the related text boxes combo box and radio button.
        private void listViewDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            int currentEntry = listViewDisplay.SelectedIndices[0];
            DisplayForOne(currentEntry);
        }
        private void DisplayForOne(int x)
        {
            textBoxName.Text = Wiki[x].getName();
            int catIndex = comboBoxCategory.FindString(Wiki[x].getCategory());
            if (catIndex != -1)
                comboBoxCategory.SelectedIndex = catIndex;
            if (Wiki[x].getStructure() == "Linear")
                SetRadioButtons(0);
            else if (Wiki[x].getStructure() == "Non-Linear")
                SetRadioButtons(1);
            textBoxDefinition.Text = Wiki[x].getDefinition();
        }
        // 6.12 Create a custom method that will clear and reset the TextBoxes, ComboBox and Radio button
        private void ResetInputs()
        {
            textBoxName.Clear();
            comboBoxCategory.SelectedIndex = -1;
            SetRadioButtons(-1);
            textBoxDefinition.Clear();
        }
        // 6.13 Create a double click event on the Name TextBox to clear the TextBoxes, ComboBox and Radio button.
        private void textBoxName_DoubleClick(object sender, EventArgs e)
        {
            ResetInputs();
        }

        // 6.14 Create two buttons for the manual open and save option;
        // this must use a dialog box to select a file or rename a saved file.
        // All Wiki data is stored/retrieved using a binary file format.
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openData = new OpenFileDialog
            {
                InitialDirectory = Application.StartupPath,
                Filter = "BIN Files|*.bin",
                Title = "Open file..."
            };
            if (openData.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openData.FileName);
            }
            else
                statusStrip.Text = "Cancelled file opening";
        }
        private void OpenFile(string openFileName)
        {
            try
            {
                using (Stream stream = File.Open(openFileName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    while (stream.Position < stream.Length)
                    {
                        Wiki = (List<Information>) bin.Deserialize(stream);
                    }
                    DisplayAllData();
                }
                statusStrip.Text = "Opened data from file";
            }
            catch (IOException ex)
            {
                statusStrip.Text = ex.ToString();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveData = new SaveFileDialog
            {
                InitialDirectory = Application.StartupPath,
                Filter = "BIN Files|*.bin",
                Title = "Save file...",
                FileName = defaultFileName,
                DefaultExt = "bin"
            };
            if (saveData.ShowDialog() == DialogResult.OK)
            {
                SaveFile(saveData.FileName);
            }
            else
                statusStrip.Text = "Cancelled saving the data";
        }
        private void SaveFile(string saveFileName)
        {
            try
            {
                using (Stream stream = File.Open(saveFileName, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, Wiki);
                }
                statusStrip.Text = "Data has been saved";
            }
            catch (IOException ex)
            {
                statusStrip.Text = ex.ToString();
            }
        }
        // 6.15 The Wiki application will save data when the form closes.
        private void FormWiki_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveFile(defaultFileName);
        }
    }
}
