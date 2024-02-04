using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using outreach3.Data.Ministries;
using outreach3.Pages.Visitations;

namespace outreach3.Pages.Residents
{
    public class EditModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public EditModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Resident SelectedResident { get; set; } = default!;

        public Mission Mission { get; set; }

       

        public async Task<IActionResult> OnGetAsync(int residentId, int missionId)
        {
            if (residentId == null || _context.Residents == null)
            {
                return NotFound();
            }
            Mission = _context.Missions.FirstOrDefault(m => m.MissionId == missionId);

            var resident =  await _context.Residents.FirstOrDefaultAsync(m => m.ResidentId == residentId);
            if (resident == null)
            {
                return NotFound();
            }
            SelectedResident = resident;

            

            foreach(var visitation in _context.Visitations.Where(v=>v.ResidentId == residentId))
            {
              
            }
           
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

            _context.Attach(SelectedResident).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidentExists(SelectedResident.ResidentId))
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

        public async Task<IActionResult> OnPostRemoveVisitationAsync(int visitationId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SelectedResident).State = EntityState.Modified;

            var visiation = _context.Visitations.FirstOrDefault(v => v.VisitationId == visitationId);
            _context.Visitations.Remove(visiation);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidentExists(SelectedResident.ResidentId))
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

        public (string, string) GetColor(int num)
        {
            if (num is > 0 and < 9)
            {
                return ("blue", "white");
            }
            if (num is >= 9 and <= 16)
            {
                return ("brown", "white");
            }
            if (num is >= 17 and <= 24)
            {
                return ("green", "white");
            }
            if (num is >= 25 and <= 32)
            {
                return ("white", "navy");
            }
            if (num is >= 33 and <= 40)
            {
                return ("red", "white");
            }
            if (num is >= 41 and <= 48)
            {
                return ("yellow", "navy");
            }
            if (num is >= 49 and <= 56)
            {
                return ("aqua", "navy");
            }
            if (num is >= 57 and <= 64)
            {
                return ("teal", "white");
            }
            if (num is >= 65 and <= 72)
            {
                return ("beige", "navy");
            }
            if (num is >= 73 and <= 80)
            {
                return ("gold", "white");
            }
            return ("black", "white");
        }
        public async Task<IActionResult> OnPostSaveAsync(int missionId)
        {
            SelectedResident.MissionId = missionId;
            SelectedResident.Mission = _context.Missions.FirstOrDefault(m => m.MissionId == missionId);
            
            

            _context.Attach(SelectedResident).State = EntityState.Modified;

            var oneToEight = SelectedResident.number;
            while(oneToEight > 8)
            {
                oneToEight-=8;
            }
            SelectedResident.OneToEight = oneToEight;
            SelectedResident.BackColor = GetColor(SelectedResident.number).Item1;
            SelectedResident.ForeColor = GetColor(SelectedResident.number).Item2;




            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidentExists(SelectedResident.ResidentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Edit", new { churchId = Request.Query["churchId"], missionId = SelectedResident.MissionId, residentId = Request.Query["ResidentId"] });
        }

        public async Task<IActionResult> OnPostCreateVisitationAsync(int residentId)
        {
           

            Visitation visitation = new Visitation();
            
            
              if (!ModelState.IsValid)
            {
                // return Page();
            }
              visitation.ResidentId = residentId;
            _context.Residents.FirstOrDefault(r => r.ResidentId == residentId).Visitations.Add(visitation);

            _context.SaveChanges();

            return RedirectToPage("../Visitations/Edit", new { churchId= Request.Query["churchId"], missionId= Request.Query["missionId"], residentId=residentId, visitationId = visitation.VisitationId});
        }


        public Visitation Visitation { get; set; }
        public string VisitationDetails { get; set; }

        public async Task<IActionResult> OnPostShowVisitationAsync(int visitationId, int missionId)
        {

            Visitation = _context.Visitations.FirstOrDefault(v => v.VisitationId == visitationId);


            //SelectedResident.Mission = _context.Missions.FirstOrDefault(m => m.MissionId == missionId);

            if (!ModelState.IsValid)
            {
              // return Page();
            }          

            _context.SaveChanges();

                        
           
            return RedirectToPage("../Visitations/Edit", new {  churchId= Request.Query["churchId"], missionId = missionId, residentId = SelectedResident.ResidentId, visitationId = Visitation.VisitationId });
        }


     



        private bool ResidentExists(int id)
        {
          return (_context.Residents?.Any(e => e.ResidentId == id)).GetValueOrDefault();
        }
    }
}
