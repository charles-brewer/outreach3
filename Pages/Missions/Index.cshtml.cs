using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Missions
{
    public class IndexModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public IndexModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Mission> Missions { get;set; } = default!;

        public string AssignedTo { get; set; } = "";
        public int _churchId { get; set; }

        public async Task<IActionResult> OnGetAsync(int churchId)
        {
            _churchId = churchId;
            if (_context.Missions != null)
            {               
                Missions = await _context.Missions.Where(m=>m.ChurchId== churchId)
               .ToListAsync();

                foreach (var mission in Missions)
                {
                    long number1 = 0;
                    bool canConvert = long.TryParse(mission.AssignedTo, out number1);
                    if (canConvert == true && number1!=0 && number1 != -1)                       
                    {
                        mission.AssignedTo = _context.Members.FirstOrDefault(m => m.MemberId == Convert.ToInt32(number1)).Name;
                    }
                }
                await _context.SaveChangesAsync();

            }
            return Page();
        }

     
        public string GetChurchName()
        {
            if(_churchId == 0)
            {
                return "";
            }
            return _context.Churches.FirstOrDefault(c => c.ChurchId == _churchId).Name;
        }
    }
}
