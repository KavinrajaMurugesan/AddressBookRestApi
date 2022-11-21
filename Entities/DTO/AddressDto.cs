using AddressBookApi.Entities.DTO;
using System.ComponentModel.DataAnnotations;

namespace AddressBookApi.Entities.DTO
{
 

    /// <summary>
    /// It  is the AddressDto to create the address List
    /// </summary>
    public class AddressDto
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string StateName { get; set; }
        public Types Type { get; set; }
        public Types Country { get; set; }
    }
}
