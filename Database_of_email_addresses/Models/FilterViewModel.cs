using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Database_of_email_addresses.Models
{
    public class FilterViewModel
    {
        public string SelectedCountry { get; set; }
        public string SelectedCity { get; set; }
        public string SelectedStreet { get; set; }
        public string SelectedHouse { get; set; }
        public string SelectedPostCode { get; set; }
        public string SelectedDate { get; set; }

        public FilterViewModel()
        {
            SelectedCountry = "";
            SelectedCity = "";
            SelectedStreet = "";
            SelectedHouse = "";
            SelectedPostCode = "";
            SelectedDate = "";
        }

        public FilterViewModel(string selectedCountry, string selectedCity, string selectedStreet,
                                string selectedHouse, string selectedPostCode, string selectedDate)
        {
            SelectedCountry = selectedCountry;
            SelectedCity = selectedCity;
            SelectedStreet = selectedStreet;
            SelectedHouse = selectedHouse;
            SelectedPostCode = selectedPostCode;
            SelectedDate = selectedDate;
        }
    }
}
