using System.ComponentModel.DataAnnotations;

namespace AluraflixAPI.ViewModels
{
    public class CreateVideoViewModel
    {
        [Required(ErrorMessage = "O campo título é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O título deve ter no máximo 50 caracteres.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo descrição é obrigatório.")]
        [MaxLength(250, ErrorMessage = "A descrição deve ter no máximo 250 caracteres.")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O campo url é obrigatório.")]
        [Url(ErrorMessage = "O campo deve ser preenchido com uma Url válida.")]
        public string Url { get; set; }

        public int IdCategoria { get; set; } = 1; // Valor default da categoria para caso não seja preenchida
    }
}
