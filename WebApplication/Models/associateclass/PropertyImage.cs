using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClassLibrary.AssociateClass
{
    public class PropertyImage
    {
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageType { get; set; }
        public int ImageLength { get; set; }

        // Constructor
        public PropertyImage(string imageName, byte[] imageData, string imageType, int imageLength)
        {
            ImageName = imageName;
            ImageData = imageData;
            ImageType = imageType;
            ImageLength = imageLength;
        }
    }
}
