using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models
{
    public class ManagersLog
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Date_time_Of_Action { get; set; }
        [Required]
        public string Action_Discriminator { get; set; }
        [Required]
        public string Action_Description { get; set; }

        //foreign key reference
        [Required]
        [ForeignKey("Manager")]
        public int Manager_Reference { get; set; }
    }
}
