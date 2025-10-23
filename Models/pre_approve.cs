namespace part1_poe.Models
{
    public class pre_approve
    {
        public int ClaimID { get; set; }
        public int NumberOfSessions { get; set; }     
        public int NumberOfHours { get; set; }        
        public decimal Rate { get; set; }             
        public decimal TotalAmount { get; set; }
        public string ModuleName { get; set; } = string.Empty;
        public string FacultyName { get; set; } = string.Empty;
        public string? SupportingDocument { get; set; }
        public string names { get; set; } = string.Empty;

        public string ClaimStatus { get; set; } = string.Empty;
        public DateTime CreatingDate { get; set; }
    }

}
