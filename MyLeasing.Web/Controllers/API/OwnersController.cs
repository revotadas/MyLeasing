using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data;

namespace MyLeasing.Web.Controllers.API {
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : Controller {
        private readonly IOwnersRepository ownersRepository;

        public OwnersController(IOwnersRepository ownersRepository) {
            this.ownersRepository = ownersRepository;
        }

        [HttpGet]
        public IActionResult GetOwners() {
            return Ok(ownersRepository.GetAll());
        }
    }
}
