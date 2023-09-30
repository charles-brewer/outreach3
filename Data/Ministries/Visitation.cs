using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace outreach3.Data.Ministries
{
    public class Visitation
    {

        public int VisitationId { get; set; }

        public DateTime VisitationDate { get; set; }

        public int ResidentId { get; set; }

        public int ChurchId { get; set; }
        public string? VisitationDetails { get; set; }

        public bool DoorHangar { get; set; } = false;

        public TypeVisit VisitationType { get; set; } = TypeVisit.None;

        public List<VisitationsMembers> VisitationMembers { get; set; } = new List<VisitationsMembers>();
    }
    public enum TypeVisit
    {
        DoorHanger,
        Greeted,
        None
    }


}