using AluraflixAPI.Services;
using AluraflixAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AluraflixAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CategoriasController : ControllerBase
    {
        private CategoriaService _service;

        public CategoriasController(CategoriaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CadastrarCategoria([FromBody] CreateCategoriaViewModel categoriaParaCadastrar)
        {
            ReadCategoriaViewModel categoriaCadastrada = _service.CadastrarCategoria(categoriaParaCadastrar);
            return CreatedAtAction(nameof(ConsultarCategoriaPorId), new { categoriaCadastrada.Id }, categoriaCadastrada);
        }

        [HttpGet("{Id}")]
        public IActionResult ConsultarCategoriaPorId(int id)
        {
            ReadCategoriaViewModel? categoriaConsultada = _service.ConsultarCategoriaPorId(id);
            if (categoriaConsultada == null)
            {
                return NotFound("Categoria não encontrada.");
            }
            return Ok(categoriaConsultada);
        }

        [HttpGet("{Id}/videos")]
        public IActionResult ConsultarFilmesPorCategoria(int id)
        {
            ReadVideosPorCategoriaViewModel? categoriaConsultada = _service.ConsultarFilmesPorCategoria(id);
            if (categoriaConsultada == null)
            {
                return NotFound("Categoria não encontrada.");
            }
            return Ok(categoriaConsultada);
        }

        [HttpGet]
        public IActionResult ConsultarCategorias()
        {
            List<ReadCategoriaViewModel> colecaoDeCategorias = _service.ConsultarCategorias();
            if (colecaoDeCategorias == null)
            {
                return NotFound("Coleção nula ou sem categorias cadastradas.");
            }
            return Ok(colecaoDeCategorias);
        }

    }
}
