using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FahimKniteComposite.Models
{
    public class GarmentDbContext : IdentityDbContext
    {
        public GarmentDbContext()
        {
        }

        public GarmentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet <Category> Categories { get; set; }
        public DbSet <Products> products { get; set; }
    }
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<GarmentDbContext>
    {
        public GarmentDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GarmentDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=GarmentsManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new GarmentDbContext(optionsBuilder.Options);
        }
    }
}
