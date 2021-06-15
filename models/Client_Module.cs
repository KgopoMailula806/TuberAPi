using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models
{
    public class Client_Module
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string DateAssigned { get; set; }
        public int Is_Active { get; set; }

        //Foreign Keys references relationship is one-one        
        public Module Module_Reference { get; set; }

        public Client Client_Reference { get; set; }
        [ForeignKey("Module")]
        public int ModuleId { get; set; }
        [ForeignKey("Client")]
        public int clientRef { get; set; }
    }
}
