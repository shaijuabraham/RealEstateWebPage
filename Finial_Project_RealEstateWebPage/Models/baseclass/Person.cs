using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Person
    {
        // Private fields
        private string userID;
        private string firstName;
        private string lastName;
        private string street;
        private string city;
        private string state;
        private int zipCode;
        private string phoneNumber;
        private string email;

        // Constructor to initialize the fields
        public Person(string userID, string firstName, string lastName, string street, string city, string state, int zipCode, string phoneNumber, string email)
        {
            this.userID = userID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.street = street;
            this.city = city;
            this.state = state;
            this.zipCode = zipCode;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }



        // Public properties with get and set
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
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

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}
