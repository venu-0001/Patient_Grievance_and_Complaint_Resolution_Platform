namespace Patient_Grievance_and_Complaint_Resolution.DTOs
{
    public class CreateResolutionDto
    {
        public int GrievanceId { get; set; }

        public int InvestigatorId { get; set; }

        public string PreventiveAction { get; set; }

        public string CorrectiveAction { get; set; }
    }
}
