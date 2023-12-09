using Application.Services;
using Core.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiApplication2Context") ?? throw new InvalidOperationException("Connection string 'WebApiApplication2Context' not found.")));


// Add services to the container.
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ICountryService, CountryService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
