using Azure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace outreach3.Data.Ministries
{
    public class Church
    {

        private int _churchId;
        private string _name = "";
        private string _churchFullName = "";
        private string _churchAddress = "";
        private string _churchPhone = "";
        private string _pastorsName = "";

        public int ChurchId
        {
            get { return _churchId; }
            set { _churchId = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string ChurchFullName
        {
            get { return _churchFullName; }
            set { _churchFullName = value; }
        }

        public string ChurchAddress
        {
            get { return _churchAddress; }
            set { _churchAddress = value; }
        }
        public string ChurchPhone
        {
            get { return _churchPhone; }
            set { _churchPhone = value; }
        }
        public string PastorsName
        {
            get { return _pastorsName; }
            set { _pastorsName = value; }
        }


        public List<Mission>? Missions { get; set; } = new List<Mission>();

        public List<ChurchGoal>? Goals { get; set; } = new List<ChurchGoal>();

    }




}
