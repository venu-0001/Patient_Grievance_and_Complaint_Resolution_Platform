using System;
using System.Collections.Generic;

namespace Patient_Grievance_and_Complaint_Resolution.Models;

public partial class PatientCommunication
{
    public int CommId { get; set; }

    public int GrievanceId { get; set; }

    public int PatientId { get; set; }

    public int StatusId { get; set; }

    public DateTime SentAt { get; set; }

    public virtual Grievance Grievance { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
