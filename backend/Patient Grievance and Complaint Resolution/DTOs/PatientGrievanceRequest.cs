namespace Patient_Grievance_and_Complaint_Resolution.DTOs
{
    public class PatientGrievanceRequest
    {
        public string Email { get; set; } = null!;

        public DateTime EncounterDate { get; set; }

        public string Category { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
