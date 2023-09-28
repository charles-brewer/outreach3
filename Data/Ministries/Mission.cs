
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace outreach3.Data.Ministries
{
    public class Mission
    {
        private int missionId;
        private string _name;
        private string _filename;
        private string mapFileName;
        private string status;
        private DateTime? dueDate = new DateTime();
        private DateTime? assignedDate = new DateTime();
        private DateTime? dateLastCompleted = new DateTime();
        private string assignedTo = "0";
        private List<Resident> missionHistories = new List<Resident>();
        private string mapAddress = "#";
        private bool _noSoliciting = false;



        [Key]
        public int MissionId
        {
            get { return missionId; }
            set { missionId = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public string Status { get => status; set => status = value; }

        public string? AssignedTo { get => assignedTo; set => assignedTo = value; }

        public virtual List<Resident> Residents { get; set; }

        public virtual MissionMap MissionMap { get; set; } = new MissionMap();

        public int MissionMapId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateLastCompleted { get => dateLastCompleted; set => dateLastCompleted = value; }

        [DataType(DataType.Date)]
        public DateTime? DateAssigned { get => assignedDate; set => assignedDate = value; }


        public int ChurchId { get; set; }



    }
}