using _21WithEmilyWeb.Api.Data;
using _21WithEmilyWeb.Api.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add database.
var connection = new SqliteConnection("Data Source=:memory:");
connection.Open();

builder.Services.AddSingleton(connection);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connection));

// Add services to the container.
builder.Services.AddScoped<GameService>();
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Ensure database is ready
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
