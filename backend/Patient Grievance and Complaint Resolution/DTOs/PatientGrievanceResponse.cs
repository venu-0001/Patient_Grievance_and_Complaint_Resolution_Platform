namespace Patient_Grievance_and_Complaint_Resolution.DTOs
{
    public class PatientGrievanceResponse
    {

        public string GrievanceNumber { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Department { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? DueDate { get; set; }

    }
}
