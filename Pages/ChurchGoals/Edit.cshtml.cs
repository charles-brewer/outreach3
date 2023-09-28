using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> OnGetAsync(int? churchId)
        {
            if (churchId == null || _context.ChurchGoals == null)
            {
                return NotFound();
            }

            var churchgoal =  await _context.ChurchGoals.FirstOrDefaultAsync(m => m.ChurchId == churchId);
            if (churchgoal == null)
            {
                return NotFound();
            }
            churchgoal.ChurchId = Convert.ToInt32(Request.Query["churchId"]);
            ChurchGoal = churchgoal;
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

            return RedirectToPage("./Index");
        }

        private bool ChurchGoalExists(int id)
        {
          return (_context.ChurchGoals?.Any(e => e.ChurchGoalId == id)).GetValueOrDefault();
        }
    }
}
