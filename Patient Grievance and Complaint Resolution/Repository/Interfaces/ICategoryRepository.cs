namespace Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<string>> GetCategories();
    }
}
