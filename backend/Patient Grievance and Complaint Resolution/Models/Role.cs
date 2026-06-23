namespace Patient_Grievance_and_Complaint_Resolution.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
