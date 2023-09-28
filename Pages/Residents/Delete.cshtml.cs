﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Residents
{
    public class DeleteModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public DeleteModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Residents == null)
            {
                return NotFound();
            }
            var resident = await _context.Residents.FindAsync(id);

            if (resident != null)
            {
                Resident = resident;
                _context.Residents.Remove(Resident);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
