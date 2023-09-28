
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using outreach3.Data;
using outreach3.Data.Ministries;
using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;

namespace outreach3.Pages.Admin
{
    public class RoleUsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private IServiceProvider _serviceProvider;
        
       
      
        public RoleUsersModel(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }


        public List<Member> Members { get; set; } = new List<Member>();

        public IList<IdentityUser> Users { get; set; } = new List<IdentityUser>();

        public List<IdentityRole> AllRoles { get; set; } = new List<IdentityRole>();

    
        public async Task<IActionResult> OnGetAsync()
        {
            await GetAllRoles();
           
            return Page();
        }



        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostSelectRoleAsync(string role)
        {
            await GetAllRoles();
            var userManager = _serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser>>();
            Users = await userManager.GetUsersInRoleAsync(role);

            return Page();
        }

           

        private async Task GetAllRoles()
        {
            var roleManager = _serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();
            AllRoles = roleManager.Roles.ToList();
          
        }





        private bool MemberExists(int id)
        {
          return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }

      
    }
}
