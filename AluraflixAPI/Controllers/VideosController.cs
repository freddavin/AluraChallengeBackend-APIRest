﻿using AluraflixAPI.Contexts;
using AluraflixAPI.Models;
using AluraflixAPI.Services;
using AluraflixAPI.ViewModels;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AluraflixAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class VideosController : ControllerBase
    {
        private IVideoService _service;

        public VideosController(IVideoService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CadastrarVideo([FromBody] CreateVideoViewModel videoParaCadastrar)
        {
            ReadVideoViewModel videoCadastrado = _service.CadastrarVideo(videoParaCadastrar);
            return CreatedAtAction(nameof(ConsultarVideoPorId), new { videoCadastrado.Id }, videoCadastrado);
        }

        [HttpGet("{id}")]
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
        public IActionResult ConsultarVideos([FromQuery] string? titulo = null, int pagina = 0)
        {
            List<ReadVideoViewModel>? colecaoDeVideos = _service.ConsultarVideos(titulo, pagina);
            if (colecaoDeVideos == null)
            {
                return NotFound("Coleção nula ou sem vídeos cadastrados.");
            }
            return Ok(colecaoDeVideos);
        }

        [HttpGet("free")]
        [AllowAnonymous]
        public IActionResult ConsultarVideosFree()
        {
            List<ReadVideoViewModel>? colecaoDeVideos = _service.ConsultarVideos(null, 1);
            if (colecaoDeVideos == null)
            {
                return NotFound("Coleção nula ou sem vídeos cadastrados.");
            }
            return Ok(colecaoDeVideos);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverVideoPorId(int id)
        {
            Result resultadoDaRemocao = _service.RemoverVideoPorId(id);
            if (resultadoDaRemocao.IsFailed)
            {
                return NotFound(resultadoDaRemocao.Errors.First().Message);
            }
            return Ok("Vídeo deletado com sucesso.");
        }

        [HttpPut("{id}")]
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
