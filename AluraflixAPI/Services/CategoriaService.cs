using AluraflixAPI.Contexts;
using AluraflixAPI.Models;
using AluraflixAPI.ViewModels;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace AluraflixAPI.Services
{
    public class CategoriaService : ICategoriaService
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
            Categoria? categoriaEncontrada = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            return ConverterParaReadViewModel(categoriaEncontrada);
        }

        public ReadVideosPorCategoriaViewModel? ConsultarFilmesPorCategoriaId(int id)
        {
            Categoria? categoriaEncontrada = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            return ConverterParaReadFilmesPorCategoriaViewModel(categoriaEncontrada);
        }

        public List<ReadCategoriaViewModel>? ConsultarCategorias(int pagina)
        {
            List<Categoria> colecaoDeCategorias = _context.Categorias.ToList();
            if (pagina > 0)
            {
                colecaoDeCategorias = ConfigurarPaginacaoDeCategorias(colecaoDeCategorias, pagina);
            }
            return ConverterParaReadViewModel(colecaoDeCategorias);
        }

        public Result RemoverCategoriaPorId(int id)
        {
            Categoria? categoriaEncontradaParaRemover = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            if (categoriaEncontradaParaRemover == null)
            {
                return Result.Fail("Categoria não encontrada.");
            }
            DeletarCategoriaDoBD(categoriaEncontradaParaRemover);
            return Result.Ok();
        }

        public ReadCategoriaViewModel? AtualizarCategoriaPorId(int id, CreateCategoriaViewModel categoriaComNovosDados)
        {
            Categoria? categoriaEncontrada = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            if (categoriaEncontrada == null)
            {
                return null;
            }            
            AtualizarCategoriaNoBD(categoriaEncontrada, categoriaComNovosDados);
            return ConverterParaReadViewModel(categoriaEncontrada);
        }

        private List<Categoria> ConfigurarPaginacaoDeCategorias(List<Categoria> colecaoDeCategorias, int pagina)
        {
            int inicioPagina = (pagina - 1) * 5;
            int quantidadeVideos = colecaoDeCategorias.Count() - inicioPagina < 5 ? colecaoDeCategorias.Count() - 5 : 5;
            return colecaoDeCategorias.GetRange(inicioPagina, quantidadeVideos);
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

        private void AtualizarCategoriaNoBD(Categoria categoriaAtual, CreateCategoriaViewModel categoriaNova)
        {
            categoriaAtual.Titulo = categoriaNova.Titulo;
            categoriaAtual.Cor = categoriaNova.Cor;
            _context.SaveChanges();
        }

        private void DeletarCategoriaDoBD(Categoria categoriaEncontradaParaRemover)
        {
            _context.Categorias.Remove(categoriaEncontradaParaRemover);
            _context.SaveChanges();
        }
    }
}
