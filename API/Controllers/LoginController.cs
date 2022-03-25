using API.Data;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] Usuario model)
        {
            // Recuperar o usuario do repositório
            var user = UserRepository.Get(model.User, model.Senha);

            if (user == null)
            {
                return NotFound(new {message = "Usuario ou senha inválidos"});
            }

            //Gera o token
            var token = TokenService.GenerateToken(user);

            //Oculta a senha
            user.Senha = "";

            //Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }
    }
}
