using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.EmpresaOrganizacao;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Application.DTOs.Paginacao;
using System.Threading.Tasks;

namespace WorkWell.Tests.Controllers
{
    public class SetorControllerTests
    {
        private readonly Mock<ISetorService> _serviceMock;
        private readonly SetorController _controller;

        public SetorControllerTests()
        {
            _serviceMock = new Mock<ISetorService>();
            _controller = new SetorController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllPaged_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllPagedAsync(1, 10)).ReturnsAsync(new PagedResultDto<SetorDto>
            {
                Page = 1,
                PageSize = 10,
                TotalCount = 1
            });
            var result = await _controller.GetAllPaged(1, 10);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_ReturnsFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(123)).ReturnsAsync(new SetorDto { Id = 123 });
            var result = await _controller.GetById(123);
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<SetorDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_IfMissing()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(10)).ReturnsAsync((SetorDto?)null);
            var result = await _controller.GetById(10);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<SetorDto>())).ReturnsAsync(5);
            var result = await _controller.Create(new SetorDto { Nome = "Setor", EmpresaId = 10 });
            var created = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(5L, (long)(created.Value ?? 0L));
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            var dto = new SetorDto { Id = 2, Nome = "Setor", EmpresaId = 1 };
            var result = await _controller.Update(2, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var result = await _controller.Update(1, new SetorDto { Id = 7, Nome = "S", EmpresaId = 1 });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(44);
            Assert.IsType<NoContentResult>(result);
        }
    }
}