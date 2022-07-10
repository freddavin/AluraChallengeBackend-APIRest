using AluraflixAPI.Models;

namespace AluraflixAPI.ViewModels
{
    public class ReadVideosPorCategoriaViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cor { get; set; }

        public List<object> Videos { get; set; }
    }
}
