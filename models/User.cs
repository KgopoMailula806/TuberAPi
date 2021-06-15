using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuberAPI.models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(60)]
        public string FullNames { get; set; }
       
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
       
        [Required]
        [StringLength(10)]
        public string Valid_Phone_Number { get; set; }        
        
        [Required]
        public string Email_Address { get; set; }
       
        [Required]       
        public string PassWord { get; set; }
        [Required]
        public string Gender { get; set; }

        [ForeignKey("Client")]
        public int Image { get; set; }

        [Required]        
        public int Age { get; set; }
        
        [Required]
        [StringLength(15)]
        public string User_Discriminator { get; set; }

        public int isActive { get; set; }
        
    }
}
