using Newtonsoft.Json;
using Service.Bank.Exceptions;
using Service.Bank.Proxy.ServicesRegister;
using Service.Dto;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Service.Bank.Proxy.ServiceHttpClient
{
    public class TransferServiceHttpClient : ITransferServiceHttpClient
    {
        private readonly string _authenticationSchema = "Basic";
        private readonly HttpClient _httpClient;

        private readonly Encoding _jsonEncoding = Encoding.UTF8;
        private readonly string _jsonMimeType = @"application/json";
        private readonly string _transferActionLocation = "/transfer";
        private readonly ITransferServicesRegister _transferServicesRegister;

        public string CombinedAuthenticationData
            => _transferServicesRegister.Login + ":" + _transferServicesRegister.Password;

        private AuthenticationHeaderValue AuthenticationHeader
            => new AuthenticationHeaderValue(_authenticationSchema, ConvertToBase64(CombinedAuthenticationData));

        public TransferServiceHttpClient(HttpClient httpClient, ITransferServicesRegister transferServicesRegister)
        {
            _httpClient = httpClient;
            _transferServicesRegister = transferServicesRegister;

            ConfigureHttpClient();
        }

        public void SendTransfer(InterbankTransferDescription transferDescription, string transferServiceAddress)
        {
            var transferPostMessage = CreatePostMessageBody(transferDescription);

            var transferActionUri = CreateTransferActionUri(transferServiceAddress);

            var transferTask = _httpClient.PostAsync(transferActionUri, transferPostMessage);

            transferTask.Wait();

            var transferResult = transferTask.Result;

            HandleTransferError(transferResult);
        }

        private void ConfigureHttpClient()
        {
            _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeader;
        }

        private string ConvertToBase64(string data) => Convert.ToBase64String(Encoding.ASCII.GetBytes(data));

        private HttpContent CreatePostMessageBody(InterbankTransferDescription transferDescription) =>
            new StringContent(JsonConvert.SerializeObject(transferDescription), _jsonEncoding, _jsonMimeType);

        private Uri CreateTransferActionUri(string serviceAddressBase)
            => new Uri(serviceAddressBase + _transferActionLocation);

        private void HandleTransferError(HttpResponseMessage transferResult)
        {
            if (transferResult.IsSuccessStatusCode)
                return;

            var readResponseBodyTask = transferResult.Content.ReadAsStringAsync();
            readResponseBodyTask.Wait();

            var transferError = ParseTransferError(readResponseBodyTask.Result);
            throw new ExternalTransferException($"Error during external transfer: {transferError.Error}", (int)transferResult.StatusCode);
        }

        private InterbankTransferError ParseTransferError(string transferError)
            => JsonConvert.DeserializeObject<InterbankTransferError>(transferError);
    }
}