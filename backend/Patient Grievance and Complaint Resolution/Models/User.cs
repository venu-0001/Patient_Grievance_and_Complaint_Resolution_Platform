using System.Data;

namespace Patient_Grievance_and_Complaint_Resolution.Models
{
    public partial class User
    {
        public int UserId { get; set; }

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;

        public virtual ICollection<Admin> Admins { get; set; }
            = new List<Admin>();

        public virtual ICollection<Patient> Patients { get; set; }
            = new List<Patient>();

        public virtual ICollection<Investigator> Investigators { get; set; }
            = new List<Investigator>();
    }
}
