using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.Repositories;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configuring DbContext using Dependency Injection
//Injecting Context in Builder Services
builder.Services.AddDbContext<StudentAdminContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("StudentAdminPortalDb")));

//Here IStudentRepository is Injected through Builder Service using IoC Principle
//AddScoped operation is used to implement DI
builder.Services.AddScoped<IStudentRepository, SqlStudentRepository>();

//Injecting IImage repository 
builder.Services.AddScoped<IImageRepository, LocalStorageImageRepository>();

//We need to define our CORS policy here to enable API to communicate with our Angular App
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("*")
                    .AllowAnyHeader()
                    .WithMethods("*")
                    .WithExposedHeaders("*");
        });
});


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

//Since Local Storage(Resources folder) needs to be accessed to upload a file by the outside world
//we need to declare our folder as static files folder
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(AppContext.BaseDirectory, "Resources")),
//    RequestPath = "/Resources"
//});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Resources")),
    RequestPath = "/Resources"
});


app.UseRouting();

//Use the CORS policy here
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
