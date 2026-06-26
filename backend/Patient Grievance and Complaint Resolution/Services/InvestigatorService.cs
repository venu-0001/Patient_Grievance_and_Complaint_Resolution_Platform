using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.services
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
    int userId,
    CancellationToken cancellationToken)
        {
            var investigatorId =await  _repository.GetInvestigatorByUserIdAsync(userId,cancellationToken);
            if (!investigatorId.HasValue)
            {
                return new NotFoundObjectResult(new
                {
                    message = "No investigator profile is linked to this user."
                });
            }

            var dashboardData =
                await _repository.GetDashboardDataAsync(
                    investigatorId??0,
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
    int userId,
    CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Submitting resolution for grievance {GrievanceId} by user {UserId}",
                dto.GrievanceId,
                userId);

            // Convert JWT UserId -> InvestigatorId.
            var investigatorId = await _repository.GetInvestigatorByUserIdAsync(
                userId,
                cancellationToken);

            if (!investigatorId.HasValue)
            {
                return new NotFoundObjectResult(new
                {
                    message = "No investigator profile is linked to this user."
                });
            }

            var grievance = await _repository.GetGrievanceByIdAsync(
                dto.GrievanceId,
                cancellationToken);

            if (grievance == null)
            {
                return new NotFoundObjectResult(new
                {
                    message = "Grievance not found."
                });
            }

            var existingResolution = await _repository.GetResolutionByGrievanceIdAsync(
                dto.GrievanceId,
                cancellationToken);

            if (existingResolution != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Resolution already submitted for this grievance."
                });
            }

            var assignment = await _repository.GetAssignmentByGrievanceIdAsync(
                dto.GrievanceId,
                cancellationToken);

            if (assignment == null)
            {
                return new NotFoundObjectResult(new
                {
                    message = "Assignment not found for this grievance."
                });
            }

            // Security check: investigator can close only their own assigned grievance.
            if (assignment.InvestigatorId != investigatorId.Value)
            {
                return new ForbidResult();
            }

            var closedStatusId = await _repository.GetStatusIdByNameAsync(
                "Closed",
                cancellationToken);

            if (closedStatusId == 0)
            {
                return new NotFoundObjectResult(new
                {
                    message = "Closed status is not configured in the Status table."
                });
            }

            var resolution = new Resolution
            {
                GrievanceId = dto.GrievanceId,
                InvestigatorId = investigatorId.Value,

                RootCause = dto.RootCause?.Trim() ?? string.Empty,
                CorrectiveAction = dto.CorrectiveAction?.Trim() ?? string.Empty,
                PreventiveAction = dto.PreventiveAction?.Trim() ?? string.Empty,

                ResolutionSummary = string.IsNullOrWhiteSpace(dto.ResolutionSummary)
                    ? "Grievance investigated and resolved by the assigned investigator."
                    : dto.ResolutionSummary.Trim(),

                // Admin approval happens later, so keep it null.
                ApprovedByAdminId = null,

                ResolvedAt = DateTime.UtcNow
            };

            await _repository.AddResolutionAsync(
                resolution,
                cancellationToken);

            grievance.StatusId = closedStatusId;
            grievance.ClosedAt = DateTime.UtcNow;

            assignment.CompletedAt = DateTime.UtcNow;
            if (grievance.ClosedAt == null)
            {

                return new BadRequestObjectResult(
                        "Grievance is not closed yet.");

            }

            // ✅ Current grievance resolution duration
            var currentDuration =
                grievance.ClosedAt.Value - grievance.CreatedAt;

            // ✅ Case 1: Matched grievance exists
            if (!string.IsNullOrWhiteSpace(grievance.MatchedGrievanceNumber))
            {
                var matchedGrievance =
                    await _repository.GetGrievanceByNumberAsync(
                        grievance.MatchedGrievanceNumber,
                        cancellationToken);

                if (matchedGrievance != null && matchedGrievance.ClosedAt != null)
                {
                    var matchedDuration =
                        matchedGrievance.ClosedAt.Value - matchedGrievance.CreatedAt;

                    grievance.EstimatedTimeSavedHrs =
                        (decimal)(matchedDuration - currentDuration).TotalHours;
                }
            }
            else
            {
                // ✅ Case 2: No matched grievance → SLA logic
                if (grievance.DueDate != null)
                {
                    grievance.EstimatedTimeSavedHrs =
                        (decimal)(grievance.DueDate.Value - grievance.ClosedAt.Value)
                            .TotalHours;
                }
            }


            await _repository.SaveChangesAsync(cancellationToken);

            return new OkObjectResult(new
            {
                success = true,
                grievanceId = dto.GrievanceId,
                resolutionId = resolution.ResolutionId,
                status = "Closed",
                message = "Grievance closed successfully."
            });
        }
        public async Task<IActionResult> GetDashboardSummaryAsync(
    int userId,
    CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Fetching dashboard summary for user {UserId}",
                userId);

            // Step 1: Get InvestigatorId using logged-in UserId
            var investigatorId = await _repository.GetInvestigatorByUserIdAsync(
                userId,
                cancellationToken);

            // If this user is not linked to an investigator profile
            if (!investigatorId.HasValue)
            {
                return new NotFoundObjectResult(new
                {
                    message = "No investigator profile is linked to this logged-in user."
                });
            }

            _logger.LogInformation(
                "Investigator {InvestigatorId} found for user {UserId}",
                investigatorId.Value,
                userId);

            // Step 2: Fetch summary using InvestigatorId
            var summary = await _repository.GetDashboardSummaryAsync(
                investigatorId.Value,
                cancellationToken);

            // For dashboard summary, return zeros instead of 404
            if (summary == null)
            {
                return new OkObjectResult(new InvestigatorDashboardSummaryDto
                {
                    MyAssignments = 0,
                    InProgress = 0,
                    DueToday = 0,
                    Escalations = 0
                });
            }

            return new OkObjectResult(summary);
        }
    }

}
