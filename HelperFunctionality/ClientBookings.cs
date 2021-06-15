using Newtonsoft.Json;
using QuadCore_Website.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuadCore_Website.HelperFunctionality
{
    public class ClientBookings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userTableReference"></param>
        /// <returns></returns>
        public static List<ClientBooking> GetAllUpcmmingClientBookings(string userTableReference)
        {
            string clientTableId = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Client/GetTutorId/" + userTableReference);
            string sessionBookings = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/ClientBooking/getAllActiveClientBookings/" + clientTableId);
            List<ClientBooking> BookingObj = JsonConvert.DeserializeObject<List<ClientBooking>>(sessionBookings);

            return BookingObj;

        }

        /// <summary>
        /// returns the set of client bookings accordint to the
        /// active status of the session
        /// </summary>
        /// <param name="userTableReference"></param>
        /// <param name="sesssionStatus">can either be -1,0,1</param>
        /// <returns></returns>
        public static List<ClientBooking> GetAllUpcmmingClientBookingsWithStatus(string userTableReference, int sesssionStatus)
        {
            string clientTableId = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Client/GetTutorId/" + userTableReference);
            string sessionBookings = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/ClientBooking/getClientBookingsWithStatus/" + clientTableId +"/" + sesssionStatus);
            List<ClientBooking> BookingObj = JsonConvert.DeserializeObject<List<ClientBooking>>(sessionBookings);

            return BookingObj;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userTableReference"></param>
        /// <returns></returns>
        public static List<ClientBooking> GetAllTutorBookingsWithStatus(string userTableReference, int sesssionStatus)
        {
            string tutorTableId = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/GetTutorId/" + userTableReference);
            string sessionBookings = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/ClientBooking/getAllTutorBookingsBookingsWithStatus/" + tutorTableId + "/" + sesssionStatus);
            List<ClientBooking> BookingObj = JsonConvert.DeserializeObject<List<ClientBooking>>(sessionBookings);

            return BookingObj;
        }

        /// <summary>
        /// Retrieves all of the active turo bookings from the database
        /// </summary>
        /// <param name="userTableReference"></param>
        /// <returns></returns>
        public static List<ClientBooking> GetAllTutorBookings(string userTableReference)
        {
            string tutorTableId = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/GetTutorId/" + userTableReference);
            string sessionBookings = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/ClientBooking/getAllActiveTutorBookings/" + tutorTableId);
            List<ClientBooking> BookingObj = JsonConvert.DeserializeObject<List<ClientBooking>>(sessionBookings);
            return BookingObj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userTableReference"></param>
        /// <returns></returns>
        public static ClientBooking GetBooking(string BookingID)
        {                       
            string sessionBookings = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/ClientBooking/getClientBOOKINGByClientBookingID/" + BookingID);
            if (sessionBookings.Equals("0"))
                return null;
            ClientBooking BookingObj = JsonConvert.DeserializeObject<ClientBooking>(sessionBookings);
            return BookingObj;
        }
    }
}