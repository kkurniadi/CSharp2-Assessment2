using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentTwo
{
    [Serializable]
    internal class Information
    {
        private string Name;
        private string Category;
        private string Structure;
        private string Definition;

        public Information()
        { }

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
    }
}
