using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using outreach3.Data;

namespace outreach3.Pages.Missions
{
    public class AddressesModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public AddressesModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public MissionMap MissionMap { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Missions == null)
            {
                return NotFound();
            }

            var missionMap = await _context.MissionMaps.FirstOrDefaultAsync(m => m.MissionMapId == id);
            if (missionMap == null)
            {
                return NotFound();
            }
            else
            {
                MissionMap = missionMap;

                foreach(var mapMarker in _context.MapMarkers.Where(m=>m.MissionMapId == id))
                {
                    MissionMap.MapMarkers.Add(mapMarker);
                }
            }
            return Page();
        }
    }
}
