using AddressBookApi.Entities.Model;

namespace AddressBookApi.Contract
{
    public interface IAddress
    {
        /// <summary>
        ///  To get the Address By the user Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>List of address</returns>
        public List<Address> GetByUserId(Guid Id);
    }
}
