using Microsoft.EntityFrameworkCore;
using Patient_Grievance_and_Complaint_Resolution.Data;
using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Repository
{
    public class ResolutionRepository : IResolutionRepository
    {
        private readonly ApplicationDbContext _context;

        public ResolutionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: All resolutions created by logged-in investigator
        public async Task<IEnumerable<Resolution>> GetByInvestigatorAsync(
            int investigatorId,
            CancellationToken cancellationToken)
        {
            return await _context.Resolutions
                .Include(r => r.Grievance)
                .Where(r => r.InvestigatorId == investigatorId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        // GET: One complete resolution report for PDF download
        public async Task<Resolution?> GetResolutionReportAsync(
            int resolutionId,
            int investigatorId,
            CancellationToken cancellationToken)
        {
            return await _context.Resolutions
                .Include(r => r.Investigator)
                .Include(r => r.Grievance)
                    .ThenInclude(g => g.Patient)
                .Include(r => r.Grievance)
                    .ThenInclude(g => g.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    r => r.ResolutionId == resolutionId &&
                         r.InvestigatorId == investigatorId,
                    cancellationToken);
        }

        // POST: Add resolution
        public async Task AddResolutionAsync(
            Resolution resolution,
            CancellationToken cancellationToken)
        {
            await _context.Resolutions.AddAsync(resolution, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}