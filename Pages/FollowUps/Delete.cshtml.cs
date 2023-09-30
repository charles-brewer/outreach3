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
    public class DeleteModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public DeleteModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public FollowUp FollowUp { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FollowUp == null)
            {
                return NotFound();
            }

            var followup = await _context.FollowUp.FirstOrDefaultAsync(m => m.FollowUpId == id);

            if (followup == null)
            {
                return NotFound();
            }
            else 
            {
                FollowUp = followup;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.FollowUp == null)
            {
                return NotFound();
            }
            var followup = await _context.FollowUp.FindAsync(id);

            if (followup != null)
            {
                FollowUp = followup;
                _context.FollowUp.Remove(FollowUp);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
