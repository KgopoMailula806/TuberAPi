using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models
{
    public class Rating
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Tutor_ID { get; set; }
        [Required]
        public int Client_ID { get; set; }
        public string Comment { get; set; }
        public double Client_Rating { get; set; }
        public double Tutor_Rating { get; set; }
        [Required]
        public int Session_Reference { get; set; }

    }
}
