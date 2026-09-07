using _21WithEmilyWeb.Api.Data;
using _21WithEmilyWeb.Api.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connect to database.
if (builder.Environment.IsProduction())
{
    var connectionStringName = "AZURE_SQL_CONNECTIONSTRING";
    var connectionString = Environment.GetEnvironmentVariable(connectionStringName);
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else
{
    var connection = new SqliteConnection("Data Source=:memory:");
    connection.Open();

    builder.Services.AddSingleton(connection);
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(connection));
}

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

    if (db.Database.IsSqlite())
    {
        // For in-memory/dev SQLite: create the schema automatically
        db.Database.EnsureCreated();
    }
    else
    {
        // For SQL Server (production): apply migrations only if you accept auto-migration
        // Otherwise remove this line and apply migrations from your deployment pipeline
        db.Database.Migrate();
    }
}

app.Run();
