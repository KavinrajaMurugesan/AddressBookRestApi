using AddressBookApi.Entities.DTO;
using System.ComponentModel.DataAnnotations;

namespace AddressBookApi.Entities.Model
{
    public class RefTerm:EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public string? Key { get; set; }
    }
}
