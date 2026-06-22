using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Service.Interface;

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

        [HttpGet("dashboard/{investigatorId}")]
        public async Task<IActionResult> GetDashboard(
            int investigatorId,
            CancellationToken cancellationToken)
        {
            return await _service.GetDashboardAsync(
                investigatorId,
                cancellationToken);
        }

        [HttpPost("submit-resolution")]
        public async Task<IActionResult> SubmitResolution(
            [FromBody] CreateResolutionDto dto,
            CancellationToken cancellationToken)
        {
            return await _service.SubmitResolutionAsync(
                dto,
                cancellationToken);

        }
        [HttpPatch("close")]
        public async Task<IActionResult> CloseGrievance(
            [FromBody] CreateResolutionDto dto,
            CancellationToken cancellationToken)
        {
            return await _service.SubmitResolutionAsync(
                dto,
                cancellationToken);
        }
        [HttpGet("dashboard-summary/{investigatorId}")]
        public async Task<IActionResult>
GetDashboardSummary(
    int investigatorId,
    CancellationToken cancellationToken)
        {
            return await _service
                .GetDashboardSummaryAsync(
                    investigatorId,
                    cancellationToken);
        }
    }
}
