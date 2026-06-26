using Microsoft.AspNetCore.Mvc;

namespace Patient_Grievance_and_Complaint_Resolution.Services.Interfaces
{
    public interface IComplaintService
    {
        Task<IActionResult> GetPatientComplaintAsync(int patientId);
    }
}
