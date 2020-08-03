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
    public class ModulesController : ControllerBase
    {
        private TuberDbContext dbContext;

        public ModulesController(TuberDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        ///    URI Route: api/Modules/GetModules
        /// </summary>
        /// <returns>All of the listed modules available</returns>
        [HttpGet("[action]")]
        public IEnumerable<Module> GetModules()
        {
            return dbContext.Modules;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        [HttpGet("[action]/{moduleName}")]
        public IActionResult GetModuleByName(string moduleName)
        {
            var module = (from p in dbContext.Modules
                          where p.Module_Name.Equals(moduleName)
                          select p).FirstOrDefault();

            if (module != null)
            {
                return Ok(module);
            }
            else
                return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetModules(int id)
        {
            var module = dbContext.Modules.Find(id);
            if (module != null)
            {
                return Ok(module);
            }
            else { return Ok(); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetClientModulesByUserTableID(int id)
        {
            //Get the Clients Id with the give user table Id 
            var clientTableID = (from C in dbContext.Clients
                                 where C.User_Table_Reference.Equals(id)
                                 select C.Id).FirstOrDefault();

            if (clientTableID > 0)
            {
                var cMods = (from cm in dbContext.Client_Modules
                             where cm.clientRef.Equals(clientTableID) && cm.Is_Active == 1
                             select cm.ModuleId).ToList();

                var modules = (from mods in dbContext.Modules
                               where cMods.Contains(mods.Id)
                               select mods).ToList();

                if (modules != null)
                    return Ok(modules);
                else
                    return Ok(0);
            }
            return Ok(0);
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetTutorModulesByUserTableID(int id)
        {
            //Get the Clients Id with the give user table Id 
            var tutorTableID = (from C in dbContext.Tutors
                                where C.User_Table_Reference.Equals(id)
                                select C.Id).FirstOrDefault();

            if (tutorTableID > 0)
            {
                var tMods = (from cm in dbContext.Tutor_Modules
                             where cm.Tutor_Reference.Equals(tutorTableID) && cm.Is_Active == 1
                             select cm.Module_Reference).ToList();

                var modules = (from mods in dbContext.Modules
                               where tMods.Contains(mods.Id)
                               select mods).ToList();

                if (modules != null)
                    return Ok(modules);
                else
                    return Ok(0);
            }
            return Ok(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetModuleName(int id)
        {
            var module = dbContext.Modules.Find(id);
            if (module != null)
            {
                return Ok(module.Module_Name);
            }
            else { return Ok(); }
        }

        /// <summary>
        /// Give it a client table primary key not user table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult getClientModuleByClientTablePrimaryKey(int id)
        {
            var cMods = (from cm in dbContext.Client_Modules
                         where cm.clientRef.Equals(id)
                         select cm.ModuleId);

            try
            {
                List<Module> clientModule = new List<Module>();
                if (!ModelState.IsValid)
                {
                    //Entry doesnt exist
                }
                foreach (int mId in cMods)
                {
                    var IndividualModule = (from cm in dbContext.Modules
                                            where cm.Id.Equals(mId)
                                            select cm);

                    clientModule.Add((Module)IndividualModule);

                }
                return Ok(clientModule);
            }
            catch (Exception e)
            {
                return Ok(0);
            }



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newQuote"></param>
        [HttpPost("[action]")]
        public IActionResult AddModule([FromBody]Module module)
        {
            dbContext.Modules.Add(module);
            dbContext.SaveChanges();
            return Ok(module);
        }

        /// <summary>
        ///     (PUT) Basically editing the details of the module
        /// </summary>
        /// <param name="id"></param>
        /// <param name="module"></param>
        [HttpPut("[action]/{id}")]
        public IActionResult editDetails(int id, [FromBody]Module module)
        {
            var moduleEntity = dbContext.Modules.Find(id);

            if (moduleEntity != null)
            {
                // should not you edit all details not just
                moduleEntity.Module_Name = module.Module_Name;
                moduleEntity.Module_Code = module.Module_Code;
                dbContext.SaveChanges();
                return Ok(moduleEntity);

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
            var moduleEntity = dbContext.Clients.Find(id);
            if (moduleEntity != null)
            {
                dbContext.Clients.Remove(moduleEntity);
                dbContext.SaveChanges();
                return Ok(moduleEntity);
            }

            return Ok();
        }
    }
}