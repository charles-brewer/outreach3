using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Members
{
    public class EditModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;
        private IServiceProvider _serviceProvider;

        public EditModel(outreach3.Data.ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _context = context;
        }

        [BindProperty]
        public Member Member { get; set; } = default!;


        [BindProperty]
        public List<Church> Churches { get; set; } = default!;



        public async Task<IActionResult> OnGetAsync(int memberId)
        {
            if (memberId == null || _context.Members == null)
            {
                return NotFound();
            }

            var member =  await _context.Members.FirstOrDefaultAsync(m => m.MemberId == memberId);
            if (member == null)
            {
                return NotFound();
            }
            Member = member;

            
          

            

           

            Churches = await _context.Churches.ToListAsync();


            

          

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(Member.MemberId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        [BindProperty]
        public Church SelectedChurch { get; set; }



        public async Task<IActionResult> OnPostRemoveAsync(int memberId)
        {
            if (!ModelState.IsValid) 
            {
                return NotFound();
            }
            _context.Members.FirstOrDefault(m => m.MemberId == memberId).ChurchId = 0;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Edit", new { memberId = Member.MemberId, churchId=SelectedChurch.ChurchId });
        }


        public async Task<IActionResult> OnPostAddAsync(int memberId, int churchId)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _context.Members.FirstOrDefault(m => m.MemberId == memberId).ChurchId = churchId;
            await _context.SaveChangesAsync();
            return RedirectToPage("./Edit", new { memberId = Member.MemberId, churchId = churchId });
        }

        public async Task<IActionResult> OnPostRemoveAccountAsync(int memberId, int churchId)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var member = _context.Members.FirstOrDefault(m => m.MemberId == memberId);
            var email = member.Email;
            _context.Members.Remove(member);


            var _userManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await _userManager.FindByEmailAsync(email);




            var rolesForUser = await _userManager.GetRolesAsync(user);

            using (var transaction = _context.Database.BeginTransaction())
            {
                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await _userManager.RemoveFromRoleAsync(user, item);
                    }
                }

                await _userManager.DeleteAsync(user);
                transaction.Commit();
            }



            await _context.SaveChangesAsync();
            return RedirectToPage("../Admin/AllUsers", new { memberId = Member.MemberId, churchId = churchId });
        }

        private bool MemberExists(int id)
        {
          return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
