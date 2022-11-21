using AddressBookApi.Contract;
using AddressBookApi.Data;
using AddressBookApi.Entities.Model;

namespace AddressBookApi.Repository
{
    public class FileRepository : IFile
    {
        private readonly AddressBookDbContext context;
        public FileRepository(AddressBookDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        ///  To get the all files
        /// </summary>
        /// <returns>List of files</returns>
        public List<Files> GetAll()
        {
            return context.files.ToList();
        }
        /// <summary>
        /// It takes the input Files and add them to database
        /// <param name="file"></param>
        /// <returns>boolean</returns>

        public bool PostFile(Files file)
        {
            context.files.Add(file);
            return true;
        }
    }
}
