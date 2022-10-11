using PetanqueCL.Models;
using PetanqueCL.Repositories.Interfaces;
using Repositories;
namespace PetanqueCL.Repositories.EF
{
    public class EFFederationRepository : IFederationRepository
    {
        private  PetanqueDbContext context;

        public EFFederationRepository(PetanqueDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Federation> Federations => context.Federations.OrderBy(c=>c.Id);


        public RepositoryResult Create(Federation arg)
        {
            try
            {
                context.Federations.Add(arg);
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
        public RepositoryResult Delete(Federation arg)
        {
            try
            {
                context.Federations.Remove(arg);
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
        public RepositoryResult Save(Federation arg)
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
