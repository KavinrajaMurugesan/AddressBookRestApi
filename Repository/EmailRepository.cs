using AddressBookApi.Contract;
using AddressBookApi.Data;
using AddressBookApi.Entities.Model;

namespace AddressBookApi.Repository
{
    public class EmailRepository : IEmail
    {
        private readonly AddressBookDbContext context;
        public EmailRepository(AddressBookDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        ///  To get the all email table as list
        /// </summary>
        /// <returns>list of email</returns>
        public List<Email> GetAll()
        {
            return context.Emails.ToList();
        }

        /// <summary>
        ///   To get the email by userId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>List of the email</returns>
        public List<Email> GetByUserId(Guid Id)
        {
            return context.Emails.Where(x => x.UserId==Id).ToList();
        }
    }
}
