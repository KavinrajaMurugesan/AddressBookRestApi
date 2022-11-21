using AddressBookApi.Contract;
using AddressBookApi.Entities.DTO;
using AddressBookApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AddressBookApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController:ControllerBase
    {
        private readonly IAddressService addressService;

        public AccountController(IAddressService addressService)
        {
            this.addressService = addressService;
        }
        /// <summary>
        ///  This post Method is used for the creating the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>IActionResult</returns>

        [HttpPost]
        public IActionResult Post(UserDetailsCreateDto user)
        {
            try
            {
                if (addressService.ValidateUserName(user) == true)
                {
                    return Conflict("UserName is already Taken");
                }
                else if (!addressService.EmailValidate(user))
                {
                    return Conflict("EmailAddress is already Exist");
                }
                else if (!addressService.GetRefterm(user.Addresses, user.Emails, user.Phones))
                {
                    return NotFound("RefTerm Not Found");
                }
                var User = addressService.AddUser(user);
                return Ok(User);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
            return BadRequest();
        }
        /// <summary>
        ///  This Get is used to getall the user
        /// </summary>
        /// <param name="Page">Pagination as input</param>
        /// <returns>List of user</returns>
        
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromBody] Pagination Page)
        {
            var identify = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            if (addressService.ValidateUser(identity))
            {
                try
                {
                   
                    return Ok(addressService.GetAllUsers(Page));
                }
                catch (InvalidOperationException)
                {
                    return NoContent();
                }
            }
            return Unauthorized();
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public IActionResult GetById(Guid id)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            if (addressService.ValidatingUser(identity, id))
            {
                try
                {
                    
                    return Ok(addressService.GetById(id));
                }
                catch (InvalidOperationException)
                {
                    
                    return NotFound();
                }
            }
           else
            {
               
                return Unauthorized();
            } 
            return StatusCode(500);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var identify = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            if (addressService.ValidatingUser(identity, id))
              {
                if (addressService.DeleteUser(id) == true)
                {
                    return Ok();
                }
                return NotFound("Id Not Found");
             }
            return Unauthorized();
        }
        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public IActionResult Update(UserDetailsCreateDto user,Guid id)
        {
             var identify = HttpContext.User.Identity as ClaimsIdentity;
             ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
             if (addressService.ValidatingUser(identity, id))
             {
                var updatedUser = addressService.Update(user, id);
                if (addressService.Update(user, id) == null)
                {
                    return NotFound("UserdId Not found");
                }
                return Ok(updatedUser);
             }
             return Unauthorized();
        }
        [HttpGet]
        [Route("count")]
        [Authorize]
        public IActionResult GetCount()
        {
            var identify = HttpContext.User.Identity as ClaimsIdentity;
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            if (addressService.ValidateUser(identity))
            {
                return Ok(addressService.CountUser());
            }
            return Unauthorized();
        }
    }
}
