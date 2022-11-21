using AddressBookApi.Contract;
using AddressBookApi.Data;
using AddressBookApi.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace AddressBookApi.Repository
{
    public class LoginRepository : ILogin
    {
        private readonly AddressBookDbContext context;
        public LoginRepository(AddressBookDbContext context)
        {
            this.context = context;
        }

        public void CreateLogin(LoginCredential credential)
        {
            context.LoginCredential.Add(credential);
        }
        public bool IdIsPresent(Guid Id)
        {
            return context.LoginCredential.Any(x => x.Id == Id);
        }

        public List<LoginCredential> GetAll()
        {
            return context.LoginCredential.ToList();
        }
        public void Update(LoginCredential newlogin)
        {
            context.LoginCredential.Update(newlogin);
        }
    }
}
