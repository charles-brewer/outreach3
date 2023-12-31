﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using outreach3.Data;
using outreach3.Data.Ministries;

namespace outreach3.Pages.FollowUps
{
    public class CreateModel : PageModel
    {
        private readonly outreach3.Data.ApplicationDbContext _context;

        public CreateModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        
        public int ResidentId { get; set; } = 0;
        private int _missionId=0;
        public FollowUpStatus Status { get; set; } = FollowUpStatus.Scheduled;
        public IActionResult OnGet(int residentId, int missionId)
        {
            ResidentId = residentId;
            _missionId = missionId;
            return Page();
        }

        [BindProperty]
        public FollowUp FollowUp { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostSaveAsync(int residentId)
        {
          if (!ModelState.IsValid || _context.FollowUp == null || FollowUp == null)
            {
                return Page();
            }
            
            FollowUp.ResidentId = residentId;

            _context.FollowUp.Add(FollowUp);

            await _context.SaveChangesAsync();


            var res = _context.Residents.FirstOrDefault(r=>r.ResidentId == residentId);
            res.FollowUpId = FollowUp.FollowUpId;
            await _context.SaveChangesAsync();

            return RedirectToPage("./UpdateStatus", new { residentId = residentId, missionId = Request.Query["missionId"] });
        }
    }
}
