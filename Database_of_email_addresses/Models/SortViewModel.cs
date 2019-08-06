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

        public SortViewModel(SortState sortState, bool noInvertSort)
        {
            IDSort = SortState.IDAsc;
            CountrySort = SortState.CountryAsc;
            CitySort = SortState.CityAsc;
            StreetSort = SortState.StreetAsc;
            HouseSort = SortState.HouseAsc;
            PostCodeSort = SortState.PostCodeAsc;
            DateSort = SortState.DateAsc;
            Up = true;

            if (!noInvertSort)
            {
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
            else
            {
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
                    case SortState.DateAsc:
                        Current = DateSort = SortState.DateAsc;
                        break;
                    case SortState.DateDesc:
                        Current = DateSort = SortState.DateDesc;
                        break;
                }
            }


        }

        public static IQueryable<Address> Sort(IQueryable<Address> addresses, SortState sortOrder)
        {
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
                case SortState.DateAsc:
                    addresses = addresses.OrderBy(s => s.Date);
                    break;
                case SortState.DateDesc:
                    addresses = addresses.OrderByDescending(s => s.Date);
                    break;
            }
            return addresses;
        }
    }
}
