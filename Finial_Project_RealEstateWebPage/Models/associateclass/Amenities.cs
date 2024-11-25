using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Amenities
    {
        private List<string> selectedAmenities = new List<string>();

        public Amenities(List<string> selectedUtility)
        {
            this.selectedAmenities = selectedUtility;
        }
        //Property to access the selectedUtility
        public List<string> SelectedAmenities
        {
            get { return selectedAmenities; }
            set { selectedAmenities = value; }
        }
    }
}
