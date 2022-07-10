using AluraflixAPI.Contexts;
using AluraflixAPI.Models;
using AluraflixAPI.ViewModels;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace AluraflixAPI.Services
{
    public class VideoService
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
            return ConverterParaReadViewModel(video);
        }

        public ReadVideoViewModel? ConsultarVideoPorId(int id)
        {
            Video? videoEncontrado = _context.Videos.FirstOrDefault(video => video.Id == id);
            return ConverterParaReadViewModel(videoEncontrado);
        }

        public List<ReadVideoViewModel>? ConsultarVideos()
        {
            List<Video> colecaoDeVideos = _context.Videos.ToList();
            return ConverterParaReadViewModel(colecaoDeVideos);
        }

        public Result RemoverVideoPorId(int id)
        {
            Video? videoEncontradoParaRemover = _context.Videos.FirstOrDefault(video => video.Id == id);
            if (videoEncontradoParaRemover == null)
            {
                return Result.Fail("Vídeo não encontrado.");
            }
            DeletarVideoDoBD(videoEncontradoParaRemover);
            return Result.Ok();
        }
        
        public Result AtualizarVideoPorId(int id, CreateVideoViewModel videoComNovosDados)
        {
            Video? videoEncontradoParaAtualizar = _context.Videos.FirstOrDefault(video => video.Id == id);
            if (videoEncontradoParaAtualizar == null)
            {
                return Result.Fail("Vídeo não encontrado.");
            }
            AtualizarVideoNoBD(videoEncontradoParaAtualizar, videoComNovosDados);
            return Result.Ok();
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

        private void DeletarVideoDoBD(Video video)
        {
            _context.Videos.Remove(video);
            _context.SaveChanges();
        }

        private void AtualizarVideoNoBD([FromRoute] Video videoAtual, [FromBody] CreateVideoViewModel videoNovo)
        {
            videoAtual.Titulo = videoNovo.Titulo;
            videoAtual.Descricao = videoNovo.Descricao;
            videoAtual.Url = videoNovo.Url;
            videoAtual.IdCategoria = videoNovo.IdCategoria;
            _context.SaveChanges();
        }
    }
}
