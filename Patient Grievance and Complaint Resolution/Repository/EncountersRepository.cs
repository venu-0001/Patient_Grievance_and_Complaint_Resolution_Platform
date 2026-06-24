using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;

namespace Patient_Grievance_and_Complaint_Resolution.Repository
{
    public class EncountersRepository : IEncountersRepository
    {
        private readonly ApplicationDbContext _context;

        public EncountersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<DateTime>> GetEncounterDates(int patientId)
        {
            return await _context.Encounters.Where(e => e.PatientId == patientId).Select(e => e.EncounterDate).ToListAsync();
        }
    }
}
