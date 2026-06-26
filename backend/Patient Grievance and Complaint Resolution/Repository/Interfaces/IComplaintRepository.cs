using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Models;

namespace Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces
{
    public interface IComplaintRepository
    {
        Task<IEnumerable<Complaint>> GetAllAsync(
            CancellationToken cancellationToken);
    }
}