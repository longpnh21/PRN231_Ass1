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
    public class ProductsController : Controller
    {
        private readonly HttpClient client = null;
        private readonly IMapper _mapper;
        private string productApiUrl = string.Empty;
        private string categoryApiUrl = string.Empty;
        public ProductsController(IMapper mapper)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "https://localhost:44372/api/Products";
            categoryApiUrl = "https://localhost:44372/api/Categories";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(productApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            List<ProductDto> products = JsonSerializer.Deserialize<List<ProductDto>>(strData, options);

            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            HttpResponseMessage categoriesResponse = await client.GetAsync(categoryApiUrl);
            string strCategoriesData = await categoriesResponse.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            List<CategoryDto> categories = JsonSerializer.Deserialize<List<CategoryDto>>(strCategoriesData, options);

            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto product)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize<CreateProductDto>(product), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(productApiUrl, stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error");

        }

        public async Task<IActionResult> Details(int productId)
        {
            HttpResponseMessage response = await client.GetAsync(productApiUrl + $"/{productId}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            ProductDto product = JsonSerializer.Deserialize<ProductDto>(strData, options);

            return View(product);
        }

        public async Task<IActionResult> Edit(int productId)
        {
            HttpResponseMessage productResponse = await client.GetAsync(productApiUrl + $"/{productId}");
            string strProductData = await productResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            ProductDto product = JsonSerializer.Deserialize<ProductDto>(strProductData, options);

            HttpResponseMessage categoriesResponse = await client.GetAsync(categoryApiUrl);
            string strCategoriesData = await categoriesResponse.Content.ReadAsStringAsync();
            List<CategoryDto> categories = JsonSerializer.Deserialize<List<CategoryDto>>(strCategoriesData, options);

            ViewBag.Categories = categories;

            return View(_mapper.Map<UpdateProductDto>(product));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateProductDto product)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize<UpdateProductDto>(product), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(productApiUrl + $"/{product.ProductId}", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error");

        }


        public async Task<IActionResult> Delete(int productId)
        {
            HttpResponseMessage productResponse = await client.GetAsync(productApiUrl + $"/{productId}");
            string strProductData = await productResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            ProductDto product = JsonSerializer.Deserialize<ProductDto>(strProductData, options);

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int productId)
        {
            HttpResponseMessage response = await client.DeleteAsync(productApiUrl + $"/{productId}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error");
        }
    }
}

