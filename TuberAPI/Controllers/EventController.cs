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
    public class EventController : ControllerBase
    {
        private TuberDbContext dbContext;

        public EventController(TuberDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetEvents()
        {
            try
            {
                return Ok(dbContext.Events);
            }
            catch
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetEvent(int id)
        {
            var event_ = dbContext.Events.Find(id);

            if (event_ != null)
                return Ok(event_);
            else
                return Ok();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult pushEvent([FromBody] Event event_)
        {
            try
            {
                dbContext.Events.Add(event_);
                dbContext.SaveChanges();
                return Ok(event_.ID);
            }
            catch (Exception ex)
            {
                return Ok(0);
            }

        }
    }
}