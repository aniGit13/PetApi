using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetApiRepository
{
    public interface IRepository<T> where T:class
    {
        List<T> GetAll();
    }
}
