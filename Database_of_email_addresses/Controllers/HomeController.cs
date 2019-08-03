using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Database_of_email_addresses.Models;
using Microsoft.EntityFrameworkCore;
using Database_of_email_addresses.DBController.DBContexts;
using Database_of_email_addresses.DBController;

namespace Database_of_email_addresses.Controllers
{
    public class HomeController : Controller
    {
        AddressContext addrContext;

        public HomeController(AddressContext context)
        {
            addrContext = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Address address)
        {
            addrContext.Addresses.Add(address);
            await addrContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(string selectedCountry, string selectedCity,
                                                string selectedStreet, int? selectedHouse,
                                                string selectedPostCode, string selectedDate,
                                                int rowsCount = 3, int page = 1,
                                                SortState sortOrder = SortState.IDAsc)
        {

            IQueryable<Address> addresses = addrContext.Addresses;
            /*
            //Фильтрация.....
            if (!String.IsNullOrEmpty(selectedCountry))
            {
                addresses = addresses.Where(p => p.Country.Contains(selectedCountry));
            }
            if (!String.IsNullOrEmpty(selectedCity))
            {
                addresses = addresses.Where(p => p.City.Contains(selectedCity));
            }
            if (!String.IsNullOrEmpty(selectedStreet))
            {
                addresses = addresses.Where(p => p.Street.Contains(selectedStreet));
            }
            if (selectedHouse != null && selectedHouse.Value != 0)
            {
                addresses = addresses.Where(p => p.House == selectedHouse.Value);
            }
            if (!String.IsNullOrEmpty(selectedPostCode))
            {
                addresses = addresses.Where(p => p.PostCode.Contains(selectedPostCode));
            }
            if (!String.IsNullOrEmpty(selectedDate))
            {
                addresses = addresses.Where(p => p.Date.Equals(selectedDate));
            }
            //.....Фильтрация
            */
            //Сортировка.....
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
            //.....Сортировка
            /*
            // пагинация
            var count = await addresses.CountAsync();
            var items = await addresses.Skip((page - 1) * rowsCount).Take(rowsCount).ToListAsync();
            */
            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel
            {
                Addresses = await addresses.AsNoTracking().ToArrayAsync(),
                //PageViewModel = new PageViewModel(count, page, rowsCount),
                SortViewModel = new SortViewModel(sortOrder)
               /* FilterViewModel = new FilterViewModel(addrContext.Addresses.ToList(), selectedCountry,
                                                        selectedCity, selectedStreet, selectedHouse,
                                                        selectedPostCode, selectedDate),
                Addresses = items*/
            };
            return View(viewModel);
        }

        //public async Task<IActionResult> Index(SortState sortOrder = SortState.IDAsc)
        //{

        //}

            /*
            public IActionResult About()
            {
                ViewData["Message"] = "Your application description page.";

                return View();
            }

            public IActionResult Contact()
            {
                ViewData["Message"] = "Your contact page.";

                return View();
            }

            public IActionResult Privacy()
            {
                return View();
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }*/
        }
}
