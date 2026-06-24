using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncountersController : ControllerBase
    {
        private readonly IEncountersService _encountersService;
        public EncountersController(IEncountersService encountersService)
        {
            _encountersService = encountersService;
        }
        [HttpGet]
        public async Task<IActionResult> GetEncounterDates([FromQuery] int patientId)
        {
            return await _encountersService.GetEncounterDates(patientId);
        }
    }
}
