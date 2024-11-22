using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Rooms
    {
        private int id;
        private int length;
        private int width;
        private string roomName;

        public Rooms(int height,int width,string roomName)
        {
           this.length = height;
           this.width = width;
           this.roomName = roomName;
        }
        public Rooms( int id, int height, int width, string roomName)
        {
            this.id = id;
            this.length = height;
            this.width = width;
            this.roomName = roomName;
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public string RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
    }
}
