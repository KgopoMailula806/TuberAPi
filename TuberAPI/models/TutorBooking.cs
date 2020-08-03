using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models
{
    public class TutorBooking
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Date_Time_For_The_Booking { get; set; }

        public string Is_Active { get; set; }

        [Required]
        public int Client_Reference { get; set; }
        [Required]
        public int Tutor_Table_Reference { get; set; }

    }
}
