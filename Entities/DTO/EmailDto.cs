using System.ComponentModel.DataAnnotations;

namespace AddressBookApi.Entities.DTO
{

    /// <summary>
    /// Emaildto to create the email
    /// </summary>
    public class EmailDto
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public Types Type { get; set; }
    }
}
