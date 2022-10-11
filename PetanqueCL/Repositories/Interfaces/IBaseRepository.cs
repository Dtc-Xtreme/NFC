using PetanqueCL.Models;
using Repositories;

namespace PetanqueCL.Repositories
{
    public interface IBaseRepository<T>
    {
        RepositoryResult Create(T arg);
        RepositoryResult Save(T arg);
        RepositoryResult Delete(T arg);
    }
}
