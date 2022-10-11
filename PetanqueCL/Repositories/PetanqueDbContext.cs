using Microsoft.EntityFrameworkCore;
using PetanqueCL;
using PetanqueCL.Models;

namespace Repositories
{
    public class PetanqueDbContext : DbContext
    {
        public PetanqueDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=PetanqueDB;Trusted_Connection=True;");
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
