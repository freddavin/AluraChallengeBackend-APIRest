using AluraflixAPI.Services;
using AluraflixAPI.ViewModels;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace AluraflixAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CategoriasController : ControllerBase
    {
        private ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CadastrarCategoria([FromBody] CreateCategoriaViewModel categoriaParaCadastrar)
        {
            ReadCategoriaViewModel categoriaCadastrada = _service.CadastrarCategoria(categoriaParaCadastrar);
            return CreatedAtAction(nameof(ConsultarCategoriaPorId), new { categoriaCadastrada.Id }, categoriaCadastrada);
        }

        [HttpGet("{id}")]
        public IActionResult ConsultarCategoriaPorId(int id)
        {
            ReadCategoriaViewModel? categoriaConsultada = _service.ConsultarCategoriaPorId(id);
            if (categoriaConsultada == null)
            {
                return NotFound("Categoria não encontrada.");
            }
            return Ok(categoriaConsultada);
        }

        [HttpGet("{id}/videos")]
        public IActionResult ConsultarFilmesPorCategoria(int id)
        {
            ReadVideosPorCategoriaViewModel? categoriaConsultada = _service.ConsultarFilmesPorCategoriaId(id);
            if (categoriaConsultada == null)
            {
                return NotFound("Categoria não encontrada.");
            }
            return Ok(categoriaConsultada);
        }

        [HttpGet]
        public IActionResult ConsultarCategorias([FromQuery] int pagina = 0)
        {
            List<ReadCategoriaViewModel> colecaoDeCategorias = _service.ConsultarCategorias(pagina);
            if (colecaoDeCategorias == null)
            {
                return NotFound("Coleção nula ou sem categorias cadastradas.");
            }
            return Ok(colecaoDeCategorias);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverCategoriaPorId(int id)
        {
            Result resultadoDaRemocao = _service.RemoverCategoriaPorId(id);
            if (resultadoDaRemocao.IsFailed)
            {
                return NotFound(resultadoDaRemocao.Errors.First().Message);
            }
            return Ok("Categoria deletada com sucesso.");
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCategoriaPorId([FromRoute] int id, [FromBody] CreateCategoriaViewModel categoriaComNovosDados)
        {
            ReadCategoriaViewModel? categoriaAtualizada = _service.AtualizarCategoriaPorId(id, categoriaComNovosDados);
            if (categoriaAtualizada == null)
            {
                return NotFound("Categoria não encontrada.");
            }
            return Ok(categoriaAtualizada);
        }


    }
}
