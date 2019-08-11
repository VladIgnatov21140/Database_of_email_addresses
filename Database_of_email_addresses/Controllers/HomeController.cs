using Database_of_email_addresses.DBController;
using Database_of_email_addresses.DBController.DBContexts;
using Database_of_email_addresses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Database_of_email_addresses.Controllers
{
    public class HomeController : Controller
    {
        private readonly AddressContext addrContext;

        public HomeController(AddressContext context)
        {
            addrContext = context;
            addrContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Address> addresses = addrContext.Addresses;

            addresses = addresses.OrderBy(s => s.AddrID);

            #region Pagination
            var count = await addresses.CountAsync();
            var items = await addresses.Skip(0).Take(5).ToListAsync();
            #endregion

            #region BuildingViewModel
            IndexViewModel viewModel = new IndexViewModel
            {
                Addresses = items,
                PageViewModel = new PageViewModel(count, pageNumber: 1, pageSize: 5),
                SortViewModel = new SortViewModel(sortState: SortState.IDAsc),
                FilterViewModel = new FilterViewModel(),
            };
            #endregion

            HttpContext.Session.SetString("indexViewModelInJson", JsonConvert.SerializeObject(viewModel));
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SetFilters(string SNewParameters)
        {
            var date = DateTime.Now;
            string indexViewModelInJson = HttpContext.Session.GetString("indexViewModelInJson");
            try
            {
                foreach (var NewParameter in JsonConvert.DeserializeObject<(string key, string value)[]>(SNewParameters))
                {
                    string pattern = @"""+" + NewParameter.key + @"""+:""+\w*""+";
                    Regex regex = new Regex(pattern);
                    indexViewModelInJson = regex.Replace(indexViewModelInJson, "\"" + NewParameter.key + "\":\"" + NewParameter.value + "\"", 1);
                }
            }
            catch
            {

            }
            finally
            {
                HttpContext.Session.SetString("indexViewModelInJson", indexViewModelInJson);
            }
            
            IndexViewModel indexViewModel = JsonConvert.DeserializeObject<IndexViewModel>(indexViewModelInJson);
            IQueryable<Address> addresses = addrContext.Addresses;

            addresses = FilterViewModel.SetFiltering(addresses, indexViewModel);

            addresses = SortViewModel.Sort(addresses, indexViewModel.SortViewModel.Current);

            #region Pagination
            indexViewModel.PageViewModel.RowsCount = await addresses.CountAsync();
            indexViewModel.Addresses = await addresses.Skip((1 - 1) * indexViewModel.PageViewModel.PageSize).Take(indexViewModel.PageViewModel.PageSize).ToListAsync();
            #endregion

            #region BuildingViewModel
            IndexViewModel viewModel = new IndexViewModel
            {
                Addresses = indexViewModel.Addresses,
                PageViewModel = new PageViewModel(indexViewModel.PageViewModel.RowsCount, 1, indexViewModel.PageViewModel.PageSize),
                SortViewModel = new SortViewModel(indexViewModel.SortViewModel),
                FilterViewModel = new FilterViewModel(indexViewModel.FilterViewModel.SelectedCountry, indexViewModel.FilterViewModel.SelectedArea, indexViewModel.FilterViewModel.SelectedCity,
                                                    indexViewModel.FilterViewModel.SelectedStreet, indexViewModel.FilterViewModel.SelectedHousing, indexViewModel.FilterViewModel.SelectedHouse,
                                                    indexViewModel.FilterViewModel.SelectedPostCode),
            };
            #endregion

            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetPage(int PageNumber)
        {

            IndexViewModel indexViewModel = JsonConvert.DeserializeObject<IndexViewModel>(HttpContext.Session.GetString("indexViewModelInJson"));
            indexViewModel.PageViewModel.PageNumber = PageNumber;

            IQueryable<Address> addresses = addrContext.Addresses;

            addresses = FilterViewModel.SetFiltering(addresses, indexViewModel);

            addresses = SortViewModel.Sort(addresses, indexViewModel.SortViewModel.Current);

            #region Pagination
            indexViewModel.PageViewModel.RowsCount = await addresses.CountAsync();
            indexViewModel.Addresses = await addresses.Skip((indexViewModel.PageViewModel.PageNumber - 1) * indexViewModel.PageViewModel.PageSize).Take(indexViewModel.PageViewModel.PageSize).ToListAsync();
            #endregion

            #region BuildingViewModel
            IndexViewModel viewModel = new IndexViewModel
            {
                Addresses = indexViewModel.Addresses,
                PageViewModel = new PageViewModel(indexViewModel.PageViewModel.RowsCount, indexViewModel.PageViewModel.PageNumber, indexViewModel.PageViewModel.PageSize),
                SortViewModel = new SortViewModel(indexViewModel.SortViewModel),
                FilterViewModel = new FilterViewModel(indexViewModel.FilterViewModel.SelectedCountry, indexViewModel.FilterViewModel.SelectedArea, indexViewModel.FilterViewModel.SelectedCity,
                                            indexViewModel.FilterViewModel.SelectedStreet, indexViewModel.FilterViewModel.SelectedHousing, indexViewModel.FilterViewModel.SelectedHouse,
                                            indexViewModel.FilterViewModel.SelectedPostCode),
            };
            #endregion

            HttpContext.Session.SetString("indexViewModelInJson", JsonConvert.SerializeObject(viewModel));
            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SetPageSize(int PageSize)
        {

            IndexViewModel indexViewModel = JsonConvert.DeserializeObject<IndexViewModel>(HttpContext.Session.GetString("indexViewModelInJson"));
            indexViewModel.PageViewModel.PageSize = PageSize;

            IQueryable<Address> addresses = addrContext.Addresses;

            addresses = FilterViewModel.SetFiltering(addresses, indexViewModel);

            addresses = SortViewModel.Sort(addresses, indexViewModel.SortViewModel.Current);

            #region Pagination
            indexViewModel.PageViewModel.RowsCount = await addresses.CountAsync();
            indexViewModel.Addresses = await addresses.Skip((1 - 1) * indexViewModel.PageViewModel.PageSize).Take(indexViewModel.PageViewModel.PageSize).ToListAsync();
            #endregion

            #region BuildingViewModel
            IndexViewModel viewModel = new IndexViewModel
            {
                Addresses = indexViewModel.Addresses,
                PageViewModel = new PageViewModel(indexViewModel.PageViewModel.RowsCount, 1, indexViewModel.PageViewModel.PageSize),
                SortViewModel = new SortViewModel(indexViewModel.SortViewModel),
                FilterViewModel = new FilterViewModel(indexViewModel.FilterViewModel.SelectedCountry, indexViewModel.FilterViewModel.SelectedArea, indexViewModel.FilterViewModel.SelectedCity,
                                indexViewModel.FilterViewModel.SelectedStreet, indexViewModel.FilterViewModel.SelectedHousing, indexViewModel.FilterViewModel.SelectedHouse,
                                indexViewModel.FilterViewModel.SelectedPostCode),
            };
            #endregion

            HttpContext.Session.SetString("indexViewModelInJson", JsonConvert.SerializeObject(viewModel));
            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SetSorting(SortState sortState)
        {

            IndexViewModel indexViewModel = JsonConvert.DeserializeObject<IndexViewModel>(HttpContext.Session.GetString("indexViewModelInJson"));

            IQueryable<Address> addresses = addrContext.Addresses;

            addresses = FilterViewModel.SetFiltering(addresses, indexViewModel);

            #region SortingWithSortDirectionControl
            if (indexViewModel.SortViewModel.Current == sortState)
                indexViewModel.SortViewModel = new SortViewModel(indexViewModel.SortViewModel, true);
            else
                indexViewModel.SortViewModel = new SortViewModel(sortState);

            addresses = SortViewModel.Sort(addresses, indexViewModel.SortViewModel.Current);
            #endregion

            #region Pagination
            indexViewModel.PageViewModel.RowsCount = await addresses.CountAsync();
            indexViewModel.Addresses = await addresses.Skip((indexViewModel.PageViewModel.PageNumber - 1) * indexViewModel.PageViewModel.PageSize).Take(indexViewModel.PageViewModel.PageSize).ToListAsync();
            #endregion

            #region BuildingViewModel
            IndexViewModel viewModel = new IndexViewModel
            {
                Addresses = indexViewModel.Addresses,
                PageViewModel = new PageViewModel(indexViewModel.PageViewModel.RowsCount, indexViewModel.PageViewModel.PageNumber, indexViewModel.PageViewModel.PageSize),
                SortViewModel = new SortViewModel(indexViewModel.SortViewModel),
                FilterViewModel = new FilterViewModel(indexViewModel.FilterViewModel.SelectedCountry, indexViewModel.FilterViewModel.SelectedArea, indexViewModel.FilterViewModel.SelectedCity,
                                indexViewModel.FilterViewModel.SelectedStreet, indexViewModel.FilterViewModel.SelectedHousing, indexViewModel.FilterViewModel.SelectedHouse,
                                indexViewModel.FilterViewModel.SelectedPostCode),
            };
            #endregion

            HttpContext.Session.SetString("indexViewModelInJson", JsonConvert.SerializeObject(viewModel));
            return View("Index", viewModel);
        }
    }
}
