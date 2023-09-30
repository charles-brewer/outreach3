using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using NuGet.Versioning;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Visitations
{
    public class EditModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public EditModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Visitation Visitation { get; set; }

        [BindProperty, Required]
        public TypeVisit VisitationType { get; set; }

        public List<Member> Visitors { get; set; }

        public List<VisitationsMembers> MembersOfVisitation { get; set; } = new List<VisitationsMembers>();

        public int ResidentId { get; set; }
        public SelectList Options { get; set; }

        public async Task<IActionResult> OnGetAsync(int residentId, int visitationId, int missionId)
        {
            if (visitationId == null)
            {
                return NotFound();
            }
            ResidentId = residentId;

            Visitation = await _context.Visitations.FirstOrDefaultAsync(e => e.VisitationId == visitationId);
            
            if (Visitation==null)
            {
                return NotFound();
            }

            MembersOfVisitation = await _context.VisitationMembers.Where(e => e.VisitationId==Visitation.VisitationId).ToListAsync();

            Visitors = _context.Members.ToList();
            
            //Visitation.Visitors= _context.Members.Where(m=>m.Visitations.Any(v=>v.VisitationId==Visitation.VisitationId)).ToList();

            var mission = _context.Missions.Where(e => e.MissionId == missionId).FirstOrDefault();

            var churchMembers = _context.Members.Where(cm => cm.ChurchId == mission.ChurchId);
          
            var churchId = mission.ChurchId;

            Options = new SelectList(_context.Members.Where(m=>m.ChurchId==churchId), nameof(Member.MemberId), nameof(Member.Name));           
            
            Options.Select(e => e.Selected);        
            
          
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

            _context.Attach(Visitation).State = EntityState.Modified;

            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitationExists(Visitation.VisitationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Edit", new { churchId=Request.Query["churchId"], missionId = Request.Query["missionId"], residentId = Request.Query["residentId"], visitationId = Visitation.VisitationId });
        }

       

        public async Task<IActionResult> OnPostAddMemberAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Visitation).State = EntityState.Modified;

            MembersOfVisitation = await _context.VisitationMembers.Where(e => e.VisitationId == Visitation.VisitationId).ToListAsync();


            ResidentId = Convert.ToInt32(Request.Query["residentId"]);

            Visitation.ResidentId = ResidentId; 

            var selectedMemberId = Convert.ToInt32(Request.Form["churchMembers"]);
            if (selectedMemberId > 0)
            {
                var member = await _context.Members.FirstOrDefaultAsync(m => m.MemberId == selectedMemberId);
                var vm = new VisitationsMembers
                {
                    MemberId = selectedMemberId,
                    VisitationId = Visitation.VisitationId

                };
                Visitation.VisitationMembers.Add(vm);
            }
            



            try
            {


                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitationExists(Visitation.VisitationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

           


            return RedirectToPage("./Edit", new { churchId=Request.Query["churchId"], missionId = Request.Query["missionId"], residentId = Request.Query["residentId"], visitationId = Visitation.VisitationId });
        }

        public async Task<IActionResult> OnPostRemoveVisiteeAsync()
        {


            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Visitation).State = EntityState.Modified;

            ResidentId = Convert.ToInt32(Request.Query["residentId"]);
            Visitation.ResidentId = ResidentId;

            //Visitation.Visitors = _context.Members.Where(m => m.Visitations.Any(v => v.VisitationId == Visitation.VisitationId)).ToList();

            var visiteeIds = Request.Form["visiteeId"].ToString().Split(",".ToCharArray());
            var memberId = 0;

            for(var i =0;i < visiteeIds.Length; i++)
            {
                if (visiteeIds[i] == "Remove")
                {
                    memberId = Convert.ToInt32(visiteeIds[i - 1]);
                }
            }

            var m = await _context.VisitationMembers.FirstOrDefaultAsync(vm => vm.VisitationId == Visitation.VisitationId && vm.MemberId == memberId);

            if(m!=null)
            {
                _context.VisitationMembers.Remove(m);
            }



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitationExists(Visitation.VisitationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Edit", new { churchId = Request.Query["churchId"], missionId = Request.Query["missionId"].ToString(), residentId = Request.Query["residentId"].ToString(), visitationId = Visitation.VisitationId });
        }




        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (!ModelState.IsValid)
            {
               
            }

            if (Visitation.VisitationType == TypeVisit.None)
            {
                _context.Visitations.Remove(Visitation);
                await _context.SaveChangesAsync();
               return RedirectToPage("../Residents/Edit", new { churchId=Request.Query["churchId"], missionId = Request.Query["missionId"], residentId = Request.Query["residentId"]});
 

            }
            _context.Attach(Visitation).State = EntityState.Modified;

            Visitation.ResidentId = Convert.ToInt32(Request.Query["residentId"]);
            Visitation.ChurchId = Convert.ToInt32(Request.Query["churchId"]);
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitationExists(Visitation.VisitationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Edit", new { churchId=Request.Query["churchId"], missionId = Request.Query["missionId"], residentId = Request.Query["residentId"], visitationId = Visitation.VisitationId });
        }



        private bool VisitationExists(int id)
        {
          return (_context.Visitations?.Any(e => e.VisitationId == id)).GetValueOrDefault();
        }
    }
}
