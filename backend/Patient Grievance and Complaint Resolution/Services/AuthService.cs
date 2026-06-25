using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Helpers;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IJwtService _jwtService;

        public AuthService(
            IUserRepository repository,
            IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }


        public async Task<LoginResponseDto?> LoginAsync(
            LoginRequestDto request)
        {
            var user =
                await _repository.GetUserByEmailAsync(
                    request.Email);

            if (user == null)
                return null;

            var hashedPassword =
                PasswordHelper.HashPassword(
                    request.Password);

            if (hashedPassword != user.PasswordHash)
                return null;

            var token =
                _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                UserId = user.UserId,
                Email = user.Email,
                Role = user.Role.RoleName
            };
        }
    }

}
