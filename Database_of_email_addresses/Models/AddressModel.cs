using System;
using System.ComponentModel.DataAnnotations;

namespace Database_of_email_addresses.Models
{
    public class Address
    {
        [Key]
        public int AddrID { get; set; }
        [MaxLength(40)]
        public string Country { get; set; }
        [MaxLength(40)]
        public string Area { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Street { get; set; }
        [MaxLength(40)]
        public string Housing { get; set; }
        [MaxLength(50)]
        public int House { get; set; }
        [MaxLength(30)]
        public string PostCode { get; set; }

        public Address(string area, string country, string city, string street, string housing, int house, string postCode)
        {
            #region SettingValuesWithValidation
            if (country != null && country != "")
                Country = country;
            else
                throw new Exception(message: "Incorrect data for country name");

            if (area != null && area != "")
                Area = area;
            else
                throw new Exception(message: "Incorrect data for area name");

            if (city != null && city != "")
                City = city;
            else
                throw new Exception(message: "Incorrect data for city name");

            if (street != null && street != "")
                Street = street;
            else
                throw new Exception(message: "Incorrect data for street name");

            if (housing != null && housing != "")
                Housing = housing;
            else
                throw new Exception(message: "Incorrect data for housing name");

            if (house >= 0)
                House = house;
            else
                throw new Exception(message: "Incorrect data for house number");

            if (postCode != null && postCode != "")
                PostCode = postCode;
            else
                throw new Exception(message: "Incorrect data for postCode number");
            #endregion
        }

        public void Deconstruct(out int id, out string area, out string country, out string city,
                                out string street, out string housing, out int house, out string postcode)
        {
            id = this.AddrID;
            country = this.Country;
            area = this.Area;
            city = this.City;
            street = this.Street;
            housing = this.Housing;
            house = this.House;
            postcode = this.PostCode;
        }

        public void DeconstructToStrings(out string id, out string area, out string country, out string city,
                                out string street, out string housing, out string house, out string postcode)
        {
            id = this.AddrID.ToString();
            country = this.Country;
            area = this.Area;
            city = this.City;
            street = this.Street;
            housing = this.Housing;
            house = this.House.ToString();
            postcode = this.PostCode;
        }
    }
}
