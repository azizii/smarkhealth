using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string AdminName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Passward { get; set; }

       
    }
}

