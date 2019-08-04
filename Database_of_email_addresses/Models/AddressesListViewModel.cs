using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Database_of_email_addresses.Models
{
    public class AddressesListViewModel
    {
        public IEnumerable<Address> Addresses { get; set; }
        public SelectList Сountries { get; set; }
        public string Country { get; set; }
    }
}
