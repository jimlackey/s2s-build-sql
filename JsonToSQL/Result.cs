using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonToSQL
{
    class Result
    {
        public int Year { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<Team> Teams { get; set; }
        public List<Leg> Legs { get; set; }
        public List<Division> Divisions { get; set; }
        public List<Time> Times { get; set; }
    }
}
