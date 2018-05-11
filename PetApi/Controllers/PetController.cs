using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PetApiRepository;
using PetApi.Services;

namespace PetApi.Controllers
{
    public class PetController : ApiController
    {
        private readonly IPetService<Owners> _petService = null;
        public PetController(IPetService<Owners> petService)
        {
            _petService = petService;
        }

        public IHttpActionResult GetAll()
        {
            List<CatOwners> catOwners= _petService.GetAll();
            if(catOwners==null)
            {
                return NotFound();
            }
            return Ok(catOwners);
        }
    }
}
