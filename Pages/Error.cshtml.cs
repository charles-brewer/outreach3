using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Net.Mail;

namespace outreach3.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        private IWebHostEnvironment _webHostEnvironment;

        public string Error { get; set; }

        public ErrorModel(ILogger<ErrorModel> logger, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var errorDescr = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            var routeValues = this.HttpContext.Features.Get<IExceptionHandlerFeature>().RouteValues.ToList();

            var route = "";
            foreach(var routeValue in routeValues )
            {
                route += routeValue + "/";
            }

           

            if (!_webHostEnvironment.IsDevelopment())
            {
                
                try
                {
                    var status = Activity.Current.StatusDescription;

                    //Initialize WebMail helper
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("postmaster@wfwcoutreach.com");
                    msg.To.Add(new MailAddress("cgbrewer2@comcast.net"));
                    msg.Subject = "Wfwcoutreach Error";
                    msg.Body = $"{RequestId}\n\n{errorDescr.Error.Message}\n\n{route}";
                    
                    SmtpClient smtpClient = new SmtpClient("m06.internetmailserver.net", Convert.ToInt32(587));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("postmaster@wfwcoutreach.com", "Tlimlamsps@&ok100");
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(msg.From.ToString(), "cgbrewer2@comcast.net", msg.Subject.ToString(), msg.Body.ToString());
                
                }
                catch (Exception ex)
                {
                    
                    Error = ex.Message;
                    
                }

            }
          
            
        }
    }
}