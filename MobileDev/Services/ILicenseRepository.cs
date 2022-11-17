using PetanqueCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev.Services
{
    public interface ILicenseRepository<T> : IRepository<T>
    {
        public Task<List<T>> Find(string arg);
        public Task<T> FindById(int id);
    }
}
