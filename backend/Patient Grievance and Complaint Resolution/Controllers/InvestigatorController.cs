using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestigatorController : ControllerBase
    {
        private readonly IResolutionService _service;

        public InvestigatorController(IResolutionService service)
        {
            _service = service;
        }

        [HttpGet("resolutions")]
        public async Task<IActionResult> GetMyResolutions(
    [FromQuery] int investigatorId,   // ✅ explicitly from query (Swagger)
    CancellationToken cancellationToken)
        {
            // ✅ Basic validation
            if (investigatorId <= 0)
            {
                return BadRequest("Invalid investigatorId");
            }

            // ✅ Call service
            var result = await _service.GetMyResolutionsAsync(investigatorId, cancellationToken);

            // ✅ Return response
            return Ok(result);
        }
    }
}
