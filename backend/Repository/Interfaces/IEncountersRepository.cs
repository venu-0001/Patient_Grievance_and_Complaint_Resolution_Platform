using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.Models;

namespace Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces
{
    public interface IEncountersRepository
    {
        Task<List<DateTime>> GetEncounterDates(int patientId);

    }
}
