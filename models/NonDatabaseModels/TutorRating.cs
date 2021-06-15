using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuadCore_Website.models
{
    public class TutorRating
    {
        public string TutorName { get; set; }
        public double AveRating { get; set; }

        public string TutorSurname { get; set; }
        public TutorRating(string name, string surname, double rating)
        {
            this.TutorName = name;
            this.TutorSurname = surname;
            this.AveRating = rating;
        }


    }
}