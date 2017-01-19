using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using Service.Bank.Proxy.Configuration;
using Service.Bank.Proxy.ServiceHttpClient;
using Service.Dto;

namespace Test.Service.Bank.Proxy
{
    /// <summary>
    /// Cant mock http client 
    /// </summary>
    internal class TransferServiceHttpClientTest
    {
        private static string _expectedAddress = _testServiceAddress + @"/transfer";

        private static string _testServiceAddress = "https://192.168.0.10";

        private string _login = "Login";

        private string _password = "pswd";

        private HttpClient CreateClientMock(HttpStatusCode statusCode = HttpStatusCode.OK, string error = "")
        {
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(client => client.PostAsync(_expectedAddress, It.IsAny<StringContent>()))
                .Returns(() => PostResponseAction(statusCode, error))
                .Verifiable();

            return httpClientMock.Object;
        }

        private ITransferServicesRegister CreateServiceRegisterMock()
        {
            var mock = new Mock<ITransferServicesRegister>();

            mock.Setup(register => register.Login)
                .Returns(_login);

            mock.Setup(register => register.Password)
                .Returns(_password);

            return mock.Object;
        }

        private TransferServiceHttpClient CreateTransferServiceClient(HttpClient httpClient)
        {
            var serviceRegister = CreateServiceRegisterMock();
            return new TransferServiceHttpClient(httpClient, serviceRegister);
        }

        private Task<HttpResponseMessage> PostResponseAction(HttpStatusCode statusCode, string error)
        {
            var responseMessage = new HttpResponseMessage(statusCode);

            if (!string.IsNullOrEmpty(error))
            {
                var transferError = new InterbankTransferError(error);

                var messageContent = JsonConvert.SerializeObject(transferError, Formatting.Indented);

                responseMessage.Content = new StringContent(messageContent, Encoding.UTF8, "application/json");
            }

            return Task.FromResult(responseMessage);
        }

        private void SendTransferMessage_CorrectAddress_ShouldNotReturnError()
        {
            var httpClient = CreateClientMock();

            var transferServiceClient = CreateTransferServiceClient(httpClient);

            transferServiceClient.SendTransfer(Mock.Of<InterbankTransferDescription>(), _testServiceAddress);

            Mock.Get(httpClient).Verify();
        }
    }
}