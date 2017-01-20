using Core.Common.AccountNumber.Parser;
using Service.Bank.Proxy.ServiceHttpClient;
using Service.Bank.Proxy.ServicesRegister;
using Service.Contracts;
using Service.Dto;

namespace Service.Bank.Proxy
{
    /// <summary>
    /// Proxy for interbank transaction service. 
    /// </summary>
    public class InterbankTransferServiceProxy : IInterbankTransferServiceProxy
    {
        private readonly IAccountNumberParser _accountNumberParser;
        private readonly ITransferServiceHttpClient _httpClient;
        private readonly ITransferServicesRegister _transferServicesRegister;

        public InterbankTransferServiceProxy(ITransferServiceHttpClient httpClient, ITransferServicesRegister transferServicesRegister, IAccountNumberParser accountNumberParser)
        {
            _httpClient = httpClient;
            _transferServicesRegister = transferServicesRegister;
            _accountNumberParser = accountNumberParser;
        }

        public void Transfer(InterbankTransferDescription transferDescription)
        {
            var transferServiceAddress = ReadServiceAddress(transferDescription.ReceiverAccount);

            _httpClient.SendTransfer(transferDescription, transferServiceAddress);
        }

        private string ReadServiceAddress(string transferTargetBankAccountNumber)
        {
            var parsedAccountNumber = _accountNumberParser.Parse(transferTargetBankAccountNumber);

            var serviceAddress = _transferServicesRegister.GetTransferServiceAddress(parsedAccountNumber.BankId);

            return serviceAddress;
        }
    }
}