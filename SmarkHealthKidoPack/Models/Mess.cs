using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class Mess
    {
        public int MessId { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string MessName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string photopath { get; set; }

        public ICollection<Guardian> guardians { get; set; }

      


    }
}
