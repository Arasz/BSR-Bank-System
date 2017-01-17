using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Service.Bank.Proxies.Configuration;
using Service.Contracts;
using Service.Dto;
using Shared.AccountNumber.Parser;

namespace Service.Bank.Proxy
{
    /// <summary>
    /// Proxy for interbank transaction service. 
    /// </summary>
    public class InterbankTransferServiceProxy : IInterbankTransferService
    {
        private readonly IAccountNumberParser _accountNumberParser;
        private readonly HttpClient _httpClient;

        private readonly ITransferServicesRegister _transferServicesRegister;
        private string _authenticationSchema = "Basic";
        private string _transferActionLocation = "/transfer";

        private AuthenticationHeaderValue AuthorizationHeader => new AuthenticationHeaderValue(_authenticationSchema, EncodedAuthorizationData);

        private string CombinedLoginData => _transferServicesRegister.Login + ":" + _transferServicesRegister.Password;

        private string EncodedAuthorizationData => Convert.ToBase64String(Encoding.ASCII.GetBytes(CombinedLoginData));

        public InterbankTransferServiceProxy(HttpClient httpClient, ITransferServicesRegister transferServicesRegister, IAccountNumberParser accountNumberParser)
        {
            _httpClient = httpClient;
            _transferServicesRegister = transferServicesRegister;
            _accountNumberParser = accountNumberParser;
        }

        public void Transfer(InterbankTransferDescription transferDescription)
        {
            var transferServiceAddress = ReadServiceAddress(transferDescription.ReceiverAccount);

            ConfigureHttpClient(transferServiceAddress);

            MakePostRequest(transferDescription, transferServiceAddress);
        }

        private void ConfigureHttpClient(string transferServiceAddress)
        {
            _httpClient.BaseAddress = new Uri(transferServiceAddress);
            _httpClient.DefaultRequestHeaders.Authorization = AuthorizationHeader;
        }

        private void MakePostRequest(InterbankTransferDescription transferDescription, string transferServiceAddress)
        {
            var transferDescriptionJson = SerializeTransferDescription(transferDescription);

            var transferTask = _httpClient.PostAsync(TransferEndpointUri(transferServiceAddress), transferDescriptionJson);

            transferTask.Wait();

            var transferResult = transferTask.Result;

            if (!transferResult.IsSuccessStatusCode)
            {
                var readResponseBodyTask = transferResult.Content.ReadAsStringAsync();
                readResponseBodyTask.Wait();

                var transferError = ParseTransferError(readResponseBodyTask.Result);
                throw new InterbankTransferException(transferError.Error, (int)transferResult.StatusCode);
            }
        }

        private InterbankTransferError ParseTransferError(string transferError) => JsonConvert.DeserializeObject<InterbankTransferError>(transferError);

        private string ReadServiceAddress(string transferTargetBankAccountNumber)
        {
            var parsedAccountNumber = _accountNumberParser.Parse(transferTargetBankAccountNumber);

            var serviceAddress = _transferServicesRegister.GetTransferServiceAddress(parsedAccountNumber.BankId);

            return serviceAddress;
        }

        private HttpContent SerializeTransferDescription(InterbankTransferDescription transferDescription)
        {
            var jsonMime = @"application/json";
            var encoding = Encoding.UTF8;

            return new StringContent(JsonConvert.SerializeObject(transferDescription), encoding, jsonMime);
        }

        private Uri TransferEndpointUri(string serviceAddressBase) => new Uri(serviceAddressBase + _transferActionLocation);
    }
}