using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Aspose.Cloud.Marketplace.HTML.Converter.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Aspose.Cloud.Marketplace.HTML.Converter.Tests
{
    public class MarkdownControllerIntegrationTests :
        IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public MarkdownControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_EndpointsReturnSuccessAndHtmlContentType()
        {
            var client = _factory.CreateClient();
            var convertOptions = new ConverterOptions
            {
                Content = File.ReadAllText("test.md"),
                To = "html"
            };
            var jsonString = JsonSerializer.Serialize(convertOptions);
            var response = await client.PostAsync("/api/markdown", new StringContent(jsonString,Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }


        [Fact]
        public async Task Post_EndpointsReturnSuccessAndPdfContentType()
        {
            var client = _factory.CreateClient();
            var convertOptions = new ConverterOptions
            {
                Content = File.ReadAllText("test.md"),
                To = "pdf",
                Paper = new Paper {Size = "A4"}
            };
            var jsonString = JsonSerializer.Serialize(convertOptions);
            var response = await client.PostAsync("/api/markdown", new StringContent(jsonString, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/pdf",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Post_EndpointsReturnFailAndNonSupportMessage()
        {
            var client = _factory.CreateClient();
            var convertOptions = new ConverterOptions
            {
                Content = File.ReadAllText("test.md"),
                To = "jpg"
            };
            var jsonString = JsonSerializer.Serialize(convertOptions);
            var response = await client.PostAsync("/api/markdown", new StringContent(jsonString, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal("text/plain", response.Content.Headers.ContentType.ToString());
            var streamContent = response.Content as StreamContent;
            Assert.NotNull(streamContent);
        }
    }
}
