using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace outreach3.Data.Ministries
{
    public class MissionHistory
    {
        private int missionHistoryId;
        private int missionId;
        private DateTime? assignedDate = new DateTime();
        private DateTime? dateCompleted = new DateTime();
        private string assignedTo = "";

        [Key]
        public int MissionHistoryId
        {
            get { return missionHistoryId; }
            set { missionHistoryId = value; }
        }

        public int MissionId
        {
            get { return missionId; }
            set { missionId = value; }
        }

        [Column(TypeName = "date")]
        public DateTime? AssignedDate { get => assignedDate; set => assignedDate = value; }

        [Column(TypeName = "date")]
        public DateTime? DateCompleted { get => dateCompleted; set => dateCompleted = value; }

        public string AssignedTo { get => assignedTo; set => assignedTo = value; }

    }
}