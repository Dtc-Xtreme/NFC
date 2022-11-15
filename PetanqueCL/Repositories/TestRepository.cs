using PetanqueCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetanqueCL.Repositories
{
    public class TestRepository
    {
        List<License> Licenses { get; set; }

        public TestRepository()
        {
            this.Licenses = new List<License>();

            this.Licenses.Add(new License());
            this.Licenses.Add(new License());
        }
    }
}
