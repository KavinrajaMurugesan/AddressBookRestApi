using AddressBookApi.Contract;
using AddressBookApi.Data;
using AddressBookApi.Entities.Model;
using AddressBookApi.Filters;

namespace AddressBookApi.Repository
{
    public class UserRepository : IUser
    {
        private readonly AddressBookDbContext context;
        public UserRepository(AddressBookDbContext context)
        {
            this.context = context;
        }


        public UserDetails CreateAddressBook(UserDetails userDetails)
        {
            context.UserList.Add(userDetails);
            return userDetails;
        }

        public bool DeleteUser(Guid Id)
        {
            var user = context.UserList.Find(Id);
            if (user == null)
            {
                return false;
            }
            user.IsActive = false;
            return true;
        }

        public List<UserDetails> GetAll(Pagination pages)
        {
            var user = context.UserList as IQueryable<UserDetails>;
            if (pages.sortBy.ToLower() == "lastname")
            {
                if (pages.sortOrder.ToLower() == "descending")
                {
                    user = user.OrderByDescending(x => x.LastName);
                }
                else
                {
                    user = user.OrderBy(x => x.LastName);
                }
            }
            else
            {
                if (pages.sortOrder.ToLower() == "ascending")
                {
                    user = user.OrderBy(x => x.FirstName);
                }
                else
                {
                    user = user.OrderByDescending(x => x.FirstName);
                } 
            }
            return user.Skip((pages.pageNo-1)*pages.size).Take(pages.size).ToList();
        }

        public UserDetails GetById(Guid Id)
        {
            return context.UserList.Where(x => x.IsActive == true).FirstOrDefault(x => x.UserId == Id);
        }

        public UserDetails Update(UserDetails userDetails, Guid Id)
        {
            var oldUser = context.UserList.FirstOrDefault(x => x.UserId == Id);
            if (oldUser == null)
            {
                return null;
            }
            userDetails.UpdatedOn = DateTime.Now;
            context.UserList.Update(userDetails);
            return userDetails;
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
        public int countUser()
        {
            return context.UserList.Count();
        }

    }
}
