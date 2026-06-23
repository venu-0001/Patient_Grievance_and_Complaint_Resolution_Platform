using Patient_Grievance_and_Complaint_Resolution.DTOs;
using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Patient_Grievance_and_Complaint_Resolution.Repository
{
    public class InvestigatorRepository : IInvestigatorRepository
    {
        private readonly ApplicationDbContext _context;

        public InvestigatorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Grievance>> GetAssignmentsAsync(
            int investigatorId,
            CancellationToken cancellationToken)
        {
            return await _context.Grievances
                .Where(x => x.InvestigatorId == investigatorId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Grievance?> GetGrievanceByIdAsync(
            int grievanceId,
            CancellationToken cancellationToken)
        {
            return await _context.Grievances
                .FirstOrDefaultAsync(
                    x => x.GrievanceId == grievanceId,
                    cancellationToken);
        }

        public async Task<Assignment?> GetAssignmentByGrievanceIdAsync(
            int grievanceId,
            CancellationToken cancellationToken)
        {
            return await _context.Assignments
                .FirstOrDefaultAsync(
                    x => x.GrievanceId == grievanceId,
                    cancellationToken);
        }

        public async Task<Resolution?> GetResolutionByGrievanceIdAsync(
            int grievanceId,
            CancellationToken cancellationToken)
        {
            return await _context.Resolutions
                .FirstOrDefaultAsync(
                    x => x.GrievanceId == grievanceId,
                    cancellationToken);
        }
        public async Task<List<InvestigatorDashboardDto>>
GetDashboardDataAsync(
int investigatorId,
CancellationToken cancellationToken)
        {
            return await
            (
                from g in _context.Grievances

                join p in _context.Patients
                on g.PatientId equals p.PatientId

                join d in _context.Departments
                on g.DepartmentId equals d.DepartmentId

                join s in _context.Statuses
                on g.StatusId equals s.StatusId

                where g.InvestigatorId == investigatorId

                select new InvestigatorDashboardDto
                {
                    GrievanceId = g.GrievanceId,

                    GrievanceNumber = g.GrievanceNumber,

                    PatientName =
                        p.FirstName + " " + p.LastName,

                    DepartmentName =
                        d.DepartmentName,

                    Severity =
                        g.Severity,

                    Status =
                        s.StatusName,

                    DueDate =
                        g.DueDate
                }
            )
            .ToListAsync(cancellationToken);
        }
        public async Task<List<Grievance>> GetExpiredGrievancesAsync(
    int assignedStatusId,
    CancellationToken cancellationToken)
        {
            return await _context.Grievances
                .Where(g =>
                    g.DueDate < DateTime.UtcNow &&
                    g.StatusId == assignedStatusId)
                .ToListAsync(cancellationToken);
        }
        public async Task AddEscalationAsync(
    Escalation escalation,
    CancellationToken cancellationToken)
        {
            await _context.Escalations
                .AddAsync(escalation, cancellationToken);
        }
        public async Task<int> GetStatusIdByNameAsync(
    string statusName,
    CancellationToken cancellationToken)
        {
            return await _context.Statuses
                .Where(x => x.StatusName == statusName)
                .Select(x => x.StatusId)
                .FirstOrDefaultAsync(cancellationToken);

        }
        public async Task<InvestigatorDashboardSummaryDto>
GetDashboardSummaryAsync(
    int investigatorId,
    CancellationToken cancellationToken)
        {
            var today = DateTime.Today;

            var myAssignments = await _context.Grievances
                .CountAsync(
                    x => x.InvestigatorId == investigatorId,
                    cancellationToken);

            var inProgress = await _context.Grievances
                .CountAsync(
                    x => x.InvestigatorId == investigatorId &&
                         x.StatusId == 2,
                    cancellationToken);

            var dueToday = await _context.Grievances
                .CountAsync(
                    x => x.InvestigatorId == investigatorId &&
                         x.DueDate.HasValue &&
                    
                         x.DueDate.Value.Date == today,
                    cancellationToken);

            var overdue = await _context.Grievances
                .CountAsync(
                    x => x.InvestigatorId == investigatorId &&
                         x.DueDate < today &&
                         x.StatusId != 4,
                    cancellationToken);

            

            return new InvestigatorDashboardSummaryDto
            {
                MyAssignments = myAssignments,
                InProgress = inProgress,
                DueToday = dueToday,
                Overdue = overdue,
                
            };
        }
        public async Task AddResolutionAsync(
            Resolution resolution,
            CancellationToken cancellationToken)
        {
            await _context.Resolutions
                .AddAsync(resolution, cancellationToken);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
