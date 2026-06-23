namespace Patient_Grievance_and_Complaint_Resolution.Models
{
    public class User
    {
        //hello world
        public int UserId { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public ICollection<Admin> Admins { get; set; }

        public ICollection<Patient> Patients { get; set; }

        public ICollection<Investigator> Investigators { get; set; }
    }
}
