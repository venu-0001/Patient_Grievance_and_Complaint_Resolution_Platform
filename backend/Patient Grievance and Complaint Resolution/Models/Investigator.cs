using System;
using System.Collections.Generic;

namespace Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Controllers;
  

public partial class Investigator
{
    public int InvestigatorId { get; set; }

    public string EmployeeCode { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int DepartmentId { get; set; }

    public int CurrentLoad { get; set; }

    public byte? Rating { get; set; }
    public int? UserId { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Grievance> Grievances { get; set; } = new List<Grievance>();

    public virtual ICollection<Resolution> Resolutions { get; set; } = new List<Resolution>();
}
