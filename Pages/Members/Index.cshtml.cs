using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public IndexModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Member> Members { get;set; } = default!;


        public List<bool> ChurchMembersApproval { get;set; }


     

       

        public int CurrentMemberChurchId { get; set; } = default!;

        public async Task OnGetAsync()
        {
            await GetMembers();
        }

        public async Task<IActionResult> OnPostApproveAsync()
        {
            await GetMembers();

            foreach (var member in Members)
            {
                member.Approved = false;
            }

            var approved = Request.Form["checkBoxChurchMember"];
            for (var i = 0; i < approved.Count; i++)
            {
                var memberId = Convert.ToInt32(approved[i]);
                _context.Members.FirstOrDefault(m => m.MemberId == memberId).Approved = true;  
            }
            await _context.SaveChangesAsync();

            return Page();
        }
     

        private async Task GetMembers()
        {
            if (User.IsInRole("ChurchAdmin"))
            {
                var currentMember = _context.Members.FirstOrDefault(m => m.Name == User.Identity.Name);
                CurrentMemberChurchId = _context.Members.FirstOrDefault(cm => cm.MemberId == currentMember.MemberId).ChurchId;

                Members = await _context.Members.Where(m => m.ChurchId == CurrentMemberChurchId).ToListAsync();

            }
            if (User.IsInRole("Admin"))
            {
                if (_context.Members != null)
                {
                    Members = await _context.Members.ToListAsync();
                }
            }
        }
    }
}
