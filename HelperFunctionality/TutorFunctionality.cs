using Newtonsoft.Json;
using QuadCore_Website.models.NonDatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuberAPI.models;

namespace QuadCore_Website.HelperFunctionality
{
    public class TutorFunctionality
    {
        /// <summary>
        /// This method Check if whether the tuor does not have clashes with the pre-exisiting bookings 
        /// by comparing one of the already exisiting booked sessions with
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        internal static bool tutorIsAvailable(string userID, string BookingRequestID)
        {
            // get tutor's booked session time from the Client booking table
            string TutorBookedDateTimes = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/ClientBooking/GetIndividualTutorBookingsTimes/" + userID);
            DateTime[] bookedTimes = JsonConvert.DeserializeObject<DateTime[]>(TutorBookedDateTimes);

            //Get the students upcomming booking under booking request table
            string RequestedSessionTime = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/GetClientsRequestDate/" + BookingRequestID);
            DateTime RequestedTime = DateTime.Parse(DateFormatting.getCorrectDateTimeFormat(RequestedSessionTime));
            
            foreach (DateTime tutorTime in bookedTimes)
            {
                try
                {                    
                    //if any of the booking dates have passed
                    if (RequestedTime.CompareTo(tutorTime) <= 0)
                        {
                            if ((RequestedTime - tutorTime).Hours >= -1)
                            {
                                //deactiviate them
                                if ((RequestedTime - tutorTime).Minutes >= 0)
                                {
                                    return false;
                                }
                            }
                        }
                    
                }
                catch (Exception indexEXC)
                {
                    Console.WriteLine(indexEXC.GetBaseException());
                    return false;                   
                }                              
                
            }
            return true;

        }

        public static List<Tutor> GetTutors()
        {
            string tutors = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/Tutor/GetTutors");
            List<Tutor> tuts = JsonConvert.DeserializeObject<List<Tutor>>(tutors);

            return tuts;
        }
    }
}