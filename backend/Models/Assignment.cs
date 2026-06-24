using System;
using System.Collections.Generic;

namespace Patient_Grievance_and_Complaint_Resolution.Models;

public partial class Assignment
{
    public int AssignmentId { get; set; }

    public int GrievanceId { get; set; }

    public int InvestigatorId { get; set; }

    public DateTime AssignedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual Grievance Grievance { get; set; } = null!;

    public virtual Investigator Investigator { get; set; } = null!;
}
