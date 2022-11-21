using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBookApi.Entities.DTO
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MaxLength(31)]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$",ErrorMessage ="This is not vaild type")]
        public string Password { get; set; }    
    }
}
