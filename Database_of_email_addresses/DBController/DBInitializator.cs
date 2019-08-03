using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database_of_email_addresses.Models;
using Database_of_email_addresses.DBController.DBContexts;

namespace Database_of_email_addresses.DBController
{
    public static class DBInitializator
    {
            public static void Initialize(AddressContext addrContext)
            {
                if (!addrContext.Addresses.Any())
                {
                    addrContext.AddRange(
                        new Address("Россия", "Ульяновск", "Ленина, ул.", 1, "432071", new DateTime(2014, 04, 10, 12, 12, 21)),
                        new Address("Россия", "Ульяновск", "Нариманова, прт. ", 2, "432072", new DateTime(2014, 03, 15, 12, 15, 34)),
                        new Address("Россия", "Москва", "Киевский, пр-т.", 4, "425072", new DateTime(2014, 05, 01, 12, 23, 0)),
                        new Address("Россия", "Москва", "Киевский, пр-т.", 6, "432252", new DateTime(2014, 05, 01, 12, 23, 0)),
                        new Address("Россия", "Москва", "Киевский, пр-т.", 5, "432452", new DateTime(2014, 05, 01, 12, 23, 0))
                    );
                addrContext.SaveChanges();
                }
            }
    }
}
