using BusinessObject;
using BusinessObject.Dtos;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IMemberRepository memberRepository, IConfiguration configuration)
        {
            _memberRepository = memberRepository;
            _configuration = configuration;
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginForm login)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest();
        //        }

        //        var adminEmail = _configuration.GetSection("Admin").GetSection("email").Value;
        //        var adminPassword = _configuration.GetSection("Admin").GetSection("password").Value;

        //        if (adminEmail.Equals(login.Email) && adminPassword.Equals(login.Password))
        //        {

        //        }

        //        var member = _memberRepository.Login(login.Email, login.Password);

        //        if (member == null)
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
    }
}
