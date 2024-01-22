using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ThoughtBroker.Application.ThoughtServices.Commands;
using ThoughtBroker.Application.UserServices.Commands;
using ThoughtBroker.Domain.Thoughts;
using ThoughtBroker.Domain.Users;
using ThoughtBroker.Infrastructure.Context;
using ThoughtBroker.Infrastructure.Repositories;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(AddUserCommand)));
builder.Services.AddDbContext<EfDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestingDefault")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IThoughtRepository, ThoughtRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,policy =>
    {
        policy.WithOrigins("localhost:3000") // or AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

builder.Services.AddAuthentication().AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();