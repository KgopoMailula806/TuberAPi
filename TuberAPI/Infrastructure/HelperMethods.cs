using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuberAPI.models;
using TuberAPI.Data;
using System.Security.Cryptography;
using TuberAPI.Controllers;
using TuberAPI.models.NonDatabaseModels;

namespace TuberAPI.Infrastructure
{
    public class HelperMethods
    {
        private TuberDbContext db;

        public HelperMethods(TuberDbContext databaseContext)
        {
            this.db = databaseContext;
        }

        public  bool getMatchCombo(int mId, int cId)
        {

            var Combos = (from cm in db.Client_Modules
                          select cm);

            foreach (Client_Module combo in Combos)
            {
                if ((combo.ModuleId == mId) && (combo.clientRef == cId))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckIFTutorAccountRegisteredForTheModule(int User_Table_ID, string userStatus, Client_Module client_Module)
        {
            var secondaryRoleID = 0;
            bool moduleClashes = false;
            ///
            switch (userStatus)
            {
                case "Tutor": //Get the client table ID
                    {

                        var secondaryRoleID_ = (from cb in db.Clients
                                                where cb.User_Table_Reference.Equals(User_Table_ID)
                                                select cb.Id).ToList();
                        try
                        {
                            if (secondaryRoleID_.ElementAt(0) > 0)
                            {
                                secondaryRoleID = secondaryRoleID_.ElementAt(0);
                            }
                            else
                            {
                                secondaryRoleID = -1;
                            }
                        }
                        catch (Exception possibleIndexException)
                        {
                        }
                    }
                    break;
                case "Client": //Get the tutor table ID
                    {
                        var secondaryRoleID_ = (from cb in db.Tutors
                                                where cb.User_Table_Reference.Equals(User_Table_ID)
                                                select cb.Id).ToList();
                        try
                        {
                            if (secondaryRoleID_.ElementAt(0) > 0)
                            {
                                secondaryRoleID = secondaryRoleID_.ElementAt(0);
                            }
                            else
                            {
                                secondaryRoleID = -1;
                            }
                        }
                        catch (Exception possibleIndexException)
                        {
                        }
                    }
                    break;
            }

            if (secondaryRoleID > 0)
            {
                //get The Tutor_Module briges that house all of the linq with the tutor and the registered Modules
                var tutorMods = (from cm in db.Tutor_Modules
                             where cm.Tutor_Reference.Equals(secondaryRoleID) && 
                                    cm.Is_Active == 1                                    
                             select cm).ToList();


                if (tutorMods != null)
                {
                    foreach (Tutor_Module tmods in tutorMods)
                    {
                        if (tmods.Module_Reference.Equals(client_Module.ModuleId))
                            moduleClashes = true;
                    } 
                }
                 
            }
           
            return moduleClashes;
        }
      

        internal bool CheckIFClientAccountRegisteredForTheModule(int userID, string userStatus, Tutor_Module tutor_Module)
        {
            var secondaryRoleID = 0;
            bool moduleClashes = false;
            ///
            switch (userStatus)
            {
                case "Tutor": //Get the client table ID
                    {
                        var secondaryRoleID_ = (from cb in db.Clients
                                                where cb.User_Table_Reference.Equals(userID)
                                                select cb.Id).ToList();
                        try
                        {
                            if (secondaryRoleID_.ElementAt(0) > 0)
                            {
                                secondaryRoleID = secondaryRoleID_.ElementAt(0);
                            }
                            else
                            {
                                secondaryRoleID = -1;
                            }
                        }
                        catch (Exception possibleIndexException)
                        {
                        }
                    }
                    break;
                case "Client": //Get the tutor table ID
                    {
                        var secondaryRoleID_ = (from cb in db.Tutors
                                                where cb.User_Table_Reference.Equals(userID)
                                                select cb.Id).ToList();
                        try
                        {
                            if (secondaryRoleID_.ElementAt(0) > 0)
                            {
                                secondaryRoleID = secondaryRoleID_.ElementAt(0);
                            }
                            else
                            {
                                secondaryRoleID = -1;
                            }
                        }
                        catch (Exception possibleIndexException)
                        {
                        }
                    }
                    break;
            }

            if (secondaryRoleID > 0)
            {
                //get The Tutor_Module briges that house all of the linq with the tutor and the registered Modules
                var tutorMods = (from cm in db.Client_Modules
                                 where cm.clientRef.Equals(secondaryRoleID) &&
                                        cm.Is_Active == 1
                                 select cm).ToList();


                if (tutorMods != null)
                {
                    foreach (Client_Module tmods in tutorMods)
                    {
                        if (tmods.ModuleId.Equals(tutor_Module.Module_Reference))
                            moduleClashes = true;
                    }
                }

            }


            return moduleClashes;
        }

        public int getNoOfBookings(int id)
        {
            int noOfBookings = (from p in db.BookingRequests
                                where p.ModuleID1.Equals(id) && p.Is_Accepted.Equals(0)
                                select p).Count();

            return noOfBookings;


        }

        public string getTutorNameById(int id)
        {
            User ret = null;
            var ent = (from p in db.Tutors
                       where p.Id.Equals(id)
                       select p).FirstOrDefault();

            if (ent != null)
            {
                var user = (from x in db.Users
                            where x.Id.Equals(ent.User_Table_Reference)
                            select x).First();

                if (user != null)
                {
                    ret = (User)user;
                    return ret.FullNames;
                }
            }

            return "";


        }


        internal List<Module> getModules()
        {
            List<Module> modules = (from i in db.Modules
                                    select i).ToList();

            return modules;
        }

        /// <summary>
        /// Set The Is_Active property of the Client module to 1, to make the module Active
        /// </summary>
        /// <param name="client_Module"></param>
        internal void makeModuleActive(Client_Module client_Module)
        {
            var tutorMods = (from cm in db.Client_Modules
                             where cm.Id.Equals(client_Module.Id)
                             select cm).ToList();

            if (tutorMods.Count > 0)
            {
                try
                {
                    tutorMods.ElementAt(0).Is_Active = 1;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //possibly an index exception
                }

            }
        }

        /// <summary>
        /// Set The Is_Active property of the Tutor module to 1, to make the module Active
        /// </summary>
        /// <param name="tutor_Module"></param>
        internal void makeModuleActive(Tutor_Module tutor_Module)
        {
            var tutorMods = (from cm in db.Tutor_Modules
                             where cm.Id.Equals(tutor_Module.Id)
                             select cm).ToList();

            if (tutorMods.Count > 0)
            {
                try
                {
                    tutorMods.ElementAt(0).Is_Active = 1;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //possibly an index exception
                }

            }
        }

        public List<Meeting> getAttended()
        {
            /*get the list off not rejected tutors
            var tutors = (from t in db.Tutors
                            where t.Is_Accepted  != -1
                            select t.Id).ToList();
                            */
            var meetings = (from m in db.Meetings
                            where m.Attended == 1 //&& tutors.Contains(m.Id) and there are no rejected tutor
                            select m).ToList();


            return meetings;
        }

        public bool notAccepted(int id)
        {
            var tutor = db.Tutors.Find(id);
            if (tutor != null)
            {

                if (tutor.Is_Accepted == 0)
                {
                    return true;
                }
                
            }
            return false;
        }

        public bool tDocExist(int tID, string dID)
        {
            var TutorDoc = (from doc in db.TutorDocuments
                            where (doc.DocumentType == dID) && (doc.TutorID == tID)
                            select doc).FirstOrDefault();

            if (TutorDoc != null)
                return true;

            return false;
        }

        public int getNoOfStudents(int id)
        {
            int noOfSubs = (from p in db.Client_Modules
                            where p.ModuleId.Equals(id)
                            select p).Count();

            return noOfSubs;


        }

        public Tutorial_Session getSession(int id)
        {
            var sessions = (from p in db.Tutorial_Sessions
                       where p.Id.Equals(id)
                       select p).First();
            if(sessions !=null)
            {
                Tutorial_Session sess = (Tutorial_Session)sessions;
                return sess;
            }

            return null; ;


        }


        public string getModuleName(int id)
        {
            var module = (from p in db.Modules
                          where p.Id.Equals(id)
                          select p);
            if (module != null)
            {
                Module retMod = new Module();
                retMod = (Module)module;
                return retMod.Module_Name;
            }


            return "";
        }

        public List<Invoice> getInvoices()
        {
            
            List<Invoice> invxs = (from p in db.Invoices
                                   where p.is_Paid.Equals(1)
                          select p).ToList();
            if (invxs != null)
            {
                return invxs;
            }


            return null;
        }


        public Module getModule(int id)
        {

            var mod = (from m in db.Modules
                       where m.Id.Equals(id)
                       select m).FirstOrDefault();
            if (mod != null)
            {
                Module retMod = (Module)mod;
                return retMod;
            }


            return null;
        }

        
        public BookingRequest getBookingReq(int id)
        {

            var book = (from m in db.BookingRequests
                       where m.Id.Equals(id)
                       select m).FirstOrDefault();
            if (book != null)
            {
                BookingRequest retBook = (BookingRequest)book;
                return retBook;
            }


            return null;
        }

        public ClientBooking getBooking(int id)
        {

            var book = (from m in db.ClientBookings
                        where m.Id.Equals(id)
                        select m).FirstOrDefault();
            if (book != null)
            {
                ClientBooking retBook = (ClientBooking)book;
                return retBook;
            }


            return null;
        }


        public bool checkIsInList(String name,List<ModuleReports> reports)
        {
            foreach(ModuleReports m in reports)
            {
                if (m.moduleName.Equals(name))
                    return true;
            }
            return false;
        }

        public int getIndexinlist(String name, List<ModuleReports> reports)
        {
            foreach (ModuleReports m in reports)
            {
                if (m.moduleName.Equals(name))
                    return reports.IndexOf(m);
            }

            return -1;
        }
    }
}
