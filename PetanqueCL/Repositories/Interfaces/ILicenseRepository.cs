using PetanqueCL.Models;
using Repositories;

namespace PetanqueCL.Repositories
{
    public interface ILicenseRepository : IBaseRepository<License>
    {
        IQueryable<License> Licenses { get; }
    }
}
