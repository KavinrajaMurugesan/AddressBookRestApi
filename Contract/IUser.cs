
using AddressBookApi.Entities.Model;
using AddressBookApi.Filters;

namespace AddressBookApi.Contract
{
    public interface IUser
    {
        /// <summary>
        ///  To Create the user and store the data to the database
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns> UserDetails</returns>
        public UserDetails CreateAddressBook(UserDetails userDetails);
        /// <summary>
        ///  To get the user hy Guid id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>UserDetails</returns>
        public UserDetails GetById(Guid Id);
        /// <summary>
        /// To get all userDetails
        /// </summary>
        /// <param name="pages"></param>
        /// <returns>list of userdetails</returns>
        public List<UserDetails> GetAll(Pagination pages);
        /// <summary>
        ///    To delete the user by giving the Guid id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>boolean</returns>
        public bool DeleteUser(Guid Id);
        /// <summary>
        ///    To update the user by giving the userdetails and Guid Id
        /// </summary>
        /// <param name="userDetails"></param>
        /// <param name="Id"></param>
        /// <returns> userDetails</returns>
        public UserDetails Update(UserDetails userDetails, Guid Id);
        /// <summary>
        ///  To give the count of the user
        /// </summary>
        /// <returns></returns>
        public int countUser();
        /// <summary>
        /// To commit the savechanges ih the database
        /// </summary>
        public void SaveChanges();
    }
}
