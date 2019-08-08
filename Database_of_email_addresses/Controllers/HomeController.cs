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
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Session;
using Newtonsoft;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace Database_of_email_addresses.Controllers
{
    public class HomeController : Controller
    {
        private readonly AddressContext addrContext;

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


        public async Task<IActionResult> Index()
        {
            IQueryable<Address> addresses = addrContext.Addresses;

            #region Sorting
            addresses = SortViewModel.Sort(addresses, sortOrder: SortState.IDAsc);
            #endregion

            #region Pagination
            var count = await addresses.CountAsync();
            var items = await addresses.Take(5).ToListAsync();
            #endregion

            #region BuildingViewModel
            IndexViewModel viewModel = new IndexViewModel
            {
                Addresses = items,
                PageViewModel = new PageViewModel(count, pageNumber: 1, pageSize: 5),
                SortViewModel = new SortViewModel(sortState: SortState.IDAsc, noInvertSort: true),
                FilterViewModel = new FilterViewModel(),
            };
            #endregion

            HttpContext.Session.SetString("indexViewModelInJson", JsonConvert.SerializeObject(viewModel));
            return View(viewModel);
        }
        
        public async Task<IActionResult> GetAddressesDataQuery(string SNewParameters)
        {
            string indexViewModelInJson = HttpContext.Session.GetString("indexViewModelInJson");
            using (var FileW = new StreamWriter("D:\\WebStream\\First.txt", true))
            {
                FileW.Write(indexViewModelInJson);
                FileW.Close();
            }
            try
            {
                foreach (var NewParameter in JsonConvert.DeserializeObject<(string key, string value)[]>(SNewParameters))
                {
                    string pattern = @"""+" + NewParameter.key + @"""+:""+\w*""+";
                    Regex regex = new Regex(pattern);
                    indexViewModelInJson = regex.Replace(indexViewModelInJson, "\"" + NewParameter.key + "\":\"" + NewParameter.value + "\"", 1);
                }
            }
            finally
            {
                HttpContext.Session.SetString("indexViewModelInJson", indexViewModelInJson);
            }


            IndexViewModel indexViewModel = JsonConvert.DeserializeObject<IndexViewModel>(indexViewModelInJson);
            (string key, string value)[] ps = new (string key, string value)[] { ("123", "123"), ("1235", "1234") };
            using (var FileW = new StreamWriter("D:\\WebStream\\Last.txt", true))
            {
                FileW.WriteLine(JsonConvert.SerializeObject(ps));
                FileW.Write(indexViewModelInJson);
                FileW.Close();
            }

            IQueryable<Address> addresses = addrContext.Addresses;

            #region Filtering
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedCountry) && indexViewModel.FilterViewModel.SelectedCountry != "Все")
            {
                addresses = addresses.Where(p => p.Country.Contains(indexViewModel.FilterViewModel.SelectedCountry));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedCity))
            {
                addresses = addresses.Where(p => p.City.Contains(indexViewModel.FilterViewModel.SelectedCity));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedStreet))
            {
                addresses = addresses.Where(p => p.Street.Contains(indexViewModel.FilterViewModel.SelectedStreet));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedHouse))
            {
                addresses = addresses.Where(p => p.House.ToString() == indexViewModel.FilterViewModel.SelectedHouse);
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedPostCode))
            {
                addresses = addresses.Where(p => p.PostCode.Contains(indexViewModel.FilterViewModel.SelectedPostCode));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedDate))
            {
                addresses = addresses.Where(p => p.Date.ToString("dd.MM.yyyy, hh:mm").Equals(indexViewModel.FilterViewModel.SelectedDate));
            }
            #endregion

            #region Sorting
            addresses = SortViewModel.Sort(addresses, indexViewModel.SortViewModel.Current);
            #endregion

            #region Pagination
            indexViewModel.PageViewModel.RowsCount = await addresses.CountAsync();
            indexViewModel.Addresses = await addresses.Skip((indexViewModel.PageViewModel.PageNumber - 1) * indexViewModel.PageViewModel.PageSize).Take(indexViewModel.PageViewModel.PageSize).ToListAsync();
            #endregion

            //IndexViewModel viewModel = indexViewModel;
            IndexViewModel viewModel = new IndexViewModel
            {
                Addresses = indexViewModel.Addresses,
                PageViewModel = new PageViewModel(indexViewModel.PageViewModel.RowsCount, indexViewModel.PageViewModel.PageNumber, indexViewModel.PageViewModel.PageSize),
                SortViewModel = new SortViewModel(indexViewModel.SortViewModel.Current, indexViewModel.SortViewModel.NoInvertSort),
                FilterViewModel = new FilterViewModel(indexViewModel.FilterViewModel.SelectedCountry, indexViewModel.FilterViewModel.SelectedCity,
                                                        indexViewModel.FilterViewModel.SelectedStreet, indexViewModel.FilterViewModel.SelectedHouse,
                                                        indexViewModel.FilterViewModel.SelectedPostCode, indexViewModel.FilterViewModel.SelectedDate),
            };

            return View("Index", viewModel);
        }

        public async Task<IActionResult> GetPage(int PageNumber)
        {

            IndexViewModel indexViewModel = JsonConvert.DeserializeObject<IndexViewModel>(HttpContext.Session.GetString("indexViewModelInJson"));
            indexViewModel.PageViewModel.PageNumber = PageNumber;

            IQueryable<Address> addresses = addrContext.Addresses;

            #region Filtering
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedCountry) && indexViewModel.FilterViewModel.SelectedCountry != "Все")
            {
                addresses = addresses.Where(p => p.Country.Contains(indexViewModel.FilterViewModel.SelectedCountry));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedCity))
            {
                addresses = addresses.Where(p => p.City.Contains(indexViewModel.FilterViewModel.SelectedCity));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedStreet))
            {
                addresses = addresses.Where(p => p.Street.Contains(indexViewModel.FilterViewModel.SelectedStreet));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedHouse))
            {
                addresses = addresses.Where(p => p.House.ToString() == indexViewModel.FilterViewModel.SelectedHouse);
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedPostCode))
            {
                addresses = addresses.Where(p => p.PostCode.Contains(indexViewModel.FilterViewModel.SelectedPostCode));
            }
            if (!String.IsNullOrWhiteSpace(indexViewModel.FilterViewModel.SelectedDate))
            {
                addresses = addresses.Where(p => p.Date.ToString("dd.MM.yyyy, hh:mm").Equals(indexViewModel.FilterViewModel.SelectedDate));
            }
            #endregion

            #region Sorting
            addresses = SortViewModel.Sort(addresses, indexViewModel.SortViewModel.Current);
            #endregion

            #region Pagination
            indexViewModel.PageViewModel.RowsCount = await addresses.CountAsync();
            indexViewModel.Addresses = await addresses.Skip((indexViewModel.PageViewModel.PageNumber - 1) * indexViewModel.PageViewModel.PageSize).Take(indexViewModel.PageViewModel.PageSize).ToListAsync();
            #endregion

            //IndexViewModel viewModel = indexViewModel;
            IndexViewModel viewModel = new IndexViewModel
            {
                Addresses = indexViewModel.Addresses,
                PageViewModel = new PageViewModel(indexViewModel.PageViewModel.RowsCount, indexViewModel.PageViewModel.PageNumber, indexViewModel.PageViewModel.PageSize),
                SortViewModel = new SortViewModel(indexViewModel.SortViewModel.Current, indexViewModel.SortViewModel.NoInvertSort),
                FilterViewModel = new FilterViewModel(indexViewModel.FilterViewModel.SelectedCountry, indexViewModel.FilterViewModel.SelectedCity,
                                                        indexViewModel.FilterViewModel.SelectedStreet, indexViewModel.FilterViewModel.SelectedHouse,
                                                        indexViewModel.FilterViewModel.SelectedPostCode, indexViewModel.FilterViewModel.SelectedDate),
            };

            HttpContext.Session.SetString("indexViewModelInJson", JsonConvert.SerializeObject(viewModel));
            return View("Index", viewModel);
        }

    }
}
