using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models
{
    public class BookingRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string  RequestDate { get; set; }
        [Required]
        public string RequestTime { get; set; }
        public int Periods { get; set; }
        public string EndTime { get; set; }

        [Required]
        public int Is_Accepted { get; set; } //either 1/0 
        [Required]        
        public int IsRespondedTo { get; set; }
        public string ClientProposedLocation { get; set; }
        public string tutorProposedLocation { get; set; }
        [Required]
        public int Tutor_Reference { get; set; }
        [Required]
        public int Client_Reference { get; set; }
    }
}
