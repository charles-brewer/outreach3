using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using outreach3.Data;
using outreach3.Data.Ministries;
using outreach3.Pages.Missions;

namespace outreach3.Pages.ChurchGoals
{
    public class IndexModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _manager;
        public IndexModel(outreach3.Data.ApplicationDbContext context, UserManager<IdentityUser> manager, IEmailSender emailSender, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _manager = manager;
            _emailSender = emailSender;
            _webHostEnvironment= webHostEnvironment;
        }

        public IList<ChurchGoal> ChurchGoals { get; set; } = default!;
        public string EmailNotificationAdd { get; private set; }
        public string EmailNotificationRemove { get; private set; }

        public async Task OnGetAsync()
        {
            if (_context.ChurchGoals != null)
            {
                var churchId = Convert.ToInt32(Request.Query["churchId"]);
                var churchGoalIds = await _context.ChurchGoals.Select(c => c.ChurchGoalId).ToListAsync();


                ChurchGoals = await _context.ChurchGoals.Where(g => g.ChurchId == churchId).ToListAsync();

                var visitationCount = 0;
                var visitationGreets = 0;
                foreach (var churchGoal in ChurchGoals)
                {
                    var missionIds = await _context.Missions.Where(v => v.GoalId == churchGoal.ChurchGoalId).Select(m => m.MissionId).ToListAsync();

                    foreach (var missionId in missionIds)
                    {
                        visitationCount += _context.Visitations.Where(v => v.MissionId == missionId).Count();
                        visitationGreets += _context.Visitations.Where(v => v.VisitationType == TypeVisit.Greeted && v.MissionId == missionId).Count();

                    }
                    churchGoal.NumberOfDoors = visitationCount;
                    await _context.SaveChangesAsync();

                    await SetEmailNotificationLinks(churchId);

                }
            }
        }

        private async Task SetEmailNotificationLinks(int churchId)
        {
            var usr = await _manager.GetUserAsync(HttpContext.User);
            var email = usr?.Email;
            var memberId = _context.Members.FirstOrDefault(m => m.Email == email)?.MemberId;

            if (_context.ChurchMembers.FirstOrDefault(c => c.MemberId == memberId && c.ChurchId == churchId) == null)
            {
                EmailNotificationAdd = "in-line-block";
                EmailNotificationRemove = "none";
            }
            else
            {
                EmailNotificationAdd = "none";
                EmailNotificationRemove = "in-line-block";
            }
        }

        public async Task<IActionResult> OnPostAddEmailAsync(int churchId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var usr = await _manager.GetUserAsync(HttpContext.User);
            var email = usr?.Email;
            var memberId = _context.Members.FirstOrDefault(m => m.Email == email).MemberId;

            if (_context.ChurchMembers.FirstOrDefault(c => c.MemberId == memberId && c.ChurchId == churchId) == null)
            {
                var churchMember = new ChurchMembers { ChurchId = churchId , MemberId = memberId};
                _context.ChurchMembers.Add(churchMember);
                await _context.SaveChangesAsync();
            }         

            return  RedirectToPage("./Index", new { churchId = Request.Query["churchId"] });
        }


        public async Task<IActionResult> OnPostRemoveEmailAsync(int churchId)
        {
           
            var usr = await _manager.GetUserAsync(HttpContext.User);
            var email = usr?.Email;
            var memberId = _context.Members.FirstOrDefault(m => m.Email == email).MemberId;


            _context.ChurchMembers.Where(c => c.MemberId == memberId && c.ChurchId == churchId).ExecuteDelete();

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index", new { churchId = Request.Query["churchId"] });
        }

    }
}
