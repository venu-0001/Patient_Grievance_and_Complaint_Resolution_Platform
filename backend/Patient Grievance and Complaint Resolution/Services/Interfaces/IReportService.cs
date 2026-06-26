namespace Patient_Grievance_and_Complaint_Resolution.Services.Interfaces
{
    public interface IReportService
    {
        Task<byte[]> GenerateGrievanceReportAsync(int grievanceId);
    }
}