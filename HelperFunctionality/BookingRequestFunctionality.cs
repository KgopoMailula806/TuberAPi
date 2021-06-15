using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuberAPI.models;

namespace QuadCore_Website.HelperFunctionality
{
    public class BookingRequestFunctionality
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId_"></param>
        /// <param name="v"></param>
        internal static bool FinalisesBooking(string userId_, string BookingRequestID)
        {
            
            // The paramenter are the Booking requestId and 
            string response = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL+"api/BookingRequest/FinaliseBooking/" + BookingRequestID + "/" + userId_);
            if (response.Equals("1"))
            {
                return true;
            }
            else
                return false;
            
        }

        public static BookingRequest getBooking(int id)
        {
            BookingRequest req = null;
            string resp = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/GetIndividualBookingRequest/" + id);
            if(resp !=null)
            {
                req = JsonConvert.DeserializeObject<BookingRequest>(resp);
            }


            return req;
        }
    }
}