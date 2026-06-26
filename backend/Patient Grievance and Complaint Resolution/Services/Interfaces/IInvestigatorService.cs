using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.DTOs;

namespace Patient_Grievance_and_Complaint_Resolution.Services.Interfaces
{
    public interface IInvestigatorService
    {
        Task<IActionResult> GetDashboardAsync(
            int UserId,
            CancellationToken cancellationToken);

        Task<IActionResult> SubmitResolutionAsync(
    CreateResolutionDto dto,
    int userId,
    CancellationToken cancellationToken);
        Task<IActionResult> GetDashboardSummaryAsync(
    int UserId,
    CancellationToken cancellationToken);
    }
}
