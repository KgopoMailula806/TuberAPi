using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuberAPI.Data;
using TuberAPI.models;

namespace TuberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private TuberDbContext dbContext;

        public ClientController(TuberDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IEnumerable<Client> GetAllClients()
        {
            return dbContext.Clients;
        }

        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetClient(int id)
        {
            var client = dbContext.Clients.Find(id);
            if (client != null)
            {
                return Ok(client);
            }
            return Ok();
        }

        /// <summary>
        ///  return a jason body that 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetClientBytForeignKey(int id)
        {
            var client = (from c in dbContext.Clients
                          where c.User_Table_Reference.Equals(id)
                          select c).FirstOrDefault();
            if (client != null)
            {
                return Ok(client);
            }
            else return Ok();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetClientByForeignKey(int id)
        {
            var client = (from p in dbContext.Clients
                          where p.User_Table_Reference.Equals(id)
                          select p).FirstOrDefault();

            if (client != null)
            {
                return Ok(client);
            }
            return Ok();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult doesClientExists(int id)
        {
            var tutor = (from p in dbContext.Clients
                         where p.User_Table_Reference.Equals(id)
                         select p).FirstOrDefault();

            if (tutor != null)
            {
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
        public IActionResult GetClientTablereferenceByForeignKey(int id)
        {
            var clientID = (from p in dbContext.Clients
                            where p.User_Table_Reference.Equals(id)
                            select p.Id).FirstOrDefault();

            if (clientID > 0)
            {
                return Ok(clientID);
            }
            return Ok(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetUserTableDetails(int id)
        {
            var clientId = (from p in dbContext.Clients
                            where p.Id.Equals(id)
                            select p.User_Table_Reference).FirstOrDefault();

            if (clientId > 0)
            {
                var tutorUserDetails = (from tud in dbContext.Users
                                        where tud.Id.Equals(clientId)
                                        select tud);
                return Ok(tutorUserDetails);
            }
            return Ok(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetTutorId(int id)
        {
            var clientID = (from p in dbContext.Clients
                            where p.User_Table_Reference.Equals(id)
                            select p.Id).FirstOrDefault();

            if (clientID > 0)
            {
                return Ok(clientID);
            }
            return Ok(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        [HttpPost("[action]")]
        public IActionResult AddClient([FromBody]Client client)
        {
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();
            return Ok(client);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        [HttpPut("[action]/{id}")]
        public IActionResult editDetails(int id, [FromBody]Client user)
        {
            var clientEntity = dbContext.Clients.Find(id);

            if (clientEntity != null)
            {
                clientEntity.Current_Grade = user.Current_Grade;
                dbContext.SaveChanges();
                return Ok(clientEntity);
            }
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(int id)
        {

            var clientEntity = dbContext.Clients.Find(id);
            if (clientEntity != null)
            {
                dbContext.Clients.Remove(clientEntity);
                dbContext.SaveChanges();
                return Ok(clientEntity);
            }
            return Ok();

        }
    }
}