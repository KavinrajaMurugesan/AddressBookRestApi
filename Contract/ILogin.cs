using AddressBookApi.Entities.Model;
namespace AddressBookApi.Contract
{
    public interface ILogin
    {
        /// <summary>
        ///  This methods returns the list of the logincredentials of the user.
        /// </summary>
        /// <returns>List of LoginCredential</returns>
        public List<LoginCredential> GetAll();
        public void CreateLogin(LoginCredential credential);
        public void Update(LoginCredential newlogin);
        public bool IdIsPresent(Guid Id);
    }
}
