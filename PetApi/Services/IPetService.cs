using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetApiRepository;

namespace PetApi.Services
{
    public interface IPetService<T> where T : class    
    {
        List<CatOwners> GetAll();
    }
       
}
