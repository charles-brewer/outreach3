using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using outreach3.Data;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Churches
{
    public class CreateModel : PageModel
    {



        private readonly ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Church Church { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Churches == null || Church == null)
            {
                return Page();
            }

            _context.Churches.Add(Church);

            await _context.SaveChangesAsync();
            var memberId = Request.Query["memberId"].FirstOrDefault();

            if (memberId != null)
            {
                _context.Members.FirstOrDefault(m => m.MemberId == Convert.ToInt32(memberId)).ChurchId = Church.ChurchId;
                await _context.SaveChangesAsync();
            }


            return RedirectToPage("./Index");
        }
    }
}
