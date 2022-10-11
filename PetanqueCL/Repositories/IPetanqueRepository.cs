using PetanqueCL.Models;
using PetanqueCL.Repositories;

namespace Repositories
{
    public interface IPetanqueRepository
    {
        IQueryable<Club> Clubs { get; }
        IQueryable<Federation> Federations { get; }
        IQueryable<Gender> Genders { get; }
        IQueryable<License> Licenses { get; }
        IQueryable<Province> Provinces { get; }
    }
}
