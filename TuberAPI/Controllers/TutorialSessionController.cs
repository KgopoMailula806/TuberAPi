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
    public class TutorialSessionController : ControllerBase
    {

        private TuberDbContext dbContext;

        public TutorialSessionController(TuberDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetAllTutorSessions()
        {
            return Ok(dbContext.Tutorial_Sessions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientBookingID"></param>
        /// <returns></returns>
        [HttpGet("[action]/{clientBookingID}")]
        public IActionResult checkIfSessionExists(int clientBookingID)
        {
            var sessionID = (from d in dbContext.Tutorial_Sessions
                             where d.ClientBookingID.Equals(clientBookingID)
                             select d.Id).FirstOrDefault();
            if (sessionID > 0)
                return Ok(sessionID);
            else return Ok(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult getRatings()
        {

            var ratings = (from r in dbContext.Ratings
                           select r);
            if (ratings != null)
                return Ok(ratings);

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult saveRating([FromBody] Rating rate)
        {
            var rating = (from sRating in dbContext.Ratings
                          where sRating.Session_Reference.Equals(rate.Session_Reference)
                          select sRating).FirstOrDefault();

            if(rating != null)
            {
                return Ok(rating);
            }            

            dbContext.Ratings.Add(rate);
            dbContext.SaveChanges();                
            return Ok(rate);



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tutorial_Session"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult recordTutorialSession([FromBody] Tutorial_Session tutorial_Session)
        {
            var PossibleTutorialSession = (from ts in dbContext.Tutorial_Sessions
                                           where ts.ClientBookingID.Equals(tutorial_Session.ClientBookingID)
                                           select ts).FirstOrDefault();

            if (PossibleTutorialSession != null)
                return Ok(PossibleTutorialSession.Id);

            dbContext.Tutorial_Sessions.Add(tutorial_Session);
            try
            {
                dbContext.SaveChanges();
                return Ok(tutorial_Session.Id);
             }
            catch(Exception ex)
            {

            }
                return Ok(0);           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tutorial_Session"></param>
        /// <returns></returns>
        [HttpGet("[action]/{clientBookingID}")]
        public IActionResult getTutorial_SessionViaClientBookingID(int clientBookingID)
        {
            var PossibleTutorialSession = (from ts in dbContext.Tutorial_Sessions
                                           where ts.ClientBookingID.Equals(clientBookingID)
                                           select ts).ToList();

                return Ok(PossibleTutorialSession);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TutorialSeesionID"></param>
        /// <param name="clientBookingID"></param>
        /// <returns></returns>
        [HttpGet("[action]/{TutorialSeesionID}/{clientBookingID}")]
        public IActionResult closeSession(int TutorialSeesionID,int clientBookingID)
        {
            var LiveSession = (from tS in dbContext.Tutorial_Sessions
                               where tS.Id.Equals(TutorialSeesionID) || tS.ClientBookingID.Equals(clientBookingID)
                               select tS).FirstOrDefault();
            
            if(LiveSession != null)
            {
                ((Tutorial_Session)LiveSession).IsCompleted = 1;
                dbContext.SaveChanges();
                return Ok(LiveSession.Id);

                }
            else
                return Ok(0);
            
        }

        [HttpGet("[action]/{id}")]
        public IActionResult getSession(int id)
        {
            var sess = (from p in dbContext.Tutorial_Sessions
                        where p.Id.Equals(id)
                        select p).FirstOrDefault();
            if(sess !=null)
            {
                return Ok(sess);
            }

            return Ok(-1);
        }

        [HttpGet("[action]")]
        public IActionResult getCompletedSessions()
        {
            var activeSess = (from a in dbContext.Tutorial_Sessions
                              where a.IsCompleted.Equals(1)
                              select a);
            if (activeSess != null)
                return Ok(activeSess);

            return null;
        }


            
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(int id)
        {

            var tutorialSession = dbContext.Tutorial_Sessions.Find(id);
            if (tutorialSession != null)
            {
                dbContext.Tutorial_Sessions.Remove(tutorialSession);
                dbContext.SaveChanges();
                return Ok(tutorialSession);
            }
            else
            {
                return Ok(0);
            }
        }


    }
}
