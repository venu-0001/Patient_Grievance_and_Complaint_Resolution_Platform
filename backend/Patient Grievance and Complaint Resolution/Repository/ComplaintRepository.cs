using Microsoft.EntityFrameworkCore;
using Patient_Grievance_and_Complaint_Resolution.Data;
using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Repository
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ApplicationDbContext _context;

        public ComplaintRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Complaint>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.Complaints
                .Include(g => g.Patient)     // ✅ MRN
                .Include(g => g.Category)    // ✅ category_name
                .Include(g => g.Department)  // ✅ Department
                .Include(g => g.Status)      // ✅ status
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}