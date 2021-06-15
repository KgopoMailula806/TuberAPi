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
    public class DocumentController : ControllerBase
    {
        private TuberDbContext dbContext;

        //Constructor to setup the db
        public DocumentController(TuberDbContext databaseContext)
        {
            this.dbContext = databaseContext;
        }


        [HttpGet("[action]")]
        public IActionResult GetAllDocument()
        {
            var docs = dbContext.Documents;
            if (docs != null)
                return Ok(docs);

            return Ok(null);
        }
        /// <summary>
        /// GET api/<DocumentController>/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetDocument(int id)
        {
            var doc = dbContext.Documents.Find(id);
            if (doc != null)
                return Ok(doc);

            return Ok(null);
        }

        // POST api/<DocumentController>
        [HttpPost("[action]")]
        public IActionResult UploadDocument([FromBody] Document doc)
        {
            if (doc != null)
            {
                dbContext.Documents.Add(doc);
                dbContext.SaveChanges();
                return Ok(doc.Id);
            }

            return Ok(-1);
        }

        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(int id)
        {

            var document = dbContext.Documents.Find(id);
            if (document != null)
            {
                dbContext.Documents.Remove(document);
                dbContext.SaveChanges();
                return Ok(document);
            }
            else
            {
                return Ok();
            }
        }
    }
}
