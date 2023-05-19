using Microsoft.EntityFrameworkCore;
using RoomReservationSystem;
using RoomReservationSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RoomReservationDb")));

builder.Services.AddScoped<RoomReservationApiSeeder>();

builder.Services.AddCors(opt => opt.AddPolicy("CorsPolicy", builder =>
{
    builder.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

var app = builder.Build();

var scope = app.Services?.CreateScope();
var seeder = scope?.ServiceProvider.GetRequiredService<RoomReservationApiSeeder>();
seeder?.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
