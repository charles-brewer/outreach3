using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Missions
{
    public class DeleteModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public DeleteModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Mission Mission { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? missionId)
        {
            if (missionId == null || _context.Missions == null)
            {
                return NotFound();
            }

            var mission = await _context.Missions.FirstOrDefaultAsync(m => m.MissionId == missionId);

            if (mission == null)
            {
                return NotFound();
            }
            else 
            {
                Mission = mission;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? missionId)
        {
            if (missionId == null || _context.Missions == null)
            {
                return NotFound();
            }
            var mission = await _context.Missions.FindAsync(missionId);

            if (mission != null)
            {
                Mission = mission;
                _context.Missions.Remove(Mission);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
