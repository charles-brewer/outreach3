using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
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

        public FollowUp FollowUp { get; set; }

        public IList<Resident> Residents { get; set; } = new List<Resident>();
        public string MissionName { get; set; } = "";
        public string ChurchId { get; set; } 

        public string GetStatus(int? followUpId)
        {
            if(followUpId == null) { return "None"; }
            return _context.FollowUp.FirstOrDefault(f => f.FollowUpId == followUpId).Status.ToString();

        }

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
