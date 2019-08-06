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
                                                int pageSize = 3, int page = 1,
                                                SortState sortOrder = SortState.IDAsc,
                                                bool noInvertSort = false)
        {

            IQueryable<Address> addresses = addrContext.Addresses;

            //Фильтрация.....
            if (!String.IsNullOrWhiteSpace(selectedCountry) && selectedCountry != "Все")
            {
                addresses = addresses.Where(p => p.Country.Contains(selectedCountry));
            }
            if (!String.IsNullOrWhiteSpace(selectedCity))
            {
                addresses = addresses.Where(p => p.City.Contains(selectedCity));
            }
            if (!String.IsNullOrWhiteSpace(selectedStreet))
            {
                addresses = addresses.Where(p => p.Street.Contains(selectedStreet));
            }
            if (selectedHouse != null && selectedHouse.Value != 0)
            {
                addresses = addresses.Where(p => p.House == selectedHouse.Value);
            }
            if (!String.IsNullOrWhiteSpace(selectedPostCode))
            {
                addresses = addresses.Where(p => p.PostCode.Contains(selectedPostCode));
            }
            if (!String.IsNullOrWhiteSpace(selectedDate))
            {
                addresses = addresses.Where(p => p.Date.ToString("dd.MM.yyyy, hh:mm").Equals(selectedDate));
            }
            //.....Фильтрация

            //Сортировка.....
            addresses = SortViewModel.Sort(addresses, sortOrder);
            //.....Сортировка
            
            // пагинация
            var count = await addresses.CountAsync();
            var items = await addresses.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            
            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel
            {
                Addresses = items,
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder, noInvertSort),
                FilterViewModel = new FilterViewModel(addrContext.Addresses.ToList(), selectedCountry,
                                                        selectedCity, selectedStreet, selectedHouse,
                                                        selectedPostCode, selectedDate),
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
