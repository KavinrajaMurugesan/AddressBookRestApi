using AddressBookApi.Contract;
using AddressBookApi.Data;
using AddressBookApi.Entities.Model;

namespace AddressBookApi.Repository
{
    public class PhoneRepository : IPhone
    {
        private readonly AddressBookDbContext context;
        public PhoneRepository(AddressBookDbContext context)
        {
            this.context = context;
        }
        public List<Phone> GetByUserId(Guid Id)
        {
            return context.Phones.Where(x =>x.UserId==Id).ToList();
        }
    }
}
