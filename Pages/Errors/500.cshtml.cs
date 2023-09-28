using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;

namespace outreach3.Pages.Errors
{
    public class _500Model : PageModel
    {

        public string ErrorMessage { get; set; } = "Default Error Message";


        public async Task OnGetAsync()
        {
           
            ErrorMessage = "500";           

        }

    }
}
