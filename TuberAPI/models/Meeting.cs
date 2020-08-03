using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuberAPI.models
{
    public class Meeting
    {
        //Entity ID
        [Required]
        public int Id { get; set; }

        //Requred Fields
        public string Date { get; set; }
        public string Time { get; set; }
        public string Venue { get; set; }
        public string Type { get; set; }
        public string Minutes { get; set; }


        public int Attended { get; set; }

        //Foreign Key reference
        [Required]
        [ForeignKey("Tutor")]
        public int TutorID { get; set; }
    }
}
