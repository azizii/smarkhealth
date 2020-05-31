using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.ViewModel
{
    public class AdminViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string AdminName { get; set; }
        [Display(Name = "password")]
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public IFormFile Photo { get; set; }
    }
}
