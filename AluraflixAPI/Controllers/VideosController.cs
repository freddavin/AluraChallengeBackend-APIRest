using AluraflixAPI.Contexts;
using AluraflixAPI.Models;
using AluraflixAPI.ViewModels;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace AluraflixAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class VideosController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public VideosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CadastrarVideo([FromBody] CreateVideoViewModel videoViewModel)
        {
            Video video = ConverterParaVideoModel(videoViewModel);
            AdicionarVideoNoBD(video);
            return CreatedAtAction(nameof(RecuperarVideo), new { video.Id }, video);
        }

        [HttpGet("{Id}")]
        public IActionResult RecuperarVideo(int id)
        {
            Result<Video> resultado = RecuperarVideoPorId(id);
            if (resultado.IsFailed)
            {
                return NotFound(resultado.Errors.FirstOrDefault().Message);
            }            
            ReadVideoViewModel videoViewModel = ConverterParaReadViewModel(resultado.Value);
            return Ok(videoViewModel);
        }

        [HttpGet]
        public IActionResult RecuperarColecaoDeVideos()
        {
            List<Video> colecaoDeVideosRecuperados = _context.Videos.ToList();
            if (EstaVazioOuNulo(colecaoDeVideosRecuperados))
            {
                return NotFound("Coleção nula ou sem vídeos cadastrados.");
            }
            return Ok(colecaoDeVideosRecuperados);
        }

        [HttpDelete("{Id}")]
        public IActionResult DeletarVideo(int id)
        {
            Result<Video> resultado = RecuperarVideoPorId(id);
            if (resultado.IsFailed)
            {
                return NotFound(resultado.Errors.FirstOrDefault().Message);
            }
            DeletarVideoDoBD(resultado.Value);
            return Ok("Vídeo deletado com sucesso.");
        }

        public Result<Video> RecuperarVideoPorId(int id)
        {
            Video? videoRecuperado = _context.Videos.FirstOrDefault(video => video.Id == id);
            if (videoRecuperado == null)
            {
                return Result.Fail("Vídeo não encontrado.");
            }
            return Result.Ok(videoRecuperado);
        }

        public bool EstaVazioOuNulo(IEnumerable<Video> colecaoDeVideos)
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

    }
}
