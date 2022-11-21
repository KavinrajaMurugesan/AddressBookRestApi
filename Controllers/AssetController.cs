using AddressBookApi.Contract;
using AddressBookApi.Entities.DTO;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AddressBookApi.Controllers
{
    [Route("api/[Controller]")]
    public class AssetController : ControllerBase
    {
        private readonly IAddressService addressService;
        public AssetController(IAddressService addressService)
        {
            this.addressService = addressService;
        }
        [HttpPost]
        [Route("UploadFile")]
        [Authorize]
        public IActionResult Post(IFormFile file)
        {
            var identify = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            if (addressService.ValidateUser(identity))
            {
                return Ok(addressService.UploadFile(file));
            }
            return Unauthorized();
        }
        [HttpGet]
        [Route("DownloadFile/{Id}")]
        public IActionResult Get(Guid Id)
        {
            var identify = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            if (addressService.ValidateUser(identity))
            {
                FileDownloadDto image = addressService.fileDownload(Id);
                return File(image.FileContent, image.FileType);
            }
            return Unauthorized();
        }
    }
}

