using AluraflixAPI.Contexts;
using AluraflixAPI.Models;
using AluraflixAPI.Services;
using AluraflixAPI.ViewModels;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace AluraflixAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class VideosController : ControllerBase
    {
        private VideoService _service;

        public VideosController(VideoService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CadastrarVideo([FromBody] CreateVideoViewModel videoParaCadastrar)
        {
            ReadVideoViewModel videoCadastrado = _service.CadastrarVideo(videoParaCadastrar);
            return CreatedAtAction(nameof(ConsultarVideoPorId), new { videoCadastrado.Id }, videoCadastrado);
        }

        [HttpGet("{Id}")]
        public IActionResult ConsultarVideoPorId(int id)
        {
            ReadVideoViewModel? videoConsultado = _service.ConsultarVideoPorId(id);
            if (videoConsultado == null)
            {
                return NotFound("Vídeo não encontrado.");
            }
            return Ok(videoConsultado);
        }

        [HttpGet]
        public IActionResult RecuperarColecaoDeVideos()
        {
            List<ReadVideoViewModel>? colecaoDeVideos = _service.RecuperarColecaoDeVideos();
            if (colecaoDeVideos == null)
            {
                return NotFound("Coleção nula ou sem vídeos cadastrados.");
            }
            return Ok(colecaoDeVideos);
        }

        [HttpDelete("{Id}")]
        public IActionResult RemoverVideoPorId(int id)
        {
            Result resultadoDaRemocao = _service.RemoverVideoPorId(id);
            if (resultadoDaRemocao.IsFailed)
            {
                return NotFound(resultadoDaRemocao.Errors.First().Message);
            }
            return Ok("Vídeo deletado com sucesso.");
        }

        [HttpPut("{Id}")]
        public IActionResult AtualizarVideoPorId(int id, [FromBody] CreateVideoViewModel videoComNovosDados)
        {
            Result resultadoDaAtualizacao = _service.AtualizarVideoPorId(id, videoComNovosDados);
            if (resultadoDaAtualizacao.IsFailed)
            {
                return NotFound(resultadoDaAtualizacao.Errors.First().Message);
            }
            return Ok("Vídeo atualizado com sucesso.");
        }


    }
}
