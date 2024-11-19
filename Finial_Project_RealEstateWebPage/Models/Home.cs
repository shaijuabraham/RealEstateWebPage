﻿namespace Finial_Project_RealEstateWebPage.Models
{
    public class Home
    {
        private double askingPrice;
        private int bedRooms;
        private int bathRooms;
        private string homeSize;
        private string street;
        private string city;
        private string state;
        private int zip;

        public Home()
        {

        }
        public Home(double askingPrice, int bedRooms, int bathRooms, string homeSize, string street, string city, string state, int zip)
        {
            this.askingPrice = askingPrice;
            this.bedRooms = bedRooms;
            this.bathRooms = bathRooms;
            this.homeSize = homeSize;
            this.street = street;
            this.city = city;
            this.state = state;
            this.zip = zip;
        }

        public double AskingPrice
        {
            get { return askingPrice; }
            set { askingPrice = value; }
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

        public String HomeSize
        {
            get { return homeSize; }
            set { homeSize = value; }
        }

        public String Street
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

        public int Zip
        {
            get { return zip; }
            set { zip = value; }
        }
    }
}
