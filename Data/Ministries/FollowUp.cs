using System.ComponentModel.DataAnnotations.Schema;

namespace outreach3.Data.Ministries
{
    public class FollowUp
    {
        public int FollowUpId { get; set; }
        public int ResidentId { get; set; }
        public DateTime DateDue { get; set; }
        public FollowUpStatus Status { get; set; } = FollowUpStatus.Sheduled;
        public Member? Member { get; set; }
    }

    public enum FollowUpStatus
    {
        Sheduled,
        Due,
        PastDue,
        Cancelled,
        Complete
    }

}
