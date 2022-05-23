using AutoMapper;
using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eStoreClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient client = null;
        private string loginApi = string.Empty;
        public LoginController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            loginApi = "https://localhost:44372/api/Authentication";
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginForm login)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize<LoginForm>(login), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(loginApi, stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToRoute(nameof(HomeController));
            }
            return View("Error");
        }
    }
}
