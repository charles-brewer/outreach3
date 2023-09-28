using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.RulesetToEditorconfig;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Win32.SafeHandles;
using NuGet.Common;
using outreach3.Data.Ministries;
using SelectPdf;

namespace outreach3.Pages.Missions
{
    public class AssignmentsModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        public AssignmentsModel(outreach3.Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment= webHostEnvironment;
        }

        [BindProperty]
        public Mission Mission { get; set; } = default!;

        public SelectList Options { get; set; }

        public string MemberName = "";
        public SelectList Statuses { get; set; }

        public async Task<IActionResult> OnGetAsync(int churchId, int missionId)
        {

            //Validate
            if (missionId == 0 || _context.Missions == null)
            {
                return NotFound();
            }

            var mission = await _context.Missions.FirstOrDefaultAsync(m => m.MissionId == missionId);
            if (mission == null)
            {
                return NotFound();
            }

            //Mission
            Mission = mission;
            Mission.ChurchId = churchId;


            //Member Assignment
            Options = new SelectList(_context.Members.Where(m => m.ChurchId == churchId), nameof(Member.MemberId), nameof(Member.Name), "Select Member");
            //Mission.AssignedTo = mission.AssignedTo;

            //Date Assigned
            if (mission.DateAssigned == DateTime.MinValue)
            {
                mission.DateAssigned = DateTime.Now;
            }

            //Status
            var listItems = new List<string>() { "New", "Active", "Complete" };
            Statuses = new SelectList(listItems);

            

            return Page();
        }
        public async Task<IActionResult> OnPostResetAsync(int missionId)
        {
            Mission = await _context.Missions.FirstOrDefaultAsync(m => m.MissionId == missionId);
            Mission.DateAssigned = DateTime.MinValue;
            Mission.AssignedTo = "";
            Mission.Status = "New";
            Mission.DateLastCompleted = DateTime.MinValue;
            await _context.SaveChangesAsync();
            return RedirectToPage("./Assignments", new { churchId = Request.Query["churchId"], missionId = Mission.MissionId });
        }

        public async Task<IActionResult> OnPostSaveAsync(int missionId)
        {
            if (!ModelState.IsValid)
            {
                // return Page();
            }
            Mission = _context.Missions.FirstOrDefault(m => m.MissionId == missionId);

            var missionMap = _context.MissionMaps.FirstOrDefault(m => m.MissionMapId == Mission.MissionMapId);

            _context.Attach(Mission).State = EntityState.Modified;

            Mission.MissionMap = missionMap;
            Mission.MissionMapId = missionMap.MissionMapId;
            Mission.AssignedTo = Request.Form["assignedTo"];
            Mission.DateAssigned = Convert.ToDateTime(Request.Form["dateAssigned"]);
            Mission.Status = Request.Form["status"];

            var churchId = _context.Missions.Where(m => m.MissionId == missionId).FirstOrDefault().ChurchId;
            var churchName = _context.Churches.Where(c => c.ChurchId == churchId).FirstOrDefault().Name;



            if (Mission.Status == "Complete")
            {
                Mission.DateLastCompleted = DateTime.Now;
            }


            await _context.SaveChangesAsync();

            try
            {
                //await _context.SaveChangesAsync();
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

            return RedirectToPage("./Assignments", new { churchId = Request.Query["churchId"], missionId = Mission.MissionId });
        }


        public async Task<IActionResult> OnGetDownloadFIleAsync(int missionId)
        {
            var mission = await _context.Missions.FirstOrDefaultAsync(m => m.MissionId == missionId);

            /********************Start HTML*******************/
            var htmlPage = "<!DOCTYPE html><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta charset=\"utf-8\" />";

            /*******************Head**********************/
            htmlPage += "<title></title>";
            htmlPage += " <style>";
            htmlPage += "table,tr,th,td{border-collapse: collapse;border:1px solid navy;text-align:center;font-size:10pt; !important}";
            htmlPage += " .tdNumber{width:10%;}";
            htmlPage += " .tdName{width:30%;}";
            htmlPage += " .tdAddress{ width:50%;}";
            htmlPage += " .tdDoorHanger{ width:10%;}";
            htmlPage += "@page { size: portrait !important;}";

            htmlPage += "@media print {@page { margin: 40px;  !important;}body { margin: 1.6cm; !important; }}";


            htmlPage += "</style>";

            htmlPage += "</head>";
            /*******************Head**********************/

            /*******************Body**********************/
            htmlPage += "<body style='font-size:12pt;'><div>";

            var churchId = mission.ChurchId;
            var churchName = _context.Churches.FirstOrDefault(c => c.ChurchId == churchId).Name;
            if (_webHostEnvironment.IsDevelopment())
            {
                htmlPage += $"<div style='border:none; width: 100%;'><img src='/images/logo.png' style='width:32px;'></img><span style='font-size:20px;'>{churchName}, {mission.Name}</span></div><br />";
            }
            else
            {
                htmlPage += $"<div style='border:none; width: 100%;'><img src='https://www.wfwcoutreach.com/images/logo.png' style='width:32px;'></img><span style='font-size:20px;'>{churchName}, {mission.Name}</span></div><br />";

            }
            htmlPage += "";

            htmlPage += "Date Last Completed: " + mission.DateLastCompleted.ToString().Replace("12:00:00 AM", "") + "<br/>";
            htmlPage += "<div>Outreacher_______________________________________   Date___________________________</div><br/>";
            /***********************Table of Residents**************************/
            htmlPage += "<table style='width:98%;'><tr><td>Number</td><td>Name</td><td>Address</td><td>Door Hanger</td></tr>";

            var residents = _context.Residents.Where(r => r.MissionId == missionId).ToList();
            var map = await _context.MissionMaps.FirstOrDefaultAsync(m => m.MissionMapId == mission.MissionMapId);

            foreach (var res in residents)
            {
                var imageName = res.BackColor + res.OneToEight;
                if (_webHostEnvironment.IsDevelopment())
                {
                    htmlPage += $"<tr style='height:18px;'><td class='tdNumber'><img src='/Images/{res.BackColor}{res.OneToEight}.png' style='width:18px;' /></td>";
                }
                else
                {
                    htmlPage += $"<tr style='height:18px;'><td class='tdNumber'><img src='https://www.wfwcoutreach.com/Images/{res.BackColor}{res.OneToEight}.png' style='width:18px;' /></td>";
                }
                    htmlPage += "<td class='tdName'>" + res.FirstName + ", " + res.LastName + "</td>";
                htmlPage += "<td class='tdAddress'>" + res.Address + "</td>";
                htmlPage += "<td class='tdDoorhanger'><img src='/images/box.png' /></td>";
                htmlPage += "</tr>";
            }
            htmlPage += "</table>";
            /***********************Table of Residents**************************/
            htmlPage += "<div style='page-break-after:always;'>&nbsp;</div>";


            htmlPage += "<br /><h3>" + mission.Name + "</h3>";
            htmlPage += "Last Date Mission Completed:" + mission.DateLastCompleted.ToString().Replace("12:00:00 AM", "") + "<br/>";
            /***********************Acquaintances**************************/
            int acqCount = 0;

            var MembersOfVisitation = await _context.VisitationMembers.Select(e => e.Member).ToListAsync();

            foreach (Resident res in residents)
            {
                foreach (var visit in _context.Visitations.Where(v => v.ResidentId == res.ResidentId))
                {                    
                    acqCount++;
                    htmlPage += "<hr style='color:navy;' />";
                    htmlPage += "<b>Date: </b> " + visit.VisitationDate.ToString().Replace("12:00:00 AM", "") + "<br />";
                    htmlPage += "<div style='page-break-after:avoid;'>";
                    htmlPage += "<b>Residents Name:</b> " + res.FirstName + " " + res.LastName + "<br/>";
                    htmlPage += "<b>Address:</b> " + res.Address + "<br />";
                    //Get the map number graphic
                    var imageName = res.BackColor + res.OneToEight;

                    if (_webHostEnvironment.IsDevelopment())
                    {
                        htmlPage += $"<tr style='height:18px;'><td class='tdNumber'><img src='/Images/{res.BackColor}{res.OneToEight}.png' style='width:18px;' /></td>";
                    }
                    else
                    {
                        htmlPage += $"<tr style='height:18px;'><td class='tdNumber'><img src='https://www.wfwcoutreach.com/Images/{res.BackColor}{res.OneToEight}.png' style='width:18px;' /></td>";
                    }

                    htmlPage += $"<div><span style='font-weight:bold;'>Outreachers:</span><br/>";

                    
                   
                    foreach (var visitationMember in MembersOfVisitation)
                    {
                        htmlPage += $"{visitationMember.Name}<br />";
                    }
                    
                    
                    htmlPage += "</div><br />";

                   
                    htmlPage += "<b>Details: </b>"; 
                    if(visit.DoorHangar)
                    {
                        htmlPage += "Left Door Hanger.<br />";
                    }
                    htmlPage += "<div style='page-break-after:avoid;'>" + visit.VisitationDetails + "</div><br/>";

                    htmlPage += "</div>";
                    htmlPage += "<hr style='color:navy;' />";
                }
            }
            /***********************Acquaintances**************************/

            if (acqCount == 0)
            {
                htmlPage += "<h5>No Acquaintances have been made in this Mission. <br/> Be the first to let these folks know how much Jesus loves them. :)</h5>";
            }
            htmlPage += "<div style='page-break-after:always;'>&nbsp;</div>";



            ///***********************Map**************************/


            htmlPage += "<h3>" + mission.Name + "</h3>";
            htmlPage += "Date Last Completed:" + mission.DateLastCompleted.ToString().Replace("12:00:00 AM", "") + "<br/><br/>";
            htmlPage += "<table style='width:98%;height:95%;'><tr><td style='text-align:center;height:95%;'>";
            if(_webHostEnvironment.IsDevelopment())
            {
                htmlPage += "<img src='/maps/" + mission.Name.Replace(" ", "").Replace("'", "''") + "_map.png" + "' style='height:900px;' />";

            }
            else 
            {
                htmlPage += "<img src='https://www.wfwcoutreach.com/maps/" + mission.Name.Replace(" ", "").Replace("'", "''") + "_map.png" + "' style='height:900px;' />";
            }
            htmlPage += "</td></tr></table>";
            htmlPage += "<div style='page-break-after:always;'>&nbsp;</div>";
            /***********************Map**************************/   


            htmlPage += "<br /><h3>" + mission.Name + "</h3>";
            htmlPage += "Last Date Mission Completed:" + mission.DateLastCompleted.ToString().Replace("12:00:00 AM", "") + "<br/>";
            htmlPage += "<div>Outreacher_______________________________________   Date___________________________</div><br/>";
            /***********************Blank Collection Form**************************/
            htmlPage += "<table style='text-align:left;width:98%;'><tr><td style='width:25%;text-align:left;'>Map Color & Number:</td><td style='text-align:left;'>Address:</td><td style='text-align:left;'>Name:</td></tr>";
            htmlPage += "<tr><td colspan='3'><div style='text-align:left;border:1 solid blue;text-align:left;width:98%;height:400px;'>Details:</div></td></tr></table>";

            htmlPage += "<table style='text-align:left;width:98%;'><tr><td style='width:25%;text-align:left;'>Map Color & Number:</td><td style='width:37%;text-align:left;'>Address:</td><td style='text-align:left;'>Name:</td></tr>";
            htmlPage += "<tr><td colspan='3'><div style='text-align:left;border:1 solid blue;text-align:left;width:98%;height:400px;'>Details:</div></td></tr></table>";
            
            htmlPage += "<table style='text-align:left;width:98%;'><tr><td style='width:25%;text-align:left;'>Color & Number:</td><td style='width:37%;text-align:left;'>Address:</td><td style='text-align:left;'>Name:</td></tr>";
            htmlPage += "<tr><td colspan='3'><div style='text-align:left;border:1 solid blue;text-align:left;width:98%;height:400px;'>Details:</div></td></tr></table>";


            htmlPage += "</body></html>";
            return await ExportToPDF(mission.Name, htmlPage);



            return RedirectToPage("./Assignments", new { churchId = Request.Query["churchId"], missionId = Mission.MissionId });
        }



        public async Task<IActionResult> ExportToPDF(string missionName, string htmlPage)
        {
            missionName = missionName.Replace(" ", "");

            HtmlToPdf converter = new HtmlToPdf();



            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(htmlPage, "https://localhost:7282/");


            var path = $"Content/Missions/{missionName}.pdf";
            // save pdf document
            doc.Save(path);

            // close pdf document
            doc.Close();


            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", $"{missionName}.pdf");


        }



        public bool IsDisabled()
        {
            return Mission.Status == "Complete";
        }


        private bool MissionExists(int id)
        {
            return (_context.Missions?.Any(e => e.MissionId == id)).GetValueOrDefault();
        }
    }
}
