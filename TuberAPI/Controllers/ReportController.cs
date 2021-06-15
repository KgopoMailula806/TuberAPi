using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuberAPI.Data;
using TuberAPI.Infrastructure;
using TuberAPI.models;
using TuberAPI.models.NonDatabaseModels;
using Module = TuberAPI.models.Module;

namespace TuberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private TuberDbContext dbContext;
        public ReportController(TuberDbContext databaseContext)
        {
            dbContext = databaseContext;
        }


        [HttpGet("[action]")]
        public IActionResult GetNoOfStudentsPerModule()
        {
            //10 modules
            var modules = (from p in dbContext.Modules
                           select p).ToList();
            List<ModuleReports> reps = new List<ModuleReports>();
            HelperMethods help = new HelperMethods(this.dbContext);
            foreach (Module m in modules)
            {
                reps.Add(new ModuleReports(m.Module_Name, help.getNoOfStudents(m.Id)));

            }


            return Ok(reps);


        }

        [HttpGet("[action]")]
        public IActionResult getGrossIncomePerModule()
        {
            var modules = (from p in dbContext.Modules
                           select p).ToList();
            List<ModuleReports> reps = new List<ModuleReports>();
           
            HelperMethods help = new HelperMethods(this.dbContext);

            foreach (Invoice i in help.getInvoices())
            {
                //Get payments for each module
                Tutorial_Session tut = help.getSession(i.Session_ID);
                if (tut != null)
                {
                    // Get module Id
                    ClientBooking book = help.getBooking(tut.ClientBookingID);
                    if (book != null)
                    {


                        BookingRequest booking = help.getBookingReq(book.BookingDetails_BookingRequestTable_Reference);
                        if (booking != null)
                        {
                            //Get module
                            Module mod = help.getModule(booking.ModuleID1);

                            if (reps.Count().Equals(0))
                            {
                                ModuleReports temp = new ModuleReports(mod.Module_Name, 0.0);
                                reps.Add(temp);

                            }

                            for (int j = 0; j < reps.Count(); j++)
                            {

                                if (help.checkIsInList(mod.Module_Name, reps))
                                {

                                    reps.ElementAt(help.getIndexinlist(mod.Module_Name, reps)).doubleValue += Double.Parse(i.Amount);
                                    break;


                                }
                                else
                                {
                                    ModuleReports temp = new ModuleReports(mod.Module_Name, 0.0);
                                    reps.Add(temp);


                                }
                            }



                        }
                    }
                }



            }




            return Ok(reps);
        }


        [HttpGet("[action]")]
        public IActionResult getNumOfOutstandingPays()
        {
            var num = (from p in dbContext.Invoices
                       where p.is_Paid.Equals(0)
                       select p);
            if (num != null)
                return Ok(num);

            return Ok(-1);
        }

        [HttpGet("[action]/{tutorID}")]
        public IActionResult sawpRatings(int tutorID)
        {
            var num = (from p in dbContext.Ratings
                       where p.Tutor_ID.Equals(tutorID)
                       select p).FirstOrDefault();
            if (num != null)
            {
                double r = num.Client_Rating;
                num.Client_Rating = num.Tutor_Rating;
                num.Tutor_Rating = r;
                dbContext.SaveChanges();
                return Ok(num.Client_Rating);
            }
                

            return Ok(-1);
        }

        [HttpGet("[action]")]
        public IActionResult getNumOfOutstandingTutorApps()
        {
            var numb = (from p in dbContext.Tutors
                        where p.Is_Accepted.Equals(0)
                        select p).Count();
            if (numb != -1)
                return Ok(numb);

            return Ok(-1);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult getTutorRatings(int id)
        {
            var ratings = (from b in dbContext.Ratings
                           where b.Tutor_ID.Equals(id)
                           select b);

            if (ratings != null)
                return Ok(ratings);

            return Ok(-1);
        }

        [HttpGet("[action]")]
        public IActionResult getNoOfBookingsPerModule()
        {
            //Get all the modules
            var modules = (from p in dbContext.Modules
                           select p).ToList();
            List<ModuleReports> reps = new List<ModuleReports>();
            HelperMethods help = new HelperMethods(this.dbContext);
            foreach (Module m in modules)
            {
                reps.Add(new ModuleReports(m.Module_Name ,help.getNoOfBookings(m.Id)));

            }


            return Ok(reps);
           
        }


        [HttpGet("[action]/{id}")]
        public IActionResult getNoOfCompletedSessionForTutor(int id)
        {
            TutorReport rep = null;
            HelperMethods help = new HelperMethods(this.dbContext);
            int sessions = (from v in dbContext.Tutorial_Sessions
                            where v.IsCompleted.Equals(1) && v.Tutor_Id.Equals(id)
                            select v).Count();
            if (sessions == 0)
            {
                rep = new TutorReport(id,help.getTutorNameById(id), 0);
            }else
            {
                rep = new TutorReport(id, help.getTutorNameById(id), sessions);
                return Ok(rep);
            }
            return Ok(rep);
        }

        [HttpGet("[action]")]
        public IActionResult getNoOfTutorsPerModule()
        {
            List<ModuleReports> reps = new List<ModuleReports>();
            HelperMethods help = new HelperMethods(this.dbContext);
            foreach(Module m in help.getModules())
            {
                //Calculate how many times this module Id appears in the tutor modules table
                int count = 0;
                count = (from i in dbContext.Tutor_Modules
                         where i.Module_Reference.Equals(m.Id)
                         select i).Count();
                ModuleReports report = new ModuleReports(m.Module_Name, count);
                reps.Add(report);
            }

            return Ok(reps);
        }
    }
}
