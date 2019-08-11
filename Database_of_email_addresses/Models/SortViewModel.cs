using Database_of_email_addresses.DBController;
using Newtonsoft.Json;
using System.Linq;

namespace Database_of_email_addresses.Models
{
    public class SortViewModel
    {
        public SortState IDSort { get; set; }
        public SortState CountrySort { get; set; }
        public SortState AreaSort { get; set; }
        public SortState CitySort { get; set; }
        public SortState StreetSort { get; set; }
        public SortState HousingSort { get; set; }
        public SortState HouseSort { get; set; }
        public SortState PostCodeSort { get; set; }
        public SortState Current { get; set; }
        public bool Up { get; set; }

        [JsonConstructor]
        public SortViewModel(SortState sortState)
        {
            IDSort = SortState.IDAsc;
            CountrySort = SortState.CountryAsc;
            AreaSort = SortState.AreaAsc;
            CitySort = SortState.CityAsc;
            StreetSort = SortState.StreetAsc;
            HousingSort = SortState.HousingAsc;
            HouseSort = SortState.HouseAsc;
            PostCodeSort = SortState.PostCodeAsc;

            Up = true;


            if (sortState == SortState.IDDesc || sortState == SortState.CountryDesc
                || sortState == SortState.AreaDesc || sortState == SortState.CityDesc
                || sortState == SortState.StreetDesc || sortState == SortState.HousingDesc
                || sortState == SortState.HouseDesc || sortState == SortState.PostCodeDesc)
            {
                Up = false;
            }

            #region SetValueCurrent
            switch (sortState)
            {
                case SortState.IDAsc:
                    Current = IDSort = SortState.IDAsc;
                    break;
                case SortState.IDDesc:
                    Current = IDSort = SortState.IDDesc;
                    break;
                case SortState.CountryAsc:
                    Current = CountrySort = SortState.CountryAsc;
                    break;
                case SortState.CountryDesc:
                    Current = CountrySort = SortState.CountryDesc;
                    break;
                case SortState.AreaAsc:
                    Current = AreaSort = SortState.AreaAsc;
                    break;
                case SortState.AreaDesc:
                    Current = AreaSort = SortState.AreaDesc;
                    break;
                case SortState.CityAsc:
                    Current = CitySort = SortState.CityAsc;
                    break;
                case SortState.CityDesc:
                    Current = CitySort = SortState.CityDesc;
                    break;
                case SortState.StreetAsc:
                    Current = StreetSort = SortState.StreetAsc;
                    break;
                case SortState.StreetDesc:
                    Current = StreetSort = SortState.StreetDesc;
                    break;
                case SortState.HousingAsc:
                    Current = HousingSort = SortState.HousingAsc;
                    break;
                case SortState.HousingDesc:
                    Current = HousingSort = SortState.HousingDesc;
                    break;
                case SortState.HouseAsc:
                    Current = HouseSort = SortState.HouseAsc;
                    break;
                case SortState.HouseDesc:
                    Current = HouseSort = SortState.HouseDesc;
                    break;
                case SortState.PostCodeAsc:
                    Current = PostCodeSort = SortState.PostCodeAsc;
                    break;
                case SortState.PostCodeDesc:
                    Current = PostCodeSort = SortState.PostCodeDesc;
                    break;
            }
            #endregion
        }

        public SortViewModel(SortViewModel sortViewModel)
        {
            IDSort = sortViewModel.IDSort;
            CountrySort = sortViewModel.CountrySort;
            AreaSort = sortViewModel.AreaSort;
            CitySort = sortViewModel.CitySort;
            StreetSort = sortViewModel.StreetSort;
            HousingSort = sortViewModel.HousingSort;
            HouseSort = sortViewModel.HouseSort;
            PostCodeSort = sortViewModel.PostCodeSort;
            Current = sortViewModel.Current;
            Up = sortViewModel.Up;
        }

        //Invert sorting
        public SortViewModel(SortViewModel sortViewModel, bool InvertSorting)
        {
            IDSort = SortState.IDAsc;
            CountrySort = SortState.CountryAsc;
            AreaSort = SortState.AreaAsc;
            CitySort = SortState.CityAsc;
            StreetSort = SortState.StreetAsc;
            HousingSort = SortState.HousingAsc;
            HouseSort = SortState.HouseAsc;
            PostCodeSort = SortState.PostCodeAsc;

            if (sortViewModel.Up == true)
                Up = false;
            else
                Up = true;
            #region InvertValueCurrent
            switch (sortViewModel.Current)
            {
                case SortState.IDAsc:
                    Current = IDSort = SortState.IDDesc;
                    break;
                case SortState.IDDesc:
                    Current = IDSort = SortState.IDAsc;
                    break;
                case SortState.CountryAsc:
                    Current = CountrySort = SortState.CountryDesc;
                    break;
                case SortState.CountryDesc:
                    Current = CountrySort = SortState.CountryAsc;
                    break;
                case SortState.AreaAsc:
                    Current = AreaSort = SortState.AreaDesc;
                    break;
                case SortState.AreaDesc:
                    Current = AreaSort = SortState.AreaAsc;
                    break;
                case SortState.CityAsc:
                    Current = CitySort = SortState.CityDesc;
                    break;
                case SortState.CityDesc:
                    Current = CitySort = SortState.CityAsc;
                    break;
                case SortState.StreetAsc:
                    Current = StreetSort = SortState.StreetDesc;
                    break;
                case SortState.StreetDesc:
                    Current = StreetSort = SortState.StreetAsc;
                    break;
                case SortState.HousingAsc:
                    Current = HousingSort = SortState.HousingDesc;
                    break;
                case SortState.HousingDesc:
                    Current = HousingSort = SortState.HousingAsc;
                    break;
                case SortState.HouseAsc:
                    Current = HouseSort = SortState.HouseDesc;
                    break;
                case SortState.HouseDesc:
                    Current = HouseSort = SortState.HouseAsc;
                    break;
                case SortState.PostCodeAsc:
                    Current = PostCodeSort = SortState.PostCodeDesc;
                    break;
                case SortState.PostCodeDesc:
                    Current = PostCodeSort = SortState.PostCodeAsc;
                    break;
            }
            #endregion
        }

        public static IQueryable<Address> Sort(IQueryable<Address> addresses, SortState sortOrder)
        {
            #region SetSortingCommand
            switch (sortOrder)
            {
                case SortState.IDAsc:
                    addresses = addresses.OrderBy(s => s.AddrID);
                    break;
                case SortState.IDDesc:
                    addresses = addresses.OrderByDescending(s => s.AddrID);
                    break;
                case SortState.CountryAsc:
                    addresses = addresses.OrderBy(s => s.Country);
                    break;
                case SortState.CountryDesc:
                    addresses = addresses.OrderByDescending(s => s.Country);
                    break;
                case SortState.AreaAsc:
                    addresses = addresses.OrderBy(s => s.Area);
                    break;
                case SortState.AreaDesc:
                    addresses = addresses.OrderByDescending(s => s.Area);
                    break;
                case SortState.CityAsc:
                    addresses = addresses.OrderBy(s => s.City);
                    break;
                case SortState.CityDesc:
                    addresses = addresses.OrderByDescending(s => s.City);
                    break;
                case SortState.StreetAsc:
                    addresses = addresses.OrderBy(s => s.Street);
                    break;
                case SortState.StreetDesc:
                    addresses = addresses.OrderByDescending(s => s.Street);
                    break;
                case SortState.HousingAsc:
                    addresses = addresses.OrderBy(s => s.Housing);
                    break;
                case SortState.HousingDesc:
                    addresses = addresses.OrderByDescending(s => s.Housing);
                    break;
                case SortState.HouseAsc:
                    addresses = addresses.OrderBy(s => s.House);
                    break;
                case SortState.HouseDesc:
                    addresses = addresses.OrderByDescending(s => s.House);
                    break;
                case SortState.PostCodeAsc:
                    addresses = addresses.OrderBy(s => s.PostCode);
                    break;
                case SortState.PostCodeDesc:
                    addresses = addresses.OrderByDescending(s => s.PostCode);
                    break;
            }
            #endregion
            return addresses;
        }
    }
}
