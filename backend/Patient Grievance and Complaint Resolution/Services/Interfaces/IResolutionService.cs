using Patient_Grievance_and_Complaint_Resolution.DTOs;

namespace Patient_Grievance_and_Complaint_Resolution.Services.Interfaces
{
    public interface IResolutionService
    {
        // ✅ Existing GET service method
        Task<IEnumerable<InvestigatorResolution>> GetMyResolutionsAsync(
            int investigatorId,
            CancellationToken cancellationToken);

        // ✅ NEW POST service method
    }
}