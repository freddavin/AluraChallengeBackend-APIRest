using System.ComponentModel.DataAnnotations;

namespace AluraflixAPI.Requests
{
    public class UsuarioRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Senha { get; set; }

    }
}
