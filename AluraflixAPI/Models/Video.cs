using System.ComponentModel.DataAnnotations;

namespace AluraflixAPI.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }

    }
}
