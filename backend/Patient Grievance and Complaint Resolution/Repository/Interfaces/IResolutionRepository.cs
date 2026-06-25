using Patient_Grievance_and_Complaint_Resolution.Models;

namespace Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces
{
    public interface IResolutionRepository
    {
        Task<IEnumerable<Resolution>> GetByInvestigatorAsync(
            int investigatorId,
            CancellationToken cancellationToken);

        Task<Resolution?> GetResolutionReportAsync(
            int resolutionId,
            int investigatorId,
            CancellationToken cancellationToken);

        Task AddResolutionAsync(
            Resolution resolution,
            CancellationToken cancellationToken);
    }
}