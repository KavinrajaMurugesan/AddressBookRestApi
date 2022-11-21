using AddressBookApi.Contract;
using AddressBookApi.Data;
using AddressBookApi.Entities.Model;

namespace AddressBookApi.Repository
{
    public class AddressRepository : IAddress
    {
        private readonly AddressBookDbContext context;
        public AddressRepository(AddressBookDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        ///  To get the Address By the user Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>List of address</returns>
        public List<Address> GetByUserId(Guid Id)
        {
            return context.Addresses.Where(x=>x.UserId==Id).ToList();
        }
    }
}
