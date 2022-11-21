namespace AddressBookApi.Entities.DTO
{
    public class LoginResponsesDto
    {
        public string Token { get; set; }
        public string Type { get; set; } = "Bearer";
    }
}
