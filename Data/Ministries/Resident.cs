using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace outreach3.Data.Ministries
{
    public class Resident
    {

        private int residentId;
        private string address;
        private string firstName;
        private string lastName;
        private bool isMember;
        private string _apt = "";
        private string mapId;
        private string homePhone = "";
        private string foreColor = "";
        private string backColor = "";




        [Key]
        public int ResidentId
        {
            get { return residentId; }
            set { residentId = value; }
        }

        public int MissionId { get; set; }

        public virtual Mission Mission { get; set; } = null!;   

        public string? HomePhone { get => homePhone; set => homePhone = value; }

        public string? Address { get => address; set => address = value; }

        public bool IsProtected { get; set; }

        public string? Apt { get => _apt; set => _apt = value; }

        public string? FirstName { get => firstName; set => firstName = value; }
        public string? LastName { get => lastName; set => lastName = value; }

        public string? ForeColor { get => foreColor; set => foreColor = value; }

        public string? BackColor { get => backColor; set => backColor = value; }

        public string TitlesOfMissions { get; set; } = "";
        public int NumberOfMissions { get; set; }


        private int _number;
        public int number
        {
            get { return _number; }
            set { _number = value; }
        }

        private int _oneToEight;
        public int OneToEight
        {
            get { return _oneToEight; }
            set { _oneToEight = value; }
        }

       
        public ICollection<Visitation>? Visitations { get; set; } = new List<Visitation>();
        public int? FollowUpId { get; set; }



    }
   
}