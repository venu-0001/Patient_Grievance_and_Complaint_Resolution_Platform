using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Services
{
    public class EncountersService : IEncountersService
    {
        private readonly IEncountersRepository _encountersRepository;
        public EncountersService(IEncountersRepository encountersRepository)
        {
            _encountersRepository = encountersRepository;
        }
        public async Task<IActionResult> GetEncounterDates(int patientId)
        {
            var encounterDates = await _encountersRepository.GetEncounterDates(patientId);
            if (encounterDates == null)
            {
                throw new KeyNotFoundException("Encounter Dates Not Found");
            }
            return new OkObjectResult(encounterDates);
        }
    }
}
