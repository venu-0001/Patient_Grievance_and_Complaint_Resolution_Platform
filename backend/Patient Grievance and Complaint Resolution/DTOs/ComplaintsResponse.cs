namespace Patient_Grievance_and_Complaint_Resolution.DTOs
{
    public class ComplaintsResponse
    {
        public int ComplaintId { get; set; }
        public string Mrn { get; set; }
        public int EncounterId { get; set; }
        public string category_name { get; set; }
        public string Department { get; set; }
        public string status { get; set; }
        public string Description { get; set; }
        
    }
}
