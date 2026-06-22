using Microsoft.EntityFrameworkCore;
using Patient_Grievance_and_Complaint_Resolution.BackgroundServices;
using Patient_Grievance_and_Complaint_Resolution.Middleware;
using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Repository;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interface;
using Patient_Grievance_and_Complaint_Resolution.Service;
using Patient_Grievance_and_Complaint_Resolution.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IInvestigatorRepository,
                           InvestigatorRepository>();

builder.Services.AddScoped<IInvestigatorService,
                           InvestigatorService>();
builder.Services.AddScoped<
    IEscalationService,
    EscalationService>();
builder.Services.AddHostedService<
    EscalationBackgroundService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
