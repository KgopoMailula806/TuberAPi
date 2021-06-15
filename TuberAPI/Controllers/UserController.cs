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
    public class UserController : ControllerBase
    {
        private TuberDbContext dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public UserController(TuberDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IEnumerable<User> GetUsers()
        {
            return dbContext.Users;
        }

        /// <summary>
        ///  Get an individual user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = dbContext.Users.Find(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return Ok();
            }
        }

        /// <summary>
        ///  Get users client ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetUserClientID(int id)
        {
            var client = (from u in dbContext.Clients
                          where u.User_Table_Reference.Equals(id)
                          select u).FirstOrDefault();

            if (client != null)
            {
                return Ok(client);
            }
            else
                return Ok(-1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetUserDeatilsByClientID(int id)
        {
            var client = (from u in dbContext.Clients
                          where u.Id.Equals(id)
                          select u.User_Table_Reference).ToList();

            if (client != null)
            {

                var details = (from u in dbContext.Users
                               where u.Id.Equals(client.ElementAt(0))
                               select u).FirstOrDefault();

                return Ok(details);
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
        public IActionResult GetUserDetailsByTutorID(int id)
        {
            var tutorID = (from u in dbContext.Tutors
                          where u.Id.Equals(id)
                          select u.User_Table_Reference).ToList();

            if (tutorID != null)
            {

                var details = (from u in dbContext.Users
                               where u.Id.Equals(tutorID.ElementAt(0))
                               select u).FirstOrDefault();

                return Ok(details);
            }
            else
                return Ok();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newQuote"></param>
        [HttpPost("[action]")]
        public IActionResult AddUser([FromBody]User newUser)
        {
            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
            return Ok(newUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        [HttpPut("[action]/{id}")]
        public IActionResult editDetails(int id, [FromBody]User user)
        {
            var entityUser = dbContext.Users.Find(id);
            //var entityUser2 = dbContext.Users.Find()
            if (entityUser != null)
            {
                //The if statements are to make sure that only the edited data is the one that is modified
                if (user.FullNames != null)
                    entityUser.FullNames = user.FullNames;
                if (user.Surname != null)
                    entityUser.Surname = user.Surname;
                if (user.Valid_Phone_Number != null)
                    entityUser.Valid_Phone_Number = user.Valid_Phone_Number;
                if (user.Email_Address != null)
                    entityUser.Email_Address = user.Email_Address;
                if (user.PassWord != null)
                    entityUser.PassWord = user.PassWord;
                if (user.Gender != null)
                    entityUser.Gender = user.Gender;
                if (user.Image != null)
                    entityUser.Image = user.Image;
                if (user.Age != 0)
                    entityUser.Age = user.Age;
                if (user.User_Discriminator != null)
                    entityUser.User_Discriminator = user.User_Discriminator;
                dbContext.SaveChanges();
                return Ok(entityUser);
            }
            else
            {
                //TODO
                return Ok(0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(int id)
        {

            var entityUser = dbContext.Users.Find(id);
            if (entityUser != null)
            {
                dbContext.Users.Remove(entityUser);
                dbContext.SaveChanges();
                return Ok(entityUser);
            }
            else
            {
                return Ok();
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        [HttpGet("[action]/{email}/{password}")]
        public IActionResult Login(string email, string password)
        {
            var DBuser = (from p in dbContext.Users
                          where p.PassWord.Equals(password) && p.Email_Address.Equals(email)
                          select p).FirstOrDefault();

            if (DBuser != null)
            {
                return Ok(DBuser);
            }
            else { return Ok(DBuser); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        [HttpGet("[action]/{password}")]
        public IActionResult CheckIfPasswordExists(string password_)
        {
            var password = (from p in dbContext.Users
                            where p.PassWord.Equals(password_)
                            select p).FirstOrDefault();

            if (password != null)
            {
                return Ok(1);
            }
            else { return Ok(0); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        [HttpGet("[action]/{email}")]
        public IActionResult CheckIfEmailExists(string email)
        {
            var userEmail = (from p in dbContext.Users
                             where p.Email_Address.Equals(email)
                             select p.Email_Address).FirstOrDefault();

            if (userEmail != null)
            {
                return Ok(1);
            }
            else { return Ok(0); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult getCurrentUserStatus(int id)
        {
            var userCurrentDescriminator = (from dis in dbContext.Users
                                            where dis.Id.Equals(id)
                                            select dis.User_Discriminator).FirstOrDefault();

            if (userCurrentDescriminator != null)
            {
                return Ok(userCurrentDescriminator);
            }
            else
                return Ok(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}/{User_Discriminator}")]
        public IActionResult changeCurrentUserStatus(int id, string User_Discriminator)
        {
            var userCurrentDescriminator = (from dis in dbContext.Users
                                            where dis.Id.Equals(id)
                                            select dis).FirstOrDefault();

            if (userCurrentDescriminator != null)
            {
                userCurrentDescriminator.User_Discriminator = User_Discriminator;
                dbContext.SaveChanges();
                return Ok(1);
            }
            else
                return Ok(0);
        }

        [HttpPost("[action]/{id}")]
        public IActionResult DeactivateAccount(int id, [FromBody]Reason reason)
        {
            var samp = dbContext.Users.Find(id);

            if (samp != null)
            {
                //1 is active 0 - not active
                samp.isActive = 0;

                //Save the reason

                dbContext.Reasons.Add(reason);

                //Save changes
                
                dbContext.SaveChanges();
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }

        }


        /// <summary>
        /// 
        /// </summary>       
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetTutorName(int id)
        {
                var name = (from p in dbContext.Users
                            where p.Id.Equals(id)
                            select p).FirstOrDefault();
                if (name != null)
                {
                    return Ok(name.FullNames + " " + name.Surname);
                }

            
            return Ok("");
        }

        /// <summary>
        /// 
        /// </summary>       
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetTutornameByTutorTablePK(int id)
        {
            var tutorID = (from p in dbContext.Tutors
                        where p.Id.Equals(id)
                        select p.User_Table_Reference).FirstOrDefault();

            if(tutorID != null)
            {
                var name = (from p in dbContext.Users
                            where p.Id.Equals(tutorID)
                            select p).ToList();
                if (name != null)
                {
                    return Ok(name.ElementAt(0).FullNames + " " + name.ElementAt(0).Surname);
                }


                return Ok("");
            }
            else
            {
                return Ok("");
            }
            
        }

        /// <summary>
        /// 
        /// </summary>       
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public IActionResult GetClientByClientTablePK(int id)
        {
            var tutorID = (from p in dbContext.Clients
                           where p.Id.Equals(id)
                           select p.User_Table_Reference).ToList();

            if (tutorID != null)
            {
                var name = (from p in dbContext.Users
                            where p.Id.Equals(tutorID.ElementAt(0))
                            select p).FirstOrDefault();
                if (name != null)
                {
                    return Ok(name.FullNames + " " + name.Surname);
                }


                return Ok("");
            }
            else
            {
                return Ok("");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("[action]/{email}/{password}")]
        public IActionResult getUserByEmail(string email,string password)
        {
            var DBuser = (from p in dbContext.Users
                          where p.PassWord.Equals(password) && p.Email_Address.Equals(email)
                          select p).FirstOrDefault();

            if (DBuser != null)
            {
                return Ok(DBuser);
            }else
            {
                return Ok(-1);
            }
        }

        [HttpGet("[action]/{email}")]
        public IActionResult getUserByEmail(string email)
        {
            var DBuser = (from p in dbContext.Users
                          where p.Email_Address.Equals(email)
                          select p).FirstOrDefault();

            if (DBuser != null)
            {
                return Ok(DBuser);
            }
            else
            {
                return Ok(-1);
            }

        }

        [HttpGet("[action]")]
        public IEnumerable<User> getActiveUsers()
        {
            var users = (from p in dbContext.Users
                               where p.isActive.Equals(1)
                               select p);
            List<User> lUsers = new List<User>();
            if (users !=null)
            {
               
                return users;
            }else
            {
                return lUsers; //Emty list
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> userTable ID</param>
        /// <param name="flag">flag either 0,1,-1</param>
        /// <returns></returns>
        [HttpPost("[action]/{id}/{flag}")]
        public IActionResult changeUserISActiveFlag(int id, int flag)
        {
            var userDetails = (from p in dbContext.Users
                           where p.Id.Equals(id)
                           select p).FirstOrDefault();

            if (userDetails != null)
            {
                userDetails.isActive = flag;
                dbContext.SaveChanges();
                
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }

        }

    }
}
