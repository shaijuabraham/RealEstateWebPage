using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClassLibrary.AssociateClass
{
    public class Company
    {
        
            public string CompanyName { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public int ZipCode { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }


            public Company(string companyName, string street, string city, string state, int zipCode, string phoneNumber, string email)
            {
                CompanyName = companyName;
                Street = street;
                City = city;
                State = state;
                ZipCode = zipCode;
                PhoneNumber = phoneNumber;
                Email = email;
            }


    }
}
