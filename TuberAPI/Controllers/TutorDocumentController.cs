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
    public class TutorDocumentController : ControllerBase
    {
        private TuberDbContext dbContext;

        //Constructor to setup the db
        public TutorDocumentController(TuberDbContext databaseContext)
        {
            this.dbContext = databaseContext;
        }

        [HttpGet("[action]/{tID}")]
        public IActionResult GetClearance(int tID)
        {
            if(tDocExist(tID, "Police Clearance"))
            {
                var TutorDoc = (from doc in dbContext.TutorDocuments
                                where (doc.TutorID == tID) && (doc.DocumentType == "Police Clearance")
                                select doc).FirstOrDefault();

                return Ok(TutorDoc.DocID);
            }

            return Ok(null);
        }

        [HttpGet("[action]/{tID}")]
        public IActionResult GetCV(int tID)
        {
            if (tDocExist(tID, "Tutor CV"))
            {
                var TutorDoc = (from doc in dbContext.TutorDocuments
                                where (doc.TutorID == tID) && (doc.DocumentType == "Tutor CV")
                                select doc).FirstOrDefault();

                return Ok(TutorDoc.DocID);
            }

            return Ok(-1);
        }

        [HttpGet("[action]/{tID}")]
        public IActionResult GetTranscript(int tID)
        {
            if (tDocExist(tID, "Academic Record"))
            {
                var TutorDoc = (from doc in dbContext.TutorDocuments
                                where (doc.TutorID == tID) && (doc.DocumentType == "Academic Record")
                                select doc).FirstOrDefault();

                return Ok(TutorDoc.DocID);
            }

            return Ok(-1);
        }

        [HttpPost("[action]")]
        public IActionResult UploadTutorDocument([FromBody] TutorDocument doc)
        {
            if (doc != null)
            {
                dbContext.TutorDocuments.Add(doc);
                dbContext.SaveChanges();
                return Ok(doc.Id);
            }

            return Ok(-1);
        }

        private bool tDocExist(int tID, string dID)
        {
            var TutorDoc = (from doc in dbContext.TutorDocuments
                            where (doc.DocumentType == dID) && (doc.TutorID == tID)
                            select doc).FirstOrDefault();

            if (TutorDoc != null)
                return true;

            return false;
        }
    }
}
