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
        public TutorReports(int tutorId, string tutorName, int noOfSessCompleted)
        {
            this.tutorId = tutorId;
            this.name = tutorName;
            this.noOfsess = noOfSessCompleted;
        }
    }
}