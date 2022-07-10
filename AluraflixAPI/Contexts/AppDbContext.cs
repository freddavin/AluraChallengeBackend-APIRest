using AluraflixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AluraflixAPI.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Categoria categoriaLivre = new Categoria
            {
                Id = 1,
                Titulo = "LIVRE",
                Cor = "Verde"
            };
            modelBuilder.Entity<Categoria>().HasData(categoriaLivre);

            modelBuilder.Entity<Categoria>()
                .HasMany(categoria => categoria.Videos)
                .WithOne(video => video.Categoria)
                .HasForeignKey(video => video.IdCategoria);

        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
