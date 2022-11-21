using AddressBookApi.Contract;
using AddressBookApi.Data;
using AddressBookApi.Entities.Model;

namespace AddressBookApi.Repository
{
    public class ReftermRepository:IRefTerm
    {
        private readonly AddressBookDbContext context;
        public ReftermRepository(AddressBookDbContext context)
        {
            this.context = context;
        }


        public RefTerm GetById(Guid Id)
        {
            return context.Types.FirstOrDefault(x => x.Id == Id && x.IsActive == true);
        }

        public RefTerm GetByKey(string Key)
        {
            return context.Types.Where(x=>x.Key==Key).FirstOrDefault();
        }
    }
}
