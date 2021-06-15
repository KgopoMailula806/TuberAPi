using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models.NonDatabaseModels
{
    public class TutorReport
    {
        public int tutorId { get; set; }
        public string name { get; set; }
        public int noOfsess { get; set; }
        public TutorReport(int tutorId, string tutorName, int noOfSessCompleted)
        {
            this.tutorId = tutorId;
            this.name = tutorName;
            this.noOfsess = noOfSessCompleted;
        }
    }
}
