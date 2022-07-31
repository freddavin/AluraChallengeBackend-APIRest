using AluraflixAPI.Requests;
using AluraflixAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AluraflixAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TokenController : ControllerBase
    {
        private readonly ITokenService _service;

        public TokenController(ITokenService service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult RequestToken([FromBody] UsuarioRequest request)
        {
            var resultado = _service.GerarToken(request);

            if(resultado.IsSuccess)
            {
                return Ok(new { Token = resultado.Successes.FirstOrDefault().Message });
            }
            return BadRequest("Usuário e/ou Senha Inválida.");
        }


    }
}
