using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuberAPI.models
{
    public class Client
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Current_Grade { get; set; }
        public string Institution { get; set; }
        //Foreign Key reference
        [Required]
        [ForeignKey("User")]
        public int User_Table_Reference { get; set; }
    }
}
