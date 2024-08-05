using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Staj.Model;
using StajWeb.DataAccess.Repository.IRepository;
using StajWeb.Models;
using System.Diagnostics;

namespace Staj.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "http://localhost:5198/api/Product";

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, HttpClient httpClient)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<ProductViewModel>>(content);
                return View(categories);
            }
            else
            {
                //Handle error response
                List<ProductViewModel> productList = _unitOfWork.Product.GetAll().Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Author = x.Author,
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    ISBN = x.ISBN,
                    ListPrice = x.ListPrice,
                    Price = x.Price,
                    Category = x.Category,
                    Title = x.Title,
                    ImgUrl = x.ImgUrl

                }).ToList();
                return View(productList);
            }
            //IEnumerable<Product> productlist = _unitOfWork.Product.GetAll(includeProperties: "Category");
            //return View(productlist);
        }

        public async Task<IActionResult> Details(int productId)
        {
            string apiUrlWithId = $"{_apiUrl}/{productId}";
            var response = await _httpClient.GetAsync(apiUrlWithId);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<ProductViewModel>(content);
                return View(product);
            }
            else
            {
                // Handle error response
                Product product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category");
                var productViewModel = new ProductViewModel
                {
                    Id = product.Id,
                    Author = product.Author,
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    ISBN = product.ISBN,
                    ListPrice = product.ListPrice,
                    Price = product.Price,
                    Category = product.Category,
                    Title = product.Title,
                    ImgUrl = product.ImgUrl
                };
                return View(productViewModel);
            }
            //Product product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category");
            //return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
