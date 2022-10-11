using Microsoft.EntityFrameworkCore;
using PetanqueCL.Models;
using PetanqueCL.Repositories.Interfaces;
using Repositories;
namespace PetanqueCL.Repositories.EF
{
    public class EFProvinceRepository : IProvinceRepository
    {
        private  PetanqueDbContext context;

        public EFProvinceRepository(PetanqueDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Province> Provinces => context.Provinces.Include(c=>c.Federation).OrderBy(c=>c.Id).Include(c=>c.Clubs);


        public RepositoryResult Create(Province arg)
        {
            try
            {
                context.Provinces.Add(arg);
                if (context.SaveChanges() != 0)
                {
                    return new RepositoryResult(true, "Toegevoegd!");
                }
                return new RepositoryResult(true, "Geen exception maar is niet gewijzigd omdat het niet veranderd is.");
            }
            catch (Exception ex)
            {
                return new RepositoryResult(false, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        public RepositoryResult Delete(Province arg)
        {
            try
            {
                context.Provinces.Remove(arg);
                if (context.SaveChanges() != 0)
                {
                    return new RepositoryResult(true, "Verwijderd!");
                }
                return new RepositoryResult(true, "Geen exception maar is niet gewijzigd omdat het niet veranderd is.");
            }
            catch (Exception ex)
            {
                return new RepositoryResult(false, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        public RepositoryResult Save(Province arg)
        {
            try
            {
                if (context.SaveChanges() != 0)
                {
                    return new RepositoryResult(true, "Opgeslagen!");
                }
                return new RepositoryResult(true, "Geen exception maar is niet gewijzigd omdat het niet veranderd is.");
            }
            catch (Exception ex)
            {
                return new RepositoryResult(false, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

    }
}
