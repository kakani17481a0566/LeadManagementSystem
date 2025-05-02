using Amazon.Lambda.Core;
using LeadManagementSystem.Data;
using LeadManagementSystem.Services;
using LeadManagementSystem.Services.Calllog;
using LeadManagementSystem.Services.Lead;
using LeadManagementSystem.Services.Role;
using LeadManagementSystem.Services.ServiceImpl;
using LeadManagementSystem.Services.User;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// AWS Lambda Hosting
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

// Controllers and API support
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database: PostgreSQL Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Service Registrations
builder.Services.AddScoped<LeadService>();
builder.Services.AddScoped<LeadService_>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICallLogService, CallLogService>();


// SalesPerson Service
builder.Services.AddTransient<SalesPersonService, SalesPersonServiceImpl>();

// Logging
builder.Services.AddLogging();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") 
               .AllowAnyHeader()
               .AllowAnyMethod();
    }); 
}); 

var app = builder.Build();

// Middleware for Development and Production environments
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Detailed error page
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Generic error page
    app.UseHsts(); // Enforce HTTPS
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowFrontend"); 

app.Run();
