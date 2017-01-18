using System;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Test.Learning
{
    public class HttpClientTest
    {
        private string _getRequestUri = "http://www.brainjar.com/java/host/test.html";

        [Fact]
        public async Task MakeGetRequest_SetUpUriAsBaseAddress_ShouldReturnSimpleWebPage()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(_getRequestUri) };

            var httpResponseMessage = await httpClient.GetAsync("", HttpCompletionOption.ResponseContentRead);

            httpResponseMessage.StatusCode
                .Should()
                .HaveFlag(HttpStatusCode.OK);

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task MakeGetRequest_WithCorrectUri_GetSimpleWebPage()
        {
            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.GetAsync(_getRequestUri, HttpCompletionOption.ResponseContentRead);

            httpResponseMessage.StatusCode
                .Should()
                .HaveFlag(HttpStatusCode.OK);

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task ReuseHttpClient_MakeTwoGetRequestWithOneClient_GetSimpleWebPageTwoTimes()
        {
            var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.GetAsync(_getRequestUri, HttpCompletionOption.ResponseContentRead);

            httpResponseMessage.StatusCode
                .Should()
                .HaveFlag(HttpStatusCode.OK);

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();

            httpResponseMessage = await httpClient.GetAsync(_getRequestUri, HttpCompletionOption.ResponseContentRead);

            httpResponseMessage.StatusCode
                .Should()
                .HaveFlag(HttpStatusCode.OK);

            content = await httpResponseMessage.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
        }
    }
}