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
    public class CreateModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public CreateModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      

        [BindProperty]
        public ChurchGoal ChurchGoal { get; set; } = default!;

        public string  ChurchName {get;set;}


        public IActionResult OnGet()
        {
            ChurchName = _context.Churches.FirstOrDefault(c => c.ChurchId == Convert.ToInt32(Request.Query["churchId"])).Name;

            return Page();

        }

            // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
            public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ChurchGoals == null || ChurchGoal == null)
            {
                return Page();
            }

            ChurchGoal.ChurchId = Convert.ToInt32(Request.Query["churchId"]);

            _context.ChurchGoals.Add(ChurchGoal);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
