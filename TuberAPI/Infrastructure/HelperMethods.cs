using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuberAPI.models;
using TuberAPI.Data;

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
    }
}
