using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // ✅ GET: api/reports/grievances/{id}
        [HttpGet("grievances/{id}")]
        public async Task<IActionResult> GetGrievanceReport(int id)
        {
            var pdfBytes = await _reportService.GenerateGrievanceReportAsync(id);

            return File(
                pdfBytes,
                "application/pdf",
                "Grievance_Report.pdf"
            );
        }
    }
}