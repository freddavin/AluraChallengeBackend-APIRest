using AluraflixAPI.Contexts;
using AluraflixAPI.Models;
using AluraflixAPI.ViewModels;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace AluraflixAPI.Services
{
    public class VideoService : IVideoService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public VideoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadVideoViewModel CadastrarVideo(CreateVideoViewModel videoParaCadastrar)
        {
            Video video = ConverterParaVideoModel(videoParaCadastrar);
            AdicionarVideoNoBD(video);
            _context.Entry(video).Reference(v => v.Categoria).Load();
            return ConverterParaReadViewModel(video);
        }

        public ReadVideoViewModel? ConsultarVideoPorId(int id)
        {
            Video? videoEncontrado = _context.Videos.FirstOrDefault(video => video.Id == id);
            return ConverterParaReadViewModel(videoEncontrado);
        }

        public List<ReadVideoViewModel>? ConsultarVideos(string titulo, int pagina)
        {
            List<Video> colecaoDeVideos = _context.Videos.ToList();
            if (titulo != null)
            {
                colecaoDeVideos = FiltrarColecaoDeVideos(titulo);
            }
            if (pagina > 0)
            {
                colecaoDeVideos = ConfigurarPaginacaoDeVideos(colecaoDeVideos, pagina);
            }
            return ConverterParaReadViewModel(colecaoDeVideos);
        }

        public Result RemoverVideoPorId(int id)
        {
            Video? videoEncontradoParaRemover = _context.Videos.FirstOrDefault(video => video.Id == id);
            if (videoEncontradoParaRemover == null)
            {
                return Result.Fail("Vídeo não encontrado.");
            }
            RemoverVideoDoBD(videoEncontradoParaRemover);
            return Result.Ok();
        }

        public ReadVideoViewModel? AtualizarVideoPorId(int id, CreateVideoViewModel videoComNovosDados)
        {
            Video? videoEncontrado = _context.Videos.FirstOrDefault(video => video.Id == id);
            if (videoEncontrado == null)
            {
                return null;
            }
            AtualizarVideoNoBD(videoEncontrado, videoComNovosDados);
            return ConverterParaReadViewModel(videoEncontrado);
        }

        public List<Video> ConfigurarPaginacaoDeVideos(List<Video> colecaoDeVideos, int pagina)
        {
            int inicioPagina = (pagina - 1) * 5;
            int quantidadeVideos = colecaoDeVideos.Count() - inicioPagina < 5 ? colecaoDeVideos.Count() - 5 : 5;
            return colecaoDeVideos.GetRange(inicioPagina, quantidadeVideos);
        }

        public List<Video> FiltrarColecaoDeVideos(string titulo)
        {
            return _context.Videos.Where(video => video.Titulo.Contains(titulo)).ToList();
        }

        private bool EstaVazioOuNulo(List<Video> colecaoDeVideos)
        {
            if (colecaoDeVideos == null || !colecaoDeVideos.Any())
            {
                return true;
            }
            return false;
        }

        private ReadVideoViewModel? ConverterParaReadViewModel(Video video)
        {
            if (video == null)
            {
                return null;
            }
            return _mapper.Map<ReadVideoViewModel>(video);
        }

        private List<ReadVideoViewModel>? ConverterParaReadViewModel(List<Video> videos)
        {
            if (EstaVazioOuNulo(videos))
            {
                return null;
            }
            return _mapper.Map<List<ReadVideoViewModel>>(videos);
        }

        private Video ConverterParaVideoModel(CreateVideoViewModel video)
        {
            return _mapper.Map<Video>(video);
        }

        private void AdicionarVideoNoBD(Video video)
        {
            _context.Videos.Add(video);
            _context.SaveChanges();
        }

        private void RemoverVideoDoBD(Video video)
        {
            _context.Videos.Remove(video);
            _context.SaveChanges();
        }

        private void AtualizarVideoNoBD(Video videoAtual, CreateVideoViewModel videoNovo)
        {
            videoAtual.Titulo = videoNovo.Titulo;
            videoAtual.Descricao = videoNovo.Descricao;
            videoAtual.Url = videoNovo.Url;
            videoAtual.IdCategoria = videoNovo.IdCategoria;
            _context.SaveChanges();
        }
    }
}
