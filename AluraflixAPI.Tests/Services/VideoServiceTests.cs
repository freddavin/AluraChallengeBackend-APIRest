using AluraflixAPI.Controllers;
using AluraflixAPI.Models;
using AluraflixAPI.Services;
using AluraflixAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AluraflixAPI.Tests.Services
{
    public class VideoServiceTests
    {
        [Fact]
        public void CadastrarVideo_ComCreateVideoViewModel_ResultadoOk()
        {
            // Arrange
            var serviceStub = new Mock<VideoService>();
            serviceStub.Setup(s => s.CadastrarVideo(new CreateVideoViewModel { Titulo = "Teste" }))
                .Returns(new ReadVideoViewModel { Id = 1, Titulo = "Teste"});
            var controller = new VideosController(serviceStub.Object);

            // Act
            var resultado = controller.CadastrarVideo(new CreateVideoViewModel { Titulo = "Teste"});

            // Assert
            Assert.IsType<OkObjectResult>(resultado);
        }


    }
}
