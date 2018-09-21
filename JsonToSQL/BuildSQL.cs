using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JsonToSQL
{
    class BuildSQL
    {
        public static void WriteSQL(Result result, string directoryPath)
        {
            string createTablesSQL = File.ReadAllText("CreateTables.sql");
            string divisionSQL = BuildDivisionsSQL(result.Divisions, result.Year);
            string teamsSQL = BuildTeamsSQL(result.Teams, result.Year);
            string timesSQL = BuildTimesSQL(result.Times, result.Year);

            File.WriteAllText(Path.Combine(directoryPath, $"Build{result.Year.ToString()}Data.sql"), createTablesSQL + Environment.NewLine + "GO" + Environment.NewLine + divisionSQL + Environment.NewLine + teamsSQL + Environment.NewLine + timesSQL);
        }

        static string BuildTeamsSQL(List<Team> teams, int year)
        {
            string teamInsert = string.Empty;
            string racerInsert = string.Empty;

            foreach (Team team in teams)
            {
                int teamNumber = team.Number;
                teamInsert += $"INSERT INTO [dbo].[Team] ( [Year], [Number], [Name], [DivisionCode] ) VALUES ( {year.ToString()}, {team.Number}, '{team.Name.Replace("'", "''")}', '{team.Division}' ){Environment.NewLine}";
                foreach (RacerLeg leg in team.Legs)
                {
                    string legName = leg.Name;
                    foreach (Racer racer in leg.Racers)
                    {
                        racerInsert += $"INSERT INTO [dbo].[Racer] ( [Year], [TeamNumber], [Name], [Leg], [Sex] ) VALUES ( {year.ToString()}, {teamNumber}, '{racer.Name.Replace("'", "''")}', '{leg.Name}', '{racer.Sex}' ){Environment.NewLine}";
                    }
                }
                //sql += insert + Environment.NewLine;
            }

            return teamInsert + Environment.NewLine + racerInsert;
        }

        static string BuildDivisionsSQL(List<Division> divisions, int year)
        {
            string divisionInsert = string.Empty;

            foreach (Division division in divisions)
            {
                divisionInsert += $"INSERT INTO [dbo].[Division] ( [Year], [Code], [Name] ) VALUES ( {year.ToString()}, '{division.Code}', '{division.Name}' ){Environment.NewLine}";
            }

            return divisionInsert;
        }

        static string BuildTimesSQL(List<Time> times, int year)
        {
            string resultsInsert = string.Empty;

            foreach (Time time in times)
            {
                string teamDq = BoolToSqlBit(time.TeamDisqualified);
                string isTeamTime = BoolToSqlBit(time.IsTeamTime);
                string racerDq = BoolToSqlBit(time.Disqualified);
                string earlyRelease = BoolToSqlBit(time.EarlyRelease);
                string startTime = time.StartTime.ToString("yyyy-MM-dd HH:mm:ss"); //DATETIME - format: YYYY-MM-DD HH:MI:SS
                string finishTime = time.FinishTime.ToString("yyyy-MM-dd HH:mm:ss"); //DATETIME - format: YYYY-MM-DD HH:MI:SS
                string duration = time.Duration.ToString("c"); //HH:MI:SS

                resultsInsert += $"INSERT INTO [dbo].[Time] ( [Year], [TeamDisqualified], [LastLegFinished], [IsTeamTime], [TeamNumber], [Leg], [StartTime], [FinishTime], [Duration], [PlaceAfter], [Place], [Disqualified], [EarlyRelease], [DivisionPlace] ) " +
                    $"VALUES ( {year.ToString()}, {teamDq}, '{time.LastLegFinished}', {isTeamTime}, {time.TeamNumber}, '{time.Leg}', '{startTime}', '{finishTime}', '{duration}', {time.PlaceAfter}, {time.Place}, {racerDq}, {earlyRelease}, {time.DivisionPlace} ){Environment.NewLine}";
            }

            return resultsInsert;
        }

        static string BoolToSqlBit(bool value)
        {
            return (value == true) ? "1" : "0";
        }
    }
}
