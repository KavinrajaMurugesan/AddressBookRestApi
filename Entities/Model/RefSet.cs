using AddressBookApi.Entities.DTO;
using System.ComponentModel.DataAnnotations;

namespace AddressBookApi.Entities.Model
{
    public class RefSet:EntityBase
    {
        [Key]

        public Guid RefSetId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
