using System;
using System.Collections.Generic;

namespace Patient_Grievance_and_Complaint_Resolution.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public string Mrn { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; } = null!;

    //public string Passwordhash { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }
    public int? UserId { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    public virtual ICollection<Encounter> Encounters { get; set; } = new List<Encounter>();

    public virtual ICollection<Grievance> Grievances { get; set; } = new List<Grievance>();

    public virtual ICollection<PatientCommunication> PatientCommunications { get; set; } = new List<PatientCommunication>();
}
