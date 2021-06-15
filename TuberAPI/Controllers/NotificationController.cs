using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuberAPI.Data;
using TuberAPI.models;
using TuberAPI.models.NonDatabaseModels;

namespace TuberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private TuberDbContext dbContext;

        public NotificationController(TuberDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetNotifications()
        {
            try
            {                
                return Ok(dbContext.Notifications);
            }
            catch
            {
                return BadRequest();
            }

        }

        /// <summary>
        ///  returns list of all unseen notifications belonging to the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult popUsersUnseenNotifications(int id)
        {
            var notification = (from n in dbContext.Notifications
                                where (n.Seen.Equals(0) || n.Seen.Equals(2)) && n.User_Table_Reference.Equals(id)
                                select n);

            if (notification != null)
                return Ok(notification);
            else
                return Ok();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult popUsersUnseenNotificationsMobileForIteration(int id)
        {
            var notification = (from n in dbContext.Notifications
                                where n.Seen.Equals(0) && n.User_Table_Reference.Equals(id)
                                select n).ToList();

            if (notification != null)
            {
                foreach (Notification not in notification)
                {
                    not.Seen = 2;//
                    dbContext.SaveChanges();
                }
                return Ok(notification);
            }
            else
                return Ok(notification);

        }
        [HttpGet("[action]/{id}")]
        public IActionResult popUsersUnseenNotificationsMobileForOnce(int id)
        {
            var notification = (from n in dbContext.Notifications
                                where (n.Seen.Equals(2) || n.Seen.Equals(0)) &&n.User_Table_Reference.Equals(id)
                                select n);

            if (notification != null)
                return Ok(notification);
            else
                return Ok();

        }
        /// <summary>
        ///  returns list of all seen notifications belonging to the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult popUsersSeenNotifications(int id)
        {
            var notification = (from n in dbContext.Notifications
                                where (n.Seen.Equals(2) || n.Seen.Equals(0)) && n.User_Table_Reference.Equals(id)
                                select n);

            if (notification != null)
                return Ok(notification);
            else
                return Ok();

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult popUsersGenerallyUnseenNotifications(int id)
        {
            var notification = (from n in dbContext.Notifications
                                where n.Seen.Equals(0) && n.User_Table_Reference.Equals(id)
                                select n);

            if (notification != null)
                return Ok(notification);
            else
                return Ok();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="condition"> </param>
        /// <param name="timeRange"> a range of dates </param>
        /// <returns></returns>
        [HttpPost("[action]/{userID}/{condition}/{seenFlag}")]
        public IActionResult popUsersUnseenNotificationsTimeRange(int userID,int condition, int seenFlag, [FromBody] TimeRange timeRange)
        {
            List<Notification> notificationsThatMadeTheCut = new List<Notification>();
            switch (""+ condition)
            {
                case "0": //All
                    {
                        switch (seenFlag)
                        {
                            case 0:
                                {
                                    var notification = (from n in dbContext.Notifications
                                                        where (n.Seen.Equals(2) || n.Seen.Equals(0)) && n.User_Table_Reference.Equals(userID)
                                                        select n).ToList();
                                    if (notification != null)
                                        return Ok(notification);
                                    else
                                        return Ok(0);
                                }
                                
                            case 1://seen
                                {
                                    var notification = (from n in dbContext.Notifications
                                                        where n.Seen.Equals(seenFlag) && n.User_Table_Reference.Equals(userID)
                                                        select n).ToList();
                                    if (notification != null)
                                        return Ok(notification);
                                    else
                                        return Ok(0);
                                }
                                
                        }
                        

                        
                    }   break;                 
                case "1"://From selected Date to present
                    {
                        switch (seenFlag)
                        {
                            case 0:
                                {
                                    var notifications = (from n in dbContext.Notifications
                                                         where (n.Seen.Equals(2) || n.Seen.Equals(0)) && n.User_Table_Reference.Equals(userID)
                                                         select n).ToList();

                                    if (timeRange.earlierDate != null && notifications.Count > 0)
                                    {
                                        //getPresent date and the specified date and compare it with the notification dates from the database
                                        //Ensure compatibility
                                        DateTime presentDateTime = DateTime.Today;
                                        timeRange.earlierDate = FormatingMethods.getCorrectDateTimeFormat(timeRange.earlierDate);
                                        DateTime earlierDate = DateTime.Parse(timeRange.earlierDate);
                                        foreach (Notification recordedNotifications in notifications)
                                        {
                                            //compare
                                            recordedNotifications.DatePosted = FormatingMethods.getCorrectDateTimeFormat(recordedNotifications.DatePosted);
                                            DateTime rN = DateTime.Parse(recordedNotifications.DatePosted);
                                            if (rN.CompareTo(earlierDate) >= 0 && rN.CompareTo(presentDateTime) <= 0)
                                            {
                                                notificationsThatMadeTheCut.Add(recordedNotifications);
                                            }
                                        }

                                    }

                                    if (notificationsThatMadeTheCut.Count > 0)
                                        return Ok(notificationsThatMadeTheCut);
                                    else
                                        return Ok(0);
                                }
                            case 1:
                                {
                                    var notifications = (from n in dbContext.Notifications
                                                         where n.Seen.Equals(seenFlag) && n.User_Table_Reference.Equals(userID)
                                                         select n).ToList();

                                    if (timeRange.earlierDate != null && notifications.Count > 0)
                                    {
                                        //getPresent date and the specified date and compare it with the notification dates from the database
                                        //Ensure compatibility
                                        DateTime presentDateTime = DateTime.Today;
                                        timeRange.earlierDate = FormatingMethods.getCorrectDateTimeFormat(timeRange.earlierDate);
                                        DateTime earlierDate = DateTime.Parse(timeRange.earlierDate);
                                        foreach (Notification recordedNotifications in notifications)
                                        {
                                            //compare
                                            recordedNotifications.DatePosted = FormatingMethods.getCorrectDateTimeFormat(recordedNotifications.DatePosted);
                                            DateTime rN = DateTime.Parse(recordedNotifications.DatePosted);
                                            if (rN.CompareTo(earlierDate) >= 0 && rN.CompareTo(presentDateTime) <= 0)
                                            {
                                                notificationsThatMadeTheCut.Add(recordedNotifications);
                                            }
                                        }

                                    }

                                    if (notificationsThatMadeTheCut.Count > 0)
                                        return Ok(notificationsThatMadeTheCut);
                                    else
                                        return Ok(0);
                                }
                        }
                                    
                    }break;
                    
                case "2"://From selected Date to Other SelectedDate
                    {
                        switch (seenFlag)
                        {
                            case 0:
                                {
                                    var notifications = (from n in dbContext.Notifications
                                                         where (n.Seen.Equals(2) || n.Seen.Equals(0)) && n.User_Table_Reference.Equals(userID)
                                                         select n).ToList();

                                    if (timeRange.earlierDate != null && timeRange.laterDate != null && notifications.Count > 0)
                                    {
                                        //getPresent date and the specified date and compare it with the notification dates from the database
                                        //Ensure compatibility
                                        timeRange.laterDate = FormatingMethods.getCorrectDateTimeFormat(timeRange.laterDate);
                                        DateTime laterDate = DateTime.Parse(timeRange.laterDate);
                                        timeRange.earlierDate = FormatingMethods.getCorrectDateTimeFormat(timeRange.earlierDate);
                                        DateTime earlierDate = DateTime.Parse(timeRange.earlierDate);
                                        foreach (Notification recordedNotifications in notifications)
                                        {
                                            recordedNotifications.DatePosted = FormatingMethods.getCorrectDateTimeFormat(recordedNotifications.DatePosted);
                                            //compare
                                            DateTime rN = DateTime.Parse(recordedNotifications.DatePosted);
                                            if (rN.CompareTo(earlierDate) >= 0 && rN.CompareTo(laterDate) <= 0)
                                            {
                                                notificationsThatMadeTheCut.Add(recordedNotifications);
                                            }
                                        }

                                    }

                                    if (notificationsThatMadeTheCut.Count > 0)
                                        return Ok(notificationsThatMadeTheCut);
                                    else
                                        return Ok(0);
                                }
                            case 1:
                                {
                                    var notifications = (from n in dbContext.Notifications
                                                         where n.Seen.Equals(seenFlag) && n.User_Table_Reference.Equals(userID)
                                                         select n).ToList();

                                    if (timeRange.earlierDate != null && timeRange.laterDate != null && notifications.Count > 0)
                                    {
                                        //getPresent date and the specified date and compare it with the notification dates from the database
                                        //Ensure compatibility
                                        timeRange.laterDate = FormatingMethods.getCorrectDateTimeFormat(timeRange.laterDate);
                                        DateTime laterDate = DateTime.Parse(timeRange.laterDate);
                                        timeRange.earlierDate = FormatingMethods.getCorrectDateTimeFormat(timeRange.earlierDate);
                                        DateTime earlierDate = DateTime.Parse(timeRange.earlierDate);
                                        foreach (Notification recordedNotifications in notifications)
                                        {
                                            //compare
                                            recordedNotifications.DatePosted = FormatingMethods.getCorrectDateTimeFormat(recordedNotifications.DatePosted);
                                            DateTime rN = DateTime.Parse(recordedNotifications.DatePosted);
                                            if (rN.CompareTo(earlierDate) >= 0 && rN.CompareTo(laterDate) <= 0)
                                            {
                                                notificationsThatMadeTheCut.Add(recordedNotifications);
                                            }
                                        }

                                    }

                                    if (notificationsThatMadeTheCut.Count > 0)
                                        return Ok(notificationsThatMadeTheCut);
                                    else
                                        return Ok(0);
                                }
                        }
                                    
                    }break;
                case "3"://Before selected Date
                    {
                        switch (seenFlag)
                        {
                            case 0:
                                {
                                    var notifications = (from n in dbContext.Notifications
                                                         where (n.Seen.Equals(2) || n.Seen.Equals(0)) && n.User_Table_Reference.Equals(userID)
                                                         select n).ToList();

                                    if (timeRange.laterDate != null && notifications.Count > 0)
                                    {
                                        //getPresent date and the specified date and compare it with the notification dates from the database
                                        //Ensure compatibility
                                        timeRange.laterDate = FormatingMethods.getCorrectDateTimeFormat(timeRange.laterDate);
                                        DateTime laterDate = DateTime.Parse(timeRange.laterDate);
                                        foreach (Notification recordedNotifications in notifications)
                                        {
                                            //compare
                                            recordedNotifications.DatePosted = FormatingMethods.getCorrectDateTimeFormat(recordedNotifications.DatePosted);
                                            DateTime rN = DateTime.Parse(recordedNotifications.DatePosted);
                                            if (rN.CompareTo(laterDate) <= 0)
                                            {
                                                notificationsThatMadeTheCut.Add(recordedNotifications);
                                            }
                                        }

                                    }

                                    if (notificationsThatMadeTheCut.Count > 0)
                                        return Ok(notificationsThatMadeTheCut);
                                    else
                                        return Ok(0);
                                }
                            case 1:
                                {
                                    var notifications = (from n in dbContext.Notifications
                                                         where n.Seen.Equals(seenFlag) && n.User_Table_Reference.Equals(userID)
                                                         select n).ToList();

                                    if (timeRange.laterDate != null && notifications.Count > 0)
                                    {
                                        //getPresent date and the specified date and compare it with the notification dates from the database
                                        //Ensure compatibility
                                        timeRange.laterDate = FormatingMethods.getCorrectDateTimeFormat(timeRange.laterDate);
                                        DateTime laterDate = DateTime.Parse(timeRange.laterDate);
                                        foreach (Notification recordedNotifications in notifications)
                                        {
                                            //compare
                                            recordedNotifications.DatePosted = FormatingMethods.getCorrectDateTimeFormat(recordedNotifications.DatePosted);
                                            DateTime rN = DateTime.Parse(recordedNotifications.DatePosted);
                                            if (rN.CompareTo(laterDate) <= 0)
                                            {
                                                notificationsThatMadeTheCut.Add(recordedNotifications);
                                            }
                                        }

                                    }

                                    if (notificationsThatMadeTheCut.Count > 0)
                                        return Ok(notificationsThatMadeTheCut);
                                    else
                                        return Ok(0);
                                }
                        }
                        
                    }  break;                 
            }
            return Ok(-1); //this should never happen
        }
        /// <summary>
        ///  returns every notification that belong to the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult popAllUsersNotifications(int id)
        {
            var notification = (from n in dbContext.Notifications
                                where n.User_Table_Reference.Equals(id) && (n.Seen.Equals(0) || n.Seen.Equals(2))
                                select n);

            if (notification != null)
                return Ok(notification);
            else
                return Ok();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult pushnotification([FromBody] Notification notification)
        {
            try
            {
               // notification.DatePosted = FormatingMethods.getCorrectDateTimeFormat(notification.DatePosted);
                dbContext.Notifications.Add(notification);
                dbContext.SaveChanges();
                return Ok(notification.ID);
            }
            catch (Exception ex)
            {
                return Ok(0);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult ChangeSeenStatus(int id)
        {
            /*   var notification = (from n in dbContext.Notifications
                                   where n.Seen.Equals(1) && n.ID.Equals(id)
                                   select n); */

            var notification = dbContext.Notifications.Find(id);
            if (notification != null)
            {
                notification.Seen = 1;
                dbContext.SaveChanges();
                return Ok(1);
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
        public IActionResult getNotification(int id)
        {
            /*   var notification = (from n in dbContext.Notifications
                                   where n.Seen.Equals(1) && n.ID.Equals(id)
                                   select n); */

            var notification = dbContext.Notifications.Find(id);
            if (notification != null)
            {
                
                return Ok(notification);
            }
            else
                return Ok(notification);
        }
        [HttpGet("[action]/{notificationID}")]
        public IActionResult makeNotificationUnseen(int notificationID)
        {
            /*   var notification = (from n in dbContext.Notifications
                                   where n.Seen.Equals(1) && n.ID.Equals(id)
                                   select n); */

            var notification = dbContext.Notifications.Find(notificationID);
            if (notification != null)
            {
                notification.Seen = 0;
                dbContext.SaveChanges();
                return Ok(1);
            }
            else
                return Ok(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="User_Table_Reference"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}/{User_Table_Reference}")]
        public IActionResult ChangeGeneralSeenStatus(int id, int User_Table_Reference)
        {
            /*   var notification = (from n in dbContext.Notifications
                                   where n.Seen.Equals(1) && n.ID.Equals(id)
                                   select n); */

            var notification = dbContext.Notifications.Find(id);
            if (notification != null)
            {
                notification.Seen = 1;
                notification.User_Table_Reference = User_Table_Reference;
                dbContext.SaveChanges();
                return Ok(1);
            }
            else
                return Ok(0);
        }

        /// <summary>
        ///  The method returns the number of unseen user-notifations
        /// </summary>
        /// <param name="id"></param>
        /// <param name="User_Table_Reference"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult getNumberOfUnreadNotifications(int id)
        {
            var notifications = (from n in dbContext.Notifications
                                where (n.Seen.Equals(2) || n.Seen.Equals(0)) && n.User_Table_Reference.Equals(id)
                                select n).ToList();
            if (notifications != null)
            {                                                
                return Ok(notifications.Count());
            }
            else
                return Ok(0);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult getNumberOfPossiblyUnreadNotifications(int id)
        {
            var notifications = (from not in dbContext.Notifications
                                 where not.User_Table_Reference.Equals(id) && (not.Seen.Equals(2))
                                 select not.ID).ToArray();
            if (notifications != null)
            {
                return Ok(notifications.Count());
            }
            else
                return Ok(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(int id)
        {

            var notificaitons = dbContext.Notifications.Find(id);
            if (notificaitons != null)
            {
                dbContext.Notifications.Remove(notificaitons);
                dbContext.SaveChanges();
                return Ok(notificaitons);
            }
            else
            {
                return Ok();
            }
        }
    }
}