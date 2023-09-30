using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
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
using outreach3.Data;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Missions
{
    public class EditModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public EditModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
            if (webHostEnvironment.IsDevelopment())
            {
                IsDevlopement = true;
            }
        }

        public bool IsDevlopement { get; set; } = false;

        [BindProperty]
        public Mission Mission { get; set; } = default!;

        [BindProperty]
        public MissionMap Map { get; set; } = default!;

        [BindProperty]
        public List<MapMarker> Markers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync( int churchId, int missionId)
        {

            if (missionId == null || _context.Missions == null || missionId == 0)
            {
                return NotFound();
            }

           

           
           var mission = await _context.Missions.FirstOrDefaultAsync(m => m.MissionId == missionId);
            var mapId = mission.MissionMapId;


            if (mission == null)
            {
                return NotFound();
            }
            Map = await _context.MissionMaps.Where(m => m.MissionMapId == mission.MissionMapId).FirstOrDefaultAsync();

            

            Mission = mission;

            Mission.Name = _context.Missions.FirstOrDefault(m => m.MissionId == missionId).Name;
            Mission.Status = _context.Missions.FirstOrDefault(m => m.MissionId == missionId).Status;


            Markers = _context.MapMarkers.Where(m => m.MissionMapId == Map.MissionMapId).OrderBy(m=>m.Number).ToList();

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

        public async Task<IActionResult> OnPostDeleteMarkerAsync(int? missionId, int churchId, string churchName)
        {
            var markerId = Convert.ToInt32(Request.Form["inpMapMarkerToDelete"]);

            var mapId = _context.Missions.FirstOrDefault(m => m.MissionId == missionId).MissionMapId;

            var mrkr = await _context.MapMarkers.Where(mm => mm.MissionMapId == mapId && mm.Number == markerId).FirstOrDefaultAsync();

            if (mrkr != null)
            {
                _context.MapMarkers.Remove(mrkr);

                //renumber
                foreach (var marker in _context.MapMarkers.Where(m => m.Number > markerId && m.MissionMapId == mapId))
                {
                    marker.Number = marker.Number - 1;
                    marker.Title = (marker.Number - 1).ToString();
                }
            }
            var residentToDelete = new Resident();

            //Sync the Residents
            foreach (var resident in _context.Residents.Where(r => r.MissionId == missionId))
            {
                if (resident.number == markerId)
                {
                    residentToDelete = resident;
                }
                if (resident.number > markerId)
                {
                    resident.number--;
                }
            }
            if (residentToDelete.MissionId == 0)
            {
                residentToDelete = null;
            }
            else
            {
                _context.Residents.Remove(residentToDelete);
                churchId = _context.Missions.Where(m => m.MissionId == missionId).FirstOrDefault().ChurchId;
                churchName = _context.Churches.Where(c => c.ChurchId == churchId).FirstOrDefault().Name;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Edit", new { missionId = missionId, churchId = churchId, churchName = churchName });
        }

        public async Task<IActionResult> OnPostSaveAsync(int missionId, int churchId, string churchName)
        {
            var mission = _context.Missions.Where(m=>m.MissionId == missionId).FirstOrDefault();
            var missionMap = _context.MissionMaps.Where(mm => mm.MissionMapId == mission.MissionMapId).FirstOrDefault();

            if (missionMap != null)
            {
                missionMap.MapData = Request.Form["inpMapData"];
                missionMap.MapZoom = Request.Form["inpMapZoom"];
                missionMap.MapHeading = Request.Form["inpMapHeading"];
                missionMap.MapTilt = Request.Form["inpMapTilt"];
                var markerData = Request.Form["inpMapMarkerData"];
                var allMarkerData = Request.Form["allMarkerData"];
                var movedMarkers = Request.Form["inpMovedMapMarkerData"].ToString();

                //Save the Map Marker
                foreach (var marker in markerData.ToString().Split("|"))
                {
                    if (marker == "")
                        continue;

                    var markerDataParts = marker.Split(";");
                    var markerNum = Convert.ToInt32(markerDataParts[0]);
                    var addNew = false;
                    var mrkr = await _context.MapMarkers.Where(mm => mm.MissionMapId == mission.MissionMapId && mm.Number == markerNum).FirstOrDefaultAsync();
                    if (mrkr == null)
                    {
                        addNew = true;
                        mrkr = new MapMarker();
                    }

                    mrkr.MissionMapId = mission.MissionMapId;
                    mrkr.MissionMap = await _context.MissionMaps.FirstOrDefaultAsync(mm => mm.MissionMapId == mission.MissionMapId);


                    foreach (var markerDataPart in markerDataParts)
                    {
                        mrkr.Number = markerNum;
                        mrkr.LatLng = markerDataParts[1];
                        mrkr.Color = GetColor(markerNum).Item1;                       
                        mrkr.Label = markerDataParts[3];
                        

                        mrkr.MissionMapId = mission.MissionMapId;
                        mrkr.Title = markerDataParts[2];
                    }


                    if (addNew)
                    {
                        _context.MapMarkers.Add(mrkr);
                    }

                 
                    SaveStaticMap(missionMap, markerData);
                   CreateResident(mrkr, missionId, markerDataParts[2]);


                }
            }
            churchId = _context.Missions.Where(m => m.MissionId == missionId).FirstOrDefault().ChurchId;
            churchName = _context.Churches.Where(c => c.ChurchId == churchId).FirstOrDefault().Name;
            _context.SaveChanges();           

            return RedirectToPage("./Edit", new {churchId = churchId, missionId = missionId,  churchName = churchName });
        }

        private void SaveStaticMap(MissionMap? missionMap, string mapMarkerData)
        {
            var mapData = missionMap.MapData;
            var mapDatas = mapData.Split("|".ToCharArray());
            var mapCenter = missionMap.MapData.ToString().Replace("{lat:", "").Replace("lng:", "").Replace("}", "");
            var img = "https://maps.googleapis.com/maps/api/staticmap?center=" + mapDatas[0] + "&format=png&zoom=" + missionMap.MapZoom;
            img += "&size=640x640";
            img += "&key=xxxxxxx&maptype=hybrid";
            img = img.Replace("%20", "").Replace("{lat:", "").Replace("lng:", "").Replace("}", "").Replace(" ", "");


            var md = _context.MapMarkers.Where(mm => mm.MissionMapId == missionMap.MissionMapId).ToList();


            List<string> mMarkers = mapMarkerData.Split("|".ToCharArray()).ToList();
            string MarkerText = "";
          
            foreach (var mMarker in md)
            {   
                var num = mMarker.Number;  
                var color = GetColor(num).Item1;
                var oneToEight = num;
                while(oneToEight > 8 )
                {
                    oneToEight-=8;
                }             
                var url = $"https://wfwcoutreach.com/Images/Markers/"+ color + ".png";               
                var lbl = num;
                var latlng = mMarker.LatLng.Replace("(", "").Replace(")", "");
                MarkerText += "&markers=label:" + oneToEight + "%7Ccolor:" + color + "%7C" + latlng;                
            }
            img += "&scale=2" + MarkerText;

            WebRequest request = WebRequest.Create(img);
            var response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment = this.webHostEnvironment;

                var path = Server.MapPath("maps/map_" + missionMap.MissionMapId + ".png");

                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    dataStream.CopyTo(fs);
                }
            }
            response.Close();
        }



        public (string, string) GetColor(int num)
        {
            if(num is > 0 and <9)
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


        private void CreateResident(MapMarker mapMarker, int missionId, string newAddress)
        {
            if (!_context.Residents.Any(r => r.number == mapMarker.Number && r.MissionId == missionId))
            {
                var newResident = new Resident();
                newResident.Address = newAddress;
                newResident.number = mapMarker.Number;
                newResident.OneToEight = Convert.ToInt32(mapMarker.Label);
                newResident.ForeColor = GetColor(mapMarker.Number).Item2;
                newResident.BackColor = GetColor(mapMarker.Number).Item1;
                newResident.MissionId = missionId;
                _context.Residents.Add(newResident);
            }
            else
            {
                var resident = _context.Residents.Where(r => r.number == mapMarker.Number && r.MissionId == missionId).FirstOrDefault();
                resident.Address = newAddress;
                resident.number = mapMarker.Number;
                resident.OneToEight = Convert.ToInt32(mapMarker.Label);
                resident.ForeColor = GetColor(mapMarker.Number).Item2;
                resident.BackColor = GetColor(mapMarker.Number).Item1;
            }             
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
