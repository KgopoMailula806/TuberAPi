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
    public class TutorController : ControllerBase
    {
        private TuberDbContext dbContext;

        public TutorController(TuberDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetTutors()
        {
            return Ok(dbContext.Tutors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetTutor(int id)
        {
            var tutor = dbContext.Tutors.Find(id);
            if (tutor != null)
            {
                return Ok(tutor);
            }

            return Ok();
        }


        /// <summary>
        /// 
        /// </summary>       
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetTutorByForeignKey(int id)
        {
            var tutor = (from p in dbContext.Tutors
                         where p.User_Table_Reference.Equals(id)
                         select p).FirstOrDefault();

            if (tutor != null)
            {
                return Ok(tutor);
            }
            return Ok();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult doesTutorExists(int id)
        {
            var tutor = (from p in dbContext.Tutors
                         where p.User_Table_Reference.Equals(id)
                         select p).FirstOrDefault();

            if (tutor != null)
            {
                return Ok(1);
            }
            else
            return Ok(0);
        }


        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetTutorId(int id)
        {
            var tutorID = (from p in dbContext.Tutors
                           where p.User_Table_Reference.Equals(id)
                           select p.Id).FirstOrDefault();

            if (tutorID > 0)
            {
                return Ok(tutorID);
            }
            return Ok(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{moduleName}")]
        public IActionResult GetTutorsByTheRespectiveModuleTheyTutor(string moduleName)
        {
            //get the module id
            var moduleId = (from p in dbContext.Modules
                            where p.Module_Name.Equals(moduleName)
                            select p.Id).FirstOrDefault();

            if (moduleId > 0)
            {
                var tutorsIds = (from tm in dbContext.Tutor_Modules
                                 where tm.Module_Reference.Equals(moduleId)
                                 select tm.Tutor_Reference).ToList();

                var userTableIds = (from uIds in dbContext.Tutors
                                    where tutorsIds.Contains(uIds.Id)
                                    select uIds.User_Table_Reference).ToList();

                var userdetails = (from userD in dbContext.Users
                                   where userTableIds.Contains(userD.Id)
                                   select userD).ToList();
                return Ok(userdetails);
            }
            return Ok(0);
        }

        [HttpGet("[action]")]
        public IActionResult GetPendingTutors()
        {
            var tutors = (from t in dbContext.Tutors
                          where t.Is_Accepted == 0
                          select t).ToList();

            if (tutors != null)
                return Ok(tutors);

            return Ok(null);
        }




        /// <summary>
        ///  return the userDetails if given the Tutor table primary key not foreign key  i.e. user table PK
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetUserTableDetails(int id)
        {
            var tutor = (from p in dbContext.Tutors
                         where p.Id.Equals(id)
                         select p.User_Table_Reference).FirstOrDefault();

            if (tutor > 0)
            {
                var tutorUserDetails = (from tud in dbContext.Users
                                        where tud.Id.Equals(tutor)
                                        select tud).FirstOrDefault();
                return Ok(tutorUserDetails);
            }
            return Ok(0);
        }

        /// <summary>
        ///  returns the PK of the user details table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetUserTableID(int id)
        {
            var tutorID = (from p in dbContext.Tutors
                           where p.Id.Equals(id)
                           select p.User_Table_Reference).FirstOrDefault();

            if (tutorID > 0)
            {

                return Ok(tutorID);
            }
            return Ok(0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tutor"></param>
        [HttpPost("[action]")]
        public IActionResult AddTutor([FromBody]Tutor tutor)
        {
            dbContext.Tutors.Add(tutor);
            dbContext.SaveChanges();
            return Ok(tutor);

        }



        /* /// <summary>
         ///  
         /// </summary>
         /// <param name="id"></param>
         /// <param name="tutorAR"></param>
         /// <returns></returns>
         //[Consumes("multipart/form-data")]
         [HttpPost("[action]/{id}")]
         [AllowAnonymous]
         public IActionResult insertAcademicRecord(int id, [FromBody]IFormFile tutorAR)
         {
             var tutor = dbContext.Tutors.Find(id);
             if (tutor != null)
             {

                 using (Stream inStream = tutorAR.OpenReadStream())
                 {
                     MemoryStream memoryStream = inStream as MemoryStream;

                     if (memoryStream == null) //has nothing 
                     {
                         memoryStream = new MemoryStream();
                         inStream.CopyTo(memoryStream);
                     }

                     tutor.Academic_RecordBinary = memoryStream.ToArray();
                 }

                 if (ModelState.IsValid)
                 {
                     dbContext.SaveChanges();
                     return Ok(tutor);
                 }
             }
                 return Ok();
         } */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        [HttpPut("[action]/{id}")]
        public void UpdateDetails(int id, [FromBody] Tutor user)
        {
            var entityUser = dbContext.Tutors.Find(id);
            //var entityUser2 = dbContext.Users.Find()
            if (entityUser != null)
            {
                //TODO Update for the tutor            
                dbContext.SaveChanges();
            }
            else
            {
                //TODO
            }
        }

        [HttpGet("[action]/{id}")]
        public IActionResult rejectTutor(int id)
        {
            var entityUser = dbContext.Tutors.Find(id);

            if (entityUser != null)
            {
                entityUser.Is_Accepted = -1;
                dbContext.SaveChanges();
                return Ok(1);
            }

            return Ok(-1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public IActionResult DeleteTutor(int id)
        {
            var entityUser = dbContext.Tutors.Find(id);
            if (entityUser != null)
            {
                dbContext.Tutors.Remove(entityUser);
                dbContext.SaveChanges();
                return Ok(entityUser);
            }
            else
            {
                // TODO
                return Ok();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetTutorTablePrimaryKey(int id)
        {
            var tutor = dbContext.Tutors.Find(id);
            if (tutor != null)
                return Ok(tutor.Id);
            else
                return Ok(tutor.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetTutorTableReferenceByForeignKey(int id)
        {
            var tutorID = (from t in dbContext.Tutors
                           where t.User_Table_Reference.Equals(id)
                           select t.Id).FirstOrDefault();

            if (tutorID > 0)
            {
                return Ok(tutorID);
            }
            return Ok(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetAllTutorModuleIdsWithUserID(int id)
        {
            /*  ToDO
                Select Modules.Module_Name
                from Modules
                where Modules.Id IN (Select Tutor_Modules.Module_Reference
                from Tutor_Modules
                where Tutor_Modules.Tutor_Reference IN (select Tutors.Id
                from Tutors
                where Tutors.User_Table_Reference = 1018))
             */

            var tutorId = (from t in dbContext.Tutors
                           where t.User_Table_Reference.Equals(id)
                           select t.Id).FirstOrDefault();

            if (tutorId > 0)
            {
                var moduleIds = (from m in dbContext.Tutor_Modules
                                 where m.Tutor_Reference.Equals(tutorId) && m.Is_Active.Equals(1)
                                 select m.Module_Reference);
                if (moduleIds != null)
                    return Ok(moduleIds);
                else return Ok(null);

            }
            else
                return Ok();

        }
    }
}
