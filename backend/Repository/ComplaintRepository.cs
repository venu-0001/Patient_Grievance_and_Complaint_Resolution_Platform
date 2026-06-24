using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Complaint>> GetPatientComplaintAsync(int patientId)
        {
            return await _context.Complaints
                .Include(c => c.Category)
                .Include(c => c.Department)
                .Include(c => c.Status)
                .Where(c => c.PatientId == patientId).ToListAsync();
        }
    }
}
