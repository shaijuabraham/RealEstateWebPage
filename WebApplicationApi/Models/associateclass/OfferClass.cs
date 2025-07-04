﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using System.Data.Common;

namespace Finial_Project_RealEstateWebPage.Models.associateclass
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
        public string PropertyID { get; set; }
        public string OfferId { get; set; }



        public OfferClass() { }//empty Constructor
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

        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        /*Get the list of property offcer baseed on the agentID*/

        public List<OfferClass> GetPropertyOffer(string userId)
        {
            Console.WriteLine("Available in the result set.1");
            List<OfferClass> homes = new List<OfferClass>();
            try
            {

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "GetPropertyOffer";
                objCommand.Parameters.Clear();
                objCommand.Parameters.AddWithValue("@AgentID", userId);
                DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow propertyRow in ds.Tables[0].Rows)
                    {
                        OfferClass home = new OfferClass();

                        if (propertyRow.Table.Rows.Count > 0)
                        {
                            home.PropertyID = propertyRow["PropertyId"].ToString();
                            home.OfferId = propertyRow["id"].ToString();
                            home.FullName = propertyRow["FullName"].ToString();
                            home.OfferAmount = propertyRow["OfferAmount"].ToString();
                            home.SaleType = propertyRow["SaleType"].ToString();
                            home.Contingencies = propertyRow["Contingencies"].ToString();
                            home.NeedToSell = propertyRow["NeedToSell"].ToString();
                            home.MoveInDate = propertyRow["MoveInDate"].ToString();
                            home.BuyerPhone = propertyRow["BuyerPhone"].ToString();
                            home.BuyerEmail = propertyRow["BuyerEmail"].ToString();
                        }

                        homes.Add(home);
                        Console.WriteLine("Available in the result set.2");
                    }
                }
                else
                {
                    Console.WriteLine("No data available in the result set.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving data: " + ex.Message);
            }

            return homes;

        }

        /*Add the makeoffer informanation to the database*/
        public void MakeOffer(string propertyID, string fullName, string buyerPhone, string email,
                                string offerAmount, string saleType,
                                string contingencies, string needToSell, string moveInDate)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertOfferRequest";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            objCommand.Parameters.AddWithValue("@FullName", fullName);
            objCommand.Parameters.AddWithValue("@OfferAmount", offerAmount);
            objCommand.Parameters.AddWithValue("@SaleType", saleType);
            objCommand.Parameters.AddWithValue("@Contingencies", contingencies);
            objCommand.Parameters.AddWithValue("@NeedToSell", needToSell);
            objCommand.Parameters.AddWithValue("@MoveInDate", moveInDate);
            objCommand.Parameters.AddWithValue("@BuyerPhone", buyerPhone);
            objCommand.Parameters.AddWithValue("@BuyerEmail", email);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }


        /*methode to delete the specfic property id data based on the 
             user selction using when usere decline the specfic offer*/
        public void DeleteOffer(int OfferId)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "DeletePropertyOfferDetails";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@Id", OfferId);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }
        /*when user accept a offer its delete the that offfer and all the 
         * property data based on the propertyid*/
        public void DeleteAcceptedOffer(string propertyId)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "DeleteAcceptPropertyOffer";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyId);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }
    }
}
