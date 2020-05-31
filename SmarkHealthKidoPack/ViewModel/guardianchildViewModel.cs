using SmarkHealthKidoPack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.ViewModel
{
    public class guardianchildViewModel
    {
        public Guardian guardian { get; set; }
        public List<Child> children { get; set; }
    }
}
