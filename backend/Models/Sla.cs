using System;
using System.Collections.Generic;

namespace Patient_Grievance_and_Complaint_Resolution.Models;

public partial class Sla
{
    public int SlaId { get; set; }

    public string SeverityLevel { get; set; } = null!;

    public int AcknowledgmentTimeHrs { get; set; }

    public int InvestigationTimeDays { get; set; }

    public int ResolutionTimeDays { get; set; }

    public int EscalationThresholdHrs { get; set; }

    public virtual ICollection<Grievance> Grievances { get; set; } = new List<Grievance>();
}
