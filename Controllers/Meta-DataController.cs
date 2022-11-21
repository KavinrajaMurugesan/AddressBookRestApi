using AddressBookApi.Contract;
using AddressBookApi.Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AddressBookApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]/")]
    public class Meta_DataController : ControllerBase
    {
        private readonly IAddressService addressService;
        public Meta_DataController(IAddressService addressService)
        {
            this.addressService = addressService;
        }
        [HttpGet]
        [Route("set-ref/{Key}")]
        [Authorize]
        public IActionResult GetByKey(string Key)
        {

            if(!string.IsNullOrEmpty(Key))
            {
                RefSetDto refSetDto = addressService.GetMetadata(Key);
                if (refSetDto != null)
                {
                    return Ok(refSetDto);
                }
                return NotFound("Key not found");
            }
            return StatusCode(500);
        }
    }
}
