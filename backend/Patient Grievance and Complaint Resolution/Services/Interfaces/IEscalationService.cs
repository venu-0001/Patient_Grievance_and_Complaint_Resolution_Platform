namespace Patient_Grievance_and_Complaint_Resolution.Services.Interfaces
{
    public interface IEscalationService
    {
        Task ProcessEscalationsAsync(
            CancellationToken cancellationToken);
    }
}
