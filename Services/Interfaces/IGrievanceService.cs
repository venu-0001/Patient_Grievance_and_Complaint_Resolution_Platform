using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.DTOs;

public interface IGrievanceService
{
    Task<IActionResult> SubmitGrievanceAsync(PatientGrievanceRequest request);
    Task<IActionResult> GetPatientGrievanceAsync(int PatientId);
}