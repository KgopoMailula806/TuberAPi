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
    public class BookingRequestController : ControllerBase
    {
        private TuberDbContext dbContext;

        //Constructor to setup the db
        public BookingRequestController(TuberDbContext databaseContext)
        {
            this.dbContext = databaseContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetAllBoookingRequest()
        {
            return Ok(dbContext.BookingRequests);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetIndividualBookingRequest(int id)
        {
            var request = dbContext.BookingRequests.Find(id);

            if (request != null)
            {
                return Ok(request);
            }
            return Ok();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetBookingRequestByTutorReference(int id)
        {
            //
            var request = (from p in dbContext.BookingRequests
                           where p.Tutor_Reference.Equals(id)
                           select p);

            if (request != null)
            {
                return Ok(request);
            }
            return Ok();

        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The time requested by the Client</returns>
        [HttpGet("[action]/{BookingrequestId}")]
        public IActionResult GetClientsRequestDate(int BookingrequestId)
        {
            //
            var request = (from p in dbContext.BookingRequests
                           where p.Id.Equals(BookingrequestId)
                           select p.RequestDate);

            if (request != null)
            {
                return Ok(request);
            }
            return Ok();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BookingrequestId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{BookingrequestId}/{tutorId}")]
        public IActionResult FinaliseBooking(int BookingrequestId, int tutorId)
        {

            var BookingRequestEntry = dbContext.BookingRequests.Find(BookingrequestId);
            if (BookingRequestEntry != null)
            {
                //update the request entry
                BookingRequestEntry.Is_Accepted = 1;
                BookingRequestEntry.Tutor_Reference = tutorId;
                BookingRequestEntry.IsRespondedTo = 1;

                dbContext.SaveChanges();
                //BookingRequest entry
                return Ok(1);
            }
            return Ok(0);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BookingrequestId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{BookingrequestId}")]
        public IActionResult GetBookingrequestLocation(int BookingrequestId)
        {

            var requestLocation = (from p in dbContext.BookingRequests
                                   where p.Id.Equals(BookingrequestId)
                                   select p.ClientProposedLocation);
            if (requestLocation != null)
            {
                //BookingRequest entry
                return Ok(requestLocation);
            }
            return Ok(0);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("[action]")]
        public IActionResult AddBookingRequest([FromBody] BookingRequest request)
        {
            dbContext.BookingRequests.Add(request);
            dbContext.SaveChanges();
            return Ok(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("[action]/{id}")]
        public IActionResult InsertTutorBookingRequirements(int id, [FromBody] BookingRequest value)
        {
            var bR = dbContext.BookingRequests.Find(id);
            if (bR != null)
            {
                bR.IsRespondedTo = 1;
                bR.tutorProposedLocation = value.tutorProposedLocation;
                dbContext.SaveChanges();
                return Ok(bR);
            }
            else return BadRequest();
            //Todo
        }

        /// <summary>
        ///  Method returns all of the Requests that a tutor is qulified for
        /// </summary>
        /// <param name="TutorsModule"></param>
        /// <returns></returns>
        /** [HttpPost("[action]")]
         public IActionResult getTheTutorQualifiedBookingRequests([FromBody] Module TutorsModule)
         {
             var Bk = (from bk in dbContext.BookingRequests
                       where bk.Subject.Equals(TutorsModule.Module_Name) && bk.Is_Accepted.Equals(0)
                       select bk);

             if (Bk != null)
             {
                 return Ok(Bk);
             }
             else return Ok();
     
    }
    */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            var requestEntity = dbContext.BookingRequests.Find(id);
            if (requestEntity != null)
            {
                dbContext.BookingRequests.Remove(requestEntity);
                dbContext.SaveChanges();
                return Ok(requestEntity);
            }
            else
            {

                return Ok();
            }
        }
    }
}
