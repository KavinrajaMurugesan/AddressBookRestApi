using AddressBookApi.Entities.Model;

namespace AddressBookApi.Contract
{
    public interface IRefTerm
    {
        /// <summary>
        /// To get the id by using the kay
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>RefTerm</returns>
        public RefTerm GetByKey(string Key);
        /// <summary>
        /// To get the Key by using the Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>RefTerm</returns>
        public RefTerm GetById(Guid Id);
    }
}
