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
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Global error handler
    app.UseHsts(); // Use HTTP Strict Transport Security in production
}

app.UseHttpsRedirection();


<<<<<<< Updated upstream
app.UseAuthorization();
=======
app.UseAuthorization(); //test
>>>>>>> Stashed changes

app.MapControllers(); // Register controller endpoint

app.Run();
