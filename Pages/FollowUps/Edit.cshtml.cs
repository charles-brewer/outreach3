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

namespace outreach3.Pages.FollowUps
{
    public class EditModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public EditModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FollowUp FollowUp { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? followUpId, int residentId, int missionId, int churchId)
        {
            if (followUpId == null || _context.FollowUp == null)
            {
                return NotFound();
            }

            var followup =  await _context.FollowUp.FirstOrDefaultAsync(m => m.FollowUpId == followUpId);
            if (followup == null)
            {
                return NotFound();
            }
            FollowUp = followup;
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

            _context.Attach(FollowUp).State = EntityState.Modified;

            try
            {             
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowUpExists(FollowUp.FollowUpId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Residents/Index", new { churchId = Request.Query["churchId"], missionId = Request.Query["missionId"], residentId = Request.Query["residentId"] });
        }

        private bool FollowUpExists(int id)
        {
          return (_context.FollowUp?.Any(e => e.FollowUpId == id)).GetValueOrDefault();
        }
    }
}
