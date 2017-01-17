using System;

namespace Service.Bank.Proxies.Configuration
{
    public class TransferServiceNotFoundException : Exception
    {
        public string BankId { get; }

        public override string Message => $"Transfer service for bank with id: {BankId} is not registered";

        public TransferServiceNotFoundException(string bankId)
        {
            BankId = bankId;
        }
    }
}