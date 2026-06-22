namespace Patient_Grievance_and_Complaint_Resolution.Service.Interface
{
    public interface IEscalationService
    {
        Task ProcessEscalationsAsync(
            CancellationToken cancellationToken);
    }
}
