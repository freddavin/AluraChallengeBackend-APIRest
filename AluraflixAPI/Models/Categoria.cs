using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AluraflixAPI.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cor { get; set; }

        [JsonIgnore]
        public virtual List<Video> Videos { get; set; }
    }
}
