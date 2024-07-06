using Microsoft.EntityFrameworkCore;
using WebApplication.User.API.ApplicationDb;
using WebApplication.User.API.Endpoints;
using WebApplication.User.API.Interfaces;
using WebApplication.User.API.MigrationExtensions;
using WebApplication.User.API.Services;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrate();
}

app.UseHttpsRedirection();
app.MapUserEndpoints();
app.Run();