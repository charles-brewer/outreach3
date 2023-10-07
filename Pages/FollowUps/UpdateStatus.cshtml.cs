using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using outreach3.Data.Ministries;

namespace outreach3.Pages.FollowUps
{
    public class UpdateStatusModel : PageModel
    {

        private readonly outreach3.Data.ApplicationDbContext _context;

        public UpdateStatusModel(outreach3.Data.ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> OnGet(int churchId, int missionId)
        {
            var idList = new List<(int, int?)>();

            foreach (var resident in _context.Residents.Where(r => r.MissionId == missionId))
            {
                if (resident.FollowUpId != null)
                {
                    idList.Add((resident.ResidentId, resident.FollowUpId));
                }
            }

            foreach (var id in idList)
            {
                var status = _context.FollowUp.FirstOrDefault(f => f.FollowUpId == id.Item2).Status;
                if (status == FollowUpStatus.Cancelled || status == FollowUpStatus.Complete)
                {
                    continue;
                }
                else
                {
                    var followUp = _context.FollowUp.FirstOrDefault(f => f.FollowUpId == id.Item2);
                    var due = DateOnly.FromDateTime(followUp.DateDue);
                    var today = DateOnly.FromDateTime(DateTime.Now);

                    var diff = due.DayNumber - today.DayNumber;

                    if (diff > 1)
                    {
                        _context.FollowUp.FirstOrDefault(f => f.FollowUpId == followUp.FollowUpId).Status = FollowUpStatus.Scheduled;

                    }
                    if (diff <= 1)
                    {
                        _context.FollowUp.FirstOrDefault(f => f.FollowUpId == followUp.FollowUpId).Status = FollowUpStatus.Due;
                    }

                    if (diff < 0)
                    {
                        _context.FollowUp.FirstOrDefault(f => f.FollowUpId == followUp.FollowUpId).Status = FollowUpStatus.PastDue;

                    }

                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToPage("../Residents/Index", new { churchId = churchId, missionId = missionId });
        }
    }
}
