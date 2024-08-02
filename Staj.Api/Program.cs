using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Staj.Api.Dtos;
using Staj.Api.Validations;
using StajWeb.DataAccess.Data;
using StajWeb.DataAccess.Repository;
using StajWeb.DataAccess.Repository.IRepository;
using StajWeb.Models.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IValidator<CategoryDto>, CategoryValidator>();
builder.Services.AddScoped<IValidator<ProductVM>, ProductValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
