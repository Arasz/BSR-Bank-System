using System.Net.Http;
using Service.Bank.Proxies.Configuration;
using Service.Contracts;
using Service.Dto;
using Shared.AccountNumber.Parser;

namespace Service.Bank.Proxies
{
    /// <summary>
    /// Proxy for conntact with other 
    /// </summary>
    public class InterbankTransferServiceProxy : IInterbankTransferService
    {
        private readonly IAccountNumberParser _accountNumberParser;
        private readonly HttpClient _httpClient;
        private readonly ITransferServicesRegister _transferServicesRegister;

        public InterbankTransferServiceProxy(HttpClient httpClient, ITransferServicesRegister transferServicesRegister, IAccountNumberParser accountNumberParser)
        {
            _httpClient = httpClient;
            _transferServicesRegister = transferServicesRegister;
            _accountNumberParser = accountNumberParser;
        }

        public void Transfer(InterbankTransferDescription transferDescription)
        {
            var transferServiceAddress = ReadServiceAddress(transferDescription.ReceiverAccount);
        }

        private string ReadServiceAddress(string transferTargetBankAccountNumber)
        {
            var parsedAccountNumber = _accountNumberParser.Parse(transferTargetBankAccountNumber);

            var serviceAddress = _transferServicesRegister.GetTransferServiceAddress(parsedAccountNumber.BankId);

            return serviceAddress;
        }
    }
}