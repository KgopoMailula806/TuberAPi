using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuberAPI.models
{
    public class TutorDocument
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string DocumentType { get; set; }

        [Required]
        [ForeignKey("Document")]
        public int DocID { get; set; }

        [Required]
        [ForeignKey("Tutor")]
        public int TutorID { get; set; }
    }
}
