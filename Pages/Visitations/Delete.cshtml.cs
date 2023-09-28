using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Visitations
{
    public class DeleteModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public DeleteModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Visitation Visitation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Visitations == null)
            {
                return NotFound();
            }

            var visitation = await _context.Visitations.FirstOrDefaultAsync(m => m.VisitationId == id);

            if (visitation == null)
            {
                return NotFound();
            }
            else 
            {
                Visitation = visitation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Visitations == null)
            {
                return NotFound();
            }
            var visitation = await _context.Visitations.FindAsync(id);

            if (visitation != null)
            {
                Visitation = visitation;
                _context.Visitations.Remove(Visitation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
