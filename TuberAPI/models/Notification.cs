using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace TuberAPI.models
{
    public class Notification
    {
        [Required]
        public int ID { get; set; }
        public int Seen { get; set; }
        public string DatePosted { get; set; }
        public string Time { get; set; }        
        //Foreign Key reference
        [Required]
        [ForeignKey("User")]
        public int User_Table_Reference { get; set; }

        [ForeignKey("Event")]
        public int Event_Table_Reference { get; set; }
        public string PersonTheNotificationConcerns { get; set; }
    }
}
