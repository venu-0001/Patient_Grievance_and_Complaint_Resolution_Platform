using Patient_Grievance_and_Complaint_Resolution.DTOs;

namespace Patient_Grievance_and_Complaint_Resolution.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(
            LoginRequestDto request);
    }
}
