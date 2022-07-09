using System.ComponentModel.DataAnnotations;

namespace AluraflixAPI.ViewModels
{
    public class CreateVideoViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "O título deve ter no máximo 50 caracteres.")]
        public string Titulo { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "A descrição deve ter no máximo 250 caracteres.")]
        public string Descricao { get; set; }
        [Required]
        [Url(ErrorMessage = "O campo deve ser preenchido com uma Url válida.")]
        public string Url { get; set; }
    }
}
