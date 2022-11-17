using PetanqueCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDev.Services
{
    public interface IRepository<T>
    {
        public Task<List<T>> GetAll();
    }
}
