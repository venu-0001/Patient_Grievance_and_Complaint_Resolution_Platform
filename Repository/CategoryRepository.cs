using Microsoft.EntityFrameworkCore;
using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<string>> GetCategories()
        {
            return await _context.Categories.Select(c => c.CategoryName).ToListAsync();
        }
    }
}
