﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Churches
{
    public class DeleteModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public DeleteModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Church Site { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? churchId)
        {
            if (churchId == null || _context.Churches == null)
            {
                return NotFound();
            }

            var site = await _context.Churches.FirstOrDefaultAsync(m => m.ChurchId == churchId);

            if (site == null)
            {
                return NotFound();
            }
            else
            {
                Site = site;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? churchId)
        {
            if (churchId == null || _context.Churches == null)
            {
                return NotFound();
            }
            var site = await _context.Churches.FindAsync(churchId);

            if (site != null)
            {
                Site = site;
                _context.Churches.Remove(Site);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
