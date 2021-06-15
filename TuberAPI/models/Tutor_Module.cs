using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models
{
    public class Tutor_Module
    {
        public int Id { get; set; }
        public string Date_Assigned { get; set; }
        public int Is_Active { get; set; }

        //Foreign Key reference
        [ForeignKey("Tutor")]
        public int Tutor_Reference { get; set; }
        [ForeignKey("Module")]
        public int Module_Reference { get; set; }

        //Inverse navigation property
        public Tutor Tutor { get; set; }

        public Module Module { get; set; }
    }
}
