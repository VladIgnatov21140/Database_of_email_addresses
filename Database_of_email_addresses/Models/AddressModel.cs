using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Database_of_email_addresses.Models
{
    public class Address
    {
        [Key]
        public int AddrID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
        [ForeignKey("PCode")]
        public string PostCode { get; set; }
        public DateTime Date { get; set; }

        public Address(string country, string city, string street, int house, string postCode, DateTime date)
        {
            if (country != null && country != "")
                Country = country;
            else
                throw new Exception(message: "Incorrect data for country name");

            if (city != null && city != "")
                City = city;
            else
                throw new Exception(message: "Incorrect data for city name");

            if (street != null && street != "")
                Street = street;
            else
                throw new Exception(message: "Incorrect data for street name");

            if (house >= 0)
                House = house;
            else
                throw new Exception(message: "Incorrect data for house number");

            if (postCode != null && postCode != "")
                PostCode = postCode;
            else
                throw new Exception(message: "Incorrect data for postCode number");

            if (date != null)
                Date = date;
            else
                throw new Exception(message: "Incorrect data for date");
        }

        public void Deconstruct(out int id, out string country, out string city,
                                out string street, out int house, out string postcode,
                                out DateTime date)
        {
            id = this.AddrID;
            country = this.Country;
            city = this.City;
            street = this.Street;
            house = this.House;
            postcode = this.PostCode;
            date = this.Date;
        }

        public void DeconstructToStrings(out string id, out string country, out string city,
                                out string street, out string house, out string postcode,
                                out string date)
        {
            id = this.AddrID.ToString();
            country = this.Country;
            city = this.City;
            street = this.Street;
            house = this.House.ToString();
            postcode = this.PostCode;
            date = this.Date.ToString();
        }
    }
}
