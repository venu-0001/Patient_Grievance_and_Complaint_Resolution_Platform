using Patient_Grievance_and_Complaint_Resolution.Models;

namespace Patient_Grievance_and_Complaint_Resolution.Service.Interface
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
