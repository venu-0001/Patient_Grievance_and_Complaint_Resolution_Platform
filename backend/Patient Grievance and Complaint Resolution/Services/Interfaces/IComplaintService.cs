using Patient_Grievance_and_Complaint_Resolution.DTOs;

namespace Patient_Grievance_and_Complaint_Resolution.Services.Interfaces
{
    public interface IComplaintService
    {
        Task<IEnumerable<ComplaintsResponse>> GetAllAsync(
            CancellationToken cancellationToken);
    }
}