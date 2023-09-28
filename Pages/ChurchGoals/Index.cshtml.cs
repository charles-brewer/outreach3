using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using outreach3.Data;
using outreach3.Data.Ministries;

namespace outreach3.Pages.ChurchGoals
{
    public class IndexModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public IndexModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ChurchGoal> ChurchGoals { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ChurchGoals != null)
            {
                ChurchGoals = await _context.ChurchGoals.ToListAsync();
            }
        }
    }
}
