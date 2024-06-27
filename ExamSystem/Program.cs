using System.Reflection;
using System.Text.Json;
using ExamSystem.AuthImplements;
using ExamSystem.AuthInterfaces;
using ExamSystem.Dto;
using ExamSystem.Infra;
using ExamSystem.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStudentOrder, HeadEndToMiddleStudentOrder>();
builder.Services.AddScoped<IStudentCreator, RandomStudentCreator>();
builder.Services.AddScoped<IValidator<GenerateRequest>, GenerateRequestValidator>();
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExamSystem", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddHealthChecks()
    .AddUrlGroup(new Uri("https://www.baidu.com/"), "baidu", HealthStatus.Degraded, new[] { "baidu" },
        TimeSpan.FromSeconds(10))
    .AddNpgSql("Host=localhost;Port=5432;Database=mydb;Username=myuser;Password=mypass", name: "my-postgres-db",
        tags: new[] { "postgres" });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(
            new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(e => new { name = e.Key, status = e.Value.Status.ToString() })
            });
        await context.Response.WriteAsync(result);
    }
});

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();
app.UseHttpsRedirection();
app.Run();