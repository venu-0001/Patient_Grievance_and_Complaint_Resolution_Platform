using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrievanceController : ControllerBase
    {
        private readonly IResolutionService _service;

        public GrievanceController(IResolutionService service)
        {
            _service = service;
        }

        // ✅ POST: api/grievance
        [HttpPost]
        public async Task<IActionResult> CreateResolution(
    int id,                                 // ✅ grievanceId from route
    [FromQuery] int investigatorId,         // ✅ investigatorId from Swagger
    [FromBody] CreateResolutionRequest request,
    CancellationToken cancellationToken)
        {
            if (investigatorId <= 0)
                return BadRequest("Invalid investigatorId");

            await _service.CreateResolutionAsync(
                id,                   // ✅ grievanceId
                investigatorId,       // ✅ investigatorId
                request,
                cancellationToken);

            return Ok(new
            {
                message = "Resolution submitted successfully"
            });
        }
    }
}