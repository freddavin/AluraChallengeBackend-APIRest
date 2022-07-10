using System.ComponentModel.DataAnnotations;

namespace AluraflixAPI.ViewModels
{
    public class CreateCategoriaViewModel
    {
        [Required(ErrorMessage = "O campo título é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O campo título deve ter no máximo 50 caracteres.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo cor é obrigatório.")]
        [MaxLength(20, ErrorMessage = "O campo cor deve ter no máximo 20 caracteres.")]
        public string Cor { get; set; }
    }
}
