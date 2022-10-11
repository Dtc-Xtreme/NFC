using PetanqueCL.Models;
using Repositories;

namespace PetanqueCL.Repositories
{
    public interface IProvinceRepository : IBaseRepository<Province>
    {
        IQueryable<Province> Provinces { get; }
    }
}
