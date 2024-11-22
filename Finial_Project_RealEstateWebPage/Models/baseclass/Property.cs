using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Property
    {
        // Private fields (general fields for all properties)
        private string buildingNumber;
        private string street;
        private string city;
        private string state;
        private int zipCode;
        private string propertyType;
        private int yearBuilt;

        // Constructor to initialize the general fields
        public Property(string buildingNumber, string street, string city, string state, int zipCode, string propertyType, int yearBuilt)
        {
            this.buildingNumber = buildingNumber;
            this.street = street;
            this.city = city;
            this.state = state;
            this.zipCode = zipCode;
            this.propertyType = propertyType;
            this.yearBuilt = yearBuilt;
        }

        // Public properties with get and set

        public string BuildingNumber
        {
            get { return buildingNumber; }
            set { buildingNumber = value; }
        }
        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public int ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

        public string PropertyType
        {
            get { return propertyType; }
            set { propertyType = value; }
        }

        public int YearBuilt
        {
            get { return yearBuilt; }
            set { yearBuilt = value; }
        }

    }
}
