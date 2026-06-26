using Patient_Grievance_and_Complaint_Resolution.Models;
namespace Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}
