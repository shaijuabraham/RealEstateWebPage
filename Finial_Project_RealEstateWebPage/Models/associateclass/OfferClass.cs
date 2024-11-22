using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClassLibrary.AssociateClass
{
    public class OfferClass
    {
        public string OfferAmount { get; set; }
        public string SaleType { get; set; }
        public string Contingencies { get; set; }
        public string NeedToSell { get; set; }
        public string MoveInDate { get; set; }
        public string FullName { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerEmail { get; set; }


        public OfferClass(string offerAmount, string saleType, string contingencies, string needToSell, 
                           string moveInDate, string fullName, string buyerPhone, string buyerEmail)
        {
            OfferAmount = offerAmount;
            SaleType = saleType;
            Contingencies = contingencies;
            NeedToSell = needToSell;
            MoveInDate = moveInDate;
            FullName = fullName;
            BuyerPhone = buyerPhone;
            BuyerEmail = buyerEmail;
        }
    }
}
