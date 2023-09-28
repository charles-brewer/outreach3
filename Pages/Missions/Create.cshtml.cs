using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Missions
{
    public class CreateModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public CreateModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {           
           
           
            return Page();
        }

        [BindProperty]
        public Mission Mission { get; set; }

        [BindProperty]
        public MissionMap MissionMap { get; set; } = new MissionMap();


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Mission mission = new Mission();
            var churchId = Convert.ToInt32(Request.Query["churchId"]);
            mission.ChurchId = churchId;
            mission.Name = Request.Form["inpMapName"];
            mission.Status = "New";

            var mmId = mission.MissionMapId;

            var mm = mission.MissionMap;

            if (mm != null)
            {
                mm.MapData = "{ lat: 30.248761558787976, lng: -81.81772810834147 }";
                mm.MapZoom = "18";
                mm.MapName = $"{mission.Name}_map";
                mm.MapTilt = "0";
                mm.MapHeading = "0";
            }

            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();

            var churchName = _context.Churches.Where(c => c.ChurchId == churchId).FirstOrDefault().Name;

            return RedirectToPage("./Index", new{ churchId = churchId, churchName = churchName });
        }

        public string MapData { get; set; }
        public List<MapMarker> MapMarkers { get; set; }
        public string MapZoom { get; set; }
        public string MapName { get; set; }
        public string MapHeading { get; set; }
        public async Task<IActionResult> OnPostSaveAsync(int id)
        {
            if (!ModelState.IsValid || _context.MissionMaps == null)
            {
                //return Page();
            }


            MissionMap mm = new MissionMap();
            

            _context.Missions.Add(Mission);


            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
