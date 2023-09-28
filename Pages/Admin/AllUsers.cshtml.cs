
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using outreach3.Data;
using outreach3.Data.Ministries;
using System.Data;
using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;

namespace outreach3.Pages.Admin
{
    public class AllUsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private IServiceProvider _serviceProvider;
        
       
      
        public AllUsersModel(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public List<Church> Churches { get; set; }
        public List<Member> Members { get; set; } = new List<Member>();
      

        public async Task<IActionResult> OnGetAsync()
        {   
            Churches = await _context.Churches.ToListAsync();
            Members = await _context.Members.ToListAsync();
            return Page();
        }

      





        private bool MemberExists(int id)
        {
          return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }

      
    }
}
