using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using outreach3.Data;
using outreach3.Data.Ministries;

namespace outreach3.Pages.ChurchGoals
{
    public class EditModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public EditModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ChurchGoal ChurchGoal { get; set; } = default!;

        public string DisplayError
        {
            get 
            {
                if(Error == "")
                {
                    return "none";
                }
                return "inline-block";
            }
        }
        public string Error { get; set; } = "";

    
      
        public string ErrorVisibility { get; set; } = "hidden";

        public List<SelectListItem> AllMissions { get; set; }
        public List<SelectListItem> GoalMissions { get; set; }

        public async Task<IActionResult> OnGetAsync(int? churchId, int? churchGoalId)
        {
            if (churchId == null || _context.ChurchGoals == null)
            {
                return NotFound();
            }

            var churchgoal =  await _context.ChurchGoals.FirstOrDefaultAsync(m => m.ChurchId == churchId && m.ChurchGoalId==churchGoalId);
            if (churchgoal == null)
            {
                return NotFound();
            }
            
            //churchgoal.ChurchId = Convert.ToInt32(Request.Query["churchId"]);
            ChurchGoal = churchgoal;

            AllMissions = _context.Missions.Where(m => m.ChurchId == churchId).Select(a =>
                                 new SelectListItem
                                 {
                                     Value = a.MissionId.ToString(),
                                     Text = a.Name
                                 }).ToList();


            GoalMissions = _context.Missions.Where(m => m.ChurchId == churchId && m.GoalId==churchgoal.ChurchGoalId).Select(a =>
                                 new SelectListItem
                                 {
                                     Value = a.MissionId.ToString(),
                                     Text = a.Name
                                 }).ToList();
            return Page();
        }




        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ChurchGoal).State = EntityState.Modified;

            try
            {
                ChurchGoal.ChurchId = Convert.ToInt32(Request.Query["churchId"]);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChurchGoalExists(ChurchGoal.ChurchGoalId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { churchId = Request.Query["churchId"] });
        }

        public async Task<IActionResult> OnPostAddAsync(int churchGoalId)
        {
            var missionId = Convert.ToInt32(Request.Form["allMissions"]);
            if(missionId==0)
            {
                Error = "Please select a mission for this goal.";
                return null;
            }
            var missionGoalId = _context.Missions.FirstOrDefaultAsync(m => m.MissionId == missionId).Result.GoalId;

            if (missionGoalId != 0)
            {
                Error = "This Mission already belongs to a goal.";
                return null;
            }

            _context.Missions.FirstOrDefault(m => m.MissionId == missionId).GoalId = ChurchGoal.ChurchGoalId;
            ChurchGoal = _context.ChurchGoals.Where(m => m.ChurchGoalId == churchGoalId).FirstOrDefault();
            ChurchGoal.NumberOfDoorsGoal = ChurchGoal.NumberOfDoorsGoal + _context.Residents.Where(m => m.MissionId == missionId).Count();
            ChurchGoal.NumberOfGreetsGoal = ChurchGoal.NumberOfDoorsGoal / 4;
            await _context.SaveChangesAsync();    
            return RedirectToPage("Edit", new { churchId = Request.Query["churchId"], churchGoalId = Request.Query["churchGoalId"] });
        }
        public async Task<IActionResult> OnPostDeleteAsync(int churchGoalId)
        {
            var missionId = Convert.ToInt32(Request.Form["allMissions"]);

            if(missionId == 0)
            {
                Error = "Please select a mission from the Missions List (Top List).";
                return null;

            }

            if (missionId != 0)
            {
                var missionGoalId = _context.Missions.FirstOrDefaultAsync(m => m.MissionId == missionId).Result.GoalId;

                if (missionGoalId != churchGoalId)
                {
                    Error = "This Mission cannot be removed, it is not part of this goal.";
                    return null;
                }


                _context.Missions.FirstOrDefaultAsync(m => m.MissionId == missionId).Result.GoalId = 0;
                ChurchGoal = _context.ChurchGoals.Where(m => m.ChurchGoalId == churchGoalId).FirstOrDefault();
                ChurchGoal.NumberOfDoorsGoal = ChurchGoal.NumberOfDoorsGoal - _context.Residents.Where(m => m.MissionId == missionId).Count();
                ChurchGoal.NumberOfGreetsGoal = ChurchGoal.NumberOfDoorsGoal / 4;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("Edit", new { churchId = Request.Query["churchId"], churchGoalId = Request.Query["churchGoalId"] });
        }
        public async Task<IActionResult> OnPostResetAsync(int churchGoalId)
        {
            
            foreach(var mission in _context.Missions.Where(m=>m.GoalId==churchGoalId))
            {
                mission.GoalId = 0;
            }

            ChurchGoal = _context.ChurchGoals.Where(m => m.ChurchGoalId == churchGoalId).FirstOrDefault();
            ChurchGoal.NumberOfDoorsGoal = 0;
            ChurchGoal.NumberOfGreetsGoal = 0;
            await _context.SaveChangesAsync();
            return RedirectToPage("Edit", new { churchId = Request.Query["churchId"], churchGoalId = Request.Query["churchGoalId"] });
        }

        public async Task<IActionResult> OnPostOKAsync(int churchGoalId, int churchId)
        {           
            return RedirectToPage("Edit", new { churchId, churchGoalId });
        }

        private bool ChurchGoalExists(int id)
        {
          return (_context.ChurchGoals?.Any(e => e.ChurchGoalId == id)).GetValueOrDefault();
        }
    }
}
