namespace AddressBookApi.Entities.DTO
{
    public class EntityBase
    {
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; }=DateTime.Now;
        public DateTime UpdatedOn { get; set; }
    }
}

