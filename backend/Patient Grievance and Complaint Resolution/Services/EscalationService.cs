using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interface;
using Patient_Grievance_and_Complaint_Resolution.services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.services
{
    public class EscalationService : IEscalationService
    {
        private readonly IInvestigatorRepository _repository;
        private readonly ILogger<EscalationService> _logger;

        public EscalationService(
            IInvestigatorRepository repository,
            ILogger<EscalationService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task ProcessEscalationsAsync(
            CancellationToken cancellationToken)
        {
            var assignedStatusId =
                await _repository.GetStatusIdByNameAsync(
                    "Assigned",
                    cancellationToken);

            var escalatedStatusId =
                await _repository.GetStatusIdByNameAsync(
                    "Escalated",
                    cancellationToken);

            var grievances =
                await _repository.GetExpiredGrievancesAsync(
                    assignedStatusId,
                    cancellationToken);

            foreach (var grievance in grievances)
            {
                grievance.StatusId = escalatedStatusId;

                var escalation = new Escalation
                {
                    GrievanceId = grievance.GrievanceId,
                    EscalatedAt = DateTime.UtcNow,
                    ActionTaken =
                        "Automatically escalated due to SLA breach"
                };

                await _repository.AddEscalationAsync(
                    escalation,
                    cancellationToken);
            }

            await _repository.SaveChangesAsync(
                cancellationToken);
        }
    }
}