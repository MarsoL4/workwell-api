using WorkWell.API.Extensions;
using WorkWell.API.Filters;
using WorkWell.API.HealthChecks;
using WorkWell.API.Security;
using WorkWell.Application.DependencyInjection;
using WorkWell.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add Infrastructure and DbContext (Oracle connection)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// Add controllers with global model validation filter
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ModelValidationFilter>();
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Versioning
builder.Services.AddWorkWellApiVersioning();

// Health Check
builder.Services.AddWorkWellHealthChecks();

// API Key Security
builder.Services.AddApiKeySecurity(builder.Configuration);

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global Error + API Key Middlewares
app.UseMiddleware<WorkWell.API.Middleware.ErrorHandlingMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

// Health check endpoint
app.MapHealthChecks("/health");

app.MapControllers();

app.Run();