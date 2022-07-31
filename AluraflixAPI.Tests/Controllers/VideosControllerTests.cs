using AluraflixAPI.Contexts;
using AluraflixAPI.Controllers;
using AluraflixAPI.Models;
using AluraflixAPI.Services;
using AluraflixAPI.ViewModels;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Xunit;

namespace AluraflixAPI.Tests.Controllers
{
    public class VideosControllerTests
    {
        private readonly Mock<IVideoService> _mockService;
        private readonly VideosController _controller;

        public VideosControllerTests()
        {
            _mockService = new Mock<IVideoService>();
            _controller = new VideosController(_mockService.Object);
        }

        [Fact]
        public void CadastrarVideo_ComInformacoesValidas_ResultadoCreatedAtActionComRetorno()
        {
            // Arrange
            var videoParaCadastrar = new CreateVideoViewModel
            {
                Titulo = ".NET 5 REST API Tutorial",
                Descricao = "Learn how to create a REST API end-to-end from scratch using the latest " +
                ".NET 5 innovations and Visual Studio Code. The API will be written in C#.",
                Url = "https://www.youtube.com/watch?v=ZXdFisA_hOY",
                IdCategoria = 1
            };

            var videoCadastrado = new ReadVideoViewModel
            {
                Id = 1,
                Titulo = ".NET 5 REST API Tutorial",
                Descricao = "Learn how to create a REST API end-to-end from scratch using the latest " +
                ".NET 5 innovations and Visual Studio Code. The API will be written in C#.",
                Url = "https://www.youtube.com/watch?v=ZXdFisA_hOY"
            };

            _mockService.Setup(s => s
                .CadastrarVideo(videoParaCadastrar))
                .Returns(videoCadastrado);

            // Act
            var actionResult = _controller.CadastrarVideo(videoParaCadastrar);
            var objectResult = actionResult as CreatedAtActionResult;
            var videoResult = objectResult.Value as ReadVideoViewModel;

            // Assert
            Assert.NotNull(videoResult);
            Assert.Equal(201, objectResult.StatusCode);
        }

        [Fact]
        public void ConsultarVideoPorId_ComIdValido_ResultadoOkComRetorno()
        {
            // Arrange
            var videoConsultado = new ReadVideoViewModel
            {
                Id = 1,
                Titulo = ".NET 5 REST API Tutorial",
                Descricao = "Learn how to create a REST API end-to-end from scratch using the latest " +
                ".NET 5 innovations and Visual Studio Code. The API will be written in C#.",
                Url = "https://www.youtube.com/watch?v=ZXdFisA_hOY"
            };

            _mockService.Setup(s => s.ConsultarVideoPorId(1))
                .Returns(videoConsultado);

            // Act
            var actionResult = _controller.ConsultarVideoPorId(1);
            var objectResult = actionResult as OkObjectResult;
            var videoResult = objectResult.Value as ReadVideoViewModel;

            // Assert
            Assert.NotNull(videoResult);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void ConsultarVideoPorId_ComIdInvalido_ResultadoNotFoundSemRetorno()
        {
            // Arrange
            var videoConsultado = new ReadVideoViewModel
            {
                Id = 1,
                Titulo = ".NET 5 REST API Tutorial",
                Descricao = "Learn how to create a REST API end-to-end from scratch using the latest " +
                ".NET 5 innovations and Visual Studio Code. The API will be written in C#.",
                Url = "https://www.youtube.com/watch?v=ZXdFisA_hOY"
            };

            _mockService.Setup(s => s.ConsultarVideoPorId(1))
                .Returns(videoConsultado);

            // Act
            var actionResult = _controller.ConsultarVideoPorId(2);
            var objectResult = actionResult as NotFoundObjectResult;

            // Assert
            Assert.Equal(404, objectResult.StatusCode);
            Assert.Equal("Vídeo não encontrado.", objectResult.Value);
        }

        [Fact]
        public void ConsultarVideos_ComStringValidaENaoPaginado_ResultadoOkComRetorno()
        {
            // Arrange
            List<ReadVideoViewModel> colecaoDeVideos = new List<ReadVideoViewModel>
            {
                new ReadVideoViewModel
                {
                    Id = 1,
                    Titulo = ".NET 5 REST API Tutorial C#",
                    Descricao = "Learn how to create a REST API end-to-end from scratch using the latest " +
                    ".NET 5 innovations and Visual Studio Code. The API will be written in C#.",
                    Url = "https://www.youtube.com/watch?v=ZXdFisA_hOY"
                },
                new ReadVideoViewModel
                {
                    Id = 2,
                    Titulo = "10 C# Libraries To Save You Time And Energy",
                    Descricao = "These are 10 C# libraries that I use to make my job simpler. " +
                    "Learn how to save yourself time and energy from these libraries.",
                    Url = "https://www.youtube.com/watch?v=uS0hRJqamfU"
                }
            };

            _mockService.Setup(s => s.ConsultarVideos("C#", 0))
                .Returns(colecaoDeVideos);

            // Act
            var actionResult = _controller.ConsultarVideos("C#", 0);
            var objectResult = actionResult as OkObjectResult;
            var videoResult = objectResult.Value as List<ReadVideoViewModel>;

            // Assert
            Assert.NotNull(videoResult);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void ConsultarVideosFree_Maximo5Videos_ResultadoOkComRetorno()
        {
            // Arrange
            List<ReadVideoViewModel> colecaoDeVideos = new List<ReadVideoViewModel>
            {
                new ReadVideoViewModel
                {
                    Id = 1,
                    Titulo = ".NET 5 REST API Tutorial C#",
                    Descricao = "Learn how to create a REST API end-to-end from scratch using the latest " +
                    ".NET 5 innovations and Visual Studio Code. The API will be written in C#.",
                    Url = "https://www.youtube.com/watch?v=ZXdFisA_hOY"
                },
                new ReadVideoViewModel
                {
                    Id = 2,
                    Titulo = "10 C# Libraries To Save You Time And Energy",
                    Descricao = "These are 10 C# libraries that I use to make my job simpler. " +
                    "Learn how to save yourself time and energy from these libraries.",
                    Url = "https://www.youtube.com/watch?v=uS0hRJqamfU"
                }
            };

            _mockService.Setup(s => s.ConsultarVideos(null, 1))
                .Returns(colecaoDeVideos);

            // Act
            var actionResult = _controller.ConsultarVideos(null, 1);
            var objectResult = actionResult as OkObjectResult;
            var videoResult = objectResult.Value as List<ReadVideoViewModel>;

            // Assert
            Assert.NotNull(videoResult);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void RemoverVideoPorId_ComIdValido_ResultadoOkSemRetorno()
        {
            // Arrange
            _mockService.Setup(s => s.RemoverVideoPorId(1))
                .Returns(Result.Ok);

            // Act
            var actionResult = _controller.RemoverVideoPorId(1);
            var objectResult = actionResult as OkObjectResult;

            // Assert
            Assert.Equal("Vídeo deletado com sucesso.", objectResult.Value);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void AtualizarVideoPorId_ComIdValido_ResultadoComRetorno()
        {
            // Arrange
            var videoComDadosNovos = new CreateVideoViewModel
            {
                Titulo = ".NET 5 REST API Tutorial",
                Descricao = "Learn how to create a REST API end-to-end from scratch using the latest " +
                    ".NET 5 innovations and Visual Studio Code. The API will be written in C#.",
                Url = "https://www.youtube.com/watch?v=ZXdFisA_hOY"
            };
            var videoAtualizado = new ReadVideoViewModel
            {
                Id = 1,
                Titulo = ".NET 5 REST API Tutorial",
                Descricao = "Learn how to create a REST API end-to-end from scratch using the latest " +
                    ".NET 5 innovations and Visual Studio Code. The API will be written in C#.",
                Url = "https://www.youtube.com/watch?v=ZXdFisA_hOY"
            };

            _mockService.Setup(s => s.AtualizarVideoPorId(1, videoComDadosNovos))
                .Returns(videoAtualizado);

            // Act
            var actionResult = _controller.AtualizarVideoPorId(1, videoComDadosNovos);
            var objectResult = actionResult as OkObjectResult;
            var videoResult = objectResult.Value as ReadVideoViewModel;

            // Assert
            Assert.NotNull(videoResult);
            Assert.Equal(200, objectResult.StatusCode);
        }

    }
}
