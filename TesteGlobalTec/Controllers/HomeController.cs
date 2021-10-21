using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TesteGlobalTec.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using TesteGlobalTec.Services;
using TesteGlobalTec.Repositories;

namespace TesteGlobalTec.Controllers
{
    [ApiController]
    [Route("v1/account")]
    public class HomeController : Controller
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {
            var user = UserRepository.GetUser(model.UserName, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }
    }
}