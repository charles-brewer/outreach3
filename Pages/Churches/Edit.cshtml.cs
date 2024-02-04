using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Churches
{
    public class EditModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private static int _churchId;
        public EditModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> AllUsers { get; set; }


        public List<SelectListItem> SiteUsers { get; set; }


        public string SelectedUser { get; set; }

        public Church Church { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int churchId)
        {
            if (churchId == null || _context.Churches == null)
            {
                return NotFound();
            }
            _churchId = churchId;

            var church = await _context.Churches.FirstOrDefaultAsync(m => m.ChurchId == churchId);
            if (church == null)
            {
                return NotFound();
            }

            AllUsers = _context.Members.Where(m => m.ChurchId == _churchId || m.ChurchId == 0).Select(a =>
                                 new SelectListItem
                                 {
                                     Value = a.MemberId.ToString(),
                                     Text = $"{a.Email} ({a.Name})"
                                 }).ToList();


            SiteUsers = _context.Members.Where(c => c.ChurchId == _churchId).Select(a =>
                               new SelectListItem
                               {
                                   Value = a.MemberId.ToString(),
                                   Text = $"{a.Email} ({a.Name})"
                               }).ToList();
            Church = church;
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

            _context.Attach(Church).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(Church.ChurchId))
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


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var memberId = Request.Form["allUsers"];
            int i;
            if (!int.TryParse(memberId, out i))
            {
                return RedirectToPage("Edit", new { churchId = _churchId.ToString() });
            }
            var mid = Request.Form["selectedMember"];

            var member = await _context.Members.FirstOrDefaultAsync(m => m.MemberId == Convert.ToInt32(memberId));
            member.ChurchId = _churchId;
            await _context.SaveChangesAsync();
            return RedirectToPage("Edit", new { churchId = _churchId.ToString() });



        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var mid = Request.Form["selectedMember"];

            var memberId = Convert.ToInt32(Request.Form["siteUsers"]);
            if (memberId == 0)
            {
                return RedirectToPage("Edit", new { id = _churchId.ToString() });
            }
            var cm = _context.Members.FirstOrDefault(c => c.MemberId == memberId);
            cm.ChurchId = 0;
            await _context.SaveChangesAsync();
            return RedirectToPage("Edit", new { churchId = _churchId.ToString() });
        }


      


        private bool SiteExists(int id)
        {
            return (_context.Churches?.Any(e => e.ChurchId == id)).GetValueOrDefault();
        }
    }
}
