using AP.BOL.BizLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AP.Autofac.WebUI.Models
{
    public class ModelTest
    {
       
 
        public static IEnumerable<BizAddress> CreateAddress()
        {
            yield return new BizAddress
            {
                AddressId = 0,
                SubdivisionId = 1,
                StreetId = 1,
                House = "111",
                Latitude = 0,
                Longitude = 0
            };
            yield return new BizAddress
            {
                AddressId = 0,
                SubdivisionId = 1,
                StreetId = 1,

                Latitude = 0,
                Longitude = 0
            };
            yield return new BizAddress
            {
                AddressId = 0,
                SubdivisionId = 1,
                StreetId = 1,
                House = "333",
                Latitude = 0,
                Longitude = 0
            };
        }
   
    }
}