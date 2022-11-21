using AddressBookApi.Entities.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBookApi.Entities.Model
{
    public class Phone:EntityBase
    {
        [Key]
        public Guid PhoneId { get; set; }
        [Required]
        public long PhoneNumber { get; set; }
        [Required]
        public Guid RefTermPhoneId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDetails UserDetails { get; set; }

    }
}
