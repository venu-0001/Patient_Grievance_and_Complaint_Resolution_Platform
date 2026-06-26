using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Helpers;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Services
{
    public class ResolutionService : IResolutionService
    {
        private readonly IResolutionRepository _repository;
        private readonly IInvestigatorRepository _investigatorRepository;

        public ResolutionService(
            IResolutionRepository repository,
            IInvestigatorRepository investigatorRepository)
        {
            _repository = repository;
            _investigatorRepository = investigatorRepository;
        }

        public async Task<IEnumerable<InvestigatorResolution>> GetMyResolutionsAsync(
            int userId,
            CancellationToken cancellationToken)
        {
            var investigatorId =
                await _investigatorRepository.GetInvestigatorByUserIdAsync(
                    userId,
                    cancellationToken);

            if (!investigatorId.HasValue)
            {
                return Enumerable.Empty<InvestigatorResolution>();
            }

            var data = await _repository.GetByInvestigatorAsync(
                investigatorId.Value,
                cancellationToken);

            if (data == null || !data.Any())
            {
                return Enumerable.Empty<InvestigatorResolution>();
            }

            return data.Select(r => new InvestigatorResolution
            {
                ResolutionId = r.ResolutionId,
                GrievanceNumber = r.Grievance?.GrievanceNumber ?? "-",
                RootCause = r.RootCause ?? "-",
                ResolutionSummary = r.ResolutionSummary ?? "-",
                ResolvedAt = r.ResolvedAt
            });
        }

        public async Task<byte[]?> DownloadResolutionReportAsync(
            int resolutionId,
            int userId,
            CancellationToken cancellationToken)
        {
            var investigatorId =
                await _investigatorRepository.GetInvestigatorByUserIdAsync(
                    userId,
                    cancellationToken);

            if (!investigatorId.HasValue)
            {
                return null;
            }

            var resolution = await _repository.GetResolutionReportAsync(
                resolutionId,
                investigatorId.Value,
                cancellationToken);

            if (resolution == null || resolution.Grievance == null)
            {
                return null;
            }

            return PdfBuilder.BuildGrievanceReport(
                resolution.Grievance,
                resolution);
        }
    }
}