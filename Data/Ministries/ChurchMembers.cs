using Microsoft.EntityFrameworkCore;

namespace outreach3.Data.Ministries
{

    //This many to many is used for email notifications
    [PrimaryKey(nameof(ChurchId), nameof(MemberId))]
    public class ChurchMembers
    {

        public int ChurchId { get; set; }
        public Church? Church { get; set; }
        public int MemberId { get; set; }
        public Member? Member { get; set; }
    }
}
