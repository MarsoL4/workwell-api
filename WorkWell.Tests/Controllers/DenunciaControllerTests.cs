using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Threading.Tasks;
using WorkWell.API;
using Xunit;

namespace WorkWell.Tests.Controllers
{
    public class DenunciaControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public DenunciaControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ComApiKeyFuncionario_DeveRetornar200()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Denuncia");
            req.Headers.Add("X-API-KEY", "funcionario-api-key");

            var res = await _client.SendAsync(req);

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

        [Fact]
        public async Task GetById_NaoExistente_Retorna404()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Denuncia/9999999");
            req.Headers.Add("X-API-KEY", "funcionario-api-key");

            var res = await _client.SendAsync(req);

            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
        }
    }
}