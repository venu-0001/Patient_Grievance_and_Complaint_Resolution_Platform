using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminGrievanceController : ControllerBase
    {
        private readonly IComplaintService _service;

        public AdminGrievanceController(IComplaintService service)
        {
            _service = service;
        }

        // ✅ GET: api/admin/complaints
        [HttpGet]
        public async Task<IActionResult> GetAllComplaints(
            CancellationToken cancellationToken)
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }
    }
}