using Patient_Grievance_and_Complaint_Resolution.Models;
namespace Patient_Grievance_and_Complaint_Resolution.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}
