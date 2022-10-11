using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetanqueCL.Repositories
{
    public class RepositoryResult
    {
        public bool Succes { get; set; }
        public string Message { get; set; }

        public RepositoryResult(bool succes, string message)
        {
            Succes = succes;
            Message = message;
        }
    }
}
