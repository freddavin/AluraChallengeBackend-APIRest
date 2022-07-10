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
        public IActionResult ConsultarVideos()
        {
            List<ReadVideoViewModel>? colecaoDeVideos = _service.ConsultarVideos();
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
        public IActionResult AtualizarVideoPorId([FromRoute] int id, [FromBody] CreateVideoViewModel videoComNovosDados)
        {
            ReadVideoViewModel? videoAtualizado = _service.AtualizarVideoPorId(id, videoComNovosDados);
            if (videoAtualizado == null)
            {
                return NotFound("Vídeo não encontrado.");
            }
            return Ok(videoAtualizado);
        }


    }
}
