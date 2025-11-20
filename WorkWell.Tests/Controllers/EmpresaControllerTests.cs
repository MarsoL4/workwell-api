using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WorkWell.API;
using Xunit;

namespace WorkWell.Tests.Controllers
{
    public class EmpresaControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EmpresaControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllPaged_ComApiKeyAdmin_DeveRetornar200()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Empresa?page=1&pageSize=2");
            request.Headers.Add("X-API-KEY", "admin-api-key");

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_NotFound_DeveRetornar404()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Empresa/999999");
            request.Headers.Add("X-API-KEY", "admin-api-key");

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}