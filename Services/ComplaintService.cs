using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        public ComplaintService(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }
        public async Task<IActionResult> GetPatientComplaintAsync(int patientId)
        {
            var patientComplaint = await _complaintRepository.GetPatientComplaintAsync(patientId);
            if (patientComplaint == null || !patientComplaint.Any())
            {
                throw new KeyNotFoundException("Patient Complaints not found.");
            }
            List<PatientComplaintResponse> response = new();
            foreach (var complaint in patientComplaint)
            {
                response.Add(new PatientComplaintResponse()
                {
                    ComplaintId = complaint.ComplaintId,
                    Status = complaint.Status.StatusName,
                    Category = complaint.Category?.CategoryName ?? "N/A",
                    Department = complaint.Department.DepartmentName,
                    CreatedAt = complaint.CreatedAt,
                    DueDate = complaint.ResolvedAt
                });
            }
            return new OkObjectResult(response);
        }
    }
}
