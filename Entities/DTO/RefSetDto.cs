namespace AddressBookApi.Entities.DTO
{
    public class RefSetDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public List<string> AvialbleTypes{ get;set; }
    }
}
