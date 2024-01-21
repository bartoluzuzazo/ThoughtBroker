using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ThoughtBroker.Application.UserServices.Commands;
using ThoughtBroker.Domain.Users;
using ThoughtBroker.Infrastructure.Context;
using ThoughtBroker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(AddUserCommand)));
builder.Services.AddDbContext<EfDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestingDefault")));
builder.Services.AddScoped<IUserRepository, UserRepository>();

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