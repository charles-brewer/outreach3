using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Churches
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private IServiceProvider _serviceProvider;
        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Church> Churches { get; set; } = new List<Church>();
        public bool IsApproved = false;
        public int MemberId = 0;



        public async Task OnGetAsync()
        {
            if (User.IsInRole("Admin"))
            {
                Churches = await _context.GetChurchesAsync();
            }
            else
            {

                var UserName = User.Identity.Name;
                var member = await _context.GetMemberAsync(UserName);
                MemberId = member.MemberId;
                var churchId = member.ChurchId;
                if (churchId != 0)
                {
                    Churches.Add(await _context.GetChurchAsync(churchId));
                }
            }
        }



        public bool ChurchAccessIsAppoved(int churchId, int memberId)
        {
            if (_context.Members.FirstOrDefault(c => c.ChurchId == churchId && c.MemberId == MemberId) == null)
            {

            }
            else
            {
                return _context.Members.FirstOrDefault(c => c.ChurchId == churchId && c.MemberId == memberId).Approved;
            }
            return false;
        }
    }
}
