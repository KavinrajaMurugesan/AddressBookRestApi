using AddressBookApi.Entities.Model;

namespace AddressBookApi.Contract
{
    public interface IFile
    {
        /// <summary>
        /// It takes the input Files and add them to database
        /// <param name="file"></param>
        /// <returns>boolean</returns>
        public bool PostFile(Files file);
        /// <summary>
        ///  To get the all files
        /// </summary>
        /// <returns>List of files</returns>
        public List<Files> GetAll();

    }
}
