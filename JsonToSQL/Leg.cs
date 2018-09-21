using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonToSQL
{
    class Leg
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }

        public static List<Leg> AddLegs(dynamic item)
        {
            List<Leg> legs = new List<Leg>();

            //"legs":[{"code":"xc","short_name":"XC Ski","long_name":"Cross Country Ski"},{"code":"dh","short_name":"DH Ski","long_name":"Downhill Ski"},
            foreach (dynamic item1 in item)
            {
                foreach (dynamic item2 in item1)
                {
                    Dictionary<String, Object> res = Helper.Dyn2Dict(item2);
                    Leg leg = new Leg();

                    if (res.ContainsKey("long_name"))
                    {
                        leg.Name = res["long_name"].ToString();
                    }
                    if (res.ContainsKey("short_name"))
                    {
                        leg.ShortName = res["short_name"].ToString();
                    }
                    if (res.ContainsKey("code"))
                    {
                        leg.Code = res["code"].ToString();
                    }

                    legs.Add(leg);
                }
            }
            return legs;
        }
    }
}
