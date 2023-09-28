using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Visitations
{
    public class CreateModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public CreateModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Visitation Visitation { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Visitations == null || Visitation == null)
            {
                return Page();
            }

            _context.Visitations.Add(Visitation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
