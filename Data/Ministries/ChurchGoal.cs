namespace outreach3.Data.Ministries
{
    public class ChurchGoal
    {
        private int _churchId;
        public ChurchGoal() 
        {
        
        }

        public string Name { get; set; }

        public int ChurchGoalId
        {
            get;set;
        }
        public int ChurchId
        {
            get { return _churchId; }
            set { _churchId = value; }
        }

        public DateTime StartDate { get; set; }

        public DateTime GoalDate { get; set; }




        public int NumberOfConnections { get; set; }

        //public int NumberOfDoors { get; set; }

        //public int NumberOfResidentsInGoal { get; set; }

        //public int NumberOfDoorHangersLeft { get; set; }

        public int NumberOfDoorsGoal { get; set; }

        public int NumberOfGreetsGoal { get; set; }
        //public int NumberOfConnectionsMade { get; set; }


    }

   
}
