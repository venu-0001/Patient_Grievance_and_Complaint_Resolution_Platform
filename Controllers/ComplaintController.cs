using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _complaintService;
        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }
        [HttpGet(Name = "PatientComplaintRequest")]
        public async Task<IActionResult> GetPatientGrievances([FromQuery] int PatientId)
        {
            return await _complaintService.GetPatientComplaintAsync(PatientId);
        }
    }
}
