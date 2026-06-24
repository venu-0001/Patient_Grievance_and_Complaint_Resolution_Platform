using System;
using System.Collections.Generic;

namespace Patient_Grievance_and_Complaint_Resolution.Models;

public partial class Escalation
{
    public int EscalationId { get; set; }

    public int GrievanceId { get; set; }

    public int EscalatedFrom { get; set; }

    public int EscalatedTo { get; set; }

    public DateTime EscalatedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public string? ActionTaken { get; set; }

    public virtual Department EscalatedFromNavigation { get; set; } = null!;

    public virtual Department EscalatedToNavigation { get; set; } = null!;

    public virtual Grievance Grievance { get; set; } = null!;
}
