using Microsoft.EntityFrameworkCore;
using Patient_Grievance_and_Complaint_Resolution.Data;
using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Repository
{
    public class ResolutionRepository : IResolutionRepository
    {
        private readonly AppDbContext _context;

        public ResolutionRepository(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET: Resolutions by Investigator
        public async Task<IEnumerable<Resolution>> GetByInvestigatorAsync(
            int investigatorId,
            CancellationToken cancellationToken)
        {
            return await _context.Resolutions
                .Include(r => r.Grievance)              // for GrievanceNumber
                .Where(r => r.InvestigatorId == investigatorId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        // ✅ POST: Add Resolution
        public async Task AddResolutionAsync(
            Resolution resolution,
            CancellationToken cancellationToken)
        {
            await _context.Resolutions.AddAsync(resolution, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}