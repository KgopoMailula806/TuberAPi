using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TuberAPI.Data;
using TuberAPI.Infrastructure;
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
        /// <param name="id">User table ID</param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetBookingRequestByTutorReference(int id)
        {
            //get the user table reference 
            var tutorID = (from p in dbContext.Tutors
                           where p.User_Table_Reference.Equals(id)
                           select p.Id).ToList();

            if (tutorID != null)
            {
                var request = (from p in dbContext.BookingRequests
                               where p.Tutor_Reference.Equals(tutorID.ElementAt(0))
                               select p);

                if (request != null)
                {
                    return Ok(request);
                }
                return Ok(request);
            }return Ok(0);
            

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [HttpGet("[action]/{id}")]
        public IActionResult GetBookingRequestByClientReference(int id)
        {
            //get the user table reference 
            var clientID = (from p in dbContext.Clients
                           where p.User_Table_Reference.Equals(id)
                           select p.Id).ToList();

            var request = (from p in dbContext.BookingRequests
                           where p.Client_Reference.Equals(clientID.ElementAt(0))
                           select p).ToList();

            return Ok(request);            

        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetLocationByRequestPKID(int id)
        {
            //get the user table reference 
            var location = (from p in dbContext.BookingRequests
                            where p.Id.Equals(id)
                            select p.ClientProposedLocation).FirstOrDefault();

            if (location != null)
                return Ok(location);
            else return Ok(0);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [HttpGet("[action]/{id}/{code}")]
        public IActionResult changeBookingRequestAcceptedStatus(int id,int code)
        {

            var request = (from p in dbContext.BookingRequests
                           where p.Id.Equals(id)
                           select p).FirstOrDefault();
            try
            {
                if(request != null)
                {
                    request.Is_Accepted = code;
                    dbContext.SaveChanges();

                    return Ok(1);
                }
                else
                {
                    return Ok(0);
                }
            }catch (Exception ex)
            {
                return Ok(-1);
            }
        }

        [HttpGet("[action]/{id}/{code}/{tutor_Table_Reference}")]
        public IActionResult changeBookingRequestAcceptedStatusCheckTutorAvailability(int id, int code,int tutor_Table_Reference)
        {
            AvailabitlityChecking.dbContext = this.dbContext;
            if (AvailabitlityChecking.tutorIsAvailable(tutor_Table_Reference, id))
            {
                var request = (from p in dbContext.BookingRequests
                               where p.Id.Equals(id)
                               select p).FirstOrDefault();
                try
                {
                    if (request != null)
                    {
                        request.Is_Accepted = code;
                        dbContext.SaveChanges();

                        return Ok(1);
                    }
                    else
                    {
                        return Ok(0);
                    }
                }
                catch (Exception ex)
                {
                    return Ok(-1);
                }
            }
            else
                return Ok(-2);
            
        }

        [HttpGet("[action]/{id}/{code}/{client_Table_Reference}")]
        public IActionResult changeBookingRequestAcceptedStatusCheckCLientAvailability(int id, int code,int client_Table_Reference)
        {

            AvailabitlityChecking.dbContext = this.dbContext;
            if (AvailabitlityChecking.clientIsAvailable(client_Table_Reference, id))
            {
                var request = (from p in dbContext.BookingRequests
                               where p.Id.Equals(id)
                               select p).FirstOrDefault();
                try
                {
                    if (request != null)
                    {
                        request.Is_Accepted = code;
                        dbContext.SaveChanges();

                        return Ok(1);
                    }
                    else
                    {
                        return Ok(0);
                    }
                }
                catch (Exception ex)
                {
                    return Ok(-1);
                }
            }
            else return Ok(-2);
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult getClientIDByBookingRequestID(int id)
        {

            var clientID = (from p in dbContext.BookingRequests
                           where p.Id.Equals(id)
                           select p.Client_Reference).FirstOrDefault();
            
                if (clientID > 0)
                {
                    
                   
                    return Ok(clientID);
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
        /// <returns>The time requested by the Client</returns>
        [HttpGet("[action]/{BookingrequestId}")]
        public IActionResult GetClientsRequestDate(int BookingrequestId)
        {
            //
            var request = (from p in dbContext.BookingRequests
                           where p.Id.Equals(BookingrequestId)
                           select p.RequestDate).FirstOrDefault();

            if (request != null)
            {
                return Ok(FormatingMethods.getCorrectDateTimeFormat(request));
            }
            return Ok();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BookingrequestId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{BookingrequestId}/{tutorId}")]
        public IActionResult FinaliseBooking(int BookingrequestId, int id)
        {

            var BookingRequestEntry = dbContext.BookingRequests.Find(BookingrequestId);

            var tutorID = (from p in dbContext.Tutors
                           where p.User_Table_Reference.Equals(id)
                           select p.Id).FirstOrDefault();

            if (BookingRequestEntry != null)
            {
                //update the request entry
                BookingRequestEntry.Is_Accepted = 1;
                BookingRequestEntry.Tutor_Reference = tutorID;
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
            //request.EndTime = FormatingMethods.getCorrectDateTimeFormat(request.EndTime);
            //request.RequestDate = FormatingMethods.getCorrectDateTimeFormat(request.RequestDate);
            //request.RequestTime = FormatingMethods.getCorrectDateTimeFormat(request.RequestTime);
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
