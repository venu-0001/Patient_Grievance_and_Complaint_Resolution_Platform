using System;
using System.Collections.Generic;

namespace Patient_Grievance_and_Complaint_Resolution.Models;

public partial class Complaint
{
    public int ComplaintId { get; set; }

    public int? EncounterId { get; set; }

    public int PatientId { get; set; }

    public int? CategoryId { get; set; }

    public int DepartmentId { get; set; }

    public int StatusId { get; set; }

    public bool ConvertedToGrievance { get; set; }

    public int? GrievanceId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Encounter? Encounter { get; set; }

    public virtual Grievance? Grievance { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
