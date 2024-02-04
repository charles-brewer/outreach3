
using Azure;
using Coravel.Invocable;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using outreach3.Areas.Identity.Pages.Account.Manage;
using outreach3.Pages.ChurchGoals;
using SelectPdf;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Mail;
using System.Reflection;
using static System.Net.WebRequestMethods;

namespace outreach3.Data.Ministries
{
    public class SendWeeklyEmailReports : IInvocable

    {
        public string ChartJson { get; set; }

        public string ChurchName { get; set; }
        public int ChurchId { get; set; }
        public string GetLabels { get; set; }
        public string GetVisited { get; set; }
        public string GetGreetedGoal { get; set; }
        public string GetGreeted { get; set; }
        public string GetGoalline { get; set; }


        private ApplicationDbContext _context;
        private string _html = "";
        public SendWeeklyEmailReports(ApplicationDbContext context)
        {
            _context = context;





        }


        public async Task Invoke()
        {
            Debug.WriteLine("Report Started....");
            var goals = new List<ChurchGoal>();
            
            var churchIds = new List<int>();
            var memberIds = new List<int>();
            var emails = new List<(string, int)>();
            foreach (var church in _context.Churches)
            {
                churchIds.Add(church.ChurchId);
                 
            }

            foreach (var id in churchIds)
            {
                foreach (var memberId in _context.ChurchMembers.Where(m => m.ChurchId == id))
                {
                    memberIds.Add(memberId.MemberId);
                }
            }

            
            foreach (var memberId in memberIds)
            {
                foreach (var email in _context.Members.Where(m => m.MemberId == memberId))
                {
                    emails.Add((email.Email, email.ChurchId));
                    ChurchId = email.ChurchId;
                }
            }


            goals = GetGoals(ChurchId);
           


            foreach (var email in emails)
            {
                var chartUrls = GetChartUrls(goals);

                var message = GetMessageBody(goals, chartUrls);


                if (string.IsNullOrEmpty(email.Item1))
                {
                    continue;
                }
                
                goals = GetGoals(email.Item2);

                try
                {
                    var status = Activity.Current?.StatusDescription;

                    //Initialize WebMail helper
                    MailMessage msg = new MailMessage();
                    msg.IsBodyHtml = true;
                    msg.From = new MailAddress("postmaster@wfwcoutreach.com");
                    msg.To.Add(new MailAddress(email.Item1));
                    msg.Subject = "Weekly Outreach Report";

                    msg.BodyEncoding = System.Text.Encoding.Default;
                    msg.Body = message;
                    msg.IsBodyHtml = true;


                    SmtpClient smtpClient = new SmtpClient("m06.internetmailserver.net", Convert.ToInt32(587));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("postmaster@wfwcoutreach.com", "Tlimlamsps@&ok100");
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.SendAsync(msg, null);


                }
                catch (Exception Ex) { Debug.Write("________________________________________" + Ex.Message); }



            }
            Debug.WriteLine($"Report was sent at {DateTime.UtcNow}.");
        }

        private string GetMessageBody(List<ChurchGoal> goals, List<string> chartUrls)
        {
            var html = "<!DOCTYPE html><html><head><title></title></head><body style='font-size:12pt;font-family:comic-sans;'>";
            html += "<h2>Outreach Ministry Weekly Progress Reports</h2>";
            for (var i= 0;i < goals.Count; i++)
            {
                html += $"<div style='font-size:16pt;font-weight:bold;text-decoration:underline;'>Goal: {goals[i].Name}</div>";
                html += $"<div style='font-size:10pt;'>({goals[i].StartDate} - {goals[i].GoalDate})</div>";
                html += $"<div>Number of Doors Goal: {goals[i].NumberOfDoorsGoal}</div>";
                html += $"<div>Completed Doors: {goals[i].NumberOfDoors}</div>";
                html += $"<div>Number of Contacts Goal: {goals[i].NumberOfGreetsGoal}</div>";
                html += $"<div>Contacts Made: {goals[i].NumberOfDoors}</div>";

                html += $"<div style='font-size:10pt;'><img src=\"{chartUrls[i]}\" /></div>";
                html += $"<br /><br />";
            }   
            html += "</body></html>";



            return html;
        }

      
        private List<ChurchGoal> GetGoals(int churchId)
        {
           
            var church = _context.Churches.FirstOrDefault(c=>c.ChurchId == churchId);

            var churchGoals = _context.ChurchGoals.Where(g=>g.ChurchId==churchId).ToList();  

            return churchGoals;
        }

        private List<string> GetChartUrls(List<ChurchGoal> goals)
        {
           
            var chartData = new List<string>();

            foreach (var churchgoal in goals)
            {
                var goal = new Goals(_context);
                

                var g = churchgoal;
                goal.GetGoal(g);
                GetLabels = goal.GetLabels;
                GetLabels = goal.GetLabels;
                GetVisited = goal.GetVisited;
                GetGreetedGoal = goal.GetGreetedGoal;
                GetGreeted = goal.GetGreeted;
                GetGoalline = goal.GetGoalline;

                var html = "";             
                html += "{type: 'line',data: { labels: " + goal.GetLabels + ",  datasets: ";
                html += "[{ label: 'Goal Line', data: " + goal.GetGoalline + ", borderWidth: 1, pointRadius: 1,borderColor:'Navy' }, ";
                html += "{ label: 'Visited', data: " + goal.GetVisited + ", borderWidth: 2, pointRadius: 1, borderColor: 'SkyBlue' },";
                html += "{ label: 'Greeted', data: " + goal.GetGreeted + ", borderWidth: 2, pointRadius: 1, borderColor:'tan'},";
                html += "{ label: 'Greeted Goal Line', data: " + goal.GetGreetedGoal + ", borderWidth: 1, pointRadius: 1, borderColor: 'green' }]}}";
                
                chartData.Add("https://quickchart.io/chart?height=200&c=" + html);
            }

            return chartData;
        }
    }

  
}
