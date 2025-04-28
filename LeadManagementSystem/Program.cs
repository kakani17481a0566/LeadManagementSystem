using Amazon.Lambda.Core;




using Microsoft.EntityFrameworkCore;
using LeadManagementSystem.Data;
using LeadManagementSystem.Services.Role;
using LeadManagementSystem.Services.User;
using LeadManagementSystem.Services.Lead;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<LeadService>();
builder.Services.AddScoped<LeadService_>();


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
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Global error handler
    app.UseHsts(); // Use HTTP Strict Transport Security in production
}

app.UseHttpsRedirection();

app.UseAuthorization(); // Add if using [Authorize]

app.MapControllers(); // Register controller endpoints

app.Run();
