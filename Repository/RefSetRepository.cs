using AddressBookApi.Contract;
using AddressBookApi.Data;
using AddressBookApi.Entities.Model;

namespace AddressBookApi.Repository
{
    public class RefSetRepository : IRefSet
    {
        private readonly AddressBookDbContext context;
        public RefSetRepository(AddressBookDbContext context)
        {
            this.context = context;
        }
        public List<RefSet> GetAll()
        {
            return context.RefSets.ToList();
        }
    }
}
