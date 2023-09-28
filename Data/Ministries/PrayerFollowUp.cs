using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace outreach3.Data.Ministries
{
    public class PrayerFollowUp
    {
        private int _prayerFollowUpId;
        private int _prayerRequestId;

        public int PrayerFollowUpId { get => _prayerFollowUpId; set => _prayerFollowUpId = value; }

        public int PrayerRequestId { get => _prayerRequestId; set => _prayerRequestId = value; }

        public string FollowUpText { get; set; }

        public DateTime FollowUpDate { get; set; }

        public string UserName { get; set; }





    }
}