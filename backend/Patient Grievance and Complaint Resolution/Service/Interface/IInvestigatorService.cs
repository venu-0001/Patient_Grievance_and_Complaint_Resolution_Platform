using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.DTOs;

namespace Patient_Grievance_and_Complaint_Resolution.Service.Interface
{
    public interface IInvestigatorService
    {
        Task<IActionResult> GetDashboardAsync(
            int investigatorId,
            CancellationToken cancellationToken);

        Task<IActionResult> SubmitResolutionAsync(
            CreateResolutionDto dto,
            CancellationToken cancellationToken);
        Task<IActionResult> GetDashboardSummaryAsync(
    int investigatorId,
    CancellationToken cancellationToken);
    }
}
