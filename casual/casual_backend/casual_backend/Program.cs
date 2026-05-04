using casual_backend;
using casual_backend.Services;
using casual_backend.Utils;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
string? connectingString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyDbContext>(options =>options.UseMySQL(connectingString));
builder.Services.AddScoped<IJobDataService, JobDataService>();
builder.Services.AddHostedService<DataSyncWorker>();
//注册CORS
builder.Services.AddCors( options =>
{
    options.AddPolicy("Frontend", polocy => 
    {
        polocy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    //注入scalar 服务
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("Frontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
