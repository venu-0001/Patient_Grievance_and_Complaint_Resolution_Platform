using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interface;
using Patient_Grievance_and_Complaint_Resolution.Service.Interface;

namespace Patient_Grievance_and_Complaint_Resolution.Service
{
    public class InvestigatorService : IInvestigatorService
    {
        private readonly IInvestigatorRepository _repository;
        private readonly ILogger<InvestigatorService> _logger;

        public InvestigatorService(
            IInvestigatorRepository repository,
            ILogger<InvestigatorService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IActionResult> GetDashboardAsync(
    int investigatorId,
    CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Fetching dashboard for investigator {Id}",
                investigatorId);

            var dashboardData =
                await _repository.GetDashboardDataAsync(
                    investigatorId,
                    cancellationToken);

            if (dashboardData == null || !dashboardData.Any())
            {
                return new NotFoundObjectResult(
                    "No assignments found");
            }

            return new OkObjectResult(dashboardData);
        }

        public async Task<IActionResult> SubmitResolutionAsync(
    CreateResolutionDto dto,
    CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Submitting resolution for grievance {Id}",
                dto.GrievanceId);

            var grievance =
                await _repository.GetGrievanceByIdAsync(
                    dto.GrievanceId,
                    cancellationToken);

            if (grievance == null)
            {
                return new NotFoundObjectResult(
                    "Grievance not found.");
            }

            var existingResolution =
                await _repository.GetResolutionByGrievanceIdAsync(
                    dto.GrievanceId,
                    cancellationToken);

            if (existingResolution != null)
            {
                return new BadRequestObjectResult(
                    "Resolution already submitted.");
            }

            var assignment =
                await _repository.GetAssignmentByGrievanceIdAsync(
                    dto.GrievanceId,
                    cancellationToken);

            if (assignment == null)
            {
                return new NotFoundObjectResult(
                    "Assignment not found.");
            }

            var closedStatusId =
                await _repository.GetStatusIdByNameAsync(
                    "Closed",
                    cancellationToken);

            if (closedStatusId == 0)
            {
                return new NotFoundObjectResult(
                    "Closed status not configured.");
            }

            var resolution = new Resolution
            {
                GrievanceId = dto.GrievanceId,
                InvestigatorId = dto.InvestigatorId,
                PreventiveAction = dto.PreventiveAction,
                CorrectiveAction = dto.CorrectiveAction,
                ResolvedAt = DateTime.UtcNow
            };

            await _repository.AddResolutionAsync(
                resolution,
                cancellationToken);

            grievance.StatusId = closedStatusId;
            grievance.ClosedAt = DateTime.UtcNow;

            assignment.CompletedAt = DateTime.UtcNow;

            await _repository.SaveChangesAsync(
                cancellationToken);

            _logger.LogInformation(
                "Grievance {Id} closed successfully",
                dto.GrievanceId);

            return new OkObjectResult(new
            {
                Success = true,
                GrievanceId = dto.GrievanceId,
                Status = "Closed",
                Message = "Grievance Closed Successfully"
            });

        }
        public async Task<IActionResult>
GetDashboardSummaryAsync(
    int investigatorId,
    CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Fetching dashboard summary for investigator {Id}",
                investigatorId);

            var summary =
                await _repository.GetDashboardSummaryAsync(
                    investigatorId,
                    cancellationToken);

            if (summary == null)
            {
                return new NotFoundObjectResult(
                    "No dashboard data found");
            }

            return new OkObjectResult(summary);
        }
    }

}
