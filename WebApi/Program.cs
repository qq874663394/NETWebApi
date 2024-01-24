using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Services;
using WebApi.Repositories.WebApiDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WebApiDbContext>((serviceProvider, options) =>
{
    options.UseSqlServer("Data Source=.;Initial Catalog=WebApi;User ID=sa;Password=123456");
});

//services IOC
builder.Services.AddDomainService();


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
