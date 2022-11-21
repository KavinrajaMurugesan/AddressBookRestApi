using AddressBookApi.Entities.Model;

namespace AddressBookApi.Contract
{
    public interface IEmail
    {
        /// <summary>
        ///   To get the email by userId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>List of the email</returns>
        public List<Email> GetByUserId(Guid Id);
        /// <summary>
        ///  To get the all email table as list
        /// </summary>
        /// <returns>list of email</returns>
        public List<Email> GetAll();
    }
}
