using ClassLibrary;
using FunctionClassLibrary.AssociateClass;
using System.ComponentModel.DataAnnotations;



namespace Finial_Project_RealEstateWebPage.Models
{
    public class HomeInfo
    {
            // Private fields specific to Home
            private string propertyID;
            private string agentID;
            private int bedRooms;
            private int bathRooms;
            private string heating;
            private string cooling;
            private decimal askingPrice;
            private string homeSize;
            private string description;
            private string garage;
            private string buildingNumber;
            private string street;
            private string city;
            private string state;
            private int zipCode;
            private string propertyType;
            private int yearBuilt;
            private Utility utility; //Utlity is a part of Home class.
            private Amenities amenities; //Utlity is a part of Home class.
            private List<Rooms> rooms; // List of rooms
            private List<PropertyImage> propertyImages;//List of Property Images
                                                       // Constructor to initialize the fields, including those from Property

        public HomeInfo()
        {

        }
        public HomeInfo(string buildingNumber, string propertyID, string agentID, string street, string city, string state, int zipCode,
                        string propertyType, int yearBuilt, int bedRooms, int bathRooms, string heating, string cooling,
                        decimal askingPrice, string homeSize, string description, string garage,
                        List<string> utilities, List<string> amenities, List<Rooms> rooms, List<PropertyImage> propertyImages) // Initialize Property fields
            {
                this.propertyID = propertyID;
                this.agentID = agentID;
                this.bedRooms = bedRooms;
                this.bathRooms = bathRooms;
                this.heating = heating;
                this.cooling = cooling;
                this.askingPrice = askingPrice;
                this.homeSize = homeSize;
                this.description = description;
                this.garage = garage;
                this.utility = new Utility(utilities);// Utlities Object with select Items.
                this.amenities = new Amenities(amenities);// amenities Object with select Items.
                this.rooms = rooms; // Store the list of rooms directly
                this.propertyImages = propertyImages;
                this.buildingNumber = buildingNumber;
                this.street = street;
                this.city = city;
                this.state = state;
                this.zipCode = zipCode;
                this.propertyType = propertyType;
                this.yearBuilt = yearBuilt;
        }

        // Public properties with get and set for the specific fields
            [Required(ErrorMessage = "Property ID is required.")]
            public string PropertyID
            {
                get { return propertyID; }
                set { propertyID = value; }
            }

            [Required(ErrorMessage = "Agent ID is required.")]
            public string AgentID
            {
                get { return agentID; }
                set { agentID = value; }
            }

            [Required(ErrorMessage = "Bed Rooms is required.")]
            public int BedRooms
            {
                get { return bedRooms; }
                set { bedRooms = value; }
            }

            [Required(ErrorMessage = "Bath Rooms is required.")]
            public int BathRooms
            {
                get { return bathRooms; }
                set { bathRooms = value; }
            }

            [Required(ErrorMessage = "Heating is required.")]
            public string Heating
            {
                get { return heating; }
                set { heating = value; }
            }

            [Required(ErrorMessage = "Cooling is required.")]
            public string Cooling
            {
                get { return cooling; }
                set { cooling = value; }
            }

            [Required(ErrorMessage = "Asking Price is required.")]
            public decimal AskingPrice
            {
                get { return askingPrice; }
                set { askingPrice = value; }
            }

            [Required(ErrorMessage = "Home Size is required.")]
            public string HomeSize
            {
                get { return homeSize; }
                set { homeSize = value; }
            }

            [Required(ErrorMessage = "Description is required.")]
            public string Description
            {
                get { return description; }
                set { description = value; }
            }

            [Required(ErrorMessage = "Garage is required.")]
            public string Garage
            {
                get { return garage; }
                set { garage = value; }
            }

            [Required(ErrorMessage = "Home Utility is required.")]
            public Utility HomeUtility
            {
                get { return utility; }
                set { utility = value; }
            }

            [Required(ErrorMessage = "Home Amenities is required.")]
            public Amenities HomeAmenities
            {
                get { return amenities; }
                set { amenities = value; }
            }

            [Required(ErrorMessage = "Home Rooms is required.")]
            public List<Rooms> HomeRooms
            {
                get { return rooms; }
                set { rooms = value; }
            }

            [Required(ErrorMessage = "Property Images is required.")]
            public List<PropertyImage> PropertyImages
            {
                get { return propertyImages; }
                set { propertyImages = value; }
            }

            [Required(ErrorMessage = "Building Number is required.")]
            public string BuildingNumber
            {
                get { return buildingNumber; }
                set { buildingNumber = value; }
            }

            [Required(ErrorMessage = "Street is required.")]
            public string Street
            {
                get { return street; }
                set { street = value; }
            }

            [Required(ErrorMessage = "City is required.")]
            public string City
            {
                get { return city; }
                set { city = value; }
            }

            [Required(ErrorMessage = "State is required.")]
            public string State
            {
                get { return state; }
                set { state = value; }
            }

            [Required(ErrorMessage = "Zip Code is required.")]
            public int ZipCode
            {
                get { return zipCode; }
                set { zipCode = value; }
            }

            [Required(ErrorMessage = "Property Type is required.")]
            public string PropertyType
            {
                get { return propertyType; }
                set { propertyType = value; }
            }

            [Required(ErrorMessage = "Year built is required.")]
            public int YearBuilt
            {
                get { return yearBuilt; }
                set { yearBuilt = value; }
            }

    }
}
