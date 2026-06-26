using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.DTOs;

namespace Patient_Grievance_and_Complaint_Resolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrievanceController : ControllerBase
    {
        private readonly IGrievanceService _grievanceService;

        public GrievanceController(IGrievanceService grievanceService)
        {
            _grievanceService = grievanceService;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitGrievance(
            [FromBody] PatientGrievanceRequest request)
        {
            return await _grievanceService.SubmitGrievanceAsync(request);
        }
        [HttpGet("PatientGrievanceRequest")]
        public async Task<IActionResult> GetPatientGrievances([FromQuery] int PatientId)
        {
            return await _grievanceService.GetPatientGrievanceAsync(PatientId);
        }
    }
}