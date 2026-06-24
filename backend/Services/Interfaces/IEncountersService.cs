using Microsoft.AspNetCore.Mvc;

namespace Patient_Grievance_and_Complaint_Resolution.Services.Interfaces
{
    public interface IEncountersService
    {
        Task<IActionResult> GetEncounterDates(int patientId);
    }
}
