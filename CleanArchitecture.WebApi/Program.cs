using CleanArchitecture.Application.Services; 
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddAutoMapper(typeof(CleanArchitecture.Persistance.AssemblyReferance).Assembly);

string connectionString = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers()
    .AddApplicationPart(typeof(CleanArchitecture.Presentation.AssemblyReference).Assembly);

builder.Services.AddMediatR(cfr => cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReferance).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
