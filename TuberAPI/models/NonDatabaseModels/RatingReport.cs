using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models.NonDatabaseModels
{
    public class RatingReport
    {
        public string TutorName { get; set; }
        public double ranking { get; set; }

        public RatingReport(string name, double ranking)
        {
            this.TutorName = name;
            this.ranking = ranking;
        }

    }
}
