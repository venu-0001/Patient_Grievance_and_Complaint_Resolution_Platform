using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Models;

namespace Patient_Grievance_and_Complaint_Resolution.Repository.Interface
{
    public interface IInvestigatorRepository
    {
        Task<List<Grievance>> GetAssignmentsAsync(
            int investigatorId,
            CancellationToken cancellationToken);

        Task<Grievance?> GetGrievanceByIdAsync(
            int grievanceId,
            CancellationToken cancellationToken);

        Task<Assignment?> GetAssignmentByGrievanceIdAsync(
            int grievanceId,
            CancellationToken cancellationToken);

        Task<Resolution?> GetResolutionByGrievanceIdAsync(
            int grievanceId,
            CancellationToken cancellationToken);
        Task<List<InvestigatorDashboardDto>>
GetDashboardDataAsync(
int investigatorId,
CancellationToken cancellationToken);
        Task<int> GetStatusIdByNameAsync(
    string statusName,
    CancellationToken cancellationToken);
        Task<List<Grievance>> GetExpiredGrievancesAsync(
    int assignedStatusId,
    CancellationToken cancellationToken);

        Task AddEscalationAsync(
            Escalation escalation,
            CancellationToken cancellationToken);
        Task<InvestigatorDashboardSummaryDto>
GetDashboardSummaryAsync(
    int investigatorId,
    CancellationToken cancellationToken);



        Task AddResolutionAsync(
            Resolution resolution,
            CancellationToken cancellationToken);

        Task SaveChangesAsync(
            CancellationToken cancellationToken);


    }
}
