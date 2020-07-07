using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.ViewModel
{
    public class GuardianBalanaceViewModel
    {
        public int GuardianId { get; set; }
      
        public string Guardianname { get; set; }

        public string phonenumber { get; set; }

        public string adress { get; set; }

        public int OldBalance { get; set; }
        public int newBalance { get; set; }

        public string passward { get; set; }

        public int messId { get; set; }

    }
}
