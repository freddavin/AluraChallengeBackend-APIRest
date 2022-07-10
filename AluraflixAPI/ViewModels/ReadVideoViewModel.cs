using AluraflixAPI.Models;

namespace AluraflixAPI.ViewModels
{
    public class ReadVideoViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }

        //public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }
    }
}
