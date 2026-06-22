using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Services
{
    public class ResolutionService : IResolutionService
    {
        private readonly IResolutionRepository _repository;

        public ResolutionService(IResolutionRepository repository)
        {
            _repository = repository;
        }

        // ✅ GET: My Resolutions (used by Swagger GET)
        public async Task<IEnumerable<InvestigatorResolution>> GetMyResolutionsAsync(
            int investigatorId,
            CancellationToken cancellationToken)
        {
            var data = await _repository.GetByInvestigatorAsync(investigatorId, cancellationToken);

            if (!data.Any())
                throw new KeyNotFoundException("No resolutions found.");

            return data.Select(r => new InvestigatorResolution
            {
                ResolutionId = r.ResolutionId,
                GrievanceNumber = r.Grievance.GrievanceNumber,
                RootCause = r.RootCause,
                ResolutionSummary = r.ResolutionSummary,
                ResolvedAt = r.ResolvedAt
            });
        }

        // ✅ POST: Create Resolution (used by POST API)
        public async Task CreateResolutionAsync(
            int grievanceId,
            int investigatorId,
            CreateResolutionRequest request,
            CancellationToken cancellationToken)
        {
            var resolution = new Resolution
            {
                GrievanceId = grievanceId,
                InvestigatorId = investigatorId,
                RootCause = request.RootCause,
                CorrectiveAction = request.CorrectiveAction,
                PreventiveAction = request.PreventiveAction,
                ResolutionSummary = request.ResolutionSummary,
                ApprovedByAdminId = request.ApprovedByAdminId,
                ResolvedAt = DateTime.UtcNow
            };

            await _repository.AddResolutionAsync(resolution, cancellationToken);
        }
    }
}