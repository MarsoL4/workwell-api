using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.EmpresaOrganizacao;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Application.DTOs.Paginacao;

namespace WorkWell.Tests.Controllers
{
    public class EmpresaControllerTests
    {
        private readonly Mock<IEmpresaService> _serviceMock;
        private readonly EmpresaController _controller;

        public EmpresaControllerTests()
        {
            _serviceMock = new Mock<IEmpresaService>();
            _controller = new EmpresaController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllPaged_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllPagedAsync(1, 10)).ReturnsAsync(new PagedResultDto<EmpresaDto>());
            var result = await _controller.GetAllPaged(1, 10);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsEmpresa_IfFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(new EmpresaDto { Id = 5 });
            var result = await _controller.GetById(5);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<EmpresaDto>(ok.Value);
            Assert.Equal(5, dto.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_IfMissing()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(111)).ReturnsAsync((EmpresaDto?)null);
            var result = await _controller.GetById(111);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<EmpresaDto>())).ReturnsAsync(77);
            var result = await _controller.Create(new EmpresaDto { Nome = "NN", EmailAdmin = "x", TokenAcesso = "y", LogoUrl = "", CorPrimaria = "", CorSecundaria = "", Missao = "", PoliticaBemEstar = "" });
            var action = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(77, action.Value);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_IfIdsOkAndExists()
        {
            var dto = new EmpresaDto { Id = 2, Nome = "--", EmailAdmin = "x", TokenAcesso = "y", LogoUrl = "", CorPrimaria = "", CorSecundaria = "", Missao = "", PoliticaBemEstar = "" };
            _serviceMock.Setup(x => x.GetByIdAsync(2)).ReturnsAsync(dto);
            var result = await _controller.Update(2, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var result = await _controller.Update(1, new EmpresaDto { Id = 8 });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_IfMissing()
        {
            var dto = new EmpresaDto { Id = 9 };
            _serviceMock.Setup(x => x.GetByIdAsync(dto.Id)).ReturnsAsync((EmpresaDto?)null);
            var result = await _controller.Update(dto.Id, dto);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_IfExists()
        {
            var dto = new EmpresaDto { Id = 123 };
            _serviceMock.Setup(x => x.GetByIdAsync(123)).ReturnsAsync(dto);
            var result = await _controller.Delete(123);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_IfNotExists()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(55)).ReturnsAsync((EmpresaDto?)null);
            var result = await _controller.Delete(55);
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}