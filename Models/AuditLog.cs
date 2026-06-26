using System;
using System.Collections.Generic;

namespace Patient_Grievance_and_Complaint_Resolution.Models;

public partial class AuditLog
{
    public long AuditId { get; set; }

    public string TableName { get; set; } = null!;

    public int RecordId { get; set; }

    public string Action { get; set; } = null!;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public string? ChangedField { get; set; }

    public int? ChangedByPatientId { get; set; }

    public int? ChangedByAdminId { get; set; }

    public int? ChangedByInvestigatorId { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Admin? ChangedByAdmin { get; set; }

    public virtual Investigator? ChangedByInvestigator { get; set; }

    public virtual Patient? ChangedByPatient { get; set; }
}
