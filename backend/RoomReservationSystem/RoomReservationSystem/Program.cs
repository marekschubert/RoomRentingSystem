using Microsoft.EntityFrameworkCore;
using RoomReservationSystem;
using RoomReservationSystem.Models;
using RoomReservationSystem.Repository.Implementations;
using RoomReservationSystem.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

/*
TODO: Endpoints:

- layer/id/X/Y (GET): Room availability on layer {id} from time X to time Y (ex. 7:15am -> X = 435). In JSON there should be room names and their availability (true/false)
- users (GET): All users with ids, first names and last names
- user/id (GET): First name and last name of user {id}
- reservations (POST): JSON: Room name, organizer’s id, start time, end time,  participants’ id. Time should be in minutes
- reservations/id (GET): All reservations made by organizer of {id}. In JSON there should be room name, start time, end time, participants’ id
- reservations (GET): All reservations 

 * 
 */

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RoomReservationDb")));

builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ILayerService, LayerService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<RoomReservationApiSeeder>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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
