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
    public class ManagerController : ControllerBase
    {
        private TuberDbContext dbContext;

        public ManagerController(TuberDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [HttpGet("[action]")]
        public IEnumerable<Manager> GetManagers()
        {
            return dbContext.Managers;
        }

        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetManager(int id)
        {
            var manager = dbContext.Managers.Find(id);

            if (manager != null)
            {
                return Ok(manager);
            }
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetManagerByForeignKey(int id)
        {
            var manager = (from p in dbContext.Managers
                           where p.User_Table_Reference.Equals(id)
                           select p).FirstOrDefault();

            if (manager != null)
            {
                return Ok(manager);
            }
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetUserTableDetails(int id)
        {
            var managerID = (from p in dbContext.Managers
                             where p.Id.Equals(id)
                             select p.User_Table_Reference).FirstOrDefault();

            if (managerID > 0)
            {
                var tutorUserDetails = (from tud in dbContext.Users
                                        where tud.Id.Equals(managerID)
                                        select tud);
                return Ok(tutorUserDetails);
            }
            return Ok(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newQuote"></param>
        [HttpPost("[action]")]
        public IActionResult AddManager([FromBody]Manager newManager)
        {
            dbContext.Managers.Add(newManager);
            dbContext.SaveChanges();
            return Ok(newManager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="manager"></param>
        [HttpPut("[action]/{id}")]
        public IActionResult editDetails(int id, [FromBody]Manager manager)
        {
            var managerEntity = dbContext.Managers.Find(id);
            if (managerEntity != null)
            {
                dbContext.SaveChanges();
                return Ok(manager);
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
            var managerEntity = dbContext.Managers.Find(id);

            if (managerEntity != null)
            {
                dbContext.Managers.Remove(managerEntity);
                dbContext.SaveChanges();
                return Ok(managerEntity);
            }
            return Ok();

        }
    }
}