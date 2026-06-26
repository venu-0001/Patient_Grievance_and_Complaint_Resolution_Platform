using System;
using System.Collections.Generic;

namespace Patient_Grievance_and_Complaint_Resolution.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    public virtual ICollection<Grievance> Grievances { get; set; } = new List<Grievance>();

    public virtual ICollection<PatientCommunication> PatientCommunications { get; set; } = new List<PatientCommunication>();
}
