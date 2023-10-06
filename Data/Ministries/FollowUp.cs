using System.ComponentModel.DataAnnotations.Schema;

namespace outreach3.Data.Ministries
{
    public class FollowUp
    {
        public int FollowUpId { get; set; }
        public int ResidentId { get; set; }
        public DateTime DateDue { get; set; }
        public FollowUpStatus Status { get; set; } = FollowUpStatus.None;
        public Member? Member { get; set; }
    }

    public enum FollowUpStatus
    {
        None = 1,
        Scheduled = 2,
        Due = 3,
        PastDue = 4,
        Cancelled = 5,
        Complete = 6
    }

}
