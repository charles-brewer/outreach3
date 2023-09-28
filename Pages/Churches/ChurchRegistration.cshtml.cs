using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using outreach3.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Net.Mail;
using outreach3.Data.Ministries;

namespace outreach3.Pages.Churches
{
    public class ChurchRegistration : PageModel
    {
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private IServiceProvider _serviceProvider;
        public ChurchRegistration(ApplicationDbContext context, IServiceProvider serviceProvider, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _webHostEnvironment = webHostEnvironment;
        }

        public IList<Church> Churches { get; set; } = default!;

        public Church Church { get; set; } = new Church();

        public SelectList Members { get; set; }

        public int MemberId = 0;

        public async Task OnGetAsync(string email, string userName)
        {
            //Create the Member
            await Init(email, userName);
        }



        private async Task Init(string email, string userName)
        {

            if (!_context.Members.Any(m => m.Email == email))
            {
                var member = new Member();
                member.Email = email;
                member.Name = userName;
                await _context.Members.AddAsync(member);
                await _context.SaveChangesAsync();
            }


            MemberId = _context.Members.FirstOrDefault(m => m.Email == email).MemberId;
            Churches = _context.Churches.ToList();
        }

        public async Task<IActionResult> OnPostJoinMemberToChurchAsync(int churchId)
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var email = userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Email;
            Init(email, User.Identity.Name);
            Churches = _context.Churches.ToList();
            var joinId = Request.Form["JoinIds"];
            if (joinId != 0)
            {
                Church = _context.Churches.FirstOrDefault(c => c.ChurchId == Convert.ToInt32(joinId));
                _context.Members.FirstOrDefault(m => m.Email == email).ChurchId = Convert.ToInt32(joinId);
                await _context.SaveChangesAsync();
            }

            await SendEmail(email, $"Outreach Ministry", $"Thank you for joining the Outreach Ministry team for {Church.ChurchFullName}.  " +
                $"Please contact your Church Administrator if you have any questions.  If you are uncertain who that is, " +
                $"please send inquiry to <a href='mailto:cgbrewer2@comcast.net'> Outreach Missions</a>.<br /><br />Thank you.");

            return RedirectToPage("../Churches/Index");
        }


        [BindProperty]
        public Church ChurchForCreation { get; set; } = default!;
        public async Task<IActionResult> OnPostCreateChurchAsync()
        {
            _context.Churches.Add(ChurchForCreation);

            await _context.SaveChangesAsync();

            var userManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var email = userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Email;


            await SendEmail(email, "Outreach3 Ministry Church Registration", $"Thank you for creating a church in the Outreach Ministry site for {ChurchForCreation.ChurchFullName}.  " +
                "Please allow up to 72 hours prior to church access becoming avalable.<br />" +
                $"Please send any inquiries to <a href='mailto:cgbrewer2@comcast.net'> Outreach Missions</a>.<br /><br />Thank you.");

            await SendEmail("cgbrewer2@comcast.net", "Outreach Ministry Church Creation.", $"{email} has created the church {ChurchForCreation.ChurchFullName}.");





            return RedirectToPage("../Churches/Create");
        }

        public async Task<bool> SendEmail(string to, string subject, string body)
        {
            if (!_webHostEnvironment.IsDevelopment())
            {
                try
                {
                    var status = Activity.Current.StatusDescription;

                    //Initialize WebMail helper
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("postmaster@wfwcoutreach.com");
                    msg.To.Add(new MailAddress(to));
                    msg.Subject = subject;
                    msg.Body = body;

                    SmtpClient smtpClient = new SmtpClient("m06.internetmailserver.net", Convert.ToInt32(587));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("postmaster@wfwcoutreach.com", "Tlimlamsps@&ok100");
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.SendAsync(msg.From.ToString(), msg.To.ToString(), msg.Subject.ToString(), msg.Body.ToString(), null);
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
