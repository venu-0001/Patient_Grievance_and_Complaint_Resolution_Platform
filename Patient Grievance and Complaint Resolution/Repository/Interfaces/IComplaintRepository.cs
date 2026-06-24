using Patient_Grievance_and_Complaint_Resolution.Models;

namespace Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces
{
    public interface IComplaintRepository
    {
        Task<List<Complaint>> GetPatientComplaintAsync(int patientId);
    }
}
