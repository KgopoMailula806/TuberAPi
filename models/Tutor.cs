using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models
{
    public class Tutor
    {
        [Required]
        public int Id { get; set; }

        public int Is_Accepted { get; set; }

        //Foreign Key reference
        [Required]
        [ForeignKey("User")]
        public int User_Table_Reference { get; set; }

        //navigation property
        public IEnumerable<Tutor_Module> Tutor_Modules { get; set; }

    }
}
