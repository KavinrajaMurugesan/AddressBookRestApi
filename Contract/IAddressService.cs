using AddressBookApi.Entities.DTO;
using AddressBookApi.Filters;
using System.Security.Claims;

namespace AddressBookApi.Contract
{
    public interface IAddressService
    {
        /// <summary>
        /// This method is used to validate the  Email Is already exists or not
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>boolean</returns>
        public bool EmailValidate(UserDetailsCreateDto newUser);
        /// <summary>
        /// This method is used to validate the UserName Is already exists or not
        /// </summary>
        /// <param name="user"></param>
        /// <returns>boolean</returns>
        public bool ValidateUserName(UserDetailsCreateDto user);
        /// <summary>
        /// This method takes input as IFormFile and returns the File Details and downloadUrl
        /// </summary>
        /// <param name="image"></param>
        /// <returns>FileUploadResponsesDto</returns>
        public bool GetRefterm(List<AddressDto> address, List<EmailDto> email, List<PhoneDto> phone);
        public FileUploadResponsesDto UploadFile(IFormFile image);

        /// <summary>
        ///   Gets the UserDetailsDto and convert into UserDetails then posted in the dbcontext by using 
        ///   CreateAddressBook method
        /// </summary>
        /// <param name="newUser">Gets the UserDetailsDto As Input</param>
        /// <returns> returns the userId of the created address book output as Guid</returns>
        public Guid AddUser(UserDetailsCreateDto newUser);


        /// <summary>
        ///      This GetAllUsers returs the all Address book user.
        /// </summary>
        /// <returns> returns all addressbook as IEnumerable</returns>

        public IEnumerable<UserDetailsDto> GetAllUsers(Pagination page);

        /// <summary>
        ///  This method the takes input ClaimsIdentity and Id as input,checks the username and Id 
        ///  are matching in the database
        /// </summary>
        /// <param name="userIdentity"></param>
        /// <param name="Id"></param>
        /// <returns>boolean is the return type</returns>
        public bool ValidatingUser(ClaimsIdentity userIdentity, Guid Id);

        /// <summary>
        ///   This Method takes the userId gives the userdetails as UserDetailDto
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>UserDetailDto</returns>
        public UserDetailsDto GetById(Guid Id);


        /// <summary>
        ///  This method takes the userId and perform the softdelete
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>retur type as boolean</returns>
        public bool DeleteUser(Guid Id);

        /// <summary>
        ///  This method Updates the user by Getting UserDetailsDto and Id
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Id"></param>
        /// <returns>UserdetailsDto</returns>
        public UserDetailsDto Update(UserDetailsCreateDto user,Guid Id);

        /// <summary>
        ///  This method returns the count of the user in the database
        /// </summary>
        /// <returns>int </returns>
        public int CountUser();

        /// <summary>
        ///  This method takes the LoginDto as input and Generate
        ///  the validation token if th user is present inside the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        ///  returns the Token as LoginResponsesDto
        /// </returns>
        public LoginResponsesDto GenerateToken(LoginDto user);

        /// <summary>
        ///    This method takes the id and returns the FileDownloadDto
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>FileDownloadDro</returns>
        public FileDownloadDto fileDownload(Guid Id);

        /// <summary>
        /// this method get the key and returns the ref-set details of the user.
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public RefSetDto GetMetadata(string Key);
        /// <summary>
        /// Thid method is used for validation methods like getall and count.
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public bool ValidateUser(ClaimsIdentity identity);

    }
}
