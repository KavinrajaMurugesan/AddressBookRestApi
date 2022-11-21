using System.ComponentModel.DataAnnotations;

namespace AddressBookApi.Entities.DTO
{
    public class Types
    {
        [Required]
        public string? Key { get; set; }
    }
}
