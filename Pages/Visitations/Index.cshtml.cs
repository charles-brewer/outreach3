
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Visitations
{
    public class IndexModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public IndexModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Visitation> Visitation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Visitations != null)
            {
                Visitation = await _context.Visitations.ToListAsync();
            }
        }
    }
}
