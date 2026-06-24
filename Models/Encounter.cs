using System;
using System.Collections.Generic;

namespace Patient_Grievance_and_Complaint_Resolution.Models;

public partial class Encounter
{
    public int EncounterId { get; set; }

    public int PatientId { get; set; }

    public int DepartmentId { get; set; }

    public DateTime EncounterDate { get; set; }

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Grievance> Grievances { get; set; } = new List<Grievance>();

    public virtual Patient Patient { get; set; } = null!;
}
