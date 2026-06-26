using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Services
{
    public class ComplaintServices : IComplaintService
    {
        private readonly IComplaintRepository _repository;

        public ComplaintServices(IComplaintRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ComplaintsResponse>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var grievances = await _repository.GetAllAsync(cancellationToken);

            return grievances.Select(g => new ComplaintsResponse
            {
                ComplaintId = g.ComplaintId,
                Mrn = g.Patient.Mrn,
                EncounterId = g.EncounterId ?? 0,
                category_name = g.Category?.CategoryName,
                Department = g.Department.DepartmentName,
                status = g.Status.StatusName,
                Description = g.Description
            });
        }
    }
}