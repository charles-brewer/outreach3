using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using outreach3.Data;
using outreach3.Data.Ministries;

namespace outreach3.Pages.FollowUps
{
    public class IndexModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public IndexModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FollowUp> FollowUp { get;set; }

        public async Task OnGetAsync(int churchId, int MissionId, int residentId)
        {
            if (_context.FollowUp != null)
            {
                FollowUp = await _context.FollowUp.Where(f=>f.ResidentId==residentId).ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int churchId, int missionId,int residentId, int? followUpId)
        {
            var followUp = await _context.FollowUp.FirstOrDefaultAsync(f => f.FollowUpId == followUpId);

            if (followUp != null)
            {
                _context.FollowUp.Remove(followUp);
            }
           

            if (_context.Residents.FirstOrDefault(r => r.ResidentId == residentId).FollowUpId != null)
            {
                _context.Residents.FirstOrDefault(r => r.ResidentId == residentId).FollowUpId = null;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("../Residents/Index", new { churchId, missionId });
        }
    }
}
