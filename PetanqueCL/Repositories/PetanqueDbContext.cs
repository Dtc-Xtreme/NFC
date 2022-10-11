using Microsoft.EntityFrameworkCore;
using PetanqueCL;
using PetanqueCL.Models;

namespace Repositories
{
    public class PetanqueDbContext : DbContext
    {
        private Settings settings = new Settings();
        public PetanqueDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(settings.sql);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Gender> Genders { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Federation> Federations { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<License> Licenses { get; set; }
    }
}
