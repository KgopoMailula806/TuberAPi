using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuberAPI.Data;
using TuberAPI.models;

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
                                where n.Seen.Equals(0) && n.User_Table_Reference.Equals(id)
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
        ///  returns every notification that belong to the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult popAllUsersNotifications(int id)
        {
            var notification = (from n in dbContext.Notifications
                                where n.User_Table_Reference.Equals(id) && n.Seen.Equals(0)
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
            var notifications = (from not in dbContext.Notifications
                                 where not.User_Table_Reference.Equals(id) && not.Seen.Equals(0)
                                 select not.ID).ToArray();
            if (notifications != null)
            {                                                
                return Ok(notifications.Count());
            }
            else
                return Ok(0);
        }
    }
}