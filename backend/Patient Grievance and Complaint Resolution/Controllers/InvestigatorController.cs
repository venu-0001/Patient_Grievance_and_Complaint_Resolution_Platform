using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;
using System.Security.Claims;

namespace Patient_Grievance_and_Complaint_Resolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Investigator")]
    public class InvestigatorController : ControllerBase
    {
        private readonly IResolutionService _service;

        public InvestigatorController(IResolutionService service)
        {
            _service = service;
        }

        [HttpGet("resolutions")]
        public async Task<IActionResult> GetMyResolutions(
            CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new
                {
                    message = "Invalid or missing UserId in token."
                });
            }

            var result = await _service.GetMyResolutionsAsync(
                userId,
                cancellationToken);

            return Ok(result);
        }
    }
}