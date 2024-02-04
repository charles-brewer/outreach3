using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Json;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using outreach3.Data;
using outreach3.Data.Ministries;


namespace outreach3.Pages.ChurchGoals
{
    public class DetailsModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;
        
       
        public string ChartJson { get; set; }
        public string ChartJson2 { get; set; }

        IWebHostEnvironment _environment;
     
        public DetailsModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }

      public ChurchGoal ChurchGoal { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int churchId)
        {

            /************Page Validation************/
            if (churchId == null || _context.ChurchGoals == null)
            {
                return NotFound();
            }
            var churchgoal = _context.ChurchGoals.FirstOrDefault(m => m.ChurchId == churchId && m.ChurchGoalId == Convert.ToInt32(Request.Query["churchGoalId"]));

            if (churchgoal == null)
            {
                return NotFound();
            }
            else
            {
                ChurchGoal = churchgoal;
            }


            var goal = new Goals(_context);
            goal.GetGoal(churchgoal);
            GetLabels = goal.GetLabels;

            GetLabels = goal.GetLabels;
            GetVisited = goal.GetVisited;
            GetGreetedGoal = goal.GetGreetedGoal;
            GetGreeted = goal.GetGreeted;
            GetGoalline = goal.GetGoalline;
            return Page();
        }

        public string GetLabels { get; set; }
        public string GetVisited { get; set; }
        public string GetGreetedGoal { get; set; }
        public string GetGreeted { get; set; }
        public string GetGoalline { get; set; }
      


    }
}
