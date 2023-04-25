using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using westcoast_education.api.Data;
using westcoast_education.api.Data.json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Konfigurerat databashanteringen...
builder.Services.AddDbContext<EducationContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo{
        Version = "v1",
        Title = "Westcoast Education API",
        Description = "Ett api för att hantera kurser, lärare och studenter",
        TermsOfService = new Uri("https://westcoast-education.se/terms"),
        Contact = new OpenApiContact{
            Name = "Teknisk support",
            Url = new Uri("https://westcoast-education.se/contact")
        },
        License = new OpenApiLicense{
            Name = "Licens",
            Url = new Uri("https://westcoast-education.se/license")
        }
    });
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

builder.Services.AddCors();

var app = builder.Build();

// Seed the database...
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<EducationContext>();
    await context.Database.MigrateAsync();

    await SeedData.LoadTeacherData(context);
    await SeedData.LoadCourseData(context);
    await SeedData.LoadStudentData(context);
    await SeedData.LoadStudentCourseData(context);
}
catch (Exception ex)
{
    System.Console.WriteLine(ex.Message);
    throw;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseAuthorization();

app.UseCors(c => c.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://127.0.0.1:5500"));

app.MapControllers();

app.Run();
