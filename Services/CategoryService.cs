using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Services
{
    public class CategoryService : ICategoryService

    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            if (categories.Count == 0)
            {
                throw new KeyNotFoundException("Categories Not Found");
            }
            return new OkObjectResult(categories);
        }
    }
}
