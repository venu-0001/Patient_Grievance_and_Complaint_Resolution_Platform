namespace Patient_Grievance_and_Complaint_Resolution.DTOs
{
    public class CloseGrievanceResponseDto
    {
        public int GrievanceId { get; set; }

        public string Status { get; set; }

        public DateTime ClosedAt { get; set; }

        public string Message { get; set; }
    }
}
