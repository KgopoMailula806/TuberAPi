using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models.NonDatabaseModels
{
    public class ModuleReports
    {
        public string moduleName { get; set; }
        
        public double doubleValue { get; set; }
      

       


        public ModuleReports(string module_Name, double val)
        {
            this.moduleName = module_Name;
            this.doubleValue = val;
        }


        //Added module ID just to get rid of the overshaddowing 
        //public ModuleReports(string name,int moduleID, int noOfBookings)
        // {
        //    this.moduleName = name;
        // this.noOfBookings = noOfBookings;
        //}

    }
}
