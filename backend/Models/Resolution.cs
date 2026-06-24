using System;
using System.Collections.Generic;

namespace Patient_Grievance_and_Complaint_Resolution.Models;

public partial class Resolution
{
    public int ResolutionId { get; set; }

    public int GrievanceId { get; set; }

    public int InvestigatorId { get; set; }

    public string? RootCause { get; set; }

    public string? CorrectiveAction { get; set; }

    public string? PreventiveAction { get; set; }

    public string? ResolutionSummary { get; set; }

    public int? ApprovedByAdminId { get; set; }

    public DateTime ResolvedAt { get; set; }

    public virtual Admin? ApprovedByAdmin { get; set; }

    public virtual Grievance Grievance { get; set; } = null!;

    public virtual Investigator Investigator { get; set; } = null!;
}
