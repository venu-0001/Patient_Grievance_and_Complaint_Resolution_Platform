using Patient_Grievance_and_Complaint_Resolution.Models;

namespace Patient_Grievance_and_Complaint_Resolution.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
