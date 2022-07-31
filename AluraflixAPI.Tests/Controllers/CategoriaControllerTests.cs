using AluraflixAPI.Controllers;
using AluraflixAPI.Services;
using AluraflixAPI.ViewModels;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AluraflixAPI.Tests.Controllers
{
    public class CategoriaControllerTests
    {
        private readonly Mock<ICategoriaService> _mockService;
        private readonly CategoriasController _controller;

        public CategoriaControllerTests()
        {
            _mockService = new Mock<ICategoriaService>();
            _controller = new CategoriasController(_mockService.Object);
        }

        [Fact]
        public void CadastrarCategoria_ComInformacoesValidas_ResultadoCreatedAtActionComRetorno()
        {
            // Arrange
            CreateCategoriaViewModel categoriaParaCadastrar = new CreateCategoriaViewModel
            {
                Titulo = "CORRIDA",
                Cor = "AMARELO"
            };

            ReadCategoriaViewModel categoriaCadastrada = new ReadCategoriaViewModel
            {
                Id = 5,
                Titulo = "CORRIDA",
                Cor = "AMARELO"
            };

            _mockService.Setup(s => s
                .CadastrarCategoria(categoriaParaCadastrar))
                .Returns(categoriaCadastrada);

            // Act
            var actionResult = _controller.CadastrarCategoria(categoriaParaCadastrar);
            var objectResult = actionResult as CreatedAtActionResult;
            var categoriaResult = objectResult.Value as ReadCategoriaViewModel;

            // Assert
            Assert.NotNull(categoriaResult);
            Assert.Equal(201, objectResult.StatusCode);
        }

        [Fact]
        public void ConsultarCategoriaPorId_ComIdValido_ResultadoOkComRetorno()
        {
            // Arrange
            ReadCategoriaViewModel categoriaConsultada = new ReadCategoriaViewModel
            {
                Id = 5,
                Titulo = "CORRIDA",
                Cor = "AMARELO"
            };

            _mockService.Setup(s => s
                .ConsultarCategoriaPorId(5))
                .Returns(categoriaConsultada);

            // Act
            var actionResult = _controller.ConsultarCategoriaPorId(5);
            var objectResult = actionResult as OkObjectResult;
            var categoriaResult = objectResult.Value as ReadCategoriaViewModel;

            // Assert
            Assert.NotNull(categoriaResult);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void ConsultarCategoriaPorId_ComIdInvalido_ResultadoNotFoundSemRetorno()
        {
            // Arrange
            var categoriaConsultada = new ReadCategoriaViewModel
            {
                Id = 5,
                Titulo = "CORRIDA",
                Cor = "AMARELO"
            };

            _mockService.Setup(s => s
                .ConsultarCategoriaPorId(5))
                .Returns(categoriaConsultada);

            // Act
            var actionResult = _controller.ConsultarCategoriaPorId(10);
            var objectResult = actionResult as NotFoundObjectResult;

            // Assert
            Assert.Equal(404, objectResult.StatusCode);
            Assert.Equal("Categoria não encontrada.", objectResult.Value);
        }

        [Fact]
        public void ConsultarFilmesPorCategoria_ComIdValido_ResultadoOkComRetorno()
        {
            // Arrange
            ReadVideosPorCategoriaViewModel categoriaConsultada = new ReadVideosPorCategoriaViewModel
            {
                Id = 5,
                Titulo = "CORRIDA",
                Cor = "AMARELO",
                Videos = new List<object>()
            };

            _mockService.Setup(s => s
                .ConsultarFilmesPorCategoriaId(5))
                .Returns(categoriaConsultada);

            // Act
            var actionResult = _controller.ConsultarFilmesPorCategoria(5);
            var objectResult = actionResult as OkObjectResult;
            var categoriaResult = objectResult.Value as ReadVideosPorCategoriaViewModel;

            // Assert
            Assert.NotNull(categoriaResult);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void ConsultarCategorias_NaoPaginado_ResultadoOkComRetorno()
        {
            // Arrange
            List<ReadCategoriaViewModel> colecaoDeCategorias = new List<ReadCategoriaViewModel> 
            {
                new ReadCategoriaViewModel
                {
                    Id = 5,
                    Titulo = "CORRIDA",
                    Cor = "AMARELO"
                },
                new ReadCategoriaViewModel
                {
                    Id = 6,
                    Titulo = "MÚSICA",
                    Cor = "VERDE"
                }
            };

            _mockService.Setup(s => s
                .ConsultarCategorias(0))
                .Returns(colecaoDeCategorias);

            // Act
            var actionResult = _controller.ConsultarCategorias(0);
            var objectResult = actionResult as OkObjectResult;
            var categoriaResult = objectResult.Value as List<ReadCategoriaViewModel>;

            // Assert
            Assert.NotNull(categoriaResult);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void RemoverCategoriaPorId_ComIdValido_ResultadoOkSemRetorno()
        {
            // Arrange
            _mockService.Setup(s => s
                .RemoverCategoriaPorId(1))
                .Returns(Result.Ok);

            // Act
            var actionResult = _controller.RemoverCategoriaPorId(1);
            var objectResult = actionResult as OkObjectResult;

            // Assert
            Assert.Equal("Categoria deletada com sucesso.", objectResult.Value);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void AtualizarCategoriaPorId_ComIdValido_ResultadoOkComRetorno()
        {
            // Arrange
            CreateCategoriaViewModel categoriaComDadosNovos = new CreateCategoriaViewModel
            {
                Titulo = "CORRIDA",
                Cor = "AMARELO"
            };

            ReadCategoriaViewModel categoriaAtualizada = new ReadCategoriaViewModel
            {
                Id = 5,
                Titulo = "CORRIDA",
                Cor = "BRANCO"
            };

            _mockService.Setup(s => s
                .AtualizarCategoriaPorId(5, categoriaComDadosNovos))
                .Returns(categoriaAtualizada);

            // Act
            var actionResult = _controller.AtualizarCategoriaPorId(5, categoriaComDadosNovos);
            var objectResult = actionResult as OkObjectResult;
            var categoriaResult = objectResult.Value as ReadCategoriaViewModel;

            // Assert
            Assert.NotNull(categoriaResult);
            Assert.Equal(200, objectResult.StatusCode);

        }   
    }
}
