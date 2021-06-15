using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models
{
    public class Invoice
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Session_ID { get; set; }
        [Required]
        public int Client_ID { get; set; }
        [Required]
        public string Date_Issued { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Amount { get; set; }
        [Required]
        public int is_Paid { get; set; }
        [Required]
        public string Payment_Method { get; set; }
    }
}
