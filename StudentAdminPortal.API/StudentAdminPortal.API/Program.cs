using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configuring DbContext using Dependency Injection
//Injecting Context in Builder Services
builder.Services.AddDbContext<StudentAdminContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("StudentAdminPortalDb")));

//Here IStudentRepository is Injected through Builder Service using IoC Principle
//AddScoped operation is used to implement DI
builder.Services.AddScoped<IStudentRepository, SqlStudentRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injecting AutoMapper
//When the app runs, it will go to the Program class
//Get its assembly name which is the entire application
//Search for all AutoMapper profiles that has been inherited from the Profile Class
//And Create Maps 
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

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
