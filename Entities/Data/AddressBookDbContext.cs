using AddressBookApi.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace AddressBookApi.Data
{
    public class AddressBookDbContext:DbContext
    {
        public AddressBookDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserDetails> UserList { get; set; }
        public DbSet<Address> Addresses{ get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public  DbSet<RefSet> RefSets { get; set; }
        public DbSet<SetRef> Sets { get; set; }
        public DbSet<RefTerm> Types { get; set; }
        public DbSet<LoginCredential> LoginCredential { get; set; }
        public DbSet<Files>  files { get; set; }   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefTerm>().HasData(new RefTerm()
            {
                Id = Guid.Parse("ab3cb142-52cd-402b-b142-5c64cacbb048"),
                Key = "Personnel"
            },
            new RefTerm()
            {
                Id = Guid.Parse("4703fe06-cfc0-4272-b34c-792b806bc3b2"),
                Key = "Work"
            },
            new RefTerm()
            {
                Id = Guid.Parse("6003A954-578C-43D1-AAC9-C97C5152A565"),
                Key = "Alternate"
            },
            new RefTerm()
            {
                Id = Guid.Parse("5DAF1F9D-9128-456B-A242-7E5B83BEC49C"),
                Key = "India"
            },
            new RefTerm()
            {
                Id = Guid.Parse("EAA7D815-1798-4846-932C-B5D7D8211DA6"),
                Key = "UnitedStates"
            });
            modelBuilder.Entity<RefSet>().HasData(new RefSet()
            {
                RefSetId = Guid.Parse("EC99373A-55AA-4BD6-B539-BA166E179915"),
                Name = "Address_Type",
                Description = "This Stores the AddressType of the Field"
            },
            new RefSet()
            {
                RefSetId = Guid.Parse("2814B405-8632-44A7-899D-DF02DEBD6669"),
                Name = "Email_Address_Type",
                Description = "This Stores the EmailAddress of the Field"
            },
            new RefSet()
            {
                RefSetId = Guid.Parse("571F78DE-39D5-4314-8674-BCAEAE10D2C6"),
                Name = "Phone_Number_Type",
                Description = "This Stores the PhoneNumber of the Field"
            },
            new RefSet()
            {
                RefSetId = Guid.Parse("7203EC52-E27C-403C-BB8B-56C15213E164"),
                Name = "Country",
                Description = "This Stores the Country of the Field"
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
