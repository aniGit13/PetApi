using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetApiRepository;

namespace PetApi.Services
{
    public class PetService<T> : IPetService<T> where T : class    
    {
        private readonly IRepository<T> _iPetRepository;
        public PetService(IRepository<T> petRepository)
        {
            _iPetRepository = petRepository;
        }
        
        public List<CatOwners> GetAll()
        {
            List<Owners> owners= _iPetRepository.GetAll() as List<Owners>;

            List<CatOwners> catOwners = new List<CatOwners>();

            catOwners.Add(GetGenderSpecificCats(owners, "Male"));

            catOwners.Add(GetGenderSpecificCats(owners, "Female"));


            return catOwners;
        }

        private CatOwners GetGenderSpecificCats(List<Owners> owners, string sGender)
        {
            CatOwners gCatOwners = null;

            var gOwners = owners.Where(m => m.Gender == sGender);
            if (gOwners.Count() > 0)
            {
                gCatOwners = new CatOwners();
                gCatOwners.Gender = sGender;

                List<string> catNames = new List<string>();

                foreach (var g in gOwners)
                {
                    if (g.Pets != null)
                    {
                        var cats = g.Pets.Where(c => c.Type == "Cat");

                        foreach (var cat in cats)
                        {
                            catNames.Add(cat.Name);
                        }
                    }
                }
                catNames = catNames.OrderBy(n=>n).ToList();
                gCatOwners.Cats = catNames;                
            }

            return gCatOwners;
        }
    }
}