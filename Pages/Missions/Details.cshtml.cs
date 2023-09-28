using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Missions
{
    public class DetailsModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public DetailsModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Mission Mission { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Missions == null)
            {
                return NotFound();
            }

            //var mission = await _context.Missions.FirstOrDefaultAsync(m => m.MissionId == id);
            //if (mission == null)
            //{
            //    return NotFound();
            //}
            //else 
            //{
            //    Mission = mission;
            //}
            return Page();
        }
    }
}
