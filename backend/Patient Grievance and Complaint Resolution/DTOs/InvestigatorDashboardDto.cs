namespace Patient_Grievance_and_Complaint_Resolution.DTOs
{
    public class InvestigatorDashboardDto
    {
        public int GrievanceId { get; set; }

        public string GrievanceNumber { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public string Severity { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime? DueDate { get; set; }
    }
}
