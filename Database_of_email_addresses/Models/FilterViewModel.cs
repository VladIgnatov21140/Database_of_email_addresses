
using System;
using System.Linq;

namespace Database_of_email_addresses.Models
{
    public class FilterViewModel
    {
        public string SelectedCountry { get; set; }
        public string SelectedArea { get; set; }
        public string SelectedCity { get; set; }
        public string SelectedStreet { get; set; }
        public string SelectedHousing { get; set; }
        public string SelectedHouse { get; set; }
        public string SelectedPostCode { get; set; }

        public FilterViewModel()
        {
            SelectedCountry = "";
            SelectedArea = "";
            SelectedCity = "";
            SelectedStreet = "";
            SelectedHousing = "";
            SelectedHouse = "";
            SelectedPostCode = "";
        }

        public FilterViewModel(string selectedCountry, string selectedArea, string selectedCity, string selectedStreet,
                                string selectedHousing, string selectedHouse, string selectedPostCode)
        {
            SelectedCountry = selectedCountry;
            SelectedArea = selectedArea;
            SelectedCity = selectedCity;
            SelectedStreet = selectedStreet;
            SelectedHousing = selectedHousing;
            SelectedHouse = selectedHouse;
            SelectedPostCode = selectedPostCode;
        }

        public static IQueryable<Address> SetFiltering(IQueryable<Address> addresses, IndexViewModel indexViewModel)
        {
            #region Filtering
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedCountry))
            {
                string Country = indexViewModel.FilterViewModel.SelectedCountry;
                addresses = addresses.Where(p => p.Country.Contains(Country));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedArea))
            {
                string Area = indexViewModel.FilterViewModel.SelectedArea;
                addresses = addresses.Where(p => p.Area.Contains(Area));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedCity))
            {
                string City = indexViewModel.FilterViewModel.SelectedCity;
                addresses = addresses.Where(p => p.City.Contains(City));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedStreet))
            {
                string Street = indexViewModel.FilterViewModel.SelectedStreet;
                addresses = addresses.Where(p => p.Street.Contains(Street));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedHousing))
            {
                string Housing = indexViewModel.FilterViewModel.SelectedHousing;
                addresses = addresses.Where(p => p.Housing.Contains(Housing));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedHouse))
            {
                if (int.TryParse(indexViewModel.FilterViewModel.SelectedHouse, out int house))
                    addresses = addresses.Where(p => p.House.ToString().Contains(indexViewModel.FilterViewModel.SelectedHouse));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedPostCode))
            {
                string PostCode = indexViewModel.FilterViewModel.SelectedPostCode;
                addresses = addresses.Where(p => p.PostCode.Contains(PostCode));
            }
            #endregion
            return addresses;
        }
    }
}
