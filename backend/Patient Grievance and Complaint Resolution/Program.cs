using Microsoft.EntityFrameworkCore;
using Patient_Grievance_and_Complaint_Resolution.Data;
using Patient_Grievance_and_Complaint_Resolution.Repository;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;
using Patient_Grievance_and_Complaint_Resolution.Services;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Register Repositories
builder.Services.AddScoped<IResolutionRepository, ResolutionRepository>();
builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();
;


// ✅ Register Services
builder.Services.AddScoped<IResolutionService, ResolutionService>();
builder.Services.AddScoped<IComplaintService, ComplaintService>();


// ✅ Controllers
builder.Services.AddControllers();

// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// (Auth not added yet — Swagger testing ✅)
app.UseAuthorization();

app.MapControllers();

app.Run();