using System;
using System.Collections.Generic;

namespace Patient_Grievance_and_Complaint_Resolution.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? DepartmentHead { get; set; }

    public string DepartmentEmail { get; set; } = null!;

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    public virtual ICollection<Encounter> Encounters { get; set; } = new List<Encounter>();

    public virtual ICollection<Escalation> EscalationEscalatedFromNavigations { get; set; } = new List<Escalation>();

    public virtual ICollection<Escalation> EscalationEscalatedToNavigations { get; set; } = new List<Escalation>();

    public virtual ICollection<Grievance> Grievances { get; set; } = new List<Grievance>();

    public virtual ICollection<Investigator> Investigators { get; set; } = new List<Investigator>();
}
