namespace Patient_Grievance_and_Complaint_Resolution.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public string Role { get; set; }

        public int UserId { get; set; }

        public string Email { get; set; }
    }
}