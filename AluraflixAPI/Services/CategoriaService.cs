using AluraflixAPI.Contexts;
using AluraflixAPI.Models;
using AluraflixAPI.ViewModels;
using AutoMapper;

namespace AluraflixAPI.Services
{
    public class CategoriaService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CategoriaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCategoriaViewModel CadastrarCategoria(CreateCategoriaViewModel categoriaParaCadastrar)
        {
            Categoria categoria = ConverterParaCategoriaModel(categoriaParaCadastrar);
            AdicionarCategoriaNoBD(categoria);
            return ConverterParaReadViewModel(categoria);
        }

        public ReadCategoriaViewModel? ConsultarCategoriaPorId(int id)
        {
            Categoria? categoriaencontrada = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            return ConverterParaReadViewModel(categoriaencontrada);
        }

        public ReadVideosPorCategoriaViewModel? ConsultarFilmesPorCategoria(int id)
        {
            Categoria? categoriaencontrada = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            return ConverterParaReadFilmesPorCategoriaViewModel(categoriaencontrada);
        }

        public List<ReadCategoriaViewModel>? ConsultarCategorias()
        {
            List<Categoria> colecaoDeCategorias = _context.Categorias.ToList();
            return ConverterParaReadViewModel(colecaoDeCategorias);
        }

        private List<ReadCategoriaViewModel>? ConverterParaReadViewModel(List<Categoria> colecaoDeCategorias)
        {
            if (EstaVazioOuNulo(colecaoDeCategorias))
            {
                return null;
            }
            return _mapper.Map<List<ReadCategoriaViewModel>>(colecaoDeCategorias);
        }

        private ReadCategoriaViewModel? ConverterParaReadViewModel(Categoria categoria)
        {
            if (categoria == null)
            {
                return null;
            }
            return _mapper.Map<ReadCategoriaViewModel>(categoria);
        }

        private ReadVideosPorCategoriaViewModel? ConverterParaReadFilmesPorCategoriaViewModel(Categoria categoria)
        {
            if (categoria == null)
            {
                return null;
            }
            return _mapper.Map<ReadVideosPorCategoriaViewModel>(categoria);
        }

        private Categoria ConverterParaCategoriaModel(CreateCategoriaViewModel categoria)
        {
            return _mapper.Map<Categoria>(categoria);
        }

        private bool EstaVazioOuNulo(List<Categoria> colecaoDeCategorias)
        {
            if (colecaoDeCategorias == null || !colecaoDeCategorias.Any())
            {
                return true;
            }
            return false;
        }

        private void AdicionarCategoriaNoBD(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }
    }
}
