using System;
using System.Collections.Generic;

namespace Patient_Grievance_and_Complaint_Resolution.Models;

public partial class Grievance
{
    public int GrievanceId { get; set; }

    public string GrievanceNumber { get; set; } = null!;

    public int? EncounterId { get; set; }

    public int PatientId { get; set; }

    public int? CategoryId { get; set; }

    public int DepartmentId { get; set; }

    public int? SlaId { get; set; }

    public string? Severity { get; set; }

    public string Description { get; set; } = null!;

    public int? InvestigatorId { get; set; }

    public int StatusId { get; set; }

    public bool IsEscalated { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }
    public string? GrievanceSummary { get; set; }

    public string? MatchedGrievanceNumber { get; set; }

    public decimal? EstimatedTimeSavedHrs { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    public virtual Department Department { get; set; } = null!;

    public virtual Encounter? Encounter { get; set; }

    public virtual ICollection<Escalation> Escalations { get; set; } = new List<Escalation>();

    public virtual Investigator? Investigator { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<PatientCommunication> PatientCommunications { get; set; } = new List<PatientCommunication>();

    public virtual Resolution? Resolution { get; set; }

    public virtual Sla? Sla { get; set; }

    public virtual Status Status { get; set; } = null!;
}
