using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models
{
    public class Module
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Module_Name { get; set; }
        [Required]
        [StringLength(6)]
        public string Module_Code { get; set; }      
        public string Module_Type { get; set; }

        //navigation properties
        public IEnumerable<Tutor_Module> Tutor_Modules { get; set; }
        public IEnumerable<Client_Module> Client_Modules { get; set; }

    }
}
