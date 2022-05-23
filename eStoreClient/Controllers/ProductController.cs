using BusinessObject;
using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace eStoreClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;
        private string productApiUrl = string.Empty;
        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "https://localhost:44372/api/Products";
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
    }
}
