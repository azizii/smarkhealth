using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.ViewModel
{
    public class AdminViewModel
    {
        public string AdminName { get; set; }

        public string password { get; set; }
        public IFormFile Photo { get; set; }
    }
}
