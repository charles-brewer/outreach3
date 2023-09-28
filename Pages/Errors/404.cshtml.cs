using Microsoft.AspNetCore.Mvc.RazorPages;


namespace outreach3.Pages.Errors
{
    public class _404Model : PageModel
    {

        public string ErrorMessage { get; set; } = "Default Error Message";


        public async Task OnGetAsync()
        {
          
            var  msg = Request.Query["msg"];
            ErrorMessage = msg;


            try
            {
                // Initialize WebMail helper
                //WebMail.SmtpServer = "SMTP.comcast.net";
                //WebMail.SmtpPort = 25;
                //WebMail.UserName = "Charlie";
                //WebMail.Password = "Tlimlamsps@&ok100";
                //WebMail.From = "cgbrewwer2@comcast.net";

                //// Send email
                //WebMail.Send(to: "cgbrewer2@comcast.net",
                //    subject: "Error from - Outrach",
                //    body: ErrorMessage
                //);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
            }
        }

    }
}
