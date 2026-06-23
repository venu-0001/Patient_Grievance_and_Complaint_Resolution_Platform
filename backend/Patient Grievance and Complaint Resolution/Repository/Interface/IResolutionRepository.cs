using Patient_Grievance_and_Complaint_Resolution.Models;

namespace Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces
{
    public interface IResolutionRepository
    {
        // ✅ Existing GET method
        Task<IEnumerable<Resolution>> GetByInvestigatorAsync(
            int investigatorId,
            CancellationToken cancellationToken);

        // ✅ NEW method for POST (add resolution)
        Task AddResolutionAsync(
            Resolution resolution,
            CancellationToken cancellationToken);
    }
}