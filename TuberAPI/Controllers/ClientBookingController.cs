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
        private TuberDbContext dbContext;

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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult getAllActiveClientBookings(int id)
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
        /// <param name="newQuote"></param>
        [HttpPost("[action]")]
        public IActionResult AddClientBooking([FromBody]ClientBooking clientBooking)
        {
            try
            {
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
                                 where tB.Tutor_Table_Reference.Equals(tutorsTutorTableReference) && tB.isActive.Equals(1)//TODO: Make a "Active" property
                                 select tB.Date_Time);

            if (tutorBookings != null)
            {
                return Ok(tutorBookings);
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
        public IActionResult GetReleventTutorsDetailsFromPreviousSession(int id, string moduleName)
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
        public IActionResult deactivateBooking(int id, string userStatus)
        {
            var clientBooking = dbContext.ClientBookings.Find(id);

            if (clientBooking != null)
            {
                try
                {

                    clientBooking.isActive = 0;
                    //save changes
                    dbContext.SaveChanges();
                    //set notification with event details
                    Event cancelationEvent = new Event();
                    cancelationEvent.EventType = "BookingCancelation";
                    //decription 				
                    cancelationEvent.Description = "One of pending booking have expiered" + "_" + clientBooking.Id;

                    dbContext.Events.Add(cancelationEvent);
                    //save event addition
                    dbContext.SaveChanges();

                    Notification notification = new Notification();
                    notification.DatePosted = DateTime.Today.ToString();
                    notification.Time = DateTime.Now.TimeOfDay.ToString();
                    notification.Seen = 0;
                    notification.PersonTheNotificationConcerns = "Tutor";
                    notification.User_Table_Reference = id; ;
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
    }
}
