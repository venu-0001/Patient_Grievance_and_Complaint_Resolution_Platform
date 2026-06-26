using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Patient_Grievance_and_Complaint_Resolution.Models;

namespace Patient_Grievance_and_Complaint_Resolution.Repository
{
    public class GrievanceRepository : IGrievanceRepository
    {
        private readonly ApplicationDbContext _context;

        public GrievanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Patient?> GetPatientByEmailAsync(string email)
        {
            return await _context.Patients
                .FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<Category?> GetCategoryByNameAsync(string categoryName)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryName == categoryName);
        }

        public async Task<Encounter?> GetEncounterAsync(
            int patientId,
            DateTime encounterDate)
        {
            return await _context.Encounters
                .FirstOrDefaultAsync(e =>
                    e.PatientId == patientId &&
                    e.EncounterDate.Date == encounterDate.Date);
        }
        public async Task<int> GetDepartmentIdByNameAsync(string departmentName)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentName == departmentName);

            return department.DepartmentId;
        }
        public async Task<Grievance> AddGrievanceAsync(Grievance grievance)
        {
            await _context.Grievances.AddAsync(grievance);
            await _context.SaveChangesAsync();

            return grievance;
        }
        public async Task<Complaint> AddComplaintAsync(Complaint complaint)
        {
            await _context.Complaints.AddAsync(complaint);
            await _context.SaveChangesAsync();

            return complaint;
        }

        public async Task<List<Investigator>> GetInvestigatorsAsync()
        {
            return await _context.Investigators.Include(i => i.Department).ToListAsync();
        }

        public async Task<List<Grievance>> GetPatientGrievanceByIdAsync(int patientId)
        {
            return await _context.Grievances
                .Include(g => g.Category)
                .Include(g => g.Department)
                .Include(g => g.Status)
                .Where(g => g.PatientId == patientId).ToListAsync();
        }

        public async Task<Assignment> AddAssignmentAsync(Assignment assignment)
        {
            await _context.Assignments.AddAsync(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        public async Task<string> GetDepartmentNameByIdAsync(int departmentId)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);

            return department.DepartmentName;
        }

        public async Task<Sla> GetDateBySeverity(string severity)
        {
            return await _context.Slas.FirstOrDefaultAsync(s=>s.SeverityLevel== severity);
            
        }

    }
}