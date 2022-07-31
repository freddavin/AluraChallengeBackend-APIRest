using AluraflixAPI.Requests;
using FluentResults;

namespace AluraflixAPI.Services
{
    public interface ITokenService
    {
        public Result GerarToken(UsuarioRequest request);
    }
}
