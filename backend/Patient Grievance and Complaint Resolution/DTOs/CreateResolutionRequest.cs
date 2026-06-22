using System.ComponentModel.DataAnnotations;

namespace Patient_Grievance_and_Complaint_Resolution.DTOs
{
    public class CreateResolutionRequest
    {
        [Required]
        public string RootCause { get; set; } = null!;

        [Required]
        public string CorrectiveAction { get; set; } = null!;

        public string? PreventiveAction { get; set; }

        [Required]
        public string ResolutionSummary { get; set; } = null!;

        [Required]
        public int ApprovedByAdminId { get; set; }
    }
}