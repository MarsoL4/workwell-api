using WorkWell.API.Extensions;
using WorkWell.API.HealthChecks;
using WorkWell.Application.DependencyInjection;
using WorkWell.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add Infrastructure and DbContext (Oracle connection)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// Add services to the container and configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWorkWellApiVersioning();
builder.Services.AddWorkWellHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<WorkWell.API.Middleware.ExceptionHandlingMiddleware>();

// Health check endpoint
app.MapHealthChecks("/health");

app.Run();