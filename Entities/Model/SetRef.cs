using AddressBookApi.Entities.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBookApi.Entities.Model
{
    public class SetRef:EntityBase
    {
        [Key]

        public Guid SetRefId { get; set; }

        public Guid RefSetId { get; set; }

        public Guid RefTermId { get; set; }
    }
}
