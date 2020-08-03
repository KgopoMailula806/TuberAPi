using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuberAPI.Data;
using TuberAPI.Infrastructure;
using TuberAPI.models;

namespace TuberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tutor_ModuleController : ControllerBase
    {
        private TuberDbContext dbContext;

        public Tutor_ModuleController(TuberDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetAllTutorModuleBridges()
        {
            try
            {
                return Ok(dbContext.Tutor_Modules);
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
        public IActionResult getTutorModulesEntriesByTutor_Reference(int id)
        {
            List<String> modules = new List<String>();
            var cMods = (from tm in dbContext.Tutor_Modules
                         where tm.Tutor_Reference.Equals(id) && tm.Is_Active == 1
                         select tm);


            if (!ModelState.IsValid)
            {
                //Entry doesnt exist
            }
            if (cMods != null)
            {
                return Ok(cMods);
            }
            else return Ok();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TutorId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetTutorModuleBridgeAt(int TutorId)
        {
            var tutorModules = (from tm in dbContext.Tutor_Modules
                                where tm.Tutor_Reference.Equals(TutorId)
                                select tm.Tutor_Reference);

            if (tutorModules != null)
            {
                return Ok(tutorModules);
            }
            else
            {
                return Ok();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult AddNewTutorModuleBridge([FromBody] Tutor_Module Tutor_Module)
        {
            if (Tutor_Module != null)
            {
                HelperMethods helper = new HelperMethods(this.dbContext);
                if (helper.getMatchCombo(Tutor_Module.Module_Reference, Tutor_Module.Tutor_Reference))
                {
                    var combo = (from c in dbContext.Tutor_Modules
                                 where c.Tutor_Reference.Equals(Tutor_Module.Tutor_Reference) && c.Module_Reference.Equals(Tutor_Module.Module_Reference)
                                 select c).FirstOrDefault();

                    combo.Is_Active = 1;
                    dbContext.SaveChanges();
                    return Ok(1);
                }

                try
                {
                    dbContext.Tutor_Modules.Add(Tutor_Module);
                    dbContext.SaveChanges();
                    helper.makeModuleActive(Tutor_Module);
                    return Ok(1);
                }
                catch (Exception ex)
                {

                }

                return Ok(-1);
            }
            else
                return Ok(-1);
        }

        /// <summary>
        /// return a list of All modules registered to the client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IDs}")]
        public IActionResult removeTutorModule(string IDs)
        {
            string[] arrIds = IDs.Split('_');
            int TutorID = Convert.ToInt32(arrIds[0]);
            int ModuleID = Convert.ToInt32(arrIds[1]);

            var combo = (from mod in dbContext.Tutor_Modules
                         where ((mod.Tutor_Reference == TutorID) && (mod.Module_Reference == ModuleID))
                         select mod).First();


            if (combo != null)
            {
                combo.Is_Active = 0;
                dbContext.SaveChanges();
                return Ok(1);
            }
            else
                return Ok(-1);
        }

        /**
         * Utility Method
         * */

        /// <summary>
        ///  This method addS a new module to the client after checking for any Collisions
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("[action]/{userID}/{userStatus}")]
        public IActionResult AddNewTutorModuleBridge_CollisionChecking(int userID, string userStatus, [FromBody] Tutor_Module tutor_Module)
        {
            HelperMethods helper = new HelperMethods(this.dbContext);
            if (!helper.CheckIFClientAccountRegisteredForTheModule(userID, userStatus, tutor_Module))
            {
                if (tutor_Module != null)
                {
                    /*
                        if (helper.getMatchCombo(tutor_Module.Module_Reference, tutor_Module.Tutor_Reference))
                        {
                            var combo = (from c in db.Tutor_Modules
                                         where c.Tutor_Reference.Equals(tutor_Module.Tutor_Reference) && c.Module_Reference.Equals(Tutor_Module.Module_Reference)
                                         select c).FirstOrDefault();

                            combo.Is_Active = 1;
                            db.SaveChanges();
                            return Ok(1);
                        } */

                    try
                    {
                        dbContext.Tutor_Modules.Add(tutor_Module);
                        dbContext.SaveChanges();
                        helper.makeModuleActive(tutor_Module);
                        return Ok(1);
                    }
                    catch (Exception ex)
                    {

                    }

                    return Ok(1);
                }
                else
                    return Ok(-1);
            }
            else
                return Ok(0);

        }
    }
}
