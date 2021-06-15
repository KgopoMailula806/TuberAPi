using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace TuberAPI.models
{
    public class Event
    {
        [Required]
        public int ID { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }


    }
}
