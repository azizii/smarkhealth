using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class Register
    {
        public int RegisterId { get; set; }

        public string fingerprints { get; set; }
     
    }
}
