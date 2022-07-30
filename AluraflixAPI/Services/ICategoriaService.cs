using AluraflixAPI.ViewModels;
using FluentResults;

namespace AluraflixAPI.Services
{
    public interface ICategoriaService
    {
        public ReadCategoriaViewModel CadastrarCategoria(CreateCategoriaViewModel categoriaParaCadastrar);

        public ReadCategoriaViewModel? ConsultarCategoriaPorId(int id);

        public ReadVideosPorCategoriaViewModel? ConsultarFilmesPorCategoriaId(int id);

        public List<ReadCategoriaViewModel>? ConsultarCategorias(int pagina);

        public Result RemoverCategoriaPorId(int id);

        public ReadCategoriaViewModel? AtualizarCategoriaPorId(int id, CreateCategoriaViewModel categoriaComNovosDados);
    }
}
