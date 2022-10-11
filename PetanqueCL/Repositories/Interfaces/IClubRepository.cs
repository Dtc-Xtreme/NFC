using PetanqueCL.Models;
using Repositories;

namespace PetanqueCL.Repositories.Interfaces
{
    public interface IClubRepository : IBaseRepository<Club>
    {
        IQueryable<Club> Clubs { get; }
    }
}
