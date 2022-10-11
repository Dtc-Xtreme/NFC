using PetanqueCL.Models;
using Repositories;

namespace PetanqueCL.Repositories
{
    public interface IFederationRepository : IBaseRepository<Federation>
    {
        IQueryable<Federation> Federations { get; }

    }
}
