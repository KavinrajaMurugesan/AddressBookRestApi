
using System.ComponentModel.DataAnnotations;

namespace AddressBookApi.Entities.DTO
{
    public class UserDetailsCreateDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MaxLength(31)]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$", ErrorMessage = "This is not vaild type")]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public List<EmailDto> Emails { get; set; }
        [Required]
        public List<AddressDto> Addresses { get; set; }
        [Required]
        public List<PhoneDto> Phones { get; set; }
    }
}
