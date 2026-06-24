namespace Patient_Grievance_and_Complaint_Resolution.DTOs.AiInputsandOutputs
{
    public class InvestigatorDto
    {
        public int InvestigatorId { get; set; }
        public string Department { get; set; }
        public int CurrentLoad { get; set; }
        public int? Rating { get; set; }
    }
}
