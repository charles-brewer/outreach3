using System.ComponentModel.DataAnnotations.Schema;

namespace outreach3.Data.Ministries
{
    public class Member
    {

        public int MemberId { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public int ChurchId { get; set; }

        public bool Approved { get; set; }

        [ForeignKey(nameof(MemberId))]
        public List<VisitationsMembers> VisitationMembers { get; set; } = new List<VisitationsMembers>();

    }
}