using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarkHealthKidoPack.Models
{
    public class Messages
    {
        public int MessagesId { get; set; }

        public string Messagebody { get; set; }

        public DateTime messagedate { get; set; }

        public int GuardianId { get; set; }

        public Guardian  Guardian { get; set; }

        public int messids { get; set; }
    }
}
