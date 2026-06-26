using Microsoft.EntityFrameworkCore;
using Patient_Grievance_and_Complaint_Resolution.Data;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;
using System.Reflection.PortableExecutable;
using Patient_Grievance_and_Complaint_Resolution.Helpers;
using Patient_Grievance_and_Complaint_Resolution.Models;

namespace Patient_Grievance_and_Complaint_Resolution.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> GenerateGrievanceReportAsync(int grievanceId)
        {
            // ✅ 1. Fetch grievance
            var grievance = await _context.Grievances
                .Include(g => g.Patient)
                .Include(g => g.Category)
                .Include(g => g.Department)
                .FirstOrDefaultAsync(g => g.GrievanceId == grievanceId);

            if (grievance == null)
                throw new Exception("Grievance not found");

            // ✅ 2. Fetch resolution
            var resolution = await _context.Resolutions
                .Include(r => r.Investigator)
                .FirstOrDefaultAsync(r => r.GrievanceId == grievanceId);

            if (resolution == null)
                throw new Exception("Resolution not found");

            // ✅ 3. Generate PDF in memory
            var pdfBytes = PdfBuilder.BuildGrievanceReport(
                grievance,
                resolution
            );

            return pdfBytes;
        }
    }
}