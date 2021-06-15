using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuberAPI.models;

namespace QuadCore_Website.HelperFunctionality
{
    public class GenericHelperFunctionality
    {
        public static string checkIfUserHasSecondaryAccount(string userID,string CurrentUSerStatus) {

            switch (CurrentUSerStatus)
            {
                case "Tutor":
                    {
                        string clientJsonBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Client/doesClientExists/" + userID);
                        if (clientJsonBody.Equals("1"))
                            return "1";
                    }
                    break;
                case "Client":
                    {
                        string tutorJsonBody = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/doesTutorExists/" + userID);
                        if (tutorJsonBody.Equals("1"))
                            return "1";
                    }
                    break;
            }
            return "0";
        }

    }
}