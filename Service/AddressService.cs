using AddressBookApi.Contract;
using AddressBookApi.Entities.DTO;
using System.Security.Claims;
using AddressBookApi.Entities.Model;
using AddressBookApi.Filters;


namespace AddressBookApi.Service
{
    public class AddressService : IAddressService
    {
        private readonly IUser userrepo;
        private readonly IAddress addressrepo;
        private readonly IEmail emailrepo;
        private readonly IPhone phonerepo;
        private readonly ILogin loginrepo;
        private readonly IRefTerm termRepo;
        private readonly IToken token;
        private readonly IRefSet refsetrepo;
        private readonly IFile fileRepo;
        private readonly ILogger<AddressService> logger;
        public AddressService(IUser userrepo,ILogin login,IRefTerm termrepo,IEmail emailrepo,IRefSet refsetrepo,IAddress addressrepo,IPhone phonerepo)
        {
            this.userrepo = userrepo;
            this.loginrepo = login;
            this.termRepo = termrepo;   
            this.emailrepo = emailrepo; 
            this.refsetrepo = refsetrepo;
            this.phonerepo = phonerepo;
            this.addressrepo = addressrepo;
        }
        public AddressService(IUser userrepo, IAddress addressrepo, IEmail emailrepo, 
            IPhone phonerepo,ILogin loginrepo,IRefTerm termRepo,IToken token,IRefSet refsetrepo,IFile fileRepo)
        {
            this.userrepo = userrepo;
            this.addressrepo = addressrepo;
            this.emailrepo = emailrepo;
            this.phonerepo = phonerepo;
            this.loginrepo=loginrepo;
            this.termRepo = termRepo;
            this.token= token;
            this.refsetrepo = refsetrepo;
            this.fileRepo = fileRepo;
         
        }
        /// <summary>
        /// This method will validate the user and password in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>boolean</returns>
        private bool IsChecking(LoginDto user)
        {
            List<LoginCredential> loginCredentials = loginrepo.GetAll();
            bool IsPresent=loginCredentials.Where(x=> x.UserName.ToLower() == user.UserName.ToLower() && x.Password==user.Password).Any();
            return IsPresent;
            
        }
        /// <summary>
        ///  This method checks the user had Id and password are matching it will call generate token method.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>LoginResponsesDto</returns>
        /// <exception cref="NullReferenceException"></exception>
        public LoginResponsesDto GenerateToken(LoginDto user)
        {
            if (IsChecking(user)!=true)
            {
                throw new NullReferenceException();
            }
            return new LoginResponsesDto()
            {
                Token = token.GenerateToken(user)
            };
        }

        public bool ValidatingUser(ClaimsIdentity userIdentity, Guid Id)
        {
            var userClaims = userIdentity.Claims;
            String userName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            List<LoginCredential> credentials = loginrepo.GetAll();
            bool IsPresent = credentials.Any(x => x.UserName.ToLower() == userName.ToLower());
            if (IsPresent)
            {
                Guid id = credentials.Where(x => x.UserName.ToLower() == userName.ToLower()).Select(x => x.Id).FirstOrDefault();
                if (id == Id)
                {
                    return true;
                }
                return false;
            }
            return false;
        }


        /// <summary>
        ///  This methos checks the username is in  the table,if it is not create userlogin credentials
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>bool</returns>
        public bool ValidateUserName(UserDetailsCreateDto user)
        {
            List<LoginCredential>? Credentials = loginrepo.GetAll();
            bool IsAlready=Credentials.Where(x=>x.UserName.ToLower()==user.UserName.ToLower()).Any();
            return IsAlready;
        }



        /// <summary>
        ///    This Method will get input as Metadata key and matches the key with id. and returns the RefTerm
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>Refterm</returns>
        public bool GetRefterm(List<AddressDto> address,List<EmailDto> email,List<PhoneDto> phone)
        {
            foreach (AddressDto i in address)
            {
                if (termRepo.GetByKey(i.Type.Key) == null)
                {
                    return false;
                }
            }
            foreach(EmailDto i in email)
            {
                if (termRepo.GetByKey(i.Type.Key) == null)
                {
                    return false;
                }
            }
            foreach(PhoneDto i in phone)
            {
                if (termRepo.GetByKey(i.Type.Key) == null)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        ///    This method gets the RefTermId and matches the refterm key to the Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>string </returns>
        private string GetRefTermKey(Guid Id)
        {
            RefTerm refTerm=termRepo.GetById(Id);
            if (refTerm == null)
            {
                return null;
            }
            return refTerm.Key;
        }



        /// <summary>
        ///  This Method will validate the emailaddress is already given or not.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>return the boolean</returns>
        public bool EmailValidate(UserDetailsCreateDto newUser)
        {
           List<Email> emails=emailrepo.GetAll();
           foreach(EmailDto i in newUser.Emails)
           {
                var IsPresent=emails.Where(x=>x.EmailAddress==i.EmailAddress).Any();
                if (IsPresent == true)
                {
                    return false;
                }
           }
            return true;
        }

        public Guid AddUser(UserDetailsCreateDto newUser)
        {
            try
            {
                LoginCredential credential = new LoginCredential()
                {
                    Id = new Guid(),
                    UserName = newUser.UserName,
                    Password = newUser.Password,
                };
                UserDetails user = new UserDetails()
                {
                    UserId = credential.Id,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Emails = new List<Email>(),
                    Addresses = new List<Address>(),
                    Phones = new List<Phone>(),
                };
                foreach (AddressDto i in newUser.Addresses)
                {
                    Address address = new Address()
                    {
                        Line1 = i.Line1,
                        Line2 = i.Line2,
                        City = i.City,
                        ZipCode = i.ZipCode,
                        StateName = i.StateName,
                    };

                    address.RefTermAddressId = termRepo.GetByKey(i.Type.Key).Id;
                    address.RefTermCountryId = termRepo.GetByKey(i.Country.Key).Id;
                    user.Addresses.Add(address);
                }
                foreach (EmailDto i in newUser.Emails)
                {
                    Email email = new Email()
                    {
                        EmailAddress = i.EmailAddress
                    };
                    email.RefTermEmailId = termRepo.GetByKey(i.Type.Key).Id;
                    user.Emails.Add(email);
                }
                foreach (PhoneDto i in newUser.Phones)
                {
                    Phone phone = new Phone()
                    {
                        PhoneId = new Guid(),
                        PhoneNumber = i.PhoneNumber,
                    };

                    phone.RefTermPhoneId = termRepo.GetByKey(i.Type.Key).Id;
                    user.Phones.Add(phone);
                }
                UserDetails createdUser = userrepo.CreateAddressBook(user);
                loginrepo.CreateLogin(credential);
                userrepo.SaveChanges();
                return createdUser.UserId;
            }
            catch(Exception e)
            {
               // logger.LogError("The occurred at adduser");
                throw e;
            }
        }


        /// <summary>
        ///      This GetAllUsers returns the all Address book user.
        /// </summary>
        /// <returns> returns all addressbook as IEnumerable</returns>
        public IEnumerable<UserDetailsDto> GetAllUsers(Pagination page)
        {
            List<UserDetails> users = userrepo.GetAll(page);
            List<UserDetailsDto> allUser = new List<UserDetailsDto>();
            if (users.Count(x => x.IsActive == true) == 0)
            {
                throw new InvalidOperationException();
            }
            foreach (UserDetails i in users)
            {
                allUser.Add(GetById(i.UserId));
            }
            return allUser;
        }
        

        public UserDetailsDto GetById(Guid Id)
        {
            UserDetails userList = userrepo.GetById(Id);
            if (userList == null)
            {
                throw new InvalidOperationException();
            }
            List<Address> addressList = addressrepo.GetByUserId(Id);
            List<Email> emailList = emailrepo.GetByUserId(Id);
            List<Phone> phoneList = phonerepo.GetByUserId(Id);

            UserDetailsDto user = new UserDetailsDto() {
                FirstName = userList.FirstName,
                LastName = userList.LastName,
                Addresses = new List<AddressDto>(),
                Emails = new List<EmailDto>(),
                Phones = new List<PhoneDto>(),
            };
            foreach (Address j in addressList)
            {
                AddressDto newAddress = new AddressDto()
                {
                    Line1 = j.Line1,
                    Line2 = j.Line2,
                    City = j.City,
                    StateName = j.StateName,
                    ZipCode = j.ZipCode,
                    Type = new Types()
                    {
                        Key = GetRefTermKey(j.RefTermAddressId)
                    },
                    Country=new Types()
                    {
                        Key=GetRefTermKey(j.RefTermCountryId)
                    }
                };
                user.Addresses.Add(newAddress);
            }
            foreach (Email z in emailList)
            {
                EmailDto newEmail = new EmailDto()
                {
                    EmailAddress = z.EmailAddress,
                    Type = new Types()
                    {
                        Key = GetRefTermKey(z.RefTermEmailId)
                    }
                };
                user.Emails.Add(newEmail);
            }
            foreach (Phone y in phoneList)
            {
                PhoneDto newPhone = new PhoneDto()
                {
                    PhoneNumber = y.PhoneNumber,
                    Type = new Types()
                    {
                        Key = GetRefTermKey(y.RefTermPhoneId)
                    }
                };
                user.Phones.Add(newPhone);
            }
            return user;
        }
        public bool DeleteUser(Guid Id)
        {
            if (userrepo.DeleteUser(Id) == false)
            {
                return false;
            };
            userrepo.SaveChanges();
            return true;
        }

        public UserDetailsDto Update(UserDetailsCreateDto user, Guid Id)
        {
            if (loginrepo.IdIsPresent(Id) == false)
            {
                return null;
            }
            LoginCredential newUser = new LoginCredential()
            {
                UserName = user.UserName,
                Password = user.Password,
            };
            UserDetails userDetails = new UserDetails()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Addresses = new List<Address>(),
                Emails = new List<Email>(),
                Phones = new List<Phone>()
            };
            foreach(EmailDto i in user.Emails)
            {
                Email email = new Email()
                {
                    EmailAddress = i.EmailAddress,
                };
                email.RefTermEmailId = termRepo.GetByKey(i.Type.Key).Id;
           
                userDetails.Emails.Add(email);
            }
            foreach (AddressDto i in user.Addresses)
            {
                Address address = new Address()
                {
                    Line1 = i.Line1,
                    Line2 = i.Line2,
                    City = i.City,
                    ZipCode = i.ZipCode,
                    StateName = i.StateName,
                 

                };
                address.RefTermAddressId = termRepo.GetByKey(i.Type.Key).Id;
                address.RefTermAddressId = termRepo.GetByKey(i.Country.Key).Id;
                userDetails.Addresses.Add(address);
            }
            foreach (PhoneDto i in user.Phones)
            {
                Phone phone = new Phone()
                {
                    PhoneId = new Guid(),
                    PhoneNumber = i.PhoneNumber,
                   
                };
                phone.RefTermPhoneId = termRepo.GetByKey(i.Type.Key).Id;
                userDetails.Phones.Add(phone);
            }
            userDetails.UpdatedOn = DateTime.Now;
            UserDetails updatedUser = userrepo.Update(userDetails,Id);
            userrepo.SaveChanges();
            UserDetailsDto updateDto = new UserDetailsDto()
            {
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Addresses = new List<AddressDto>(),
                Emails = new List<EmailDto>(),
                Phones = new List<PhoneDto>(),
            };
            foreach(Email i in updatedUser.Emails)
            {
                updateDto.Emails.Add(new EmailDto()
                {
                    EmailAddress = i.EmailAddress,
                    Type = new Types()
                    {
                        Key = GetRefTermKey(i.RefTermEmailId)
                    }
                }) ;
            }
            foreach (Phone i in updatedUser.Phones)
            {
                updateDto.Phones.Add(new PhoneDto()
                {
                    PhoneNumber = i.PhoneNumber,
                    Type = new Types()
                    {
                        Key = GetRefTermKey(i.RefTermPhoneId)
                    }
                });
            }
            foreach (Address i in updatedUser.Addresses)
            {
                updateDto.Addresses.Add(new AddressDto()
                {
                    Line1 = i.Line1,
                    Line2 = i.Line2,
                    City = i.City,
                    ZipCode = i.ZipCode,
                    StateName = i.StateName,
                    Type = new Types()
                    {
                        Key= GetRefTermKey(i.RefTermAddressId)   
                    },
                    Country=new Types()
                    {
                       Key=GetRefTermKey(i.RefTermCountryId)
                    }
                });
            }
            return updateDto;
        }

        public int CountUser()
        {
           return userrepo.countUser();
        }


        public RefSetDto GetMetadata(string Key)
        {
            List<RefSet> refset = refsetrepo.GetAll();
            RefSetDto refsetDto = new RefSetDto();
            if (Key.ToLower() == "addresstype")
            {
                RefSet setAddress = refset.FirstOrDefault(x => x.Name == "Address_Type");

                return refsetDto= new RefSetDto()
                {
                    Id = setAddress.RefSetId,
                    Type = setAddress.Name,
                    Description = setAddress.Description,
                    AvialbleTypes = new List<string>()
                    {
                        "Personal","Work","Alternate"
                    }
                };
            }
            else if (Key.ToLower() == "emailaddresstype")
            {
                RefSet setEmail = refset.FirstOrDefault(x => x.Name == "Email_Address_Type");
                return refsetDto = new RefSetDto()
                {
                    Id = setEmail.RefSetId,
                    Type = setEmail.Name,
                    Description = setEmail.Description,
                     AvialbleTypes = new List<string>()
                    {
                        "Personal","Work","Alternate"
                    }
                };
            }
            else if (Key.ToLower() == "phonenumbertype")
            {
                RefSet setPhone = refset.FirstOrDefault(x => x.Name == "Phone_Number_Type");
                return refsetDto = new RefSetDto()
                {
                    Id = setPhone.RefSetId,
                    Type = setPhone.Name,
                    Description = setPhone.Description,
                    AvialbleTypes = new List<string>()
                    {
                        "Personal","Work"
                    }
                };
            }
            else if (Key.ToLower() == "country")
            {
                RefSet setCountry = refset.FirstOrDefault(x => x.Name == "Country");
                return refsetDto = new RefSetDto()
                {
                    Id = setCountry.RefSetId,
                    Type = setCountry.Name,
                    Description = setCountry.Description,
                    AvialbleTypes = new List<string>()
                    {
                        "India","UnitedStates"
                    }
                };
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// This method takes input as IFormFile and returns the File Details and downloadUrl
        /// </summary>
        /// <param name="image"></param>
        /// <returns>FileUploadResponsesDto</returns>
        public FileUploadResponsesDto UploadFile(IFormFile image)
        {
            Files files = new Files();
            files.Id = new Guid();
            byte[] GetBytes(IFormFile file){
              using MemoryStream stream=new MemoryStream();
                file.CopyToAsync(stream);
                return stream.ToArray();
            }
            var Bytes = GetBytes(image);
            files.FileContent= Bytes;
            files.FileType = image.ContentType;
            files.FileName = image.FileName;
            files.FileSize = image.Length;
            fileRepo.PostFile(files);
            userrepo.SaveChanges();
            return new FileUploadResponsesDto()
            {
                Id = files.Id,
                FileName = image.FileName,
                FileSize = image.Length,
                FileType = image.ContentType,
                DownloadUrl = @"https://localhost:7027/api/asset/downloadFile/" + files.Id,
                FileContent = null
            };
        }

        public FileDownloadDto fileDownload(Guid Id)
        {
            try
            {
                List<Files> allFiles = fileRepo.GetAll();
                Files file = allFiles.FirstOrDefault(x => x.Id == Id);
                if (file == null)
                {
                    throw new NullReferenceException();
                }

                return new FileDownloadDto()
                {
                    FileContent = file.FileContent,
                    FileType = file.FileType
                };
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
        public bool ValidateUser(ClaimsIdentity identity)
        {
            var userClaims = identity.Claims;
            String userName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            List<LoginCredential> credentials = loginrepo.GetAll();
            bool IsPresent=credentials.Any(x => x.UserName.ToLower() == userName.ToLower());
            return IsPresent;
        }

       
    }
}
