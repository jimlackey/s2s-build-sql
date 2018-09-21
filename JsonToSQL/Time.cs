using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonToSQL
{
    class Time
    {
        //{"st":"07:30:04.017","du":null,      "ldq":true,           "tn":"164","lf":null,"rn":"355","dq":false,"er":true, "dr":"7", "ed":"15:48:33.021" //TEAM RESULTS INFO
        //{"st":"10:11:25.026","du":"02:11:58",           "pa":"276","tn":"164",          "rn":"213","dq":false,"er":false,"dr":"5", "ed":"12:23:23.036" //RD Bike	Kristen Reid
        //{"st":"09:08:43.011","du":"01:02:42",           "pa":"308","tn":"164",          "rn":"280","dq":false,"er":false,"dr":"6", "ed":"10:11:25.026" //Run	    Morgan Fox
        //{"st":"08:18:50.031","du":"00:49:52",           "pa":"310","tn":"164",          "rn":"341","dq":false,"er":false,"dr":"6", "ed":"09:08:43.011" //DH Ski	Abby McCormack
        //{"st":"14:32:25",    "du":"01:01:27",           "pa":null, "tn":"164",          "rn":"266","dq":false,"er":true, "dr":"7", "ed":"15:33:52.069" //CX Bike	Kirstin Anderson
        //{"st":"14:44:35",    "du":"01:03:58",           "pa":null, "tn":"164",          "rn":"118","dq":false,"er":true, "dr":"4", "ed":"15:48:33.021" //Kayak	Jen Knudsen
        //{"st":"07:30:04.017","du":"00:48:46",           "pa":"243","tn":"164",          "rn":"247","dq":false,"er":false,"dr":"6", "ed":"08:18:50.031" //XC Ski	Jeanie Pflueger
        //{"st":"12:23:23.036","du":null,                 "pa":null, "tn":"164",          "rn":null, "dq":true, "er":false,"dr":null,"ed":null           //Canoe	    Kayti Knudsen/Daana Denzel

        //TEAM ONLY:
        //ldq = Team Disqualified (true/false)
        //lf = Leg Finish (Always "ka" at the end of the race for teams who finished)

        //st = Start Time
        //du = Duration
        //pa = Place After
        //tn = Team Number
        //rn = Leg Place
        //dq = Disqualified (true/false)
        //er = Early Release (true/false)
        //dr = Division Place
        //ed = Finish Time

        //Team only
        public bool TeamDisqualified { get; set; }
        public string LastLegFinished { get; set; }
        public bool IsTeamTime { get; set; }

        //All
        public int TeamNumber { get; set; }
        public string Leg { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public TimeSpan Duration { get; set; }
        public int PlaceAfter { get; set; }
        public int Place { get; set; }
        public bool Disqualified { get; set; }
        public bool EarlyRelease { get; set; }
        public int DivisionPlace { get; set; }

        public static List<Time> AddTimes(dynamic item, string raceDateString)
        {
            List<Time> times = new List<Time>();

            //"results":{"rb":[{"st":"08:54:43.066","du":"01:25:31","pa":"1","tn":"1","rn":"1","dq":false,"er":false,"dr":"1","ed":"10:20:14.093"},
            foreach (dynamic item1 in item)
            {
                //{"rb":[{"st":"08:54:43.066","du":"01:25:31","pa":"1","tn":"1","rn":"1","dq":false,"er":false,"dr":"1","ed":"10:20:14.093"},
                foreach (dynamic item2 in item1)
                {
                    string leg = item2.Name;
                    //"rb":[{"st":"08:54:43.066","du":"01:25:31","pa":"1","tn":"1","rn":"1","dq":false,"er":false,"dr":"1","ed":"10:20:14.093"},
                    foreach (dynamic item3 in item2)
                    {
                        //[{"st":"08:54:43.066","du":"01:25:31","pa":"1","tn":"1","rn":"1","dq":false,"er":false,"dr":"1","ed":"10:20:14.093"},
                        foreach (dynamic item4 in item3)
                        {
                            //{"st":"07:30:04.017","du":null,      "ldq":true,           "tn":"164","lf":null,"rn":"355","dq":false,"er":true, "dr":"7", "ed":"15:48:33.021" //TEAM RESULTS INFO
                            //{"st":"10:11:25.026","du":"02:11:58",           "pa":"276","tn":"164",          "rn":"213","dq":false,"er":false,"dr":"5", "ed":"12:23:23.036" //RD Bike	Kristen Reid
                            //{"st":"09:08:43.011","du":"01:02:42",           "pa":"308","tn":"164",          "rn":"280","dq":false,"er":false,"dr":"6", "ed":"10:11:25.026" //Run	    Morgan Fox
                            //{"st":"08:18:50.031","du":"00:49:52",           "pa":"310","tn":"164",          "rn":"341","dq":false,"er":false,"dr":"6", "ed":"09:08:43.011" //DH Ski	Abby McCormack
                            //{"st":"14:32:25",    "du":"01:01:27",           "pa":null, "tn":"164",          "rn":"266","dq":false,"er":true, "dr":"7", "ed":"15:33:52.069" //CX Bike	Kirstin Anderson
                            //{"st":"14:44:35",    "du":"01:03:58",           "pa":null, "tn":"164",          "rn":"118","dq":false,"er":true, "dr":"4", "ed":"15:48:33.021" //Kayak	Jen Knudsen
                            //{"st":"07:30:04.017","du":"00:48:46",           "pa":"243","tn":"164",          "rn":"247","dq":false,"er":false,"dr":"6", "ed":"08:18:50.031" //XC Ski	Jeanie Pflueger
                            //{"st":"12:23:23.036","du":null,                 "pa":null, "tn":"164",          "rn":null, "dq":true, "er":false,"dr":null,"ed":null           //Canoe	    Kayti Knudsen/Daana Denzel

                            //TEAM ONLY:
                            //ldq = Team Disqualified (true/false)
                            //lf = Leg Finish (Always "ka" at the end of the race for teams who finished)

                            //st = Start Time
                            //du = Duration
                            //pa = Place After
                            //tn = Team Number
                            //rn = Leg Place
                            //dq = Disqualified (true/false)
                            //er = Early Release (true/false)
                            //dr = Division Place
                            //ed = Finish Time
                            Dictionary<String, Object> res = Helper.Dyn2Dict(item4);

                            Time time = new Time();
                            time.Leg = leg;
                            time.IsTeamTime = false;

                            if (res.ContainsKey("ldq"))
                            {
                                //This is a Team record
                                bool teamDisqualified;
                                bool.TryParse(res["ldq"].ToString(), out teamDisqualified);
                                time.TeamDisqualified = teamDisqualified;
                                time.IsTeamTime = true;
                            }
                            if (res.ContainsKey("lf"))
                            {
                                time.LastLegFinished = res["lf"].ToString();
                                time.IsTeamTime = true;
                            }

                            if (res.ContainsKey("st"))
                            {
                                DateTime startTime;
                                DateTime.TryParse($"{raceDateString} {res["st"].ToString()}", out startTime);
                                time.StartTime = startTime;
                            }
                            if (res.ContainsKey("ed"))
                            {
                                DateTime finishTime;
                                DateTime.TryParse($"{raceDateString} {res["ed"].ToString()}", out finishTime);
                                time.FinishTime = finishTime;
                            }
                            if (res.ContainsKey("du"))
                            {
                                TimeSpan duration;
                                TimeSpan.TryParse(res["du"].ToString(), out duration);
                                time.Duration = duration;
                            }
                            if (res.ContainsKey("pa"))
                            {
                                int placeAfter;
                                int.TryParse(res["pa"].ToString(), out placeAfter);
                                time.PlaceAfter = placeAfter;
                            }
                            if (res.ContainsKey("tn"))
                            {
                                int teamNumber = 0;
                                int.TryParse(res["tn"].ToString(), out teamNumber);
                                time.TeamNumber = teamNumber;
                            }
                            if (res.ContainsKey("rn"))
                            {
                                int place;
                                int.TryParse(res["rn"].ToString(), out place);
                                time.Place = place;
                            }
                            if (res.ContainsKey("dq"))
                            {
                                bool disqualified;
                                bool.TryParse(res["dq"].ToString(), out disqualified);
                                time.Disqualified = disqualified;
                            }
                            if (res.ContainsKey("er"))
                            {
                                bool earlyRelease;
                                bool.TryParse(res["er"].ToString(), out earlyRelease);
                                time.EarlyRelease = earlyRelease;
                            }

                            times.Add(time);
                        }
                    }
                }
            }
            return times;
        }
    }
}
