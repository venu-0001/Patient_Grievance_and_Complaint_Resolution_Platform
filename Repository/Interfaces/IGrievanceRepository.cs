using Patient_Grievance_and_Complaint_Resolution.Models;

public interface IGrievanceRepository
{
    Task<Patient?> GetPatientByEmailAsync(string email);

    Task<Category?> GetCategoryByNameAsync(string categoryName);

    Task<Encounter?> GetEncounterAsync(
        int patientId,
        DateTime encounterDate);

    Task<Grievance> AddGrievanceAsync(Grievance grievance);
    Task<int> GetDepartmentIdByNameAsync(string departmentName);
    Task<Complaint> AddComplaintAsync(Complaint complaint);

    Task<List<Investigator>> GetInvestigatorsAsync();

    Task<List<Grievance>> GetPatientGrievanceByIdAsync(int patientId);

    Task<Assignment> AddAssignmentAsync(Assignment assignment);
    Task<string> GetDepartmentNameByIdAsync(int departmentId);

    Task<Sla> GetDateBySeverity(string severity);
}