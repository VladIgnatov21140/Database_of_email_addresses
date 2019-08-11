using Database_of_email_addresses.Models;
using Microsoft.EntityFrameworkCore;

namespace Database_of_email_addresses.DBController.DBContexts
{
    public class AddressContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public AddressContext(DbContextOptions<AddressContext> options)
            : base(options)
        {
            Database.SetCommandTimeout(60);
            Database.EnsureCreated();
        }
    }
}
