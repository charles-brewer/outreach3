using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace outreach3.Data.Ministries
{
    public class PrayerRequest
    {
        private int _prayerRequestId;
        private int _residentId;
        private int _ministryId;
        private string _status;
        private string _title;
        private DateTime _requestDate;
        //private List<ApplicationUser> _members;
        private List<PrayerFollowUp> _followUps;
        private string _request;
        private int _acquaintanceId;

        public int PrayerRequestId { get => _prayerRequestId; set => _prayerRequestId = value; }


        public int ResidentId { get => _residentId; set => _residentId = value; }

        public int AcquaintanceId { get => _acquaintanceId; set => _acquaintanceId = value; }


        public int MinistryId { get => _ministryId; set => _ministryId = value; }


        public string Status { get => _status; set => _status = value; }

        public string Title { get => _title; set => _title = value; }

        public DateTime RequestDate { get => _requestDate; set => _requestDate = value; }

        // public virtual List<ApplicationUser> Members { get => _members; set => _members = value; }

        public virtual List<PrayerFollowUp> FollowUps { get => _followUps; set => _followUps = value; }

        public string Request { get => _request; set => _request = value; }


    }


}