using AluraflixAPI.ViewModels;
using FluentResults;

namespace AluraflixAPI.Services
{
    public interface IVideoService
    {
        public ReadVideoViewModel CadastrarVideo(CreateVideoViewModel videoParaCadastrar);
        public ReadVideoViewModel? ConsultarVideoPorId(int id);
        public List<ReadVideoViewModel>? ConsultarVideos(string titulo, int pagina);
        public Result RemoverVideoPorId(int id);
        public ReadVideoViewModel? AtualizarVideoPorId(int id, CreateVideoViewModel videoComNovosDados);

    }
}
