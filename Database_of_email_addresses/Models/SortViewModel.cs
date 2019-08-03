using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database_of_email_addresses.DBController;

namespace Database_of_email_addresses.Models
{
    public class SortViewModel
    {
        public SortState IDSort { get; set; }
        public SortState CountrySort { get; set; }
        public SortState CitySort { get; set; }
        public SortState StreetSort { get; set; }
        public SortState HouseSort { get; set; }
        public SortState PostCodeSort { get; set; }
        public SortState DateSort { get; set; }
        public SortState Current { get; set; }
        public bool Up { get; set; }

        public SortViewModel(SortState sortState)
        {
            IDSort = SortState.IDAsc;
            CountrySort = SortState.CountryAsc;
            CitySort = SortState.CityAsc;
            StreetSort = SortState.StreetAsc;
            HouseSort = SortState.HouseAsc;
            PostCodeSort = SortState.PostCodeAsc;
            DateSort = SortState.DateAsc;
            Up = true;

            if (sortState == SortState.IDDesc || sortState == SortState.CountryDesc
                || sortState == SortState.CityDesc || sortState == SortState.StreetDesc
                || sortState == SortState.HouseDesc || sortState == SortState.PostCodeDesc
                || sortState == SortState.DateDesc)
            {
                Up = false;
            }

            switch (sortState)
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
                case SortState.DateAsc:
                    Current = DateSort = SortState.DateDesc;
                    break;
                case SortState.DateDesc:
                    Current = DateSort = SortState.DateAsc;
                    break;
            }
        }
    }
}
