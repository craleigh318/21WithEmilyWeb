using _21WithEmilyWeb.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace _21WithEmilyWeb.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
    }
}
