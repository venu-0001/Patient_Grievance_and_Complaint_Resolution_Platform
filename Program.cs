using Microsoft.EntityFrameworkCore;
using Patient_Grievance_and_Complaint_Resolution.Middlewares;
using Patient_Grievance_and_Complaint_Resolution.Models;
using Patient_Grievance_and_Complaint_Resolution.Repository;
using Patient_Grievance_and_Complaint_Resolution.Repository.Interfaces;
using Patient_Grievance_and_Complaint_Resolution.Services;
using Patient_Grievance_and_Complaint_Resolution.Services.Interfaces;
using Serilog; // Added Serilog Using Statement

// 1. Initialize the logger right away to catch startup errors
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Starting web application...");

    var builder = WebApplication.CreateBuilder(args);

    // 2. Tell the host to use Serilog instead of the default .NET logger
    builder.Host.UseSerilog();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("VuePolicy", policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Vue Vite URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });
    // In Program.cs (for .NET 6/7/8)
    builder.Services.AddMemoryCache();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IGrievanceRepository, GrievanceRepository>();
    builder.Services.AddScoped<IGrievanceService, GrievanceService>();
    builder.Services.AddScoped<IComplaintService, ComplaintService>();
    builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();
    builder.Services.AddScoped<IEncountersService, EncountersService>();
    builder.Services.AddScoped<IEncountersRepository, EncountersRepository>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

    // Controllers
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // 3. Add Serilog request logging to the middleware pipeline (best placed early)
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("VuePolicy");
    app.UseMiddleware<ExceptionMiddleware>();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    // 4. Ensure all logs are flushed before the app fully closes
    Log.CloseAndFlush();
}