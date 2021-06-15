using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuadCore_Website.models.NonDatabaseModels
{
    public class TutorReports
    {
        public int tutorId { get; set; }
        public string name { get; set; }
        public int noOfsess { get; set; }

        public TutorReports(int tutorId, string name , int noOfsessCom)
        {
            this.tutorId = tutorId;
            this.name = name;
            this.noOfsess = noOfsess;
        }
    }
}