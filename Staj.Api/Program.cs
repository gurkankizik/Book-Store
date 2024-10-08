using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StajWeb.DataAccess.Data;
using StajWeb.DataAccess.Repository;
using StajWeb.DataAccess.Repository.IRepository;
using System.Reflection;

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
//builder.Services.AddScoped<IValidator<CategoryDto>, CategoryValidator>();
//builder.Services.AddScoped<IValidator<ProductDto>, ProductValidator>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
dataContext.Database.Migrate();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
