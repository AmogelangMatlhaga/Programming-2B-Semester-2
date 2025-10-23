namespace part1_poe.Models
{
    public class view_claims
    {
        public int ClaimID { get; set; }
        public int NumberOfSessions { get; set; }
        public int NumberOfHours { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ModuleName { get; set; }
        public string FacultyName { get; set; }
        public string SupportingDocument { get; set; }
        public string ClaimStatus { get; set; }
        public DateTime CreatingDate { get; set; }

        public view_claims() { }

        public view_claims(int claimID, int sessions, int hours, decimal rate, decimal totalAmount, string module, string faculty, string document, string status, DateTime date)
        {
            ClaimID = claimID;
            NumberOfSessions = sessions;
            NumberOfHours = hours;
            Rate = rate;
            TotalAmount = totalAmount;
            ModuleName = module;
            FacultyName = faculty;
            SupportingDocument = document;
            ClaimStatus = status;
            CreatingDate = date;
        }
    }
}
