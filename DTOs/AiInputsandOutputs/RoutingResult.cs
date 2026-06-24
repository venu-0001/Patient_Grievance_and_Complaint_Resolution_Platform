using System.Text.Json.Serialization;

namespace Patient_Grievance_and_Complaint_Resolution.DTOs.AiInputsandOutputs
{
    public class RoutingResult
    {
        [JsonPropertyName("RecordType")]
        public string RecordType { get; set; } // Will be exactly "Complaint" or "Grievance"

        [JsonPropertyName("Department")]
        public string Department { get; set; }

        [JsonPropertyName("Severity")]
        public string Severity { get; set; }

        [JsonPropertyName("AssignedInvestigatorId")]
        public int? AssignedInvestigatorId { get; set; } // Null or 0 if it's just a Complaint

        [JsonPropertyName("ClassificationReason")]
        public string ClassificationReason { get; set; }
        [JsonPropertyName("PatientIssueOneliner")]
        public string PatientIssueOneliner { get; set; }
        [JsonPropertyName("RefferedGrievanceNumber")]
        public string? RefferedGrievanceNumber { get; set; }
    }
}
