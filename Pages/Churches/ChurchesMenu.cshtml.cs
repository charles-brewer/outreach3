using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace outreach3.Pages.Churches
{
    public class ChurchesMenuModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        
        public ChurchesMenuModel(Data.ApplicationDbContext context)
        {
            _context = context;            
        }

        public List<int> MissionIds { get; set; }
        public void OnGet()
        {
            var userName = User.Identity.Name;
            var member = _context.Members.FirstOrDefault(m=>m.Name == userName);

            MissionIds = _context.Missions.Where(c=>c.ChurchId == member.ChurchId).Select(s=>s.MissionMapId).ToList();

        }
    }
}
