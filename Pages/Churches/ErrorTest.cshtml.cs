using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;

namespace outreach3.Pages.Churches
{
    public class ErrorTestModel : PageModel
    {
        public void OnGet()
        {
            try 
            {
                throw new Exception("Test Exception", new Exception("Test Inner Exception")); 
            }
            catch (Exception ex)
            {
                Response.Redirect("/Errors/404?msg="+ ex.Message);
            }
        }
    }
}
