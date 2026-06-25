using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Authorize(Roles = "Investigator")]
    public class ReportController : ControllerBase
    {
        private readonly IResolutionService _resolutionService;

        public ReportController(IResolutionService resolutionService)
        {
            _resolutionService = resolutionService;
        }

        // GET: api/reports/resolutions/15/download
        [HttpGet("resolutions/{resolutionId:int}/download")]
        public async Task<IActionResult> DownloadResolutionReport(
            int resolutionId,
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

            var pdfBytes = await _resolutionService.DownloadResolutionReportAsync(
                resolutionId,
                userId,
                cancellationToken);

            if (pdfBytes == null)
            {
                return NotFound(new
                {
                    message = "Resolution report not found or access is denied."
                });
            }

            return File(
                pdfBytes,
                "application/pdf",
                $"Grievance_Resolution_Report_{resolutionId}.pdf"
            );
        }
    }
}