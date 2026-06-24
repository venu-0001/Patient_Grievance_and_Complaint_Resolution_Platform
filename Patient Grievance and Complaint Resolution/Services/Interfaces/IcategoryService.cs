using Microsoft.AspNetCore.Mvc;

namespace Patient_Grievance_and_Complaint_Resolution.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IActionResult> GetCategories();
    }
}
