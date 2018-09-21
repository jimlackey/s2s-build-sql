using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonToSQL
{
    class Division
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public static List<Division> AddDivisions(dynamic item)
        {
            //"divisions":{ "ww":"Whatcom County Women","cf":"Car-Free","rw":"Recreational Women","cm":"Competitive Mixed","hs":"High School","co":"Competitive Open","cp":"Corporate","ve":"Veterans","cw":"Competitive Women","ma":"Masters","wm":"Whatcom County Mixed","wo":"Whatcom County Open","fa":"Family","rm":"Recreational Mixed","ro":"Recreational Open"}
            List<Division> divisions = new List<Division>();
            foreach (dynamic item1 in item)
            {
                foreach (dynamic item2 in item1)
                {
                    Division division = new Division();
                    division.Code = item2.Name;
                    division.Name = item2.Value;
                    divisions.Add(division);
                }
            }
            return divisions;
        }
    }
}
