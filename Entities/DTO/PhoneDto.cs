using System.ComponentModel.DataAnnotations;

namespace AddressBookApi.Entities.DTO
{
    public class PhoneDto
    {
        [Required]
        public long PhoneNumber { get; set; }
        [Required]
        public Types Type { get; set; }
    }
}
