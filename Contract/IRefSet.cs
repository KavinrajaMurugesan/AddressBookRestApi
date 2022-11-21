using AddressBookApi.Entities.Model;

namespace AddressBookApi.Contract
{
    public interface IRefSet
    {
        /// <summary>
        /// To get all the Refset
        /// </summary>
        /// <returns>List of RefSet</returns>
        public List<RefSet> GetAll();
    }
}
