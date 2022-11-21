
using AddressBookApi.Entities.DTO;

namespace AddressBookApi.Contract
{
    public interface IToken
    {
        /// <summary>
        ///  To generate the token to the corresponding user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string token of the user</returns>
        public string GenerateToken(LoginDto user);
    }
}
