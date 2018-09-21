using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonToSQL
{
    class Team
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Division { get; set; }
        public List<RacerLeg> Legs { get; set; }
        public Time Time { get; set; }

        public static List<Team> AddTeams(dynamic item)
        {
            List<Team> teams = new List<Team>();

            //"teams": { "350": { "rs": { "rb": [ { "g": "m", "n": "Leighton  Overson" } ], "ru": [ { "g": "m", "n": "Casey Barten" } ],
            foreach (dynamic tm in item)
            {
                //{ "350": { "rs": { "rb": [ { "g": "m", "n": "Leighton  Overson" } ], "ru": [ { "g": "m", "n": "Casey Barten" } ],
                foreach (dynamic team1 in tm)
                {
                    //"350": { "rs": { "rb": [ { "g": "m", "n": "Leighton  Overson" } ], "ru": [ { "g": "m", "n": "Casey Barten" } ],
                    Team team = new Team();
                    int number;
                    int.TryParse(team1.Name, out number);
                    team.Number = number;
                    foreach (dynamic rs in team1)
                    {
                        //{ "rs": { "rb": [ { "g": "m", "n": "Leighton  Overson" } ], "ru": [ { "g": "m", "n": "Casey Barten" } ],
                        foreach (dynamic leg1 in rs)
                        {
                            //"rs": { "rb": [ { "g": "m", "n": "Leighton  Overson" } ], "ru": [ { "g": "m", "n": "Casey Barten" } ],
                            //"tn":"Epic Kayaks",
                            //"dc":"co"}
                            if (leg1.Name == "rs")
                            {
                                List<RacerLeg> legs = new List<RacerLeg>();
                                //"rs": { "rb": [ { "g": "m", "n": "Leighton  Overson " } ], "ru": [ { "g": "m", "n": "Casey Barten" } ],
                                foreach (dynamic leg2 in leg1)
                                {
                                    //{ "rb": [ { "g": "m", "n": "Leighton  Overson" } ], "ru": [ { "g": "m", "n": "Casey Barten" } ],
                                    foreach (dynamic leg3 in leg2)
                                    {
                                        RacerLeg leg = new RacerLeg();
                                        leg.Name = leg3.Name;
                                        //"rb": [ { "g": "m", "n": "Leighton  Overson" } ], 
                                        //"ru": [ { "g": "m", "n": "Casey Barten" } ],
                                        foreach (dynamic leg4 in leg3)
                                        {
                                            List<Racer> racers = new List<Racer>();
                                            //[ { "g": "m", "n": "Leighton  Overson" } ], 
                                            //{"ca": [ { "g": "m", "n": "Glenn Bond" }, { "g": "m", "n": "Bob Woodman" } ]}
                                            foreach (dynamic leg5 in leg4)
                                            {
                                                Racer racer = new Racer();
                                                //"g": "m", "n": "Leighton  Overson"
                                                //"g": "m", "n": "Casey Barten"
                                                foreach (dynamic leg6 in leg5)
                                                {
                                                    //"g": "m"
                                                    //"n": "Leighton  Overson"
                                                    if (leg6.Name == "g")
                                                        racer.Sex = leg6.Value;
                                                    if (leg6.Name == "n")
                                                        racer.Name = leg6.Value;
                                                }
                                                racers.Add(racer);
                                            }
                                            leg.Racers = racers;
                                        }
                                        legs.Add(leg);
                                    }
                                }
                                team.Legs = legs;
                            }
                            if (leg1.Name == "tn")
                            {
                                team.Name = leg1.Value;
                            }
                            if (leg1.Name == "dc")
                            {
                                team.Division = leg1.Value;
                            }
                        }
                    }
                    teams.Add(team);
                }
            }
            return teams;
        }
    }
}
