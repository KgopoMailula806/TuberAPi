using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using TuberAPI.Data;
using TuberAPI.models;

namespace TuberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientBookingController : ControllerBase
    {
        private  TuberDbContext dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public ClientBookingController(TuberDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")] //TODO Change
        public IActionResult getAllBookings()
        {
            return Ok(dbContext.ClientBookings);
        }

        [HttpGet("[action]/{bookingID}")] //TODO Change
        public IActionResult getBookingAt(int bookingID)
        {
            return Ok(dbContext.ClientBookings.Find(bookingID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult getAllActiveClientBookings(int id)
        {
            //return Ok(dbContext.ClientBookings.Find(id));
            var clientBookings = (from cB in dbContext.ClientBookings
                                  where cB.Client_Table_Reference.Equals(id) && cB.isActive.Equals(1)
                                  select cB);
            if (clientBookings != null)
            {
                return Ok(clientBookings);
            }
            else
                return Ok(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{id}/{sesssionStatus}")]
        public IActionResult getClientBookingsWithStatus(int id, int sesssionStatus)
        {
            //return Ok(dbContext.ClientBookings.Find(id));
            var clientBookings = (from cB in dbContext.ClientBookings
                                  where cB.Client_Table_Reference.Equals(id) && cB.isActive.Equals(sesssionStatus)
                                  select cB);
            if (clientBookings != null)
            {
                return Ok(clientBookings);
            }
            else
                return Ok(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult getAllClientBookings(int id)
        {
            //return Ok(dbContext.ClientBookings.Find(id));
            var clientBookings = (from cB in dbContext.ClientBookings
                                  where cB.Client_Table_Reference.Equals(id) 
                                  select cB);
            if (clientBookings != null)
            {
                return Ok(clientBookings);
            }
            else
                return Ok(0);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>       
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult getAllTutorsBookings(int id)
        {
            //return Ok(dbContext.ClientBookings.Find(id));
            var clientBookings = (from cB in dbContext.ClientBookings
                                  where cB.Tutor_Table_Reference.Equals(id)
                                  select cB);
            if (clientBookings != null)
            {
                return Ok(clientBookings);
            }
            else
                return Ok(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingRequestID"></param>
        /// <returns></returns>
        [HttpGet("[action]/{bookingRequestID}")]
        public IActionResult getClientBOOKINGByClientBookingID(int bookingRequestID)
        {
            //return Ok(dbContext.ClientBookings.Find(id));
            var clientBookings = (from cB in dbContext.ClientBookings
                                  where cB.Id.Equals(bookingRequestID)
                                  select cB).FirstOrDefault();
            if (clientBookings != null)
            {
                return Ok(clientBookings);
            }
            else
                return Ok(0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult getAllActiveTutorBookings(int id)
        {
            //return Ok(dbContext.ClientBookings.Find(id));
            var tutorBookings = (from cB in dbContext.ClientBookings
                                 where cB.Tutor_Table_Reference.Equals(id) && cB.isActive.Equals(1)
                                 select cB);
            if (tutorBookings != null)
            {
                return Ok(tutorBookings);
            }
            else
                return Ok(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{id}/{sesssionStatus}")]
        public IActionResult getAllTutorBookingsBookingsWithStatus(int id,int sesssionStatus)
        {
            //return Ok(dbContext.ClientBookings.Find(id));
            var tutorBookings = (from cB in dbContext.ClientBookings
                                 where cB.Tutor_Table_Reference.Equals(id) && cB.isActive.Equals(sesssionStatus)
                                 select cB);
            if (tutorBookings != null)
            {
                return Ok(tutorBookings);
            }
            else
                return Ok(tutorBookings);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newQuote"></param>
        [HttpPost("[action]")]
        public IActionResult AddClientBooking([FromBody]ClientBooking clientBooking)
        {
            try
            {

                clientBooking.Date_Time = FormatingMethods.getCorrectDateTimeFormat(clientBooking.Date_Time);
                dbContext.ClientBookings.Add(clientBooking);
                dbContext.SaveChanges();
                return Ok(1);
            }
            catch (Exception x)
            {
                return Ok(0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetIndividualTutorBookingsTimes(int id)
        {


            var tutorsTutorTableReference = (from tId in dbContext.Tutors
                                             where tId.User_Table_Reference.Equals(id)
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
                return Ok(sessionTimes);
            }
            else
                return Ok();
        }

        /// <summary>
        /// Returns a collection of tutors that have tutor a given student
        /// </summary>
        /// <param name="id">Is the client tablee id</param>
        /// <returns></returns>
        [HttpGet("[action]/{id}/{moduleName}")]
        public IActionResult GetTutorsByTheRespectiveModuleTheyTutorFromPreviousSession(int id, string moduleName)
        {
            //Get the Clients Id with the give user table Id 
            var clientTsbleID = (from C in dbContext.Clients
                                 where C.User_Table_Reference.Equals(id)
                                 select C.Id).ToList();

            if (clientTsbleID != null)
            {
                //get tutor Ids associated with the client table ID
                var tutorIds = (from CB in dbContext.ClientBookings
                                where CB.Client_Table_Reference.Equals(clientTsbleID.ElementAt(0))
                                select CB.Tutor_Table_Reference).ToList();

                var moduleID = (from mod in dbContext.Modules
                                where mod.Module_Name.Equals(moduleName)
                                select mod.Id).ToList();

                var moduleTutorsIDs = (from tuts in dbContext.Tutor_Modules
                                       where tuts.Module_Reference.Equals(moduleID.ElementAt(0)) && tutorIds.Contains(tuts.Tutor_Reference)
                                       select tuts.Tutor_Reference).ToList();

                //get all of the user table IDs associated with the collected User tble IDs 
                var usertableIds = (from UIDs in dbContext.Tutors
                                    where moduleTutorsIDs.Contains(UIDs.Id)
                                    select UIDs.User_Table_Reference).ToList();

                var userDetails = (from Us in dbContext.Users
                                   where usertableIds.Contains(Us.Id)
                                   select Us).ToList();
                return Ok(userDetails);
            }

            return Ok(0);

        }
        /// <summary>
        /// Deactivates modules that have become overdue
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{userID}/{userStatus}")]
        public IActionResult deactivateBooking(int userID, string userStatus)
        {
            var clientBooking = dbContext.ClientBookings.Find(userID);
            int id = 0;
            switch (userStatus)
            {
                case "Client":
                    {

                        var userTableID = (from uID in dbContext.Tutors
                                           where uID.Id.Equals(clientBooking.Tutor_Table_Reference)
                                           select uID.User_Table_Reference).ToList();
                        id = userTableID.ElementAt(0);

                        userStatus = "Tutor";
                    }
                    break;

                case "Tutor":
                    {
                        var userTableID = (from uID in dbContext.Clients
                                           where uID.Id.Equals(clientBooking.Client_Table_Reference)
                                           select uID.User_Table_Reference).ToList();
                        id = userTableID.ElementAt(0);
                        userStatus = "Client";
                    }
                    break;
            }


            if (id != 0)
            {
                try
                {

                    clientBooking.isActive = -1;
                    //save changes
                    dbContext.SaveChanges();
                    //set notification with event details
                    Event cancelationEvent = new Event();
                    cancelationEvent.EventType = "BookingCancelation";
                    //decription 				
                    switch (userStatus)
                    {
                        case "Client":
                            {

                                cancelationEvent.Description = "The tutor cancel been canceled" + "_" + clientBooking.Id;
                            }
                            break;

                        case "Tutor":
                            {
                                cancelationEvent.Description = "The client canceled the booking have been canceled" + "_" + clientBooking.Id;
                            }
                            break;
                    }


                    dbContext.Events.Add(cancelationEvent);
                    //save event addition
                    dbContext.SaveChanges();

                    Notification notification = new Notification();
                    notification.DatePosted = DateTime.Today.ToString();
                    notification.Time = DateTime.Now.TimeOfDay.ToString();
                    notification.Seen = 0;
                    notification.PersonTheNotificationConcerns = userStatus;
                    notification.User_Table_Reference = id;
                    notification.Event_Table_Reference = cancelationEvent.ID;

                    dbContext.Notifications.Add(notification);
                    dbContext.SaveChanges();

                    return Ok(1);
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return Ok(-1);
                }
            }
            else
            {
                return Ok(0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{userID}/{userStatus}")]
        public IActionResult areTherAnyExpiredSessions(int userID, string userStatus)
        {
            var secondaryRoleID = 0;

            switch (userStatus)
            {
                case "Client": //Get the client table ID
                    {

                        var secondaryRoleID_ = (from cb in dbContext.Clients
                                                where cb.User_Table_Reference.Equals(userID)
                                                select cb.Id).ToList();
                        try
                        {
                            if (secondaryRoleID_.ElementAt(0) > 0)
                            {
                                secondaryRoleID = secondaryRoleID_.ElementAt(0);
                            }
                            else
                            {
                                secondaryRoleID = -1;
                            }
                        }
                        catch (Exception possibleIndexException)
                        {

                        }


                    }
                    break;
                case "Tutor": //Get the tutor table ID
                    {

                        var secondaryRoleID_ = (from cb in dbContext.Tutors
                                                where cb.User_Table_Reference.Equals(userID)
                                                select cb.Id).ToList();
                        try
                        {
                            if (secondaryRoleID_.ElementAt(0) > 0)
                            {
                                secondaryRoleID = secondaryRoleID_.ElementAt(0);
                            }
                            else
                            {
                                secondaryRoleID = -1;
                            }
                        }
                        catch (Exception possibleIndexException)
                        {                            
                        }
                    }
                    break;
            }
            //once we have the client or tutor table ID
            if (secondaryRoleID > 0)
            {
                //find any expired bookings 
                var userBooking = (from UBooking in dbContext.ClientBookings
                                   where (UBooking.Tutor_Table_Reference.Equals(secondaryRoleID)
                                   || UBooking.Client_Table_Reference.Equals(secondaryRoleID))
                                   && UBooking.isActive.Equals(1)
                                   select UBooking).ToList();


                List<ClientBooking> expiredBookings = new List<ClientBooking>();
                foreach (ClientBooking booking in userBooking)
                {
                    booking.Date_Time = FormatingMethods.getCorrectDateTimeFormat(booking.Date_Time);
                    string days = "" + (DateTime.Now - DateTime.Parse(booking.Date_Time)).Days;
                    string hours = "" + (DateTime.Now - DateTime.Parse(booking.Date_Time)).Hours;
                    string minutes = "" + (DateTime.Now - DateTime.Parse(booking.Date_Time)).Minutes;
                    if ((DateTime.Now - DateTime.Parse(booking.Date_Time)).Days >= 0)
                    {
                        if ((DateTime.Now - DateTime.Parse(booking.Date_Time)).Hours >= -1)
                        {
                            //deactiviate them
                            if ((DateTime.Now - DateTime.Parse(booking.Date_Time)).Minutes >= 0)
                            {
                                booking.isActive = 0;
                                try
                                {
                                    //save changes
                                    dbContext.SaveChanges();

                                    //set notification with event details
                                    Event cancelationEvent = new Event();
                                    cancelationEvent.EventType = "BookingCancelation";
                                    //decription 				
                                    cancelationEvent.Description = "One of pending booking have expiered" + "_" + booking.Id;

                                    dbContext.Events.Add(cancelationEvent);
                                    //save event addition
                                    dbContext.SaveChanges();

                                    Notification notification = new Notification();
                                    notification.DatePosted = DateTime.Today.ToString();
                                    notification.Time = DateTime.Now.TimeOfDay.ToString();
                                    notification.Seen = 0;
                                    notification.PersonTheNotificationConcerns = userStatus;
                                    notification.User_Table_Reference = userID;
                                    notification.Event_Table_Reference = cancelationEvent.ID;

                                    dbContext.Notifications.Add(notification);
                                    dbContext.SaveChanges();


                                }
                                catch (Exception ec)
                                {

                                }
                                expiredBookings.Add(booking);
                            }

                        }

                    }
                }
                return Ok(expiredBookings);
                //return them
            }
            else return Ok(0);
        }

        [HttpGet("[action]/{userID}/{userStatus}")]
        public IActionResult areTherAnyExpiredSessionsWithUserTableID(int userID, string userStatus)
        {

            //once we have the client or tutor table ID

            //find any expired bookings 
            var userBooking = (from UBooking in dbContext.ClientBookings
                               where (UBooking.Tutor_Table_Reference.Equals(userID)
                               || UBooking.Client_Table_Reference.Equals(userID))
                               && UBooking.isActive.Equals(1)
                               select UBooking).ToList();


            List<ClientBooking> expiredBookings = new List<ClientBooking>();
            foreach (ClientBooking booking in userBooking)
            {
                booking.Date_Time = FormatingMethods.getCorrectDateTimeFormat(booking.Date_Time);
                string days = "" + (DateTime.Now - DateTime.Parse(booking.Date_Time)).Days;
                string hours = "" + (DateTime.Now - DateTime.Parse(booking.Date_Time)).Hours;
                string minutes = "" + (DateTime.Now - DateTime.Parse(booking.Date_Time)).Minutes;
                if ((DateTime.Now - DateTime.Parse(booking.Date_Time)).Days >= 0)
                {
                    if ((DateTime.Now - DateTime.Parse(booking.Date_Time)).Hours >= -1)
                    {
                        //deactiviate them
                        if ((DateTime.Now - DateTime.Parse(booking.Date_Time)).Minutes >= 0)
                        {
                            booking.isActive = 0;
                            try
                            {
                                //save changes
                                dbContext.SaveChanges();

                                //set notification with event details
                                Event cancelationEvent = new Event();
                                cancelationEvent.EventType = "BookingCancelation";
                                //decription 				
                                cancelationEvent.Description = "One of pending booking have expiered" + "_" + booking.Id;

                                dbContext.Events.Add(cancelationEvent);
                                //save event addition
                                dbContext.SaveChanges();

                                Notification notification = new Notification();
                                notification.DatePosted = DateTime.Today.ToString();
                                notification.Time = DateTime.Now.TimeOfDay.ToString();
                                notification.Seen = 0;
                                notification.PersonTheNotificationConcerns = userStatus;
                                notification.User_Table_Reference = userID;
                                notification.Event_Table_Reference = cancelationEvent.ID;

                                dbContext.Notifications.Add(notification);
                                dbContext.SaveChanges();


                            }
                            catch (Exception ec)
                            {

                            }
                            expiredBookings.Add(booking);
                        }

                    }

                }
            }
            return Ok(expiredBookings);
            //return them           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(int id)
        {

            var entityUser = dbContext.ClientBookings.Find(id);
            if (entityUser != null)
            {
                dbContext.ClientBookings.Remove(entityUser);
                dbContext.SaveChanges();
                return Ok(entityUser);
            }
            else
            {
                return Ok(0);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientBookingID"></param>
        /// <returns></returns>
        [HttpGet("[action]/{clientBookingID}")]
        public IActionResult closeBooking(int clientBookingID)
        {
            var LiveSession = (from tS in dbContext.ClientBookings
                               where tS.Id.Equals(clientBookingID)
                               select tS).FirstOrDefault();

            if (LiveSession != null)
            {
                (LiveSession).isActive = 0;
                dbContext.SaveChanges();
                return Ok(1);

            }
            else
                return Ok(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientBookingID"></param>
        /// <returns></returns>
        [HttpGet("[action]/{clientBookingID}")]
        public IActionResult getBookingModuleID(int clientBookingID)
        {
            var LiveSession = (from tS in dbContext.ClientBookings
                               where tS.Id.Equals(clientBookingID)
                               select tS.BookingDetails_BookingRequestTable_Reference).ToList();

            if (LiveSession != null)
            {
                var moudleID = (from tS in dbContext.BookingRequests
                                   where tS.Id.Equals(LiveSession.ElementAt(0))
                                   select tS.ModuleID1).ToList();

                return Ok(moudleID.ElementAt(0));

            }
            else
                return Ok(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientBookingID"></param>
        /// <returns></returns>
        [HttpGet("[action]/{clientBookingID}")]
        public IActionResult activateBooking(int clientBookingID)
        {
            var LiveSession = (from tS in dbContext.ClientBookings
                               where tS.Id.Equals(clientBookingID)
                               select tS).FirstOrDefault();

            if (LiveSession != null)
            {
                (LiveSession).isActive = 1;
                dbContext.SaveChanges();
                return Ok(1);

            }
            else
                return Ok(0);
        }

    }
  }
