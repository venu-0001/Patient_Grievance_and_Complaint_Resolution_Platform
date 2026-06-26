using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Patient_Grievance_and_Complaint_Resolution.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Complaint> Complaints { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Encounter> Encounters { get; set; }

    public virtual DbSet<Escalation> Escalations { get; set; }

    public virtual DbSet<Grievance> Grievances { get; set; }

    public virtual DbSet<Investigator> Investigators { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientCommunication> PatientCommunications { get; set; }

    public virtual DbSet<Resolution> Resolutions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sla> Slas { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=GrievanceResolutionDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.ToTable("ADMIN", "admin");

            entity.HasIndex(e => e.Username, "UQ_ADMIN_username").IsUnique();

            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.User).WithMany(p => p.Admins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ADMIN_USER_ID");
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.ToTable("ASSIGNMENTS", "grievance");

            entity.HasIndex(e => e.GrievanceId, "IX_ASSIGNMENTS_grievance_id");

            entity.HasIndex(e => e.InvestigatorId, "IX_ASSIGNMENTS_investigator_id");

            entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");
            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("assigned_at");
            entity.Property(e => e.CompletedAt)
                .HasColumnType("datetime")
                .HasColumnName("completed_at");
            entity.Property(e => e.GrievanceId).HasColumnName("grievance_id");
            entity.Property(e => e.InvestigatorId).HasColumnName("investigator_id");

            entity.HasOne(d => d.Grievance).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.GrievanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ASSIGNMENTS_grievance_id");

            entity.HasOne(d => d.Investigator).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.InvestigatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ASSIGNMENTS_investigator_id");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId);

            entity.ToTable("AUDIT_LOG", "audit");

            entity.HasIndex(e => e.ChangedByAdminId, "idx_audit_admin").HasFilter("([changed_by_admin_id] IS NOT NULL)");

            entity.HasIndex(e => e.ChangedByInvestigatorId, "idx_audit_investigator").HasFilter("([changed_by_investigator_id] IS NOT NULL)");

            entity.HasIndex(e => e.ChangedByPatientId, "idx_audit_patient").HasFilter("([changed_by_patient_id] IS NOT NULL)");

            entity.HasIndex(e => new { e.TableName, e.RecordId }, "idx_audit_resource");

            entity.Property(e => e.AuditId).HasColumnName("audit_id");
            entity.Property(e => e.Action)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("action");
            entity.Property(e => e.ChangedByAdminId).HasColumnName("changed_by_admin_id");
            entity.Property(e => e.ChangedByInvestigatorId).HasColumnName("changed_by_investigator_id");
            entity.Property(e => e.ChangedByPatientId).HasColumnName("changed_by_patient_id");
            entity.Property(e => e.ChangedField)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("changed_field");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.NewValue)
                .IsUnicode(false)
                .HasColumnName("new_value");
            entity.Property(e => e.OldValue)
                .IsUnicode(false)
                .HasColumnName("old_value");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.TableName)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("table_name");
            entity.Property(e => e.ValidFrom)
                .HasColumnType("datetime")
                .HasColumnName("valid_from");
            entity.Property(e => e.ValidTo)
                .HasColumnType("datetime")
                .HasColumnName("valid_to");

            entity.HasOne(d => d.ChangedByAdmin).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.ChangedByAdminId)
                .HasConstraintName("FK_AUDIT_LOG_admin_id");

            entity.HasOne(d => d.ChangedByInvestigator).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.ChangedByInvestigatorId)
                .HasConstraintName("FK_AUDIT_LOG_investigator_id");

            entity.HasOne(d => d.ChangedByPatient).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.ChangedByPatientId)
                .HasConstraintName("FK_AUDIT_LOG_patient_id");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("CATEGORIES", "core");

            entity.HasIndex(e => e.CategoryName, "UQ_CATEGORIES_category_name").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.ToTable("COMPLAINTS", "grievance");

            entity.HasIndex(e => e.DepartmentId, "IX_COMPLAINTS_department_id");

            entity.HasIndex(e => e.PatientId, "IX_COMPLAINTS_patient_id");

            entity.HasIndex(e => e.StatusId, "IX_COMPLAINTS_status_id");

            entity.Property(e => e.ComplaintId).HasColumnName("complaint_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ConvertedToGrievance).HasColumnName("converted_to_grievance");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.EncounterId).HasColumnName("encounter_id");
            entity.Property(e => e.GrievanceId).HasColumnName("grievance_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.ResolvedAt)
                .HasColumnType("datetime")
                .HasColumnName("resolved_at");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_COMPLAINTS_category_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMPLAINTS_department_id");

            entity.HasOne(d => d.Encounter).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.EncounterId)
                .HasConstraintName("FK_COMPLAINTS_encounter_id");

            entity.HasOne(d => d.Grievance).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.GrievanceId)
                .HasConstraintName("FK_COMPLAINTS_grievance_id");

            entity.HasOne(d => d.Patient).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMPLAINTS_patient_id");

            entity.HasOne(d => d.Status).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMPLAINTS_status_id");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("DEPARTMENTS", "core");

            entity.HasIndex(e => e.DepartmentEmail, "UQ_DEPARTMENTS_department_email").IsUnique();

            entity.HasIndex(e => e.DepartmentName, "UQ_DEPARTMENTS_department_name").IsUnique();

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.DepartmentEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("department_email");
            entity.Property(e => e.DepartmentHead)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("department_head");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("department_name");
        });

        modelBuilder.Entity<Encounter>(entity =>
        {
            entity.ToTable("ENCOUNTERS", "clinical");

            entity.HasIndex(e => e.DepartmentId, "IX_ENCOUNTERS_department_id");

            entity.HasIndex(e => e.PatientId, "IX_ENCOUNTERS_patient_id");

            entity.Property(e => e.EncounterId).HasColumnName("encounter_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.EncounterDate)
                .HasColumnType("datetime")
                .HasColumnName("encounter_date");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Encounters)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENCOUNTERS_department_id");

            entity.HasOne(d => d.Patient).WithMany(p => p.Encounters)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENCOUNTERS_patient_id");
        });

        modelBuilder.Entity<Escalation>(entity =>
        {
            entity.ToTable("ESCALATIONS", "grievance");

            entity.HasIndex(e => e.GrievanceId, "IX_ESCALATIONS_grievance_id");

            entity.Property(e => e.EscalationId).HasColumnName("escalation_id");
            entity.Property(e => e.ActionTaken)
                .IsUnicode(false)
                .HasColumnName("action_taken");
            entity.Property(e => e.EscalatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("escalated_at");
            entity.Property(e => e.EscalatedFrom).HasColumnName("escalated_from");
            entity.Property(e => e.EscalatedTo).HasColumnName("escalated_to");
            entity.Property(e => e.GrievanceId).HasColumnName("grievance_id");
            entity.Property(e => e.ResolvedAt)
                .HasColumnType("datetime")
                .HasColumnName("resolved_at");

            entity.HasOne(d => d.EscalatedFromNavigation).WithMany(p => p.EscalationEscalatedFromNavigations)
                .HasForeignKey(d => d.EscalatedFrom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESCALATIONS_escalated_from");

            entity.HasOne(d => d.EscalatedToNavigation).WithMany(p => p.EscalationEscalatedToNavigations)
                .HasForeignKey(d => d.EscalatedTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESCALATIONS_escalated_to");

            entity.HasOne(d => d.Grievance).WithMany(p => p.Escalations)
                .HasForeignKey(d => d.GrievanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ESCALATIONS_grievance_id");
        });

        modelBuilder.Entity<Grievance>(entity =>
        {
            entity.ToTable("GRIEVANCES", "grievance");

            entity.HasIndex(e => e.DepartmentId, "IX_GRIEVANCES_department_id");

            entity.HasIndex(e => e.DueDate, "IX_GRIEVANCES_due_date");

            entity.HasIndex(e => e.InvestigatorId, "IX_GRIEVANCES_investigator_id");

            entity.HasIndex(e => e.PatientId, "IX_GRIEVANCES_patient_id");

            entity.HasIndex(e => e.StatusId, "IX_GRIEVANCES_status_id");

            entity.HasIndex(e => e.GrievanceNumber, "UQ_GRIEVANCES_grv_number").IsUnique();

            entity.Property(e => e.GrievanceId).HasColumnName("grievance_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ClosedAt)
                .HasColumnType("datetime")
                .HasColumnName("closed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.DueDate)
                .HasColumnType("datetime")
                .HasColumnName("due_date");
            entity.Property(e => e.EncounterId).HasColumnName("encounter_id");
            entity.Property(e => e.GrievanceNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("grievance_number");
            entity.Property(e => e.InvestigatorId).HasColumnName("investigator_id");
            entity.Property(e => e.IsEscalated).HasColumnName("is_escalated");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Severity)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("severity");
            entity.Property(e => e.SlaId).HasColumnName("sla_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Grievances)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_GRIEVANCES_category_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Grievances)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GRIEVANCES_department_id");

            entity.HasOne(d => d.Encounter).WithMany(p => p.Grievances)
                .HasForeignKey(d => d.EncounterId)
                .HasConstraintName("FK_GRIEVANCES_encounter_id");

            entity.HasOne(d => d.Investigator).WithMany(p => p.Grievances)
                .HasForeignKey(d => d.InvestigatorId)
                .HasConstraintName("FK_GRIEVANCES_investigator_id");

            entity.HasOne(d => d.Patient).WithMany(p => p.Grievances)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GRIEVANCES_patient_id");

            entity.HasOne(d => d.Sla).WithMany(p => p.Grievances)
                .HasForeignKey(d => d.SlaId)
                .HasConstraintName("FK_GRIEVANCES_sla_id");

            entity.HasOne(d => d.Status).WithMany(p => p.Grievances)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GRIEVANCES_status_id");
            entity.Property(e => e.GrievanceSummary).HasMaxLength(1000).IsUnicode(false).HasColumnName("grievance_summary");

            entity.Property(e => e.MatchedGrievanceNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("matched_grievance_number");

            entity.Property(e => e.EstimatedTimeSavedHrs)
                .HasColumnType("decimal(10,2)")
                .HasColumnName("estimated_time_saved_hrs");
        });

        modelBuilder.Entity<Investigator>(entity =>
        {
            entity.ToTable("INVESTIGATORS", "grievance");

            entity.HasIndex(e => e.EmployeeCode, "UQ_INVESTIGATORS_emp_code").IsUnique();

            entity.Property(e => e.InvestigatorId).HasColumnName("investigator_id");
            entity.Property(e => e.CurrentLoad).HasColumnName("current_load");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("employee_code");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Department).WithMany(p => p.Investigators)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INVESTIGATORS_department_id");

            entity.HasOne(d => d.User).WithMany(p => p.Investigators)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_INVESTIGATOR_USER_ID");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("PATIENTS", "core");

            entity.HasIndex(e => e.Email, "IX_PATIENTS_email");

            entity.HasIndex(e => e.Email, "UQ_PATIENTS_email").IsUnique();

            entity.HasIndex(e => e.Mrn, "UQ_PATIENTS_mrn").IsUnique();

            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Address)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Mrn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mrn");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.HasOne(d => d.User).WithMany(p => p.Patients)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_PATIENT_USER_ID");
        });

        modelBuilder.Entity<PatientCommunication>(entity =>
        {
            entity.HasKey(e => e.CommId);

            entity.ToTable("PATIENT_COMMUNICATIONS", "comms");

            entity.HasIndex(e => e.GrievanceId, "IX_PATIENT_COMMS_grievance_id");

            entity.HasIndex(e => e.PatientId, "IX_PATIENT_COMMS_patient_id");

            entity.Property(e => e.CommId).HasColumnName("comm_id");
            entity.Property(e => e.GrievanceId).HasColumnName("grievance_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("sent_at");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Grievance).WithMany(p => p.PatientCommunications)
                .HasForeignKey(d => d.GrievanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PATIENT_COMMS_grievance_id");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientCommunications)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PATIENT_COMMS_patient_id");

            entity.HasOne(d => d.Status).WithMany(p => p.PatientCommunications)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PATIENT_COMMS_status_id");
        });

        modelBuilder.Entity<Resolution>(entity =>
        {
            entity.ToTable("RESOLUTIONS", "grievance");

            entity.HasIndex(e => e.GrievanceId, "UQ_RESOLUTIONS_grv_id").IsUnique();

            entity.Property(e => e.ResolutionId).HasColumnName("resolution_id");
            entity.Property(e => e.ApprovedByAdminId).HasColumnName("approved_by_admin_id");
            entity.Property(e => e.CorrectiveAction)
                .IsUnicode(false)
                .HasColumnName("corrective_action");
            entity.Property(e => e.GrievanceId).HasColumnName("grievance_id");
            entity.Property(e => e.InvestigatorId).HasColumnName("investigator_id");
            entity.Property(e => e.PreventiveAction)
                .IsUnicode(false)
                .HasColumnName("preventive_action");
            entity.Property(e => e.ResolutionSummary)
                .IsUnicode(false)
                .HasColumnName("resolution_summary");
            entity.Property(e => e.ResolvedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("resolved_at");
            entity.Property(e => e.RootCause)
                .IsUnicode(false)
                .HasColumnName("root_cause");

            entity.HasOne(d => d.ApprovedByAdmin).WithMany(p => p.Resolutions)
                .HasForeignKey(d => d.ApprovedByAdminId)
                .HasConstraintName("FK_RESOLUTIONS_approved_by_admin_id");

            entity.HasOne(d => d.Grievance).WithOne(p => p.Resolution)
                .HasForeignKey<Resolution>(d => d.GrievanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RESOLUTIONS_grievance_id");

            entity.HasOne(d => d.Investigator).WithMany(p => p.Resolutions)
                .HasForeignKey(d => d.InvestigatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RESOLUTIONS_investigator_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__ROLES__8AFACE1A00EEF5AF");

            entity.ToTable("ROLES");

            entity.HasIndex(e => e.RoleName, "UQ__ROLES__8A2B616089151152").IsUnique();

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sla>(entity =>
        {
            entity.ToTable("SLA", "core");

            entity.Property(e => e.SlaId).HasColumnName("sla_id");
            entity.Property(e => e.AcknowledgmentTimeHrs).HasColumnName("acknowledgment_time_hrs");
            entity.Property(e => e.EscalationThresholdHrs).HasColumnName("escalation_threshold_hrs");
            entity.Property(e => e.InvestigationTimeDays).HasColumnName("investigation_time_days");
            entity.Property(e => e.ResolutionTimeDays).HasColumnName("resolution_time_days");
            entity.Property(e => e.SeverityLevel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("severity_level");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("STATUS", "core");

            entity.HasIndex(e => e.StatusName, "UQ_STATUS_status_name").IsUnique();

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__USERS__1788CC4CCC33B025");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.Email, "UQ__USERS__A9D105345E6005E9").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USERS_ROLE_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
