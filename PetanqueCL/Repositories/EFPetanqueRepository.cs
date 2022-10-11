using Microsoft.EntityFrameworkCore;
using PetanqueCL.Models;
using PetanqueCL.Repositories;

namespace Repositories
{
    public class EFPetanqueRepository : IPetanqueRepository
    {
        private PetanqueDbContext context;

        public EFPetanqueRepository(PetanqueDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Club> Clubs => context.Clubs.Include(c => c.Province).ThenInclude(c => c.Federation);
        public IQueryable<Federation> Federations => context.Federations;
        public IQueryable<Gender> Genders => context.Genders;
        public IQueryable<License> Licenses => context.Licenses.Include(c => c.Club).ThenInclude(c => c.Province).ThenInclude(c => c.Federation).Include(c => c.Gender);
        public IQueryable<Province> Provinces => context.Provinces.Include(c => c.Federation);

    }
}
