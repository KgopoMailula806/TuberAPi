using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuberAPI.Data;

namespace TuberAPI.Infrastructure
{
    public class AvailabitlityChecking
    {
        public static TuberDbContext dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
       /* public AvailabitlityChecking(TuberDbContext dbContext)
        {
            this.dbContext = dbContext;
        } */
        public static bool tutorIsAvailable(int tutorID, int BookingRequestID)
        {
            // get tutor's booked session time from the Client booking table
            //string TutorBookedDateTimes = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/ClientBooking/GetIndividualTutorBookingsTimes/" + userID);
            var tutorsTutorTableReference = (from tId in dbContext.Tutors
                                             where tId.User_Table_Reference.Equals(tutorID)
                                             select tId.Id).FirstOrDefault();

            var tutorBookings = (from tB in dbContext.ClientBookings
                                 where tB.Tutor_Table_Reference.Equals(tutorsTutorTableReference) && tB.isActive.Equals(1)
                                 select tB.Date_Time).ToList();
            List<DateTime> sessionTimes = new List<DateTime>();
            if (tutorBookings != null)
            {
                foreach (string date in tutorBookings)
                {
                    sessionTimes.Add(DateTime.Parse(FormatingMethods.getCorrectDateTimeFormat(date)));
                }

            }
            else
                return true;

            //Get the students upcomming booking under booking request table
            //string RequestedSessionTime = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/GetClientsRequestDate/" + BookingRequestID);
            var request = (from p in dbContext.BookingRequests
                           where p.Id.Equals(BookingRequestID)
                           select p.RequestDate).FirstOrDefault();

            DateTime RequestedTime = DateTime.Parse(FormatingMethods.getCorrectDateTimeFormat(request));

            foreach (DateTime tutorTime in sessionTimes)
            {
                try
                {
                    //if any of the booking dates have passed
                    if (Math.Abs((RequestedTime - tutorTime).Days) <= 1)
                    {
                        if (Math.Abs((RequestedTime - tutorTime).Hours) <= 3)
                        {
                            /*deactiviate them
                            if (Math.Abs((RequestedTime - tutorTime).Minutes) <= 0)
                            {
                                return false;
                            }*/
                            return false;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="BookingRequestID"></param>
        /// <returns></returns>
        public static bool clientIsAvailable(int clientID, int BookingRequestID)
        {
            // get tutor's booked session time from the Client booking table
            //string TutorBookedDateTimes = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/ClientBooking/GetIndividualTutorBookingsTimes/" + userID);
            var clientsClientTableReference = (from tId in dbContext.Clients
                                             where tId.User_Table_Reference.Equals(clientID)
                                             select tId.Id).FirstOrDefault();

            var tutorBookings = (from tB in dbContext.ClientBookings
                                 where tB.Tutor_Table_Reference.Equals(clientsClientTableReference) && tB.isActive.Equals(1)
                                 select tB.Date_Time).ToList();
            List<DateTime> sessionTimes = new List<DateTime>();
            if (tutorBookings != null)
            {
                foreach (string date in tutorBookings)
                {
                    sessionTimes.Add(DateTime.Parse(FormatingMethods.getCorrectDateTimeFormat(date)));
                } 
            }
            else
                return true;

            //Get the students upcomming booking under booking request table
            //string RequestedSessionTime = ApiComnunication.getJsonEntities(SITEConstants.BASE_URL + "api/BookingRequest/GetClientsRequestDate/" + BookingRequestID);
            var request = (from p in dbContext.BookingRequests
                           where p.Id.Equals(BookingRequestID)
                           select p.RequestDate).FirstOrDefault();

            DateTime RequestedTime = DateTime.Parse(FormatingMethods.getCorrectDateTimeFormat(request));

            foreach (DateTime clientTime in sessionTimes)
            {
                try
                {
                    //if any of the booking dates have passed
                    if (Math.Abs((RequestedTime - clientTime).Days) <= 1)
                    {
                        if (Math.Abs((RequestedTime - clientTime).Hours) <= 3)
                        {
                            /*deactiviate them
                            if (Math.Abs((RequestedTime - tutorTime).Minutes) <= 0)
                            {
                                return false;
                            }*/
                            return false;
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
    }
}
