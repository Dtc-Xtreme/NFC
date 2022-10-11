using PetanqueCL.Models;
using Repositories;

namespace PetanqueCL.Repositories.Interfaces
{
    public interface IGenderRepository : IBaseRepository<Gender>
    {
        IQueryable<Gender> Genders { get; }
    }
}
