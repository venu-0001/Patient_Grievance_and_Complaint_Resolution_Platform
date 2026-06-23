using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Service.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Patient_Grievance_and_Complaint_Resolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestigatorController : ControllerBase
    {
        private readonly IInvestigatorService _service;

        public InvestigatorController(
            IInvestigatorService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Investigator")]
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard(
    CancellationToken cancellationToken)
        {
            var userIdClaim =
                User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim);

            return await _service.GetDashboardAsync(
                userId,
                cancellationToken);
        }

        [Authorize(Roles = "Investigator")]
        [HttpPost("submit-resolution")]
        public async Task<IActionResult> SubmitResolution(
    [FromBody] CreateResolutionDto dto,
    CancellationToken cancellationToken)
        {
            var userIdClaim =
                User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized();
            }

            dto.InvestigatorId = int.Parse(userIdClaim);

            return await _service.SubmitResolutionAsync(
                dto,
                cancellationToken);
        }
        [Authorize(Roles = "Investigator")]
        [HttpPatch("close")]
        public async Task<IActionResult> CloseGrievance(
    [FromBody] CreateResolutionDto dto,
    CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized();
            }

            dto.InvestigatorId = int.Parse(userIdClaim);

            return await _service.SubmitResolutionAsync(
                dto,
                cancellationToken);
        }
        [Authorize(Roles = "Investigator")]
        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> GetDashboardSummary(
    CancellationToken cancellationToken)
        {
            var userIdClaim =
                User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim);

            return await _service.GetDashboardSummaryAsync(
                userId,
                cancellationToken);
        }
    }
}
