using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuberAPI.Data;
using TuberAPI.Infrastructure;
using TuberAPI.models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TuberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Client_ModuleController : Controller
    {
        //Adding Database context 
        private TuberDbContext db;

        //Constructor to setup the db
        public Client_ModuleController(TuberDbContext databaseContext)
        {
            this.db = databaseContext;
        }


        /// <summary>
        /// return a list of All modules registered to the client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetClient_Modules(int id)
        {
            List<String> modules = new List<String>();
            var cMods = db.Client_Modules.Where(t => t.clientRef == id).ToList();

            if (!ModelState.IsValid)
            {
                //Entry doesnt exist
            }
            return Ok(cMods);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetAllClient_Modules()
        {
            var clientModule = db.Client_Modules;

            if (clientModule != null)
            {
                return Ok(clientModule);
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
        public IActionResult getClientModulesByForeignKey(int id)
        {
            List<String> modules = new List<String>();
            var cMods = (from cm in db.Client_Modules
                         where cm.clientRef.Equals(id) && cm.Is_Active == 1
                         select cm);

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
        /// <param name="id"></param>
        /// <returns></returns>
        /*[HttpGet("[action]/{id}")]
        public IActionResult getActualClientModules(int id)
        {
            

            var cMods = (from cm in db.Client_Modules
                         where cm.clientRef.Equals(id)
                         select cm);

            List<Module> modules = new List<Module>();
            foreach(Client_Module cm in cMods)
            {
                var module = (from mod in db.Modules
                              where mod.Id.Equals(cm.ModuleId)
                              select mod);
                Module castedModule = (Module)module;
                modules.Add(castedModule);


            }

            return Ok(modules);

        } */


        /// <summary>
        /// Add module to client 
        /// </summary>
        /// <param name="c_mod"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> addModuleToClient([FromBody]Client_Module c_mod)
        {
            //check if the data annotations from the client model fails
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (db)
            {
                db.Client_Modules.Add(c_mod);
            }

            await db.SaveChangesAsync();

            //creates the object and returns the actual url  for the resource 
            return CreatedAtAction("GetClient_Modules", new Client_Module { DateAssigned = c_mod.DateAssigned, Is_Active = c_mod.Is_Active, ModuleId = c_mod.ModuleId, clientRef = c_mod.clientRef }, c_mod);
        }

        /// <summary>
        ///  This method just added a new module to the client without checking for any Collisions
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult AddNewClientModuleBridge([FromBody] Client_Module Client_Module)
        {
            if (Client_Module != null)
            {              
                HelperMethods helper = new HelperMethods(this.db);
                if (helper.getMatchCombo(Client_Module.ModuleId, Client_Module.clientRef))
                {
                    var combo = (from c in db.Client_Modules
                                 where c.clientRef.Equals(Client_Module.clientRef) && c.ModuleId.Equals(Client_Module.ModuleId)
                                 select c).FirstOrDefault();

                    combo.Is_Active = 1;
                    db.SaveChanges();
                    return Ok(1);
                }

                try
                {
                    db.Client_Modules.Add(Client_Module);
                    db.SaveChanges();
                    helper.makeModuleActive(Client_Module);
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
        ///  This method addS a new module to the client after checking for any Collisions
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("[action]/{userID}/{userStatus}")]
        public IActionResult AddNewClientModuleBridge_CollisionChecking(int userID, string userStatus,[FromBody] Client_Module Client_Module)
        {
            HelperMethods helper = new HelperMethods(this.db);
            if (!helper.CheckIFTutorAccountRegisteredForTheModule(userID, userStatus, Client_Module))
            {
                if (Client_Module != null)
                {         
                    /*
                    if (helper.getMatchCombo(Client_Module.ModuleId, Client_Module.clientRef))
                    {
                        var combo = (from c in db.Client_Modules
                                     where c.clientRef.Equals(Client_Module.clientRef) && c.ModuleId.Equals(Client_Module.ModuleId)
                                     select c).FirstOrDefault();

                        combo.Is_Active = 1;
                        db.SaveChanges();
                        return Ok(1);
                    } */
                    try
                    {
                        db.Client_Modules.Add(Client_Module);
                        db.SaveChanges();
                        helper.makeModuleActive(Client_Module);
                        return Ok(1);
                    }
                    catch(Exception ex)
                    {

                    }

                    return Ok(-1);

                }
                else
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
        public IActionResult removeModule(string IDs)
        {
            string[] arrIds = IDs.Split('_');
            int clientID = Convert.ToInt32(arrIds[0]);
            int ModuleID = Convert.ToInt32(arrIds[1]);

            var combo = (from mod in db.Client_Modules
                         where ((mod.clientRef == clientID) && (mod.ModuleId == ModuleID))
                         select mod).First();


            if (combo != null)
            {
                combo.Is_Active = 0;
                db.SaveChanges();
                return Ok(1);
            }
            else
                return Ok(-1);
        }

        /// <summary>
        /// Remove module from client
        /// </summary>
        /// <param name="id"></param>        
        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(int id)
        {

            var ClientModule = db.Client_Modules.Find(id);
            if (ClientModule != null)
            {
                db.Client_Modules.Remove(ClientModule);
                db.SaveChanges();
                return Ok(ClientModule);
            }
            else
            {
                return Ok();
            }
        }

    }
}