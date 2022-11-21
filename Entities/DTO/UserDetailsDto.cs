
namespace AddressBookApi.Entities.DTO
{
    public class UserDetailsDto
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<EmailDto> Emails { get; set; }

        public List<AddressDto> Addresses { get; set; }

        public List<PhoneDto> Phones { get; set; }
    }
}
