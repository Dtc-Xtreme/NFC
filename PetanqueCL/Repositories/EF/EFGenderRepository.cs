using PetanqueCL.Models;
using PetanqueCL.Repositories.Interfaces;
using Repositories;
namespace PetanqueCL.Repositories.EF
{
    public class EFGenderRepository : IGenderRepository
    {
        private  PetanqueDbContext context;

        public EFGenderRepository(PetanqueDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Gender> Genders => context.Genders.OrderBy(c=>c.Id);


        public RepositoryResult Create(Gender arg)
        {
            try
            {
                context.Genders.Add(arg);
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
        public RepositoryResult Delete(Gender arg)
        {
            try
            {
                context.Genders.Remove(arg);
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
        public RepositoryResult Save(Gender arg)
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
