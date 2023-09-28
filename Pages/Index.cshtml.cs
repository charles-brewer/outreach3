using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using outreach3.Data;
using System.IO;
using System.Security.Policy;

namespace outreach3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly outreach3.Data.ApplicationDbContext _context;
        private readonly ServiceProvider _serviceProvider;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;            
        }
        public string InputValue="Help Document";
        public void OnGet()
        {
           

        }

        public async Task<IActionResult> OnPostDownloadPDFAsync()
        {
            InputValue = "Downloading...";
            byte[] bytes = await new System.Net.WebClient().DownloadDataTaskAsync(new Uri("https://wfwcoutreach.com/documentation/OutreachMinistries.pdf"));
            InputValue = "Help Document";
            return File(bytes, "application/octet-stream", "OutreachMinistries.pdf");
        }
    }
}