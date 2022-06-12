using DemoApi;
using Microsoft.EntityFrameworkCore;

namespace DemoApi.Data
{
    public class ContactsAPIDbContext: DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Contact>? Contacts { get; set; }
    }
}
