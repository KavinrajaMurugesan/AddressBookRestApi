using System.Reflection.Metadata;

namespace AddressBookApi.Entities.DTO
{
    public class FileUploadResponsesDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string DownloadUrl { get; set; }
        public string FileType { get; set; }
        public long ? FileSize { get; set; }
        public byte[] FileContent { get; set; }

    }
}
