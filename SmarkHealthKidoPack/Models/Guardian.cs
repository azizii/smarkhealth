using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class Guardian
    {
        public int GuardianId { get; set; }

        public string GuardianName { get; set; }

        public string phonenumber { get; set; }

        public string adress { get; set; }

        public string passward { get; set; }

        public int Balance { get; set; }

        public int messId { get; set; }

        public Mess Mess { get; set; }

        public ICollection<Child> child { get; set; }

        public ICollection<Messages>  Messages { get; set; }



    }
}
