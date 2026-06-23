using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Service.Interface;
namespace Patient_Grievance_and_Complaint_Resolution.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(
            IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequestDto request)
        {
            var result =
                await _service.LoginAsync(request);

            if (result == null)
            {
                return Unauthorized(
                    new
                    {
                        Message =
                        "Invalid Email or Password"
                    });
            }

            return Ok(result);
        }
    }
}
