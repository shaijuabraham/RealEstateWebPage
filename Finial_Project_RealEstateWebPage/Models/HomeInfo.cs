using ClassLibrary;
using FunctionClassLibrary.AssociateClass;


namespace Finial_Project_RealEstateWebPage.Models
{
    public class HomeInfo : Property
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
            private Utility utility; //Utlity is a part of Home class.
            private Amenities amenities; //Utlity is a part of Home class.
            private List<Rooms> rooms; // List of rooms
            private List<PropertyImage> propertyImages;//List of Property Images
            // Constructor to initialize the fields, including those from Property
            public HomeInfo(string buildingNumber, string propertyID, string agentID, string street, string city, string state, int zipCode,
                        string propertyType, int yearBuilt, int bedRooms, int bathRooms, string heating, string cooling,
                        decimal askingPrice, string homeSize, string description, string garage,
                        List<string> utilities, List<string> amenities, List<Rooms> rooms, List<PropertyImage> propertyImages)
                        : base(buildingNumber, street, city, state, zipCode, propertyType, yearBuilt) // Initialize Property fields
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
            }

            // Public properties with get and set for the specific fields
            public string PropertyID
            {
                get { return propertyID; }
                set { propertyID = value; }
            }

            public string AgentID
            {
                get { return agentID; }
                set { agentID = value; }
            }

            public int BedRooms
            {
                get { return bedRooms; }
                set { bedRooms = value; }
            }

            public int BathRooms
            {
                get { return bathRooms; }
                set { bathRooms = value; }
            }

            public string Heating
            {
                get { return heating; }
                set { heating = value; }
            }

            public string Cooling
            {
                get { return cooling; }
                set { cooling = value; }
            }

            public decimal AskingPrice
            {
                get { return askingPrice; }
                set { askingPrice = value; }
            }

            public string HomeSize
            {
                get { return homeSize; }
                set { homeSize = value; }
            }

            public string Description
            {
                get { return description; }
                set { description = value; }
            }

            public string Garage
            {
                get { return garage; }
                set { garage = value; }
            }

            // Property for utility
            public Utility HomeUtility
            {
                get { return utility; }
                set { utility = value; }
            }
            public Amenities HomeAmenities
            {
                get { return amenities; }
                set { amenities = value; }
            }

            public List<Rooms> HomeRooms
            {
                get { return rooms; }
                set { rooms = value; }
            }
            public List<PropertyImage> PropertyImages
            {
                get { return propertyImages; }
                set { propertyImages = value; }
            }
        
    }
}
