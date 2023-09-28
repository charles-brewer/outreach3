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
    public class IndexModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public IndexModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Resident> Residents { get; set; } = new List<Resident>();
        public string MissionName { get; set; } = "";
        public string ChurchId { get; set; } 

        public async Task OnGetAsync(int missionId)
        {
            ChurchId = Request.Query["churchId"];
            MissionName = _context.Missions.FirstOrDefault(m => m.MissionId == missionId).Name;
            if (_context.Residents.Any(r=>r.MissionId==missionId))
            {
                Residents = await _context.Residents.Where(r=>r.MissionId==missionId).OrderBy(s=>s.number).ToListAsync();
            }
            
        }


    }
}
