using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using outreach3.Data.Ministries;

namespace outreach3.Pages.ChurchMap
{
    public class EditModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public EditModel(outreach3.Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Mission Mission { get; set; } = default!;

        [BindProperty]
        public MissionMap Map { get; set; } = default!;

        [BindProperty]
        public List<MapMarker> Markers { get; set; } = new List<MapMarker>();

        public string GetScriptWithAPIKey()
        {
            using (StreamReader r = new StreamReader("settings.json"))
            {
                string json = r.ReadToEnd();
                var res = (JObject)JsonConvert.DeserializeObject(json);
                var returnValue = res.Value<string>("googlemapsAPI");
                return returnValue;
            }
        }

        public async Task<IActionResult> OnGetAsync(int churchId)
        {

            var mapIds = _context.Missions.Where(c=>c.ChurchId == churchId).Select(m=>m.MissionMapId).ToList();
                       

            foreach (var mapId in mapIds)
            { 
               
                Markers.AddRange(_context.MapMarkers.Where(m => m.MissionMapId==mapId).OrderBy(m => m.Number).ToList());

            }

            

           



            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int missionId, int churchId, string churchName)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Mission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MissionExists(Mission.MissionId))
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
                return ("red", "white");
            }
            if (num is >= 33 and <= 40)
            {
                return ("white", "navy");
            }
            if (num is >= 40)
            {
                return ("yellow", "navy");
            }
            return ("black", "white");
        }

        private bool MissionExists(int id)
        {
            return (_context.Missions?.Any(e => e.MissionId == id)).GetValueOrDefault();
        }


     
    }


    public static class Server
    {
        public static string MapPath(string path)
        {
            return Path.Combine((string)AppDomain.CurrentDomain.GetData("WebRootPath"), path);
        }
    }

}
