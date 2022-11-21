using AddressBookApi.Entities.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBookApi.Entities.Model
{
    public class Address:EntityBase
    {
        [Key]
        public Guid AddressId { get; set; }
        [Required]
        public string? Line1 { get; set; }
        [Required]
        public string Line2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int ZipCode { get; set; }
        [Required]
        public string StateName { get; set; }
        [Required]
        public Guid RefTermAddressId { get; set; }
        [Required]
        public Guid RefTermCountryId { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public UserDetails UserDetails { get; set; }

    }
}
