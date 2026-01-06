using Amazon.Lambda.Core;


using LeadManagementSystem.Services.ServiceImpl;


using Microsoft.EntityFrameworkCore;
using LeadManagementSystem.Data;
using LeadManagementSystem.Services.Role;
using LeadManagementSystem.Services.User;
using LeadManagementSystem.Services.Lead;
using LeadManagementSystem.Services;
using LeadManagementSystem.Services.ServiceImpl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddScoped<LeadService>();
builder.Services.AddScoped<LeadService_>();
builder.Services.AddTransient<SalesPersonService, SalesPersonServiceImpl>();
// In Program.cs or Startup.cs
builder.Services.AddScoped<SalesPersonService, SalesPersonServiceImpl>();


// Database: Configure PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// DI Registrations
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();

// Optional: Logging
builder.Services.AddLogging();

var app = builder.Build();

// Environment-based middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Detailed exception page in development
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Allow CORS for local frontend
    app.UseCors(builder => builder
        .WithOrigins("http://localhost:5173", "https://lead-mgmt-msi-kakani-2025.azurewebsites.net") // Add production frontend URL here if separate, otherwise allow all or specific
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Global error handler
    app.UseHsts(); // Use HTTP Strict Transport Security in production

    // Enable CORS for Production as well (Adjust origins as needed)
     app.UseCors(builder => builder
        .AllowAnyOrigin() // For now, allow all to ensure connectivity. Ideal: specific origins.
        .AllowAnyMethod()
        .AllowAnyHeader());
}

app.UseHttpsRedirection();

// Invoke Data Seeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        DataSeeder.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.UseAuthorization(); //test


app.MapControllers(); // Register controller endpoint

app.Run();
