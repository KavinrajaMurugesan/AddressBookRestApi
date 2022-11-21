namespace AddressBookApi.Filters
{
    public class Pagination
    {
        public int size { get; set; } = 10;
        public int pageNo { get; set; } = 1;
        public string sortBy { get; set; } = "FirstName";
        public string sortOrder { get; set; } = "Ascending";
    }
}
