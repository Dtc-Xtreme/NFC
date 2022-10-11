using Microsoft.EntityFrameworkCore;
using PetanqueCL.Models;
using PetanqueCL.Repositories.Interfaces;
using Repositories;
namespace PetanqueCL.Repositories.EF
{
    public class EFClubRepository : IClubRepository
    {
        private  PetanqueDbContext context;

        public EFClubRepository(PetanqueDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Club> Clubs => context.Clubs.Include(c=>c.Province).ThenInclude(c=>c.Federation).OrderBy(c=>c.Id);


        public RepositoryResult Create(Club arg)
        {
            try
            {
                context.Clubs.Add(arg);
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
        public RepositoryResult Delete(Club arg)
        {
            try
            {
                context.Clubs.Remove(arg);
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
        public RepositoryResult Save(Club arg)
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
