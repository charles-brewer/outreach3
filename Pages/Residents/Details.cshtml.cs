using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Residents
{
    public class DetailsModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public DetailsModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Resident Resident { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Residents == null)
            {
                return NotFound();
            }

            var resident = await _context.Residents.FirstOrDefaultAsync(m => m.ResidentId == id);
            if (resident == null)
            {
                return NotFound();
            }
            else 
            {
                Resident = resident;
            }
            return Page();
        }
    }
}
