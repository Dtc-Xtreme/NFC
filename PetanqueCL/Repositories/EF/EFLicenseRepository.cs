using Microsoft.EntityFrameworkCore;
using PetanqueCL.Models;
using PetanqueCL.Repositories.Interfaces;
using Repositories;
namespace PetanqueCL.Repositories.EF
{
    public class EFLicenseRepository : ILicenseRepository
    {
        private  PetanqueDbContext context;

        public EFLicenseRepository(PetanqueDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<License> Licenses => context.Licenses.Include(c=>c.Gender).Include(c=>c.Club).ThenInclude(c=>c.Province).ThenInclude(c=>c.Federation).OrderBy(c=>c.Id);


        public RepositoryResult Create(License arg)
        {
            try
            {
                context.Licenses.Add(arg);
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
        public RepositoryResult Delete(License arg)
        {
            try
            {
                context.Licenses.Remove(arg);
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
        public RepositoryResult Save(License arg)
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
