using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.ComponentModel;

namespace JsonToSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToJson = @"C:\Users\lackeyja\Google Drive\Ski To Sea\2015\results.json";
            string directoryPath = Path.GetDirectoryName(pathToJson);
            dynamic results = JsonConvert.DeserializeObject(File.ReadAllText(pathToJson));
            //string dateString = "05/27/2018";
            //string dateString = "05/28/2017";
            //string dateString = "05/29/2016";
            string dateString = "05/30/2015";
            DateTime raceDate;
            DateTime.TryParse(dateString, out raceDate);
            int raceYear = raceDate.Year;
            Result result = new Result();
            foreach (dynamic item in results)
            {
                if (item.Name == "metadata")
                {
                    //"metadata":{"timestamp":"2018-05-29T16:11:17.960Z"}
                    foreach (dynamic metadata in item)
                    {
                        Dictionary<String, Object> res = Helper.Dyn2Dict(metadata);
                        if (res.ContainsKey("timestamp"))
                        {
                            DateTime timeStamp;
                            DateTime.TryParse(res["timestamp"].ToString(), out timeStamp);
                            result.TimeStamp = timeStamp;
                        }
                    }
                }
                if (item.Name == "teams")
                {
                    result.Teams = Team.AddTeams(item);
                }
                if (item.Name == "year")
                {
                    //"year":"2018",
                    foreach (dynamic item1 in item)
                    {
                        int year;
                        int.TryParse(item1.Value.ToString(), out year);
                        result.Year = year;
                    }
                }
                if (item.Name == "legs")
                {
                    result.Legs = Leg.AddLegs(item);
                }
                if (item.Name == "results")
                {
                    result.Times = Time.AddTimes(item, dateString);
                }
                if (item.Name == "divisions")
                {
                    result.Divisions = Division.AddDivisions(item);
                }
            }

            BuildSQL.WriteSQL(result, directoryPath);
        }

    }
}
