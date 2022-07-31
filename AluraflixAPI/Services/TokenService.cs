using AluraflixAPI.Requests;
using FluentResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AluraflixAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public Result GerarToken(UsuarioRequest request)
        {
            if (request.Nome == "admin" && request.Senha == "admin")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Nome)
                };

                var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]));

                var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "Aluraflix",
                    audience: "Aluraflix",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credenciais);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Result.Ok().WithSuccess(tokenString);
            }
            return Result.Fail("Usuário e/ou Senha inválidos.");
        }
    }
}
