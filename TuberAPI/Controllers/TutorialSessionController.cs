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

    }
}
