using Newtonsoft.Json;
using QuadCore_Website.models.NonDatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuberAPI.models;

namespace QuadCore_Website.HelperFunctionality
{
    public class ReportHelper
    {
        public static List<Invoice> getNumberOfOutstandingPayments()
        {
            string numOfOutPays = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Report/getNumOfOutstandingPays");
            List<Invoice> payObj = JsonConvert.DeserializeObject<List<Invoice>>(numOfOutPays);

            return payObj;

        }

        public static List<Rating> getTutorRatings(int id)
        {
            List<Rating> tutRats = null;
            string ratings = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Report/getTutorRatings/"+ id);
            if(ratings!= null)
            {
                tutRats = JsonConvert.DeserializeObject<List<Rating>>(ratings);
            }
             

            return tutRats;
        }

       

        
    }
}