using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using outreach3.Data;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Churches
{
    public class StatsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public StatsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public Church Church { get; set; } = default!;

        public List<Resident> Residents { get; set; } = new List<Resident>();

        public List<Member> Members { get; set; } = new List<Member>();

        public List<VisitationsMembers> VisitationMembers { get; set; } = new List<VisitationsMembers>();


        public List<Visitation> Visitations { get; set; } = new List<Visitation>();

        public List<Mission> Missions { get; set; }

        public async Task<IActionResult> OnGetAsync(int? churchId)
        {
            if (churchId == null || _context.Churches == null)
            {
                return NotFound();
            }

            var church = await _context.Churches.FirstOrDefaultAsync(m => m.ChurchId == churchId);
            Missions = await _context.Missions.Where(m=>m.ChurchId==churchId).ToListAsync();

          

            foreach(var mission in Missions)
            {
                Residents.AddRange(_context.Residents.Where(r => r.MissionId == mission.MissionId));
            }

            foreach (var resident in Residents)
            {
                Visitations.AddRange(_context.Visitations.Where(v => v.ResidentId == resident.ResidentId));

            }
            
            foreach(var visit in Visitations)
            {
                VisitationMembers.AddRange(_context.VisitationMembers.Where(m => m.VisitationId == visit.VisitationId));
            }

            foreach(var member in VisitationMembers)
            {
                Members.Add(_context.Members.FirstOrDefault(m => m.MemberId == member.MemberId));
            }
           

            if (church == null)
            {
                return NotFound();
            }
            else
            {
                Church = church;
            }
            return Page();
        }
    }
}
