using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Utility
    {
      private List<string> selectedUtility = new List<string>();

        public Utility(List<string> selectedUtility) 
        { 
            this.selectedUtility = selectedUtility;
        }
        //Property to access the selectedUtility
        public List<string> SelectedUtility
        {
            get { return selectedUtility;}
            set { selectedUtility = value;}
        }
    }
}
