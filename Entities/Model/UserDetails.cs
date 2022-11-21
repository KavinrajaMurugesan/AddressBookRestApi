using AddressBookApi.Entities.DTO;
using AddressBookApi.Entities.Model;
using System.ComponentModel.DataAnnotations;

namespace AddressBookApi.Entities.Model
{
    public class UserDetails:EntityBase
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string  FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public ICollection<Email> Emails { get; set; }
        [Required]
        public ICollection<Address> Addresses { get; set; }
        [Required]
        public ICollection<Phone> Phones { get; set; }
    }
}
