using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Threading.Tasks;
using WorkWell.API;
using Xunit;

namespace WorkWell.Tests.Controllers
{
    public class FuncionarioControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public FuncionarioControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllPaged_ComApiKeyRH_DeveRetornar200()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Funcionario?page=1&pageSize=1");
            req.Headers.Add("X-API-KEY", "rh-api-key");

            var res = await _client.SendAsync(req);

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

        [Fact]
        public async Task GetById_NaoExistente_DeveRetornar404()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Funcionario/999999");
            req.Headers.Add("X-API-KEY", "rh-api-key");

            var res = await _client.SendAsync(req);

            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
        }
    }
}