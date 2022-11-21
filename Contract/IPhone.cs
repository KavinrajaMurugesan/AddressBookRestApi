using AddressBookApi.Entities.Model;

namespace AddressBookApi.Contract
{
    public interface IPhone
    {
        /// <summary>
        /// To get the phone by using the userId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>List of Phone</returns>
        public List<Phone> GetByUserId(Guid Id);
    }
}
