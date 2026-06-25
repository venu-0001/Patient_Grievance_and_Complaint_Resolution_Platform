using Patient_Grievance_and_Complaint_Resolution.DTOs;

namespace Patient_Grievance_and_Complaint_Resolution.Services.Interfaces
{
    public interface IResolutionService
    {
        Task<IEnumerable<InvestigatorResolution>> GetMyResolutionsAsync(
            int userId,
            CancellationToken cancellationToken);

        Task<byte[]?> DownloadResolutionReportAsync(
            int resolutionId,
            int userId,
            CancellationToken cancellationToken);
    }
}