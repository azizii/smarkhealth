using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.ViewModel
{
    public class MessViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string messName { get; set; }
        [Display(Name = "password")]
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public IFormFile Photo { get; set; }
    }
}
