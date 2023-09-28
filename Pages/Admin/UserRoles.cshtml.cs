
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
    public class UserRolesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private IServiceProvider _serviceProvider;
        
       
      
        public UserRolesModel(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public List<Church> Churches { get; set; }
        public List<Member> Members { get; set; } = new List<Member>();
        public List<string> AllRoles { get; set; } = new List<string>();

        public List<string> UserRoles { get; set; } = new List<string>();

        public int SelectedChurchId { get; set; } 
        public int SelectedMemberId { get; set; }
        public string SelectedRole { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {   
            Churches = _context.Churches.ToList();           
            return Page();
        }

        public async Task<IActionResult> OnPostSelectedChurchAsync(int churchId)
        {
            Churches = _context.Churches.ToList();
            SelectedChurchId = churchId;
            Members = await _context.Members.Where(m=>m.ChurchId == churchId).ToListAsync();
            return Page();
        }


        public async Task<IActionResult> OnPostSelectedMemberAsync(int churchId, int memberId)
        {

            Churches = _context.Churches.ToList();
            SelectedChurchId = churchId;
            Members = await _context.Members.Where(m => m.ChurchId == churchId).ToListAsync();
            SelectedMemberId = memberId;
            AllRoles = await GetAllRoles();
            UserRoles = await GetUserRoles(memberId);
            return Page();
        }

        private async Task<List<string>> GetUserRoles(int memberId)
        {
            var userManager = _serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser>>();
            var roleManager = _serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();
            var selectedMemberEmail = _context.Members.FirstOrDefault(m => m.MemberId == memberId).Email;
            var user = userManager.Users.FirstOrDefault(u => u.Email == selectedMemberEmail);

            
            var roles = await userManager.GetRolesAsync(user);

            return roles.ToList();
          
        }






        public async Task<IActionResult> OnPostRemoveRoleAsync(int churchId, int memberId, string role)
        {
            Churches = _context.Churches.ToList();
            SelectedChurchId = churchId;
            Members = await _context.Members.Where(m => m.ChurchId == churchId).ToListAsync();
            SelectedMemberId = memberId;
            AllRoles = await GetAllRoles();

            var userManager = _serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser>>();
            var roleManager = _serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();
            var selectedMemberEmail = _context.Members.FirstOrDefault(m => m.MemberId == memberId).Email;
            var user = userManager.Users.FirstOrDefault(u => u.Email == selectedMemberEmail);



            await userManager.RemoveFromRoleAsync(user, role);
            await _context.SaveChangesAsync();
            UserRoles = await GetUserRoles(memberId);

            return Page();
        }

        public async Task<IActionResult> OnPostAddRoleAsync(int churchId, int memberId, string role)
        {
            Churches = _context.Churches.ToList();
            SelectedChurchId = churchId;
            Members = await _context.Members.Where(m => m.ChurchId == churchId).ToListAsync();
            SelectedMemberId = memberId;
            AllRoles = await GetAllRoles();

            var userManager = _serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser>>();
            var roleManager = _serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();
            var selectedMemberEmail = _context.Members.FirstOrDefault(m => m.MemberId == memberId).Email;
            var user = userManager.Users.FirstOrDefault(u => u.Email == selectedMemberEmail);
                        
            
           
            await userManager.AddToRoleAsync(user, role);            
            await _context.SaveChangesAsync();
            UserRoles = await GetUserRoles(memberId);

            return Page();
        }



      

     

      
        private async Task GetMembersAsync()
        {
            Members = await _context.Members.ToListAsync();            
        }


        private async Task<List<string>> GetAllRoles()
        {
           return await  _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>().Roles.Select(r => r.Name).ToListAsync();
        }





        private bool MemberExists(int id)
        {
          return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }

      
    }
}
