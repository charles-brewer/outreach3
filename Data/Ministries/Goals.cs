using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace outreach3.Data.Ministries
{
    public class Goals
    {
        private ApplicationDbContext _context;
        public Goals( ApplicationDbContext context) 
        {
            _context = context;
            
        }

        public List<double> Visited { get; set; } = new List<double>();

        public string GetVisited
        {
            get
            {
                var visited = "['0',";
                foreach (var i in Visited)
                {
                    visited += "'" + i.ToString() + "',";
                }
                visited = visited.Substring(0, visited.Length - 1) + "]";

                return visited;
            }
        }

        public List<double> Greeted { get; set; } = new List<double>();

        public string GetGreeted
        {
            get
            {
                var greeted = "['0',";
                foreach (var i in Greeted)
                {
                    greeted += "'" + i.ToString() + "',";
                }
                greeted = greeted.Substring(0, greeted.Length - 1) + "]";

                return greeted;
            }
        }


        public List<double> Goalline { get; set; } = new List<double>();

        public string GetGoalline
        {
            get
            {
                var goalline = "[";
                foreach (var i in Goalline)
                {
                    goalline += "'" + i.ToString() + "',";
                }
                goalline = goalline.Substring(0, goalline.Length - 1) + "]";
                return goalline;
            }
        }
        public List<double> GreetedGoal { get; set; } = new List<double>();

        public string GetGreetedGoal
        {
            get
            {
                var greetedGoal = "[";
                foreach (var i in GreetedGoal)
                {
                    greetedGoal += "'" + i.ToString() + "',";
                }
                greetedGoal = greetedGoal.Substring(0, greetedGoal.Length - 1) + "]";
                return greetedGoal;
            }
        }

        public List<DateTime> Labels { get; set; } = new List<DateTime>();

        public string GetLabels
        {
            get
            {
                var labels = "[";
                foreach (var label in Labels)
                {
                    labels += "'" + label.ToString("MMM dd yy") + "',";
                }
                labels = labels.Substring(0, labels.Length - 1) + "]";
                return labels;
            }
        }



        public void GetGoal(ChurchGoal? churchgoal )
        {
            /************Collect Data************/
            var stringStartDate = churchgoal.StartDate.ToString("yyyy/MM/dd").Split("/");
            var dateStart = new DateTime(Convert.ToInt32(stringStartDate[0]), Convert.ToInt32(stringStartDate[1]), Convert.ToInt32(stringStartDate[2]));

            var stringEndDate = churchgoal.GoalDate.ToString("yyyy/MM/dd").Split("/");
            var dateEnd = new DateTime(Convert.ToInt32(stringEndDate[0]), Convert.ToInt32(stringEndDate[1]), Convert.ToInt32(stringEndDate[2]));
            var dateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var lengthOfGoal = (dateEnd - dateStart).TotalDays;
            var numberOfDoors = churchgoal.NumberOfDoorsGoal;

            var days = (dateEnd - dateStart).TotalDays;
            var remainder = days % 7;

            var goalLine = new List<double>();
            var goalGreetedLine = new List<double>();
            var increment = Convert.ToDouble(numberOfDoors / ((lengthOfGoal / 7)));
            var churchGoalId = churchgoal.ChurchGoalId;
            var dateTemp = dateStart;

            /************Creating Labels and Goal Line************/

            while (dateTemp <= dateEnd)
            {_context.SaveChanges();

                goalLine.Add(Labels.Count() * increment);
                goalGreetedLine.Add(Labels.Count() * increment / 4);

                Labels.Add(dateTemp);
                dateTemp = dateTemp.AddDays(7);

                var missions = _context.Missions.Where(v => v.GoalId == churchGoalId);
                var missionIds = missions.Select(m => m.MissionId).ToList();


                if (dateTemp < DateTime.Now)
                {
                    var visitationCount = _context.Visitations.Where(v => missionIds.Contains(v.MissionId) && v.VisitationDate < dateTemp).Count();
                    if (visitationCount > 0)
                    {
                        Visited.Add(visitationCount);
                    }

                    var greetedCount = _context.Visitations.Where(v => missionIds.Contains(v.MissionId) && v.VisitationDate < dateTemp && v.VisitationType == TypeVisit.Greeted).Count();
                    if (visitationCount > 0)
                    {
                        Greeted.Add(greetedCount);
                    }

                }


            }

            if (remainder > 0)
            {
                Labels.Add(dateTemp.AddDays(remainder - 7));
                goalLine.Add(goalLine[goalLine.Count - 1] + (increment / remainder));
            }

            Goalline = goalLine;
            GreetedGoal = goalGreetedLine;
        }
    }
}
