namespace Patient_Grievance_and_Complaint_Resolution.DTOs
{
    public class InvestigatorResolution
    {
        public int ResolutionId { get; set; }

        public string GrievanceNumber { get; set; } = null!;

        public string? RootCause { get; set; }

        public string? ResolutionSummary { get; set; }

        public DateTime ResolvedAt { get; set; }
    }
}
