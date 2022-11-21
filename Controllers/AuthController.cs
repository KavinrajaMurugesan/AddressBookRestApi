using AddressBookApi.Contract;
using AddressBookApi.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Results;

namespace AddressBookApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAddressService addressService;
        public AuthController(IAddressService addressService)
        {
            this.addressService = addressService;
        }
        [HttpPost]
        [Route("signin")]
        public IActionResult Login(LoginDto login)
        {
            try
            {
                return Ok(addressService.GenerateToken(login));
            }
            catch (NullReferenceException)
            {
                return Unauthorized();
            }
            return StatusCode(500);
        }
    }
}
