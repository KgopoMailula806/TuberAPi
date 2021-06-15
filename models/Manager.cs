using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models
{
    public class Manager
    {
        [Required]
        public int Id { get; set; }

        [ForeignKey("Document")]
        public int CVID { get; set; }
        // Foreign Key reference
        [Required]
        [ForeignKey("User")]
        public int User_Table_Reference { get; set; }
    }

}
