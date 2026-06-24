using Microsoft.AspNetCore.Mvc;
using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.DTOs.AiInputsandOutputs;
using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Services;
using static System.Net.Mime.MediaTypeNames;

public class GrievanceService : IGrievanceService
{
    private readonly IGrievanceRepository _grievanceRepository;
    string configPath = Path.Combine("InputFolder", "SystemInput.txt");
    string outputPath = Path.Combine("OutputFolder", "AiOutput.txt");
    private static readonly HttpClient sharedClient = new HttpClient();

    public GrievanceService(IGrievanceRepository grievanceRepository)
    {
        _grievanceRepository = grievanceRepository;
    }

    public async Task<IActionResult> SubmitGrievanceAsync(
        PatientGrievanceRequest request)
    {
        var patient = await _grievanceRepository
            .GetPatientByEmailAsync(request.Email);

        if (patient == null)
        {
            throw new KeyNotFoundException("Patient not found.");
        }

        var category = await _grievanceRepository
            .GetCategoryByNameAsync(request.Category);

        if (category == null)
        {
            throw new KeyNotFoundException("Category not found.");
        }

        var encounter = await _grievanceRepository.GetEncounterAsync(patient.PatientId, request.EncounterDate);
        if(encounter== null)
        {
            throw new KeyNotFoundException("Encounter dates not Matched.");
        }
        var investigators = await _grievanceRepository.GetInvestigatorsAsync();
        List<InvestigatorDto> investigatorDtos = new List<InvestigatorDto>();

        foreach (var investigator in investigators)
        {


            investigatorDtos.Add(new InvestigatorDto
            {
                InvestigatorId = investigator.InvestigatorId,
                Department = investigator.Department.DepartmentName,
                CurrentLoad = investigator.CurrentLoad,
                Rating = investigator.Rating
            });
        }


        var service1 = new OpenRouterRoutingService(sharedClient, configPath, outputPath, investigatorDtos);

        var service2 = new OpenRouterRoutingBackupService(sharedClient, configPath, outputPath, investigatorDtos);

        RoutingResult result;
        try
        {
            result = await service1.RouteIssueWithExternalConfigAsync(
                request.Category,
                request.Description);
        }
        catch (Exception)
        {
            result = await service2.RouteIssueWithExternalConfigAsync(
                request.Category,
                request.Description);
        }
        if (string.IsNullOrWhiteSpace(result.RecordType))
        {
            throw new Exception("Model Doesnot return the record type.");
        }

        var department_id = await _grievanceRepository.GetDepartmentIdByNameAsync(result.Department);

        if (result.RecordType.Equals("Complaint"))
        {
            var complaint = new Complaint
            {
                EncounterId = encounter?.EncounterId,
                PatientId = patient.PatientId,
                CategoryId = category.CategoryId,
                DepartmentId = department_id,
                StatusId = 1,
                ConvertedToGrievance = false,
                GrievanceId = null,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                ResolvedAt = null
            };
            var savedComplaint = await _grievanceRepository.AddComplaintAsync(complaint);
            var response = new PatientComplaintResponse
            {
                ComplaintId = savedComplaint.ComplaintId,
                Status = "Open",
                Category = category.CategoryName,
                Department = result.Department,
                CreatedAt = savedComplaint.CreatedAt,
                DueDate = savedComplaint.ResolvedAt

            };
            return new OkObjectResult(response);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(result.Severity))
            {
                throw new Exception("Model Doesnot return the severity");
            }
            if (result.AssignedInvestigatorId==0)
            {
                throw new Exception("Model Failed to assign the investigator");
            }

            var grievance = new Grievance
            {
                GrievanceNumber = $"GRV-{DateTime.UtcNow:yyyyMMddHHmmss}",
                PatientId = patient.PatientId,
                EncounterId = encounter?.EncounterId,
                CategoryId = category.CategoryId,
                Description = request.Description,
                DepartmentId = department_id,
                Severity = result.Severity,
                InvestigatorId = result.AssignedInvestigatorId,
                StatusId = 2,
                IsEscalated = false,
                CreatedAt = DateTime.UtcNow
            };

            var savedGrievance = await _grievanceRepository.AddGrievanceAsync(grievance);
            Console.WriteLine("Assigned Investigator Id: " + result.AssignedInvestigatorId);
            var assignment = new Assignment
            {
                GrievanceId = savedGrievance.GrievanceId,
                InvestigatorId = result.AssignedInvestigatorId ?? 0,
                AssignedAt = DateTime.Now,
                CompletedAt = DateTime.Today,
            };
            var savedAssignment = await _grievanceRepository.AddAssignmentAsync(assignment);
            var oneLiner = result.PatientIssueOneliner;
            if (result.RefferedGrievanceNumber == null)
            {
                File.AppendAllText(outputPath, oneLiner + "-" + savedGrievance.GrievanceNumber + "\n");
            }
            var response = new PatientGrievanceResponse
            {
                GrievanceNumber = savedGrievance.GrievanceNumber,
                Category = category.CategoryName,
                Department = result.Department,
                Status = "Open",
                CreatedAt = savedGrievance.CreatedAt,
                DueDate = savedGrievance.DueDate

            };
            return new OkObjectResult(response);
        }

    }
    public async Task<IActionResult> GetPatientGrievanceAsync(int patientId)
    {
        var patientGrievance = await _grievanceRepository.GetPatientGrievanceByIdAsync(patientId);
        if (patientGrievance == null || !patientGrievance.Any())
        {
            throw new KeyNotFoundException("Patient Grievance not found.");
        }
        List<PatientGrievanceResponse> response = new();
        foreach (var grievance in patientGrievance)
        {
            response.Add(new PatientGrievanceResponse
            {
                GrievanceNumber = grievance.GrievanceNumber,
                Category = grievance.Category?.CategoryName ?? "N/A",
                Department = grievance.Department.DepartmentName,
                Status = grievance.Status.StatusName,
                CreatedAt = grievance.CreatedAt,
                DueDate = grievance.DueDate,

            });
        }
        return new OkObjectResult(response);

    }
}