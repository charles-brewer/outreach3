

using Microsoft.EntityFrameworkCore;

namespace outreach3.Data.Ministries
{
    [PrimaryKey(nameof(MemberId), nameof(VisitationId))]
    public class VisitationsMembers
    {

        public int VisitationId { get; set; }
        public Visitation Visitation { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}
