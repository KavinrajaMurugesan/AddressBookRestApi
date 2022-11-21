using AddressBookApi.Entities.DTO;
using System.ComponentModel.DataAnnotations;

namespace AddressBookApi.Entities.Model
{
    public class Files:EntityBase
    {
        [Key]
        public Guid Id { get; set; }   
        [Required]
        public byte[] FileContent { get; set; }
        [Required]
        public string FileType { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public long FileSize { get; set; }
    }
}
