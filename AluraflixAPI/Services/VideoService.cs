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
            Video videoCadastrado = ConverterParaVideoModel(videoParaCadastrar);
            AdicionarVideoNoBD(videoCadastrado);            
            return ConverterParaReadViewModel(videoCadastrado);
        }

        public ReadVideoViewModel? ConsultarVideoPorId(int id)
        {
            Video? videoEncontrado = _context.Videos.FirstOrDefault(video => video.Id == id);
            if (videoEncontrado == null)
            {
                return null;
            }
            return ConverterParaReadViewModel(videoEncontrado);
        }

        public List<ReadVideoViewModel>? RecuperarColecaoDeVideos()
        {
            List<Video> colecaoDeVideos = _context.Videos.ToList();
            if (EstaVazioOuNulo(colecaoDeVideos))
            {
                return null;
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

        public bool EstaVazioOuNulo(List<Video> colecaoDeVideos)
        {
            if (colecaoDeVideos == null || !colecaoDeVideos.Any())
            {
                return true;
            }
            return false;
        }

        public ReadVideoViewModel ConverterParaReadViewModel(Video video)
        {
            return _mapper.Map<ReadVideoViewModel>(video);
        }

        public List<ReadVideoViewModel> ConverterParaReadViewModel(List<Video> videos)
        {
            return _mapper.Map<List<ReadVideoViewModel>>(videos);
        }

        public Video ConverterParaVideoModel(CreateVideoViewModel videoViewModel)
        {
            return _mapper.Map<Video>(videoViewModel);
        }

        public void AdicionarVideoNoBD(Video video)
        {
            _context.Videos.Add(video);
            _context.SaveChanges();
        }

        public void DeletarVideoDoBD(Video video)
        {
            _context.Videos.Remove(video);
            _context.SaveChanges();
        }

        public void AtualizarVideoNoBD([FromRoute] Video video, [FromBody] CreateVideoViewModel videoViewModel)
        {
            video.Titulo = videoViewModel.Titulo;
            video.Descricao = videoViewModel.Descricao;
            video.Url = videoViewModel.Url;
            _context.SaveChanges();
        }
    }
}
