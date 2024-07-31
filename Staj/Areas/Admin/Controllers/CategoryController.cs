using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Staj.Model;
using StajWeb.DataAccess.Repository.IRepository;
using StajWeb.Models;
using StajWeb.Utility;
using System.Text;


namespace Staj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "http://localhost:5198/api/Category";


        public CategoryController(IUnitOfWork UnitOfWork, HttpClient httpClient)
        {
            _unitOfWork = UnitOfWork;
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(content);
                return View(categories);
            }
            else
            {
                // Handle error response
                //List<CategoryViewModel> objCategoryList = _unitOfWork.Category.GetAll().Select(x => new CategoryViewModel
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    DisplayOrder = x.DisplayOrder
                //}).ToList();
                return View(new List<CategoryViewModel>());
            }

            //List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            //return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            //if (obj.Name != null && obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(obj);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Category created successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle error response
                    ModelState.AddModelError("", "Error creating category");
                }
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(obj);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_apiUrl}/{obj.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Category updated successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle error response
                    ModelState.AddModelError("", "Error updating category");
                }
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index");
            }
            else
            {
                // Handle error response
                ModelState.AddModelError("", "Error deleting category");
            }
            return View();
        }
    }
}
