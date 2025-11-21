using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.ApoioPsicologico;
using WorkWell.Application.DTOs.ApoioPsicologico;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WorkWell.Tests.Controllers
{
    public class SOSEmergenciaControllerTests
    {
        private readonly Mock<ISOSemergenciaService> _serviceMock;
        private readonly SOSemergenciaController _controller;

        public SOSEmergenciaControllerTests()
        {
            _serviceMock = new Mock<ISOSemergenciaService>();
            _controller = new SOSemergenciaController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<SOSemergenciaDto>());
            var result = await _controller.GetAll();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_ReturnsFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(8)).ReturnsAsync(new SOSemergenciaDto { Id = 8 });
            var result = await _controller.GetById(8);
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<SOSemergenciaDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((SOSemergenciaDto?)null);
            var result = await _controller.GetById(99);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<SOSemergenciaDto>())).ReturnsAsync(44);
            var result = await _controller.Create(new SOSemergenciaDto { FuncionarioId = 11, Tipo = "Crise", DataAcionamento = DateTime.Now });
            var created = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(44L, (long)(created.Value ?? 0L));
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            var dto = new SOSemergenciaDto { Id = 9, FuncionarioId = 8, Tipo = "Crise", PsicologoNotificado = true };
            var result = await _controller.Update(9, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var dto = new SOSemergenciaDto { Id = 9, FuncionarioId = 1, Tipo = "X", PsicologoNotificado = false };
            var result = await _controller.Update(3, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(22);
            Assert.IsType<NoContentResult>(result);
        }
    }
}