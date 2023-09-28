using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using outreach3.Data;
using outreach3.Data.Ministries;
using outreach3.wwwroot.lib.ChartModels;

namespace outreach3.Pages.ChurchGoals
{
    public class DetailsModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;
        
        public ChartJs Chart { get; set; }
        public string ChartJson { get; set; }

        public ChartJs Chart2 { get; set; }
        public string ChartJson2 { get; set; }

        IWebHostEnvironment _environment;

        public DetailsModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }

      public ChurchGoal ChurchGoal { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? churchId)
        {
            if (churchId == null || _context.ChurchGoals == null)
            {
                return NotFound();
            }

            var churchgoal = await _context.ChurchGoals.FirstOrDefaultAsync(m => m.ChurchId == churchId);
            if (churchgoal == null)
            {
                return NotFound();
            }
            else 
            {
                ChurchGoal = churchgoal;
            }

            var stringStartDate = churchgoal.StartDate.ToString("yyyy/MM/dd").Split("/");
            var dateStart = new DateOnly(Convert.ToInt32(stringStartDate[0]), Convert.ToInt32(stringStartDate[1]), Convert.ToInt32(stringStartDate[2]));

            var stringEndDate = churchgoal.GoalDate.ToString("yyyy/MM/dd").Split("/");
            var dateEnd = new DateOnly(Convert.ToInt32(stringEndDate[0]), Convert.ToInt32(stringEndDate[1]), Convert.ToInt32(stringEndDate[2]));
            var dateNow = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var allDates = Enumerable.Range(0, int.MaxValue)
                         .Select(x => dateStart.AddDays(x))
                         .TakeWhile(x => x <= dateEnd);
            var numberOfDaysCount = allDates.Count();
           
            var numberOfDaysExpired = Enumerable.Range(0, int.MaxValue)
                         .Select(x => dateStart.AddDays(x))
                         .TakeWhile(x => x <= dateNow);
            var numberOfDaysLeftCount = numberOfDaysCount - numberOfDaysExpired.Count();
            
          
            
            var visitationCount = _context.Visitations.Count(v => v.ChurchId == churchId);
            var vistationGreeted = _context.Visitations.Count(v => v.ChurchId == churchId && v.VisitationType==TypeVisit.Greeted);

            int[] theData =new int[]{churchgoal.NumberOfDoorsGoal, visitationCount, churchgoal.NumberOfGreetsGoal, vistationGreeted, numberOfDaysCount, numberOfDaysLeftCount};

            //int[] theData1 = new int[] { churchgoal.NumberOfDoorsGoal, visitationCount };
            //int[] theData2 = new int[] { churchgoal.NumberOfGreetsGoal, vistationGreeted};
            //int[] theData3 = new int[] { numberOfDaysCount, numberOfDaysLeftCount };


            int[] theData1 = new int[] { churchgoal.NumberOfDoorsGoal, churchgoal.NumberOfGreetsGoal, numberOfDaysCount };
            int[] theData2 = new int[] { visitationCount , vistationGreeted, numberOfDaysExpired.Count() };
          

            var chartData = @"
        {
            type:'bar',
           
            data:
            {
                  labels: ['Knock Goal', 'Greeting Goal', 'Goal Length (in Days)'],
                datasets: [{
                    label: 'Goals',
                     data: " + JsonConvert.SerializeObject(theData1) + @",
                    backgroundColor: [
                     'steelblue',                    
                    '#6CA881',
                    'brown',
                        ],
                    borderColor: [
                    'navy',
                    'navy',
                    'navy',                    
                        ],
                    borderWidth: 1
                },
{
                    label: 'Actual Completed',
                     data: " + JsonConvert.SerializeObject(theData2) + @",
                    backgroundColor: [
                    
                    'lightsteelblue',
                    '#39744E',
                    'tan'
                   
                        ],
                    borderColor: [
                   
                    'gold',
                    'gold',
                    'gold'
                    
                        ],
                    borderWidth: 1
                }]
            },
            options:
            {               
                scales:
                {
                    yAxes: [{
                        stacked: false,
                        ticks:
                        {
                            
                            beginAtZero: true,
                            display: true,
                        }
                    }],
                    scaleLabel: {
                    display: true,
                },
                },
            responsive: true,
            maintainAspectRatio: false,
            }
        }";

        
            
            
            
            
            
            
            
            
            
            
            
            var chartData2 = @"
        {
            type:'doughnut',
           
            data:
            {
                  labels: ['Knock Goal', 'Knocks', 'Greeting Goal', 'Greets', 'Goal Length', 'Days Expired'],
                datasets: [{
                    label: 'Visitations',
                     data: " + JsonConvert.SerializeObject(theData)+ @",
                    backgroundColor: [
                     'steelblue',
                    'lightsteelblue',
                    '#6CA881',
                    '#39744E',
                    'brown',
                    'tan',
                        ],
                    borderColor: [
                    'navy',
                    'navy',
                    'gold',
                    'gold',
                    '#dddddd',
                    '#dddddd'
                        ],
                    borderWidth: 1
                }]
            },
            options:
            {
                responsive: true,

                maintainAspectRatio: false,
                scales:
                {
                    yAxes: [{
                        ticks:
                        {
                            beginAtZero: true,
                            display: true,
                        }
                    }]
                }
            }
        }";

            Chart = JsonConvert.DeserializeObject<ChartJs>(chartData);
            ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            Chart2 = JsonConvert.DeserializeObject<ChartJs>(chartData2);
            ChartJson2 = JsonConvert.SerializeObject(Chart2, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            return Page();
        }
    }
}
