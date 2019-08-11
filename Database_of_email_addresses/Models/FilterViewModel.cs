
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
    }
}
