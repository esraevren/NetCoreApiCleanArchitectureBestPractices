using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //mevcut derlemedeki tüm IEntityTypeConfiguration implementasyonlarını otomatik olarak bulup uygulamak için kullanılır.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
