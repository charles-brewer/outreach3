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
    public class DetailsModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public DetailsModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
