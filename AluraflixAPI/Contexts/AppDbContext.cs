using AluraflixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AluraflixAPI.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }
    }
}
