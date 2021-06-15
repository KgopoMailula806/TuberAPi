using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuberAPI.models
{
    public class Document
    {
        [Required]
        public int Id { get; set; }

        public int Size { get; set; }
        public string Extension { get; set; }
        public string DocumentData { get; set; }
    }
}
