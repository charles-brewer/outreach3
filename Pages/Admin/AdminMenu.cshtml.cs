using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using outreach3.Data;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Admin
{
    public class AdminMenuModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public AdminMenuModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public List<Church> Churches = new List<Church>();      

        public void OnGet()
        {
           Churches =  _context.Churches.ToList();
        }
    }
}
