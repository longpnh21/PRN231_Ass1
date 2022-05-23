using AutoMapper;
using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eStoreClient.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient client = null;
        private readonly IMapper _mapper;
        private string categoryApiUrl = string.Empty;
        public CategoriesController(IMapper mapper)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            categoryApiUrl = "https://localhost:44372/api/Categories";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(categoryApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            List<CategoryDto> categories = JsonSerializer.Deserialize<List<CategoryDto>>(strData, options);

            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryDto category)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize<CreateCategoryDto>(category), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(categoryApiUrl, stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error");

        }

        public async Task<IActionResult> Details(int categoryId)
        {
            HttpResponseMessage response = await client.GetAsync(categoryApiUrl + $"/{categoryId}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            CategoryDto category = JsonSerializer.Deserialize<CategoryDto>(strData, options);

            return View(category);
        }

        public async Task<IActionResult> Edit(int categoryId)
        {
            HttpResponseMessage categoryResponse = await client.GetAsync(categoryApiUrl + $"/{categoryId}");
            string strCategoryData = await categoryResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            CategoryDto category = JsonSerializer.Deserialize<CategoryDto>(strCategoryData, options);

            return View(_mapper.Map<UpdateCategoryDto>(category));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateCategoryDto category)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize<UpdateCategoryDto>(category), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(categoryApiUrl + $"/{category.CategoryId}", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error");

        }

        public async Task<IActionResult> Delete(int categoryId)
        {
            HttpResponseMessage categoryResponse = await client.GetAsync(categoryApiUrl + $"/{categoryId}");
            string strCategoryData = await categoryResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            CategoryDto category = JsonSerializer.Deserialize<CategoryDto>(strCategoryData, options);

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int categoryId)
        {
            HttpResponseMessage response = await client.DeleteAsync(categoryApiUrl + $"/{categoryId}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error");
        }
    }
}
