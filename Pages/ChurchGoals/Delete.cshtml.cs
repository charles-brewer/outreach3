using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using outreach3.Data;
using outreach3.Data.Ministries;

namespace outreach3.Pages.ChurchGoals
{
    public class DeleteModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public DeleteModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ChurchGoal ChurchGoal { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? churchId)
        {
            if (churchId == null || _context.ChurchGoals == null)
            {
                return NotFound();
            }

            var churchgoal = await _context.ChurchGoals.FirstOrDefaultAsync(m => m.ChurchGoalId == churchId);

            if (churchgoal == null)
            {
                return NotFound();
            }
            else 
            {
                ChurchGoal = churchgoal;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ChurchGoals == null)
            {
                return NotFound();
            }
            var churchgoal = await _context.ChurchGoals.FindAsync(id);

            if (churchgoal != null)
            {
                ChurchGoal = churchgoal;
                _context.ChurchGoals.Remove(ChurchGoal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
