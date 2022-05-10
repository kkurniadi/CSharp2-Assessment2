using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentTwo
{
    [Serializable]
    internal class Information : IComparable<Information>
    {
        // 6.1 Create a separate class file to hold the four data items of the Data Structure
        // (use the Data Structure Matrix as a guide).
        // Use auto-implemented properties for the fields which must be of type “string”.
        // Save the class as “Information.cs”.
        private string Name;
        private string Category;
        private string Structure;
        private string Definition;

        public Information()
        { }

        public Information(string itemName)
        {
            Name = itemName;
        }

        public Information(string itemName, string itemCategory, string itemStructure, string itemDefinition)
        {
            Name = itemName;
            Category = itemCategory;
            Structure = itemStructure;
            Definition = itemDefinition;
        }

        public string getName()
        {
            return Name;
        }

        public void setName(string newName)
        {
            Name = newName;
        }

        public string getCategory()
        {
            return Category;
        }

        public void setCategory(string newCategory)
        {
            Category = newCategory;
        }

        public string getStructure()
        {
            return Structure;
        }

        public void setStructure(string newStructure)
        {
            Structure = newStructure;
        }

        public string getDefinition()
        {
            return Definition;
        }

        public void setDefinition(string newDefinition)
        {
            Definition = newDefinition;
        }

        public int CompareTo(Information other)
        {
            return Name.CompareTo(other.getName());
        }
    }
}
