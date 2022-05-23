using AutoMapper;
using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace eStoreClient.Controllers
{
    public class OrdersController : Controller
    {
        private readonly HttpClient client = null;
        private readonly IMapper _mapper;
        private string orderApiUrl = string.Empty;
        private string orderDetailApiUrl = string.Empty;
        public OrdersController(IMapper mapper)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            orderApiUrl = "https://localhost:44372/api/Orders";
            orderDetailApiUrl = "https://localhost:44372/api/OrderDetails";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(orderApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            List<OrderDto> orders = JsonSerializer.Deserialize<List<OrderDto>>(strData, options);

            return View(orders);
        }

        //public async Task<IActionResult> Create()
        //{
        //    HttpResponseMessage categoriesResponse = await client.GetAsync(categoryApiUrl);
        //    string strCategoriesData = await categoriesResponse.Content.ReadAsStringAsync();

        //    var options = new JsonSerializerOptions()
        //    {
        //        PropertyNameCaseInsensitive = true,
        //    };
        //    List<CategoryDto> categories = JsonSerializer.Deserialize<List<CategoryDto>>(strCategoriesData, options);

        //    ViewBag.Categories = categories;

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(CreateOrderDto order)
        //{
        //    var stringContent = new StringContent(JsonSerializer.Serialize<CreateOrderDto>(order), Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = await client.PostAsync(orderApiUrl, stringContent);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View("Error");

        //}

        public async Task<IActionResult> Details(int orderId)
        {
            HttpResponseMessage response = await client.GetAsync(orderDetailApiUrl + $"/{orderId}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            List<OrderDetailDto> orderDetails = JsonSerializer.Deserialize<List<OrderDetailDto>>(strData, options);

            return View(orderDetails);
        }

        public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate)
        {
            var url = orderApiUrl += "/report?";
            if (startDate != null)
            {
                url += $"startDate={startDate.Value}&";
            }
            if (endDate != null)
            {
                url += $"endDate={endDate.Value}&";
            }
            HttpResponseMessage response = await client.GetAsync(orderApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            List<OrderDto> orderDetails = JsonSerializer.Deserialize<List<OrderDto>>(strData, options);

            return View(orderDetails);
        }

    }
}
