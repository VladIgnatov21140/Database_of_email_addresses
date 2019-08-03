using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Database_of_email_addresses.Models
{
    public class FilterViewModel
    {
        public SelectList CountryList { get; set; }
        public string SelectedCountry { get; set; }
        public string SelectedCity { get; set; }
        public string SelectedStreet { get; set; }
        public int? SelectedHouse { get; set; }
        public string SelectedPostCode { get; set; }
        public string SelectedDate { get; set; }

        public FilterViewModel(List<Address> addresses, string selectedCountry,
                                string selectedCity, string selectedStreet,
                                int? selectedHouse, string selectedPostCode, string selectedDate)
        {
            addresses.Insert(0, new Address(country : "Все", city : "Все", street : "Все",
                                            house : 0, postCode : "Все", date : new System.DateTime()));
            CountryList = new SelectList(addresses, "PostCode", "Country", selectedCountry);
            SelectedCountry = selectedCountry;
            SelectedCity = selectedCity;
            SelectedStreet = selectedStreet;
            SelectedHouse = selectedHouse;
            SelectedPostCode = selectedPostCode;
            SelectedDate = selectedDate;
        }
    }
}
